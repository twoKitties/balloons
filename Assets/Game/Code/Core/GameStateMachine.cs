using Game.Code.Controllers;

namespace Game.Code.Core
{
    public class GameStateMachine
    {
        public enum State
        {
            None,
            NextWave,
            ShowResult,
            BalloonPopped,
            TakeLife,
            Restart,
            Pause,
            Resume
        }
        public State CurrentState { get; private set; }
        private readonly GameScoreViewController _gameScoreViewController;
        private readonly FinalScoreViewController _finalScoreViewController;
        private readonly HealthViewController _healthViewController;
        private readonly BalloonController _balloonController;
        private readonly SoundController _soundController;

        public GameStateMachine(
            GameScoreViewController gameScoreViewController, 
            FinalScoreViewController finalScoreViewController, 
            HealthViewController healthViewController, 
            BalloonController balloonController,
            SoundController soundController)
        {
            _gameScoreViewController = gameScoreViewController;
            _finalScoreViewController = finalScoreViewController;
            _healthViewController = healthViewController;
            _balloonController = balloonController;
            _soundController = soundController;
        }


        public void SetState(State state)
        {
            CurrentState = state;
            switch (state)
            {
                case State.NextWave:
                    _balloonController.Spawn();
                    break;
                case State.ShowResult:
                    _healthViewController.SetViewActive(false);
                    _gameScoreViewController.SetViewActive(false);
                    _finalScoreViewController.SetViewActive(true);
                    break;
                case State.BalloonPopped:
                    _gameScoreViewController.AddScore(100);
                    _soundController.Play();
                    break;
                case State.TakeLife:
                    _healthViewController.Take(1);
                    break;
                case State.Restart:
                    _finalScoreViewController.Reset();
                    _gameScoreViewController.Initialize();
                    _gameScoreViewController.SetViewActive(true);
                    _healthViewController.Reset();
                    _healthViewController.SetViewActive(true);
                    _balloonController.Reset();
                    _balloonController.Spawn();
                    break;
            }            
        }
    }
}