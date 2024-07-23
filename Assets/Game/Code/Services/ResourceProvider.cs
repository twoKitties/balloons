using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Code.Services
{
    public class ResourceProvider : IResourceProvider
    {
        public async Task<TResource> LoadAsync<TResource>(string path)
        {
            var handle = Addressables.LoadAssetAsync<TResource>(path);
            if (handle.IsDone)
                return handle.Result;
            
            await handle.Task;
            return handle.Result;
        }
    }
}