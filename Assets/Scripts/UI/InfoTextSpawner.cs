using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    public class InfoTextSpawner : MonoBehaviour
    {
        [SerializeField] private WorldTextPanel _textPrefab;
        public void Spawn(float damage)
        {
            WorldTextPanel instance = Instantiate(_textPrefab, transform);
            instance.GetComponent<RectTransform>().localPosition = Vector3.zero;
            instance.ShowText(damage);
        }
    }

}

