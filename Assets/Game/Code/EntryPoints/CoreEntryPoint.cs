using Game.Code.Controllers;
using Game.Code.Core;
using Game.Code.Models;
using Game.Code.Services;
using UnityEngine;

namespace Game.Code.EntryPoints
{
    public class CoreEntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private GameScoreViewController _gameScoreViewController;
        private FinalScoreViewController _finalScoreViewController;
        private HealthViewController _healthViewController;
        private BalloonController _balloonController;
        private bool _isInitialized;

        private async void Awake()
        {
            var resourceProvider = new ResourceProvider();
            var resourcesInitializer = new CoreResourcesInitializer(resourceProvider);
            await resourcesInitializer.Initialize();
            var gameFactory = new GameFactory(resourcesInitializer.BalloonPrefab, resourcesInitializer.EffectPrefab);
            var uiFactory = new GameUIFactory(resourcesInitializer.HUDPrefab);


            var healthModel = new HealthModel { Current = 3, Max = 3 };
            var scoreModel = new ScoreModel();
            var waveModel = new WaveModel()
            {
                CurrentWaveIndex = 0,
                Waves = resourcesInitializer.WaveConfig.Waves,
            };

            _gameScoreViewController = new GameScoreViewController(scoreModel, uiFactory.CreateGameScoreView());
            _healthViewController = new HealthViewController(healthModel, uiFactory.CreateHealthView());
            _finalScoreViewController = new FinalScoreViewController(scoreModel, uiFactory.CreateFinalScoreView());

            _gameScoreViewController.Initialize();
            _healthViewController.Initialize();
            _finalScoreViewController.Initialize();


            //TODO remake this creating a gamedata {healthdata, scoredata, resourcedata)
            _balloonController = new BalloonController(gameFactory, _camera, waveModel);
            _balloonController.Initialize();
            _balloonController.Spawn();
            
            var gameStateController = new GameStateController(healthModel, _finalScoreViewController, _balloonController, _gameScoreViewController, _healthViewController);
            _isInitialized = true;
        }
        

        private void Update()
        {
            if (_isInitialized == false)
                return;

            _balloonController.Update(Time.deltaTime);
        }
    }

    public class GameStateMachine
    {
        // Initialize
        // Start
        // Pause
        // Resume
        // End
        // Restart
    }
    
    public class GameStateController
    {
        private readonly GameScoreViewController _gameScoreViewController;
        private readonly FinalScoreViewController _finalScoreViewController;
        private readonly HealthViewController _healthViewController;
        private readonly BalloonController _balloonController;
        private readonly HealthModel _healthModel;

        public GameStateController(HealthModel healthModel, 
            FinalScoreViewController finalScoreViewController,
            BalloonController balloonController,
            GameScoreViewController gameScoreViewController,
            HealthViewController healthViewController)
        {
            _healthModel = healthModel;
            _finalScoreViewController = finalScoreViewController;
            _gameScoreViewController = gameScoreViewController;
            _healthViewController = healthViewController;
            _balloonController = balloonController;
            
            
            _finalScoreViewController.OnReplay += Restart;
            _balloonController.OnBalloonPopped += AddScore;
            _balloonController.OnBalloonLeft += TakeLife;
            _balloonController.OnWaveEnded += CheckGameCondition;
        }
        
        private void CheckGameCondition()
        {
            if (_healthModel.Current <= 0)
            {
                _healthViewController.SetViewActive(false);
                _gameScoreViewController.SetViewActive(false);
                _finalScoreViewController.SetViewActive(true);
            }
            else
            {
                _balloonController.Spawn();
            }
        }
        
        private void AddScore(Balloon balloon)
        {
            //TODO maybe points per balloon popped should be in a wave config
            _gameScoreViewController.AddScore(balloon.Data.PointsPerBalloon);
        }

        private void TakeLife(Balloon balloon)
        {
            //TODO maybe life per balloon lost should be in a wave config 
            _healthViewController.Take(1);
        }
        
        private void Restart()
        {
            _finalScoreViewController.Reset();
            _gameScoreViewController.Initialize();
            _gameScoreViewController.SetViewActive(true);
            _healthViewController.Reset();
            _healthViewController.SetViewActive(true);
            _balloonController.Reset();
            _balloonController.Spawn();
        }
    }
}