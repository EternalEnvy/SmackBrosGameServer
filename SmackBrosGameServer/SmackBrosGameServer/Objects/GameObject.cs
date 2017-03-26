namespace SmackBrosGameServer
{
    public interface IGameObject
    {
        double IntersectsRay(Vector2 origin, Vector2 direction);
    }
}
