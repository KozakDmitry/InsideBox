
namespace Scripts.Data
{
    public class PlayerProgress 
    {
        public WorldData worldData;
        public State HeroState;
        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            HeroState = new State();
        }
    }
}