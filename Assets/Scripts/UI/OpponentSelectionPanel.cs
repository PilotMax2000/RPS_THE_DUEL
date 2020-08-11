using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSelectionPanel : MonoBehaviour
{
    [SerializeField] private BattleCharsIDs _battleCharsIDs;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _sceneID = 1;
    private int _onNextSceneTrigger = Animator.StringToHash("OnNextScene") ;

    public void SetOpponentID(int id)
    {
        _battleCharsIDs.SecondCharacterID = id;
        _animator.SetTrigger(_onNextSceneTrigger);
    }

    //AnimationEvent
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneID);
    }
}
