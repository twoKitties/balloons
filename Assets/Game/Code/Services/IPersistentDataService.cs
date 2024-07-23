using Game.Code.Data;

namespace Game.Code.Services
{
    public interface IPersistentDataService
    {
        PlayerData GetPlayerData();
        void SavePlayerData(PlayerData data);
    }
}