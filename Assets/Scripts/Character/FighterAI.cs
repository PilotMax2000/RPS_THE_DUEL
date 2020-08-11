using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    public class FighterAI : MonoBehaviour
    {
        public Element GetChosenElement()
        {
            return (Element)Random.Range(1, 4);
        }
    }
}
