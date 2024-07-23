using Game.Code.Data;
using UnityEngine;

namespace Game.Code.Services
{
    public class PlayerPrefsDataService : IPersistentDataService
    {
        private const string DataKey = "PlayerData";
        private PlayerData _data;

        public PlayerData GetPlayerData()
        {
            if(_data != null)
                return _data;
            
            var json = PlayerPrefs.GetString(DataKey);
            var data = JsonUtility.FromJson<PlayerData>(json);
            return data ?? new PlayerData();
        }

        public void SavePlayerData(PlayerData data)
        {
            _data = data;
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(DataKey, json);
            PlayerPrefs.Save();
        }
    }
}