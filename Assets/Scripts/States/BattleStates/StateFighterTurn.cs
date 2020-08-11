using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelsRPG;

public class StateFighterTurn : IState
{
    private Fighter _fighter;

    public StateFighterTurn(Fighter character)
    {
        _fighter = character;
    }
    public void Enter()
    {
        if (_fighter.IsAI == false)
        {
            EventsHandler.TriggerEvent(GameEvent.ShowPlayerTurnPanel);
        }
        else
        {
            _fighter.SelectedElement = _fighter.FighterAI.GetChosenElement();
            EventsHandler.TriggerEvent(GameEvent.ElementWasChosen);
        }
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        if (_fighter.IsAI == false)
        {
            EventsHandler.TriggerEvent(GameEvent.HideChooseElementPanel);
        }
    }
}
