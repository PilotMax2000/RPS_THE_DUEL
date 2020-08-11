using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    public class BaseCharStats : MonoBehaviour,ICharacter
    {
        [SerializeField] private CharacterType _charType;
        private CharactersStatsDB _statsDB;
        private int _id = -1;
        private CharactersStatsDB _statsDBCache
        {
            get
            {
                if (_statsDB == null)
                {
                    _statsDB = BattleData.Instance.GetStatsDB();
                }
                return _statsDB;
            }
        }

        public int ID
        {
            get
            {
                if (_id == -1)
                {
                    _id = BattleData.Instance.GetCharID(_charType);
                }

                if (_id == -1)
                {
                    Debug.LogError($"Can't find the for the character of type {_charType}!");
                }
                return _id;
            }

        }

        public float GetStat(Stat stat)
        {
            //TODO: add multipliers from items/skills
            return (GetBaseStats(stat));
        }

        public string GetCharName()
        {
            return _statsDBCache.GetName(ID);
        }

        public bool IsAI()
        {
            return _statsDBCache.GetCharStatsData(ID).IsAI;
        }

        private float GetBaseStats(Stat stat)
        {
            return _statsDBCache.GetStat(stat, ID);
        }
        
        
    }
    
    public enum CharacterType
    {
        Undefined,
        Player1,
        Player2
    }
}

