using System;

namespace Game.Code.Models
{
    public class HealthModel
    {
        public event Action onHealthChanged;
        public int Current
        {
            get => _current;
            set
            {
                _current = value;
                onHealthChanged?.Invoke();
            }
        }
        public int Max;
        private int _current;
    }
}