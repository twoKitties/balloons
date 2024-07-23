using System;
using Game.Code.Services;
using Game.Code.UI;

namespace Game.Code.Controllers
{
    public class MenuViewController : IDisposable
    {
        private readonly ICoroutineRunner _runner;
        private readonly MenuView _menuView;

        public MenuViewController(ICoroutineRunner runner, MenuView menuView)
        {
            _runner = runner;
            _menuView = menuView;
            _menuView.OnStartClicked += LoadCore;
            _menuView.OnExitClicked += ExitGame;
        }

        public void Dispose()
        {
            _menuView.OnStartClicked -= LoadCore;
            _menuView.OnExitClicked -= ExitGame;
        }

        private void LoadCore()
        {
            var loading = new SceneLoadingService().LoadAsync("Core", null);
            _runner.StartCoroutine(loading);
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}