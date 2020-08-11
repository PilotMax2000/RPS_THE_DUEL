using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleVictory : IState
{
    public void Enter()
    {
        EventsHandler.TriggerEvent(GameEvent.ShowVictoryPanel);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        EventsHandler.TriggerEvent(GameEvent.HideInfoPanel);
    }
}
