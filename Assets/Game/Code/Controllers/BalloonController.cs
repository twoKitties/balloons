using System;
using Game.Code.Core;
using Game.Code.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Code.Controllers
{
    public class BalloonController : IDisposable
    {
        private readonly WaveModel _waveModel;
        private readonly Camera _camera;
        private Pool<Balloon> _balloonPool;
        private Pool<BalloonExplosion> _effectPool;
        private float _screenTopVerticalThreshold;

        public BalloonController(IGameFactory gameFactory, Camera camera, WaveModel waveModel)
        {
            _waveModel = waveModel;
            _camera = camera;
            _balloonPool = new Pool<Balloon>(gameFactory.CreateBalloon);
            _effectPool = new Pool<BalloonExplosion>(gameFactory.CreateExplosion);
        }

        public void Initialize()
        {
            _screenTopVerticalThreshold = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        }

        public void Dispose()
        {
            _waveModel.Clear();
            _balloonPool = null;
            _effectPool = null;
        }

        public void Spawn()
        {
            var count = _waveModel.CurrentWave.Balloons;
            for (var i = 0; i < count; i++)
            {
                var balloon = _balloonPool.Get();
                var spawnPosition = GetRandomSpawnPosition();
                balloon.Position = spawnPosition;
                balloon.StartPosition = spawnPosition;
                balloon.OnClick += HandleClick;
                balloon.gameObject.SetActive(true); 
                _waveModel.Add(balloon);
            }

            IncrementWaveIndex();
        }


        //TODO move to wave animator

        public void Update(float deltaTime)
        {
            for (var i = _waveModel.ActiveBalloonsCount - 1; i >= 0; i--)
            {
                var balloon = _waveModel[i];
                var data = balloon.Data;
                var y = balloon.Position.y + data.FloatSpeed * deltaTime;
                var x = balloon.StartPosition.x + Mathf.Sin(Time.time * data.SwayAmount) * data.SwayAmount +
                        Mathf.Sin(3.1415f * data.SwayAmount);
                balloon.Position = new Vector3(x, y, 0);
                balloon.Rotation = Mathf.Sin(x) + Mathf.Sin(y);

                if (balloon.Position.y > _screenTopVerticalThreshold + 2)
                {
                    balloon.OnClick -= HandleClick;
                    balloon.gameObject.SetActive(false);
                    _waveModel.RemoveAt(i);
                    _balloonPool.Release(balloon);
                    _waveModel.OnBalloonLeft(balloon);
                    _waveModel.NotifyIfWaveEnded();
                }
            }
        }

        private void IncrementWaveIndex()
        {
            var waveIndex = _waveModel.CurrentWaveIndex;
            waveIndex++;
            if (waveIndex > _waveModel.Waves.Length - 1)
            {
                waveIndex = 0;
                // TODO make floating faster
            }

            _waveModel.CurrentWaveIndex = waveIndex;
        }

        public void Reset()
        {
            _waveModel.CurrentWaveIndex = 0;
            for (var index = _waveModel.ActiveBalloonsCount - 1; index >= 0; index--)
            {
                var balloon = _waveModel[index];
                _waveModel.RemoveAt(index);
                _balloonPool.Release(balloon);
            }
        }
        
        private void HandleClick(Balloon balloon)
        {
            balloon.OnClick -= HandleClick;
            balloon.gameObject.SetActive(false);
            var effect = _effectPool.Get();
            effect.PlayAt(balloon.Position, () => _effectPool.Release(effect));
            _waveModel.Remove(balloon);
            _balloonPool.Release(balloon);
            _waveModel.OnBalloonPopped(balloon);
            _waveModel.NotifyIfWaveEnded();
        }

        private Vector2 GetRandomSpawnPosition()
        {
            var offsetX = Screen.width / 3;
            var centerX = Screen.width / 2;
            var y = -Screen.height * 0.2f;
            var screenPositionX = centerX + Random.Range(-offsetX, offsetX);
            var spawnPosition = _camera.ScreenToWorldPoint(new Vector3(screenPositionX, y, 0));
            return spawnPosition;
        }
    }
}