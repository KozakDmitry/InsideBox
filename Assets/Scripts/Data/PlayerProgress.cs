
namespace Scripts.Data
{
    public class PlayerProgress 
    {
        public WorldData worldData;

        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
        }
    }
}