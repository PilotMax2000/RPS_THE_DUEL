
public class StateMachine
{
    private IState _currentlyRunningState;
    private IState _previousState;

    public IState CurrentlyRunningState => _currentlyRunningState;

    public void ChangeState(IState newState)
    {
        if(_currentlyRunningState != null)
        {
            _currentlyRunningState.Exit();
        }
        _previousState = _currentlyRunningState;
        _currentlyRunningState = newState;
        _currentlyRunningState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        var runningState = _currentlyRunningState;
        if(runningState != null)
        {
            runningState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        _currentlyRunningState.Exit();
        _currentlyRunningState = _previousState;
        _currentlyRunningState.Enter();
    }
}
