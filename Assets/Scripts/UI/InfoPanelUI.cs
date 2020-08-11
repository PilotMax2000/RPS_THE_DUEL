using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DuelsRPG;

public class InfoPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _acceptButtonText;
    [SerializeField] private UnityEngine.UI.Button _acceptButton;

    public void SetupPanel(InfoPanelStateData stateData, Fighter character, UnityAction buttonAction)
    {
        _headerText.text = stateData.HeaderText;
        if(character != null)
        {
            _messageText.text = string.Format(stateData.MessageText, character.Name);
        }
        else
        {
            _messageText.text = stateData.AlternativeMessageText;
        }

        _acceptButtonText.text = stateData.ButtonText;
        _acceptButton.onClick.RemoveAllListeners();
        _acceptButton.onClick.AddListener(buttonAction);
    }
}
