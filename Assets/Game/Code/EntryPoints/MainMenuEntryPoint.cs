using Game.Code.Controllers;
using Game.Code.UI;
using UnityEngine;

namespace Game.Code.EntryPoints
{
    public class MainMenuEntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private MenuView _menuView;
        private MenuViewController _menuViewController;

        private void Awake()
        {
            var menuView = Instantiate(_menuView);
            _menuViewController = new MenuViewController(this, menuView);
            _menuViewController.Initialize();
        }

        private void OnDestroy()
        {
            _menuViewController.Dispose();
        }
    }
}