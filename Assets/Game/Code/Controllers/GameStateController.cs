using System;
using Game.Code.Core;
using Game.Code.Models;

namespace Game.Code.Controllers
{
    public class GameStateController : IDisposable
    {
        private readonly GameStateMachine _gsm;
        private readonly HealthModel _healthModel;
        private readonly WaveModel _waveModel;
        private readonly GameStateModel _gameStateModel;

        public GameStateController(HealthModel healthModel, WaveModel waveModel, GameStateModel gameStateModel, GameStateMachine gsm)
        {
            _healthModel = healthModel;
            _waveModel = waveModel;
            _gameStateModel = gameStateModel;
            _gsm = gsm;


            _gameStateModel.onRestart += Restart;
            _waveModel.onBalloonLeft += TakeLife;
            _waveModel.onBalloonPopped += AddScore;
            _waveModel.onWaveEnded += CheckGameCondition;
        }

        public void Dispose()
        {
            _gameStateModel.onRestart -= Restart;
            _waveModel.onBalloonLeft -= TakeLife;
            _waveModel.onBalloonPopped -= AddScore;
            _waveModel.onWaveEnded -= CheckGameCondition;
        }

        private void CheckGameCondition()
        {
            _gsm.SetState(_healthModel.Current <= 0
                ? GameStateMachine.State.ShowResult
                : GameStateMachine.State.NextWave);
        }

        private void AddScore(Balloon balloon)
        {
            _gsm.SetState(GameStateMachine.State.AddScore);
        }

        private void TakeLife(Balloon balloon)
        {
            _gsm.SetState(GameStateMachine.State.TakeLife);
        }

        private void Restart()
        {
            _gsm.SetState(GameStateMachine.State.Restart);
        }
    }
}