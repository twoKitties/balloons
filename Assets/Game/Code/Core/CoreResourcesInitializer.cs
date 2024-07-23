using System;
using System.Threading.Tasks;
using Game.Code.Configs;
using Game.Code.Models;
using Game.Code.Services;
using UnityEngine;

namespace Game.Code.Core
{
    public class CoreResourcesInitializer
    {
        public BalloonExplosion EffectPrefab { get; private set; }
        public Balloon BalloonPrefab { get; private set; }
        public GameObject HUDPrefab { get; private set; }
        public WaveConfig WaveConfig { get; private set; }
        public GameStateConfig GameStateConfig { get; private set; }
        public AudioClip PopSound { get; private set; }

        private readonly IResourceProvider _resourceProvider;

        public CoreResourcesInitializer(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }

        public async Task Initialize()
        {
            try
            {
                var json = await _resourceProvider.LoadAsync<TextAsset>("Assets/Game/Core/LevelData.json");
                WaveConfig = JsonUtility.FromJson<WaveConfig>(json.text);
                json = await _resourceProvider.LoadAsync<TextAsset>("Assets/Game/Core/GameConfig.json");
                GameStateConfig = JsonUtility.FromJson<GameStateConfig>(json.text);
                HUDPrefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/Hud.prefab");
                var prefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/Balloon.prefab");
                BalloonPrefab = prefab.GetComponent<Balloon>();
                prefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/BalloonExplosion.prefab");
                EffectPrefab = prefab.GetComponent<BalloonExplosion>();
                var sound = await _resourceProvider.LoadAsync<AudioClip>("Assets/Game/Core/BalloonPop.mp3");
                PopSound = sound;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}