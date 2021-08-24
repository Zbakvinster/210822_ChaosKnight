using System;
using System.Collections;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game
{
    public class CGameManager : MonoBehaviour
    {
        [SerializeField] private CGameOverController _gameOverController;
        [SerializeField] private float _gameOverDelay;
        
        public static CGameManager Instance;
        
        public Action OnCityWin;

        private readonly List<CBaseCharacter> _chaosSide = new List<CBaseCharacter>();
        private readonly List<CBaseCharacter> _citySide = new List<CBaseCharacter>();

        public void AddChaosSide(CBaseCharacter unit) => _chaosSide.Add(unit);
        
        public void AddCitySide(CBaseCharacter unit) => _citySide.Add(unit);

        public void RemoveChaosSide(CBaseCharacter unit) => _chaosSide.Remove(unit);
        
        public void RemoveCitySide(CBaseCharacter unit) => _citySide.Remove(unit);

        public CBaseCharacter GetClosesChaosUnit(Vector3 position) => GetClosestUnit(_chaosSide, position);
        
        public CBaseCharacter GetClosesCityUnit(Vector3 position) => GetClosestUnit(_citySide, position);

        public void CityWin()
        {
            OnCityWin?.Invoke();
            StartCoroutine(GameOverTimer());
        }

        private IEnumerator GameOverTimer()
        {
            yield return new WaitForSeconds(_gameOverDelay);

            _gameOverController.StartTransition();
        }

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