using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelsRPG;

public class StateCastSelectedElements : IState
{
    private bool _firstFighterElementIsActive = false;
    private bool _secondFighterElementIsActive = false;
    public void Enter()
    {
        CastSelectedElements();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        EventsHandler.TriggerEvent(GameEvent.HideDuelResult);
    }
    
    private void CastSelectedElements()
    {
        _firstFighterElementIsActive = true;
        _secondFighterElementIsActive = true;
        
        BattleStatesHandler.Instance.FirstFighter.CastSelectedElement(() =>
        {
            OnSpellDestroyed(out _firstFighterElementIsActive);
        });
        BattleStatesHandler.Instance.SecondFighter.CastSelectedElement(() =>
        {
            OnSpellDestroyed(out _secondFighterElementIsActive);
        });
    }
    
    private void OnSpellDestroyed(out bool fighterElementIsActive)
    {
        fighterElementIsActive = false;
        if (AreCastedElementsActive() == false)
        {
            OnCastingFinished();
        }
    }

    private void OnCastingFinished()
    {
        if (BattleStatesHandler.Instance.IsVictoryConditionReached())
        {
            EventsHandler.TriggerEvent(GameEvent.ActivatePlayersVictoryStates);
            EventsHandler.TriggerEvent(GameEvent.ShowVictoryPanel);
            return;
        }
        
        EventsHandler.TriggerEvent(GameEvent.StartNewDuel);
    }

    private bool AreCastedElementsActive()
    {
        return _firstFighterElementIsActive || _secondFighterElementIsActive;
    }
}
