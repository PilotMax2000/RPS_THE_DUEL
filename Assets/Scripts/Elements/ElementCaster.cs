using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    
    public class ElementCaster : MonoBehaviour
    {
        [SerializeField] private GameObject _rockBattlePrefab;
        [SerializeField] private GameObject _paperBattlePrefab;
        [SerializeField] private GameObject _scissorsBattlePrefab;

        [Header("Casting Position")] 
        [SerializeField] private Transform _castingFromPosition;

        public void CastSelectedElement(Element selectedElement, float dmg, Action onSpellDestroyed)
        {
            switch (selectedElement)
            {
                case Element.Rock:
                    Instantiate(_rockBattlePrefab, _castingFromPosition).GetComponent<ElementHandler>().SetParams(dmg, onSpellDestroyed);
                    break;
                case Element.Paper:
                    Instantiate(_paperBattlePrefab, _castingFromPosition).GetComponent<ElementHandler>().SetParams(dmg, onSpellDestroyed);
                    break;
                case Element.Scissors:
                    Instantiate(_scissorsBattlePrefab, _castingFromPosition).GetComponent<ElementHandler>().SetParams(dmg, onSpellDestroyed);
                    break;
            }
        }
        
        
    }
}

