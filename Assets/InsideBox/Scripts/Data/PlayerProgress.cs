using Unity.VisualScripting;

namespace Scripts.Data
{
    public class PlayerProgress 
    {
        public WorldData worldData;
        public State HeroState;
        public Stats HeroStats;
        public KillData KillData;
        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            HeroState = new State();
            HeroStats = new Stats(); 
            KillData = new KillData();
        }

    }
}