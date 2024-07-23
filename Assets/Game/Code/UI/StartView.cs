using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI
{
    public class StartView : MonoBehaviour
    {
        public event Action OnStartClicked;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private CanvasGroup _group;

        public void SetViewActive(bool isActive)
        {
            var alpha = isActive ? 1 : 0;
            _group.alpha = alpha;
            _group.blocksRaycasts = isActive;
            _group.interactable = isActive;
        }
        
        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartButtonClickedHandler);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartButtonClickedHandler);
        }

        private void StartButtonClickedHandler() => 
            OnStartClicked?.Invoke();

    }
}