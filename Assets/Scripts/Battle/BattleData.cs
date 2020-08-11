using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    public class BattleData : MonoBehaviour
    {
        [SerializeField] private Fighter _player1;
        [SerializeField] private Fighter _player2;
        [SerializeField] private CharactersStatsDB _statsDB;

        [Header("Timers/Waiting time")] 
        [SerializeField] private float _waitBeforeShowingVictoryPanel = 1.5f;
        [SerializeField] private float _waitAfterSceneStart = 0.75f;
        
        [Header("Characters ID's DB")]
        [SerializeField] private BattleCharsIDs _battleCharsIDs;

        public Fighter Player1 => _player1;
        public Fighter Player2 => _player2;
        public float WaitBeforeShowingVictoryPanel => _waitBeforeShowingVictoryPanel;
        public float WaitAfterSceneStart => _waitAfterSceneStart;
        
        public static BattleData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(BattleData)) as BattleData;
                    if (_instance == null)
                    {
                        Debug.LogWarning("There should be at least one BattleData on the scene");
                    }
                }
                return _instance;
            }
        }

        private static BattleData _instance;
        

        public int GetCharID(CharacterType charType)
        {
            if (charType == CharacterType.Undefined)
            {
                Debug.LogWarning("Character type is not selected!");
                return -1;
            }
            return charType == CharacterType.Player1 ? _battleCharsIDs.FirstCharacterID : _battleCharsIDs.SecondCharacterID;
        }

        public CharactersStatsDB GetStatsDB()
        {
            return _statsDB;
        }
    }
}

