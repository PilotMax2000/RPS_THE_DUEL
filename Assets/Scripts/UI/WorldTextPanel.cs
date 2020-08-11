using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTextPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _infoText;
    [SerializeField] private Animation _anim;
    [SerializeField] private string _format = "-{0:0}HP";
    public void ShowText(float value)
    {
        _infoText.text = string.Format(_format, value);
        _anim.Play();
    }
        
    // Animation event
    public void DestroyAfterAnimationEnds()
    {
        Destroy(gameObject);
    }
}
