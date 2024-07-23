using Game.Code.Core;

namespace Game.Code.Models
{
    public class WaveModel
    {
        public Wave CurrentWave => Waves[CurrentWaveIndex];
        public Wave[] Waves;
        public int CurrentWaveIndex;
    }
}