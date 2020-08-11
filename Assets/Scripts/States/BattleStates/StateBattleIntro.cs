using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateBattleIntro : IState
{
    public void Enter()
    {
        EventsHandler.TriggerEvent(GameEvent.SpawnPlayers);
        EventsHandler.TriggerEvent(GameEvent.ShowIntroPanel);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        EventsHandler.TriggerEvent(GameEvent.HideInfoPanel);
    }
}
