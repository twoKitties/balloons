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
            _waveModel.onWaveEnded += SpawnWave;
            _healthModel.onHealthChanged += CheckGameCondition;
        }

        public void Dispose()
        {
            _gameStateModel.onRestart -= Restart;
            _waveModel.onBalloonLeft -= TakeLife;
            _waveModel.onBalloonPopped -= AddScore;
            _waveModel.onWaveEnded -= SpawnWave;
            _healthModel.onHealthChanged -= CheckGameCondition;
        }

        private void SpawnWave()
        {
            _gsm.SetState(GameStateMachine.State.NextWave);
        }

        private void CheckGameCondition()
        {
            if(_healthModel.Current > 0)
                return;

            _waveModel.onWaveEnded -= SpawnWave;
            _gsm.SetState(GameStateMachine.State.ShowResult);
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
            _waveModel.onWaveEnded += SpawnWave;
            _gsm.SetState(GameStateMachine.State.Restart);
        }
    }
}