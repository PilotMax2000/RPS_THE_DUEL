using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsHandler : MonoBehaviour
{
    private Dictionary<GameEvent, UnityEvent> _eventDictionary;
    private static EventsHandler _eventManager;

    private static EventsHandler Instance
    {
        get
        {
            if (_eventManager == false)
            {
                _eventManager = FindObjectOfType(typeof(EventsHandler)) as EventsHandler;
                if (_eventManager == null)
                {
                    Debug.LogWarning(("There needs to be one active EventManger on a GameObject in the scene"));
                }
                else
                {
                    _eventManager.Init();
                }
            }
            return _eventManager;
        }
    }

    private void Init()
    {
        if (_eventDictionary == null)
        {
            _eventDictionary = new Dictionary<GameEvent, UnityEvent>();
        }
    }

    public static void StartListening(GameEvent eventName, UnityAction listener)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(GameEvent eventName, UnityAction listener)
    {
        if (_eventManager == null)
        {
            return;
        }
        UnityEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(GameEvent eventName)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
            Debug.Log("Event was triggered: " + eventName);
        }
    }

}
public enum GameEvent
{
    ShowChooseElementPanel,
    ElementWasChosen,
    HideChooseElementPanel,
    ShowDuelResult,
    HideDuelResult,
    StartNewDuel,
    ShowIntroPanel,
    HideInfoPanel,
    ShowPlayerTurnPanel,
    CastSelectedElements,
    SpawnPlayers,
    UpdatePlayersScores,
    ShowVictoryPanel,
    ActivatePlayersVictoryStates,
    FighterStatsWasChanged,
    BeginDuel
}