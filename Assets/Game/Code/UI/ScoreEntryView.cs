using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI
{
    public class ScoreEntryView : MonoBehaviour
    {
        [SerializeField] private Text _playerNameView;
        [SerializeField] private Text _pointsView;
        
        public void SetData(string playerName, string points)
        {
            _playerNameView.text = playerName;
            _pointsView.text = points;
        }
    }
}