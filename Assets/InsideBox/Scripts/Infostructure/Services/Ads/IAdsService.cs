namespace Scripts.Infostructure.Services.Ads
{
    public interface IAdsService : IService
    {
        void Initialize();
        public int Reward { get; }
    }
}