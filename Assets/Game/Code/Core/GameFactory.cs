using Game.Code.Data;
using UnityEngine;

namespace Game.Code.Core
{
    public class GameFactory : IGameFactory
    {
        private readonly Balloon _balloonPrefab;
        private readonly BalloonExplosion _effectPrefab;

        public GameFactory(Balloon balloonPrefab, BalloonExplosion effectPrefab)
        {
            _balloonPrefab = balloonPrefab;
            _effectPrefab = effectPrefab;
        }

        public Balloon CreateBalloon()
        {
            var balloon = Object.Instantiate(_balloonPrefab, Vector2.zero, Quaternion.identity);
            //TODO this should come from a level wave data
            var floatSpeed = Random.Range(1.0f, 5.0f);
            var swayAmount = Random.Range(.3f, 2f);
            var pointsPerBalloon = 100;
            balloon.Data = new BalloonData(floatSpeed, swayAmount, pointsPerBalloon);
            return balloon;
        }
        
        public BalloonExplosion CreateExplosion()
        {
            var effect = Object.Instantiate(_effectPrefab);
            return effect;
        }

    }
}