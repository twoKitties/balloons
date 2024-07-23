using System;
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
        [SerializeField] private AudioSource _audioSource; 
        private GameStateController _gameStateController;
        private BalloonController _balloonController;
        private GameStateMachine _gameStateMachine;

        private async void Awake()
        {
            var resourceProvider = new ResourceProvider();
            var resourcesInitializer = new CoreResourcesInitializer(resourceProvider);
            await resourcesInitializer.Initialize();
            
            var gameFactory = new GameFactory(resourcesInitializer.BalloonPrefab, resourcesInitializer.EffectPrefab);
            var uiFactory = new GameUIFactory(resourcesInitializer.HUDPrefab);

            var healthModel = new HealthModel { Current = resourcesInitializer.GameStateConfig.InitialHealth, Max = resourcesInitializer.GameStateConfig.MaxHealth };
            var gameStateModel = new GameStateModel();
            var waveModel = new WaveModel
            {
                CurrentWaveIndex = 0,
                Waves = resourcesInitializer.WaveConfig.Waves,
            };

            var gameScoreViewController = new GameScoreViewController(gameStateModel, uiFactory.CreateGameScoreView());
            var healthViewController = new HealthViewController(healthModel, uiFactory.CreateHealthView());
            var finalScoreViewController = new FinalScoreViewController(gameStateModel, uiFactory.CreateFinalScoreView());
            var soundController = new SoundController(_audioSource, resourcesInitializer.PopSound);
            _balloonController = new BalloonController(gameFactory, _camera, waveModel);
            
            gameScoreViewController.Initialize();
            healthViewController.Initialize();
            finalScoreViewController.Initialize();
            _balloonController.Initialize();
            
            _gameStateMachine = new GameStateMachine(gameScoreViewController, finalScoreViewController, healthViewController, _balloonController, soundController);
            _gameStateController = new GameStateController(healthModel, waveModel, gameStateModel, _gameStateMachine);
            _gameStateMachine.SetState(GameStateMachine.State.NextWave);
        }

        private void OnDestroy()
        {
            _gameStateController.Dispose();
        }


        private void Update()
        {
            if (_gameStateMachine == null || _gameStateMachine.CurrentState == GameStateMachine.State.None)
                return;

            _balloonController.Update(Time.deltaTime);
        }
    }
}