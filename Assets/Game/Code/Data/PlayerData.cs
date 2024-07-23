using System;
using System.Collections.Generic;

namespace Game.Code.Data
{
    [Serializable]
    public class PlayerData
    {
        public List<PlayerDataEntry> Entries = new List<PlayerDataEntry>();
    }
}