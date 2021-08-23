using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game
{
    public class CGameManager : MonoBehaviour
    {
        public static CGameManager Instance;

        private readonly List<CBaseCharacter> _chaosSide = new List<CBaseCharacter>();
        private readonly List<CBaseCharacter> _citySide = new List<CBaseCharacter>();

        public void AddChaosSide(CBaseCharacter unit) => _chaosSide.Add(unit);
        
        public void AddCitySide(CBaseCharacter unit) => _citySide.Add(unit);

        public CBaseCharacter GetClosesChaosUnit(Vector3 position) => GetClosestUnit(_chaosSide, position);
        
        public CBaseCharacter GetClosesCityUnit(Vector3 position) => GetClosestUnit(_citySide, position);

        private void Awake()
        {
            Instance = this;
        }

        private CBaseCharacter GetClosestUnit(List<CBaseCharacter> units, Vector3 position)
        {
            if (units.Count <= 0)
                return null;
            
            float sqrDis = float.MaxValue;
            int closest = 0;
            for (int i = 0; i < units.Count; i++)
            {
                float tmpSqrDis = (units[i].transform.position - position).sqrMagnitude;
                if (tmpSqrDis < sqrDis)
                {
                    sqrDis = tmpSqrDis;
                    closest = i;
                }
            }

            return units[closest];
        }
    }
}