
public interface IState
{
    public void OnEnter();
    public void OnFixedUpdate();
    public void OnUpdate();
    public void OnExit();
    public bool CanEnter(CharacterState state);
    public bool CanExit();
}