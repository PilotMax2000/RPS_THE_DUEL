using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuelsRPG;

public class BattleUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private InfoPanelUI _infoPanel;
    [SerializeField] private ChooseElementPanelUI _chooseElementPanel;

    [Header("States Text Info")]
    [SerializeField] private InfoPanelStateData _introStateData;
    [SerializeField] private InfoPanelStateData _playerTurnNotificationData;
    [SerializeField] private InfoPanelStateData _duelResultData;
    [SerializeField] private InfoPanelStateData _battleResultData;

    private Animator _anim;
    private int _fadeOut = Animator.StringToHash("fadeOut");
    private const float WAIT_AFTER_SCENE_START = 2.25f;
    private const float WAIT_BEFORE_SCENE_RESET = 1.0f;
    private const int SCENE_NUMBER = 0;

    private void OnEnable()
    {
        EventsHandler.StartListening(GameEvent.ShowChooseElementPanel, ShowChooseElementPanel);
        EventsHandler.StartListening(GameEvent.HideChooseElementPanel, HideChooseElementPanel);
        EventsHandler.StartListening(GameEvent.ShowDuelResult, ShowDuelResult);
        EventsHandler.StartListening(GameEvent.HideDuelResult, HideDuelResult);
        EventsHandler.StartListening(GameEvent.ShowIntroPanel, ShowIntroPanel);
        EventsHandler.StartListening(GameEvent.HideInfoPanel, HideInfoPanel);
        EventsHandler.StartListening(GameEvent.ShowPlayerTurnPanel, ShowPlayerTurnPanel);
        EventsHandler.StartListening(GameEvent.ShowVictoryPanel, ShowVictoryPanel);
    }

    private void OnDisable()
    {
        EventsHandler.StopListening(GameEvent.ShowChooseElementPanel, ShowChooseElementPanel);
        EventsHandler.StopListening(GameEvent.HideChooseElementPanel, HideChooseElementPanel);
        EventsHandler.StopListening(GameEvent.ShowDuelResult, ShowDuelResult);
        EventsHandler.StopListening(GameEvent.HideDuelResult, HideDuelResult);
        EventsHandler.StopListening(GameEvent.ShowIntroPanel, ShowIntroPanel);
        EventsHandler.StopListening(GameEvent.HideInfoPanel, HideInfoPanel);
        EventsHandler.StopListening(GameEvent.ShowPlayerTurnPanel, ShowPlayerTurnPanel);
        EventsHandler.StopListening(GameEvent.ShowVictoryPanel, ShowVictoryPanel);
    }
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void ShowIntroPanel()
    {
        StartCoroutine(ShowIntroPanelWithDelay());
    }

    private void ShowPlayerTurnPanel()
    {
        _infoPanel.gameObject.SetActive(true);
        _infoPanel.SetupPanel(_playerTurnNotificationData, BattleStatesHandler.Instance.CurrentFighter,
            ShowChooseElementPanel);
    }

    private void ShowChooseElementPanel()
    {
        _infoPanel.gameObject.SetActive(false);
        _chooseElementPanel.gameObject.SetActive(true);
    }

    private void HideChooseElementPanel()
    {
        _chooseElementPanel.gameObject.SetActive(false);
    }

    private void HideInfoPanel()
    {
        _infoPanel.gameObject.SetActive(false);
    }

    private void ShowDuelResult()
    {
        _infoPanel.gameObject.SetActive(true);
        _infoPanel.SetupPanel(_duelResultData, BattleStatesHandler.Instance.LastDuelResult.Winner,
        () => EventsHandler.TriggerEvent(GameEvent.StartNewDuel));
    }

    private void HideDuelResult()
    {
        _infoPanel.gameObject.SetActive(false);
    }

    private IEnumerator ShowIntroPanelWithDelay()
    {
        yield return new WaitForSeconds(WAIT_AFTER_SCENE_START);
        _infoPanel.SetupPanel(_introStateData, BattleStatesHandler.Instance.FirstFighter,
            () => EventsHandler.TriggerEvent(GameEvent.StartNewDuel));
        _infoPanel.gameObject.SetActive(true);
    }

    private void ShowVictoryPanel()
    {
        StartCoroutine(WaitBeforeShowing(() =>
        {
            _infoPanel.gameObject.SetActive(true);
            _infoPanel.SetupPanel(_battleResultData,
                BattleStatesHandler.Instance.FirstFighter.Health.IsDead
                    ? BattleStatesHandler.Instance.SecondFighter
                    : BattleStatesHandler.Instance.FirstFighter,
                RestartBattle);
        }, BattleData.Instance.WaitBeforeShowingVictoryPanel));

    }

    private IEnumerator WaitBeforeShowing(Action action, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        action.Invoke();
    }

    private void RestartBattle()
    {
        StartCoroutine(RestartBattleWithDelay());
    }

    private IEnumerator RestartBattleWithDelay()
    {
        _anim.SetTrigger(_fadeOut);
        yield return new WaitForSeconds(WAIT_BEFORE_SCENE_RESET);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SCENE_NUMBER);
    }

}

[System.Serializable]
public class InfoPanelStateData
{
    public string HeaderText;
    public string MessageText;
    public string AlternativeMessageText;
    public string ButtonText;
}
