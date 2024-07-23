using Game.Code.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Code.Services
{
    public class GameUIFactory : IGameUIFactory
    {
        private readonly GameObject _hudPrefab;
        private GameObject _hudInstance;

        public GameUIFactory(GameObject hudPrefab)
        {
            _hudPrefab = hudPrefab;
        }

        public HealthView CreateHealthView()
        {
            if(_hudInstance == null)
            {
                _hudInstance = Object.Instantiate(_hudPrefab);
            }

            if (_hudInstance.TryGetComponent<HealthView>(out var view))
            {
                return view;
            }

            throw new MissingComponentException($"{nameof(HealthView)} component is missing on Hud prefab");
        }

        public ScoreView CreateGameScoreView()
        {
            if (_hudInstance == null)
            {
                _hudInstance = Object.Instantiate(_hudPrefab);
            }
            
            if(_hudInstance.TryGetComponent<ScoreView>(out var view))
            {
                return view;
            }
            
            throw new MissingComponentException($"{nameof(ScoreView)} component is missing on Hud prefab");
        }
        
        public FinalScoreView CreateFinalScoreView()
        {
            if (_hudInstance == null)
            {
                _hudInstance = Object.Instantiate(_hudPrefab);
            }
            
            if(_hudInstance.TryGetComponent<FinalScoreView>(out var view))
            {
                return view;
            }
            
            throw new MissingComponentException($"{nameof(FinalScoreView)} component is missing on Hud prefab");
        }
    }
}