using System;

namespace Game.Code.Models
{
    public class GameStateModel
    {
        public event Action onRestart;
        public int Score;

        public void OnRestart()
        {
            onRestart?.Invoke();
        }
    }
}