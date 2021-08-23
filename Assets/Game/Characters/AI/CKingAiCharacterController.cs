using System;
using System.Collections;
using Game.Characters.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public class CKingAiCharacterController : CBaseCharacter
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CPlayerCharacterController _player;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _restAnimationDuration;
        
        private Transform _playerTransformCached;
        private Action _onUpdateAction;
        private float _stoppingDistanceSqrtCached;
        private Transform _transformCached;

        public void Start()
        {
            _playerTransformCached = _player.transform;
            _onUpdateAction = GoAfterPlayer;
            _stoppingDistanceSqrtCached = Mathf.Pow(_navMeshAgent.stoppingDistance, 2);
            _transformCached = transform;
        }
        
        public void Update()
        {
            _onUpdateAction?.Invoke();
        }
        
        private void GoAfterPlayer()
        {
            if ((_playerTransformCached.position - _transformCached.position).sqrMagnitude < _stoppingDistanceSqrtCached)
            {
                _onUpdateAction = null;
                StartCoroutine(Attack());
            }
            else
                _navMeshAgent.SetDestination(_playerTransformCached.position);
        }

        private IEnumerator Attack()
        {
            // Play attack anim

            yield return new WaitForSeconds(_attackDelay);

            _player.ApplyDamage(_damage);

            yield return new WaitForSeconds(_restAnimationDuration);

            _onUpdateAction = GoAfterPlayer;
        }
    }
}