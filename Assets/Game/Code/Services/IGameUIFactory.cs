using Game.Code.UI;

namespace Game.Code.Services
{
    public interface IGameUIFactory
    {
        HealthView CreateHealthView();
        ScoreView CreateGameScoreView();
        FinalScoreView CreateFinalScoreView();
    }
}