using System.Threading.Tasks;
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

        private readonly IResourceProvider _resourceProvider;

        public CoreResourcesInitializer(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }

        public async Task Initialize()
        {
            var json = await _resourceProvider.LoadAsync<TextAsset>("Assets/Game/Core/LevelData.json");
            WaveConfig = JsonUtility.FromJson<WaveConfig>(json.text);
            HUDPrefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/Hud.prefab");
            var prefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/Balloon.prefab");
            BalloonPrefab = prefab.GetComponent<Balloon>();
            prefab = await _resourceProvider.LoadAsync<GameObject>("Assets/Game/Core/BalloonExplosion.prefab");
            EffectPrefab = prefab.GetComponent<BalloonExplosion>();
        }
    }
}