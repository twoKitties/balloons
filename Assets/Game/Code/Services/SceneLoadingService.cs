using System;
using System.Collections;
using UnityEngine.AddressableAssets;

namespace Game.Code.Services
{
    public class SceneLoadingService : ISceneLoadingService
    {
        public IEnumerator LoadAsync(string sceneName, Action onLoaded)
        {
            var addressableKey = sceneName switch
            {
                "Menu" => "MenuScene",
                "Core" => "CoreScene",
                _ => string.Empty
            };
            var operation = Addressables.LoadSceneAsync(addressableKey);
            yield return operation;
            onLoaded?.Invoke();
        }
    }
}