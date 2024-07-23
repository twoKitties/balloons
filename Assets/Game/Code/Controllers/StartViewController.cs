using System;
using Game.Code.Models;
using Game.Code.UI;

namespace Game.Code.Controllers
{
    public class StartViewController : IDisposable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly StartView _startView;

        public StartViewController(GameStateModel gameStateModel, StartView startView)
        {
            _gameStateModel = gameStateModel;
            _startView = startView;
        }

        public void Initialize()
        {
            _startView.OnStartClicked += StartGame;
        }

        public void Dispose()
        {
            _startView.OnStartClicked -= StartGame;
        }

        public void SetViewActive(bool isActive)
        {
            _startView.SetViewActive(isActive);
        }

        private void StartGame()
        {
            _startView.SetViewActive(false);
            _gameStateModel.OnRestart();
        }
    }
}