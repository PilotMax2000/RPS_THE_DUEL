using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DuelsRPG
{
    public class ElementHandler : MonoBehaviour
    {
        public Element Element => _element;
        [SerializeField] private Element _element;
        [SerializeField] private GameObject _elementVFX;
        [SerializeField] private GameObject _destoryVFX;
        [SerializeField] private GameObject _attackPlayerVFX;
        [SerializeField] private float _force = 5.0f;
        [SerializeField] private ParticleSystem _mainDestroyPS;
        [SerializeField] private ParticleSystem _mainAttackPlayerPS;
        private Collider _col;
        private Rigidbody _rb;
        private float _damage;
        private Action _onSpellDestroyed;
    
        //New values 
        [SerializeField] private UnityEvent _onElementHit;
        private Health _health;

        private void Start()
        {
            _col = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();
            _elementVFX.SetActive(true);
            _destoryVFX.SetActive(false);
            _attackPlayerVFX.SetActive(false);
        }

        private void FixedUpdate() {
            _rb.AddForce(transform.forward * _force, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision other)
        {
            ElementHandler elementCache = other.gameObject.GetComponent<ElementHandler>();
            if(elementCache != null)
            {
                if(ElementsComparingLogic.ShouldBeDestroyed(_element, elementCache.Element))
                {
                    DestroyElement();
                }
                return;
            }
            
            Health healthCache = other.gameObject.GetComponent<Health>();
            if (healthCache == null)
            {
                return;
            }
            healthCache.TakeDamage(_damage);
            ShowAttackVFXAndDestroy();
        }

        public void SetParams(float damage, Action onSpellDestoryed)
        {
            _damage = damage;
            _onSpellDestroyed = onSpellDestoryed;
        }

        private void DestroyElement()
        {
            _onElementHit.Invoke();
            _elementVFX.SetActive(false);
            _destoryVFX.SetActive(true);
            _col.enabled = false;
            StartCoroutine(DestroyOnParticlesEffectEnds(_mainDestroyPS));
        }

        private void ShowAttackVFXAndDestroy()
        {
            _onElementHit.Invoke();
            _elementVFX.SetActive(false);
            _attackPlayerVFX.SetActive(true);
            _col.enabled = false;
            _rb.velocity = Vector3.zero;
            _rb.Sleep();
            StartCoroutine(DestroyOnParticlesEffectEnds(_mainAttackPlayerPS));
        }

        private IEnumerator DestroyOnParticlesEffectEnds(ParticleSystem ps)
        {
            while(true && ps != null)
            {
                yield return new WaitForSeconds(0.25f);
                if (ps.IsAlive(true) == false)
                {
                    Destroy(gameObject);
                    yield break;
                }
            }
        }

        private void OnDestroy()
        {
            _onSpellDestroyed.Invoke();
        }
    }
}

