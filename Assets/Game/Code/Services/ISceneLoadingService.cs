using System;
using System.Collections;

namespace Game.Code.Services
{
    public interface ISceneLoadingService
    {
        IEnumerator LoadAsync(string sceneName, Action onLoaded);
    }
}