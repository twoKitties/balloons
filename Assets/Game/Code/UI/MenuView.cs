using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI
{
    public class MenuView : MonoBehaviour
    {
        public event Action OnStartClicked;
        public event Action OnExitClicked;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartButtonClickedHandler);
            _exitButton.onClick.AddListener(ExitButtonClickedHandler);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartButtonClickedHandler);
            _exitButton.onClick.RemoveListener(ExitButtonClickedHandler);
        }

        private void StartButtonClickedHandler() => 
            OnStartClicked?.Invoke();

        private void ExitButtonClickedHandler() => 
            OnExitClicked?.Invoke();

        public void EnableExitButton(bool isActive)
        {
            _exitButton.gameObject.SetActive(isActive);
        }
    }
}