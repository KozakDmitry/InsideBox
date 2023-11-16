using Unity.VisualScripting;

namespace Scripts.Data
{
    public class PlayerProgress 
    {
        public WorldData worldData;
        public State HeroState;
        public Stats HerosStats;
        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            HeroState = new State();
            HerosStats = new Stats(); 
        }
    }
}