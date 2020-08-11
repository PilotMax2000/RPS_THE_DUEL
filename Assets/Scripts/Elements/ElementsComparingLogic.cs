using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    public static class ElementsComparingLogic
    {
        private static Dictionary<Element, Element> _elementsForBeatingOpponent = new Dictionary<Element, Element>(){
            {Element.Rock, Element.Scissors},
            {Element.Scissors, Element.Paper},
            {Element.Paper, Element.Rock}
        };

        public static bool ShouldBeDestroyed(Element currentElement, Element oppositeElement)
        {
            if(currentElement == Element.None || oppositeElement == Element.None)
            {
                Debug.LogWarning("Elements in battel elements have wrong settings!");
                return false;
            }
            return _elementsForBeatingOpponent[currentElement] != oppositeElement;
        }
    }

}
