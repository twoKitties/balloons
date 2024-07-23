using Game.Code.Services;
using UnityEngine;

namespace Game.Code.EntryPoints
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            var loading = new SceneLoadingService().LoadAsync("Menu", null);
            StartCoroutine(loading);
        }
    }
}
