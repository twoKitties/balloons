using System;
using Game.Code.Models;
using Game.Code.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Code.Controllers
{
    public class HealthViewController : IDisposable
    {
        private readonly HealthView _view;
        private readonly HealthModel _model;

        public HealthViewController(HealthModel model, HealthView view)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.Initialize(_model.Max);
            EnableHealthViews(_model.Current);
        }


        public void Dispose()
        {
            Object.Destroy(_view);
        }

        public void Take(int value)
        {
            _model.Current -= value;
            EnableHealthViews(_model.Current);
        }

        public void Add(int value)
        {
            var totalLives = _model.Current + value;
            _model.Current = Mathf.Clamp(totalLives, 0, _model.Max);
            EnableHealthViews(_model.Current);
        }


        public void Reset()
        {
            _model.Current = _model.Max;
            EnableHealthViews(_model.Current);
        }

        private void EnableHealthViews(int activeViewsCount)
        {
            _view.SetCount(activeViewsCount);
        }

        public void SetViewActive(bool isActive)
        {
            _view.SetActive(isActive);
        }
    }
}