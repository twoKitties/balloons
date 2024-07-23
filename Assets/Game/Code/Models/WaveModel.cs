using System;
using System.Collections.Generic;
using Game.Code.Core;

namespace Game.Code.Models
{
    public class WaveModel
    {
        public event Action<Balloon> onBalloonLeft;
        public event Action<Balloon> onBalloonPopped;
        public event Action onWaveEnded;
        public Wave CurrentWave => Waves[CurrentWaveIndex];
        public Wave[] Waves;
        public int CurrentWaveIndex;
        public int ActiveBalloonsCount => _activeBalloons.Count;
        private readonly List<Balloon> _activeBalloons = new();

        public Balloon this[int i]
        {
            get => _activeBalloons[i];
            set => _activeBalloons[i] = value;
        }

        public void Add(Balloon balloon)
        {
            _activeBalloons.Add(balloon);
        }
        
        public void Clear() 
        {
            _activeBalloons.Clear();            
        }

        public void RemoveAt(int i)
        {
            _activeBalloons.RemoveAt(i);
        }

        public void Remove(Balloon balloon)
        {
            _activeBalloons.Remove(balloon);
        }
        
        public void OnBalloonLeft(Balloon balloon)
        {
            onBalloonLeft?.Invoke(balloon);
        }

        public void OnBalloonPopped(Balloon balloon)
        {
            onBalloonPopped?.Invoke(balloon);
        }

        public void NotifyIfWaveEnded()
        {
            if (ActiveBalloonsCount > 0)
                return;

            onWaveEnded?.Invoke();
        }
    }
}