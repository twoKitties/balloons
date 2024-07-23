using System;
using Game.Code.Data;
using Game.Code.Models;
using Game.Code.Services;
using Game.Code.UI;

namespace Game.Code.Controllers
{
    public class FinalScoreViewController : IDisposable
    {
        public event Action OnReplay; 
        private const string ErrorMessage = "Invalid player name";
        private readonly IPersistentDataService _persistentDataService;
        private readonly FinalScoreView _view;
        private readonly ScoreModel _model;

        public FinalScoreViewController(ScoreModel model, FinalScoreView view)
        {
            _view = view;
            _model = model;
            _persistentDataService = new PlayerPrefsDataService();
        }

        public void Initialize()
        {
            _view.OnSaveClicked += Save;
            _view.OnReplayClicked += Replay;
            _view.SetViewState(FinalScoreView.State.None);
        }

        public void SetViewActive(bool isActive)
        {
            var state = isActive ? FinalScoreView.State.Save : FinalScoreView.State.None;
            _view.SetViewState(state);
            _view.SetScore(_model.Score.ToString());
            var savedData = _persistentDataService.GetPlayerData();
            RefreshEntries(savedData);
        }


        public void Dispose()
        {
            _view.OnSaveClicked -= Save;
            _view.OnReplayClicked -= Replay;
        }

        public void Reset()
        {
            _view.SetScore(_model.Score.ToString());
            _view.SetViewState(FinalScoreView.State.None);
        }

        private void RefreshEntries(PlayerData savedData)
        {
            _view.Clear();
            foreach (var entry in savedData.Entries)
            {
                _view.AddEntry(entry.PlayerName, entry.Score.ToString());
            }
        }

        private void Replay()
        {
            OnReplay?.Invoke();
        }

        private void Save(string playerName)
        {
            if (IsPlayerNameInvalid(playerName))
            {
                SetPlayerName();
                return;
            }
            
            var data = _persistentDataService.GetPlayerData();
            var match = data.Entries.Find(item => item.PlayerName == playerName);
            if (match != null)
            {
                match.Score = _model.Score;
            }
            else
            {
                data.Entries.Add(new PlayerDataEntry
                {
                    PlayerName = playerName,
                    Score = _model.Score
                });
            }
            
            _persistentDataService.SavePlayerData(data);
            RefreshEntries(data);
            _view.SetViewState(FinalScoreView.State.Replay);
        }

        private static bool IsPlayerNameInvalid(string playerName)
        {
            return string.IsNullOrWhiteSpace(playerName) || playerName.Equals(ErrorMessage);
        }

        private void SetPlayerName()
        {
            _view.SetPlayerName(ErrorMessage);
        }
    }
}