namespace Game.Code.Core
{
    public interface IGameFactory
    {
        Balloon CreateBalloon();
        BalloonExplosion CreateExplosion();
    }
}