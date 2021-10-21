public interface IRaycastService : IService
{
    void Raycast();
    void Raycast(int layerMask);
}