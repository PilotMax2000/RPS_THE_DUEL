using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace DuelsRPG
{
    public class Health : MonoBehaviour
    {
        private LazyValue<float> _health;
        private bool _isDead;
        private Animator _anim;
        
        [SerializeField] private TakeDamageEvent _takeDamage;
        [SerializeField] private UnityEvent _onDie;
        private int _isDeadTrigger = Animator.StringToHash("IsDead");
        private int _wasAttacked = Animator.StringToHash("WasAttacked");
        
        public bool IsDead => _isDead;
        
        private void Awake()
        {
            _anim  = GetComponent<Animator>();
            _health = new LazyValue<float>(InitHealth);
        }
        
        void Start()
        {
            _health.ForceInit();
        }

        private float InitHealth()
        {
            float initHP = GetComponent<BaseCharStats>().GetStat(Stat.Health);
            Debug.Log($"{gameObject.name} HP was initialized with the value of {initHP}");
            return initHP;
        }

        public void TakeDamage(float damage, GameObject whoIdDealingDamage = null)
        {
            if (_isDead)
            {
                return;
            }
            
            _health.value = Mathf.Max(_health.value - damage, 0);
            _takeDamage.Invoke(damage);
            Debug.Log($"Character {gameObject.name} took damage (<color=red>{damage}</color>)<color=green> {_health.value} </color> HP left;");
            EventsHandler.TriggerEvent(GameEvent.FighterStatsWasChanged);
            if (_health.value <= 0)
            {
                //AwardExperience(whoIdDealingDamage);
                Die();
                return;
            }
            _anim.SetTrigger(_wasAttacked);
        }
        
        private void Die()
        {
            _isDead = true;
            _anim.SetTrigger(_isDeadTrigger);
            Debug.Log(gameObject.name + " died.");
            _onDie.Invoke();
        }
        
        public float GetPoints()
        {
            return _health.value;
        }

        public float GetMaxPoints()
        {
            return GetComponent<BaseCharStats>().GetStat(Stat.Health);
        }
        
        public float GetFraction()
        {
            return _health.value / GetMaxPoints();
        }
    }
    
    [Serializable]  public class TakeDamageEvent : UnityEvent<float>{}
}

