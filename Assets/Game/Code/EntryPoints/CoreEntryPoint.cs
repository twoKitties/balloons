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
            var gameStateModel = new GameStateModel();
            var waveModel = new WaveModel()
            {
                CurrentWaveIndex = 0,
                Waves = resourcesInitializer.WaveConfig.Waves,
            };

            _gameScoreViewController = new GameScoreViewController(gameStateModel, uiFactory.CreateGameScoreView());
            _healthViewController = new HealthViewController(healthModel, uiFactory.CreateHealthView());
            _finalScoreViewController = new FinalScoreViewController(gameStateModel, uiFactory.CreateFinalScoreView());

            _gameScoreViewController.Initialize();
            _healthViewController.Initialize();
            _finalScoreViewController.Initialize();


            _balloonController = new BalloonController(gameFactory, _camera, waveModel);
            _balloonController.Initialize();
            _balloonController.Spawn();

            var gameStateMachine = new GameStateMachine(_gameScoreViewController, _finalScoreViewController, _healthViewController, _balloonController);
            var gameStateController = new GameStateController(healthModel, waveModel, gameStateModel, gameStateMachine);
            _isInitialized = true;
        }
        

        private void Update()
        {
            if (_isInitialized == false)
                return;

            _balloonController.Update(Time.deltaTime);
        }
    }
}