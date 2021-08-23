using System;
using System.Collections;
using Game.Characters.Player;
using UnityEngine;

namespace Game.Characters.AI
{
    public class CKingAiCharacterController : CBaseAiCharacter
    {
        private CPlayerCharacterController _player;
        private Transform _playerTransformCached;
        private Action _onUpdateAction;
        private float _stoppingDistanceSqrtCached;
        private Transform _transformCached;

        public void Init(CPlayerCharacterController player)
        {
            _player = player;
            _playerTransformCached = _player.transform;
            _onUpdateAction = GoAfterPlayer;
            _stoppingDistanceSqrtCached = Mathf.Pow(_navMeshAgent.stoppingDistance, 2);
            _transformCached = transform;
        }
        
        public override void OnUpdate(float deltaTime)
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
            // Play animation

            yield return new WaitForSeconds(_characterConfig.DamageDelay);
            
            // attack

            yield return new WaitForSeconds(_characterConfig.RestAttackAnimationDuration);

            _onUpdateAction = GoAfterPlayer;
        }
    }
}