namespace Scripts.Infostructure
{
    public interface IState :IExitableState
    {
        void Enter();
        void Exit();
    }

   

    public interface IPayloadedState<TPayload> :IExitableState
    {
        void Enter(TPayload);
        void Exit();
    }
    public interface IExitableState
    {
        void Exit();
    }


}