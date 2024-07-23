using System;

namespace Game.Code.Core
{
    [Serializable]
    public class WaveConfig
    {
        public Wave[] Waves;
    }

    [Serializable]
    public class Wave
    {
        public int Balloons;
        public int ScorePerBalloon;
    }
}