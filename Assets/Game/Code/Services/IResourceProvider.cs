using System.Threading.Tasks;

namespace Game.Code.Services
{
    public interface IResourceProvider
    {
        Task<TResource> LoadAsync<TResource>(string path);
    }
}