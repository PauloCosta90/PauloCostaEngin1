
public interface IState
{
    public void OnStart();

    public void OnEnter();

    public void OnFixedUpdate();

    public void OnUpdate();

    public void OnExit();

    public bool CanEnter(IState currentState);

    public bool CanExit();
}