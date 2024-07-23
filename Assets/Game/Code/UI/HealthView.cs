using System.Collections.Generic;
using UnityEngine;

namespace Game.Code.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _prefab;
        private readonly List<GameObject> _lives = new();

        public void Initialize(int maxLives)
        {
            for (var i = 0; i < maxLives; i++)
            {
                var healthView = Instantiate(_prefab, _container);
                _lives.Add(healthView);
            }
        }

        public void SetCount(int livesCount)
        {
            for (var i = 0; i < _lives.Count; i++)
            {
                _lives[i].SetActive(i < livesCount);    
            }
        }

        public void SetActive(bool isActive)
        {
            _group.alpha = isActive ? 1 : 0;
        }
    }
}