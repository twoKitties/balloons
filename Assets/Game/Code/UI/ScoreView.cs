using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private CanvasGroup _group;

        public void SetScore(string value)
        {
            _text.text = value;
        }

        public void SetActive(bool isActive)
        {
            _group.alpha = isActive ? 1 : 0;
        }
    }
}