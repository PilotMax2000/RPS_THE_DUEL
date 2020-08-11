using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEngine;

namespace DuelsRPG
{
    [RequireComponent(typeof(BaseCharStats))]
    [RequireComponent(typeof(Health))]
    public class Fighter : MonoBehaviour
    {
        public string Name => _baseStats.value.GetCharName();
        public Element SelectedElement { get; set; }
        public int ID => _baseStats.value.ID;
        public Health Health => _health;
        public bool IsAI => _baseStats.value.IsAI();
        public FighterAI FighterAI => _fighterAI;

        [Header("Elements")] 
        [SerializeField] private Transform _spawnElementPosition;
        [SerializeField] private GameObject _rockBattlePrefab;
        [SerializeField] private GameObject _paperBattlePrefab;
        [SerializeField] private GameObject _scissorsBattlePrefab;

        [Header("Stage Objects")] 
        [SerializeField] private GameObject _victoryAura;

        private Animator _anim;
        private int _isAttacking = Animator.StringToHash("IsAttacking");
        private int _winsInDuels = 0;

        private LazyValue<BaseCharStats> _baseStats;
        private Health _health;
        private ElementCaster _elementCaster;

        private bool _isAI;
        private FighterAI _fighterAI;


        private void Awake()
        {
            SelectedElement = Element.None;
            _anim = GetComponent<Animator>();
            _baseStats = new LazyValue<BaseCharStats>(InitStats);
            _health = GetComponent<Health>();
            _fighterAI = GetComponent<FighterAI>();
            _elementCaster = GetComponent<ElementCaster>();
        }

        private BaseCharStats InitStats()
        {
            Debug.Log("BaseCharStats for init for " + gameObject.name);
            return GetComponent<BaseCharStats>();
        }

        private void OnEnable()
        {
            EventsHandler.StartListening(GameEvent.ActivatePlayersVictoryStates, ActivateVictoryState);
        }

        private void OnDisable()
        {
            EventsHandler.StopListening(GameEvent.ActivatePlayersVictoryStates, ActivateVictoryState);
        }
        
        public void CastSelectedElement(Action onSpellDestroyed)
        {
            float dmg = _baseStats.value.GetStat(Stat.Damage);
            _elementCaster.CastSelectedElement(SelectedElement, dmg, onSpellDestroyed);
            _anim.SetTrigger(_isAttacking);
        }

        private void ActivateVictoryState()
        {
            if (_health.IsDead == false)
            {
                _victoryAura.SetActive(true);
            }
        }
    }
    
    
   
}

