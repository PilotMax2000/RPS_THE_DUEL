using System;
using UnityEngine;
using UnityEngine.UI;
using DuelsRPG;

public class ChooseElementPanelUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button _rockButton;
    [SerializeField] private Button _paperButton;
    [SerializeField] private Button _scissorsButton;
    [SerializeField] private TMPro.TextMeshProUGUI _infoForCurrentPlayer;
    [SerializeField] private string _infoTemplate;

    private void Start()
    {
        _rockButton.onClick.AddListener(() => SelectElementForCurrentPlayer(Element.Rock));
        _paperButton.onClick.AddListener(() => SelectElementForCurrentPlayer(Element.Paper));
        _scissorsButton.onClick.AddListener(() => SelectElementForCurrentPlayer(Element.Scissors));
    }

    private void OnEnable() {
        _infoForCurrentPlayer.text = String.Format(_infoTemplate, BattleStatesHandler.Instance.CurrentFighter.Name);
    }

    private void SelectElementForCurrentPlayer(Element element)
    {
        BattleStatesHandler.Instance.CurrentFighter.SelectedElement = element;
        EventsHandler.TriggerEvent(GameEvent.ElementWasChosen);
    }
}
