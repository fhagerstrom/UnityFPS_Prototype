public abstract class BaseState
{
    // Instance of enemy class
    public Enemy enemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
