using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DuelsRPG
{
    public class FighterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fighterNameText;
        [SerializeField] private TextMeshProUGUI _hpValueText;
        [SerializeField] private Image _hpBarFillImage;
        private Health _health;
        private BaseCharStats _stats;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _stats = GetComponent<BaseCharStats>();
        }

        private void OnEnable()
        {
            EventsHandler.StartListening(GameEvent.SpawnPlayers, SetupFighterUI);
            EventsHandler.StartListening(GameEvent.FighterStatsWasChanged, UpdateFighterHP);
        }

        private void OnDisable()
        {
            EventsHandler.StopListening(GameEvent.SpawnPlayers, SetupFighterUI);
            EventsHandler.StopListening(GameEvent.FighterStatsWasChanged, UpdateFighterHP);
        }

        private void SetupFighterUI()
        {
            UpdateFighterHP();
            _fighterNameText.text = _stats.GetCharName();
        }

        private void UpdateFighterHP()
        {
            _hpValueText.text = $"{_health.GetPoints():0}/{_health.GetMaxPoints():0}";
            _hpBarFillImage.fillAmount = _health.GetFraction();
        }
    }
}

