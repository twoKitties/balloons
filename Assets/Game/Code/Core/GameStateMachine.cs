using Game.Code.Controllers;

namespace Game.Code.Core
{
    public class GameStateMachine
    {
        public enum State
        {
            None,
            Start,
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
        private readonly StartViewController _startViewController;

        public GameStateMachine(
            GameScoreViewController gameScoreViewController, 
            FinalScoreViewController finalScoreViewController, 
            HealthViewController healthViewController, 
            BalloonController balloonController,
            SoundController soundController,
            StartViewController startViewController)
        {
            _gameScoreViewController = gameScoreViewController;
            _finalScoreViewController = finalScoreViewController;
            _healthViewController = healthViewController;
            _balloonController = balloonController;
            _soundController = soundController;
            _startViewController = startViewController;
        }


        public void SetState(State state)
        {
            CurrentState = state;
            switch (state)
            {
                case State.Start:
                    _startViewController.Initialize();
                    _startViewController.SetViewActive(true);
                    _finalScoreViewController.Initialize();
                    _balloonController.Initialize();
                    _gameScoreViewController.Initialize();
                    _gameScoreViewController.SetViewActive(false);
                    _healthViewController.Initialize();
                    _healthViewController.SetViewActive(false);
                    break;
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
                    _startViewController.SetViewActive(false);
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