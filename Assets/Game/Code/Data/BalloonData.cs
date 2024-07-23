using System;

namespace Game.Code.Data
{
    [Serializable]
    public struct BalloonData
    {
        public readonly float FloatSpeed;
        public readonly float SwayAmount;
        public readonly int PointsPerBalloon;
        //public readonly Balloon Prefab;

        public BalloonData(float floatSpeed, float swayAmount, int pointsPerBalloon)
        {
            FloatSpeed = floatSpeed;
            SwayAmount = swayAmount;
            PointsPerBalloon = pointsPerBalloon;
        }
    }
}