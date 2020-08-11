using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelsRPG
{
    [CreateAssetMenu(fileName = "StatsDB", menuName = "CharsData/StatsDB", order = 0)]
    public class CharactersStatsDB : ScriptableObject
    {
        [SerializeField] private CharStatsData[] _charStatsDB;
        private Dictionary<int, Dictionary<Stat, float[]>> _charsStatsCache;
        private Dictionary<int, CharStatsData> _charsDataCache;

        public CharStatsData GetCharStatsData(int charID)
        {
            BuildCache();
            if (_charsStatsCache.ContainsKey(charID) )
            {
                return _charsDataCache[charID];
            }

            Debug.LogError($"CharStats with id _{charID}_ was not found in DB! ");
            return null;
        }
        
        public float GetStat(Stat stat, int charID, int level=0)
        {
            BuildCache();
            if (_charsStatsCache.ContainsKey(charID) == false)
            {
                return 0;
            }

            if (_charsStatsCache[charID].ContainsKey(stat) == false)
            {
                return 0;
            }

            if (_charsStatsCache[charID][stat].Length < level)
            {
                return 0;
            }
            return _charsStatsCache[charID][stat][level];
        }

        public string GetName(int charID)
        {
            BuildCache();
            if (_charsStatsCache.ContainsKey(charID) == false)
            {
                Debug.LogError($"Can't find name for the character with id {charID}");
                return "Unknown";
            }

            return _charsDataCache[charID].Name;
        }

        public float[] GetLevels(Stat stat, int charID)
        {
            BuildCache();
            
            if (_charsStatsCache.ContainsKey(charID) == false)
            {
                return null;
            }

            if (_charsStatsCache[charID].ContainsKey(stat) == false)
            {
                return null;
            }

            return _charsStatsCache[charID][stat];
        }

        private void BuildCache()
        {
            if (_charsStatsCache != null && _charsDataCache != null)
            {
                return;
            }
            
            _charsStatsCache = new Dictionary<int, Dictionary<Stat, float[]>>();
            _charsDataCache = new Dictionary<int, CharStatsData>();
            
            foreach (var character in _charStatsDB)
            {
                Dictionary<Stat, float[]> charStats = new Dictionary<Stat, float[]>();

                foreach (var curStat in character.Stats)
                {
                    charStats.Add(curStat.Stat, curStat.Levels);
                }
                _charsStatsCache.Add(character.ID, charStats);
                _charsDataCache.Add(character.ID, character);
            }
        }
        
        [System.Serializable]
        public class Stats
        {
            public Stat Stat;
            public float[] Levels; 
        }
        
        [System.Serializable]
        public class CharStatsData
        {
            public Stats[] Stats;
            public CharacterType CharacterType;
            public int ID;
            public string Name;
            public bool IsAI;
        }
    }
}

