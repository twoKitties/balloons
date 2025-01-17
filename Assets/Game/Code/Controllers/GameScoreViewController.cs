﻿using Game.Code.Models;
using Game.Code.UI;

namespace Game.Code.Controllers
{
    public class GameScoreViewController
    {
        private readonly ScoreView _view;
        private readonly GameStateModel _model;

        public GameScoreViewController(GameStateModel model, ScoreView view)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _model.Score = 0;
            
        }

        public void AddScore(int value)
        {
            _model.Score += value;
            _view.SetScore(_model.Score.ToString());
        }

        public void SetViewActive(bool isActive)
        {
            _view.SetActive(isActive);
            _view.SetScore(_model.Score.ToString());
        }
    }
}