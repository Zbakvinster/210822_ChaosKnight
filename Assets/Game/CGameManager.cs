using System;
using System.Collections;
using System.Collections.Generic;
using Game.Characters;
using Game.Characters.Player;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class CGameManager : MonoBehaviour
    {
        [SerializeField] private CPlayerCharacterController _player;
        [SerializeField] private CGameOverController _gameOverController;
        [SerializeField] private float _gameOverDelay;
        [SerializeField] private CGameWinController _gameWinController;
        [SerializeField] private float _gameWinDelay;
        
        public static CGameManager Instance;
        
        public Action OnCityWin;
        public Action OnChaosWin;

        private readonly List<CBaseCharacter> _chaosSide = new List<CBaseCharacter>();
        private readonly List<CBaseCharacter> _citySide = new List<CBaseCharacter>();

        public CPlayerCharacterController Player => _player;

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

        public void ChaosWin()
        {
            OnChaosWin?.Invoke();
            StartCoroutine(GameWinTimer());
        }

        private IEnumerator GameOverTimer()
        {
            yield return new WaitForSeconds(_gameOverDelay);

            _gameOverController.StartTransition();
        }

        private IEnumerator GameWinTimer()
        {
            yield return new WaitForSeconds(_gameWinDelay);

            _gameWinController.StartTransition();
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            for (int i = 0; i < _chaosSide.Count; i++)
            {
                _chaosSide[i].MyUpdate();
            }
            
            for (int i = 0; i < _citySide.Count; i++)
            {
                _citySide[i].MyUpdate();
            }
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