using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI
{
    public class FinalScoreView : MonoBehaviour
    {
        public enum State
        {
            None,
            Save,
            Replay,
        }
        public event Action<string> OnSaveClicked;
        public event Action OnReplayClicked;
        [SerializeField] private RectTransform _container;
        [SerializeField] private ScoreEntryView _entryPrefab;
        [SerializeField] private CanvasGroup _saveViewRoot;
        [SerializeField] private CanvasGroup _replayViewRoot;
        [SerializeField] private CanvasGroup _rootGroup;
        [SerializeField] private Text _scoreView;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _replayButton;
        //TODO move to controller
        private List<ScoreEntryView> _entries = new List<ScoreEntryView>();

        public void SetScore(string score)
        {
            _scoreView.text = score;
        }
        
        public void AddEntry(string playerName, string points)
        {
            var instance = Instantiate(_entryPrefab, _container);
            instance.SetData(playerName, points);
            _entries.Add(instance);
        }

        public void Clear()
        {
            foreach (var entry in _entries)
            {
                Destroy(entry.gameObject);
            }
            _entries.Clear();
        }

        public void SetPlayerName(string playerName)
        {
            _inputField.text = playerName;
            _inputField.placeholder.gameObject.SetActive(false);
        }

        public void SetViewState(State state)
        {
            _rootGroup.alpha = state == State.None ? 0 : 1;
            _rootGroup.interactable = state != State.None;
            _rootGroup.blocksRaycasts = state != State.None;
            
            var alpha = state == State.Save ? 1 : 0;
            _saveViewRoot.alpha = alpha;
            _saveViewRoot.interactable = state == State.Save;
            _saveViewRoot.blocksRaycasts = state == State.Save;
            
            alpha = state == State.Replay ? 1 : 0;
            _replayViewRoot.alpha = alpha;
            _replayViewRoot.interactable = state == State.Replay;
            _replayViewRoot.blocksRaycasts = state == State.Replay;
        }

        private void Awake()
        {
            _saveButton.onClick.AddListener(Save);
            _replayButton.onClick.AddListener(Replay);
        }

        private void OnDestroy()
        {
            _saveButton.onClick.RemoveListener(Save);
            _replayButton.onClick.RemoveListener(Replay);
        }

        private void Save()
        {
            OnSaveClicked?.Invoke(_inputField.text);
        }

        private void Replay()
        {
            OnReplayClicked?.Invoke();
        }
    }
}