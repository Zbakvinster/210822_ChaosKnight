using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public class CFightingAiCharacterController : CBaseCharacter
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _restAnimationDuration;
        
        protected CBaseCharacter _target;
        protected Transform _cachedTransform;
        
        private Action _onUpdateAction;
        private float _cachedStoppingDistanceSqrt;

        protected override void Start()
        {
            base.Start();
            
            _cachedTransform = transform;
            _cachedStoppingDistanceSqrt = Mathf.Pow(_navMeshAgent.stoppingDistance, 2);
            
            _onUpdateAction = GoAfterTarget;
        }
        
        protected virtual void Update()
        {
            _onUpdateAction?.Invoke();
        }
        
        private void GoAfterTarget()
        {
            if (_target == null)
                return;

            if ((_target.transform.position - _cachedTransform.position).sqrMagnitude < _cachedStoppingDistanceSqrt)
            {
                _onUpdateAction = null;
                StartCoroutine(Attack(_target));
            }
            else
                _navMeshAgent.SetDestination(_target.transform.position);
        }

        private IEnumerator Attack(CBaseCharacter target)
        {
            // Play attack anim

            yield return new WaitForSeconds(_attackDelay);

            target.ApplyDamage(_damage);

            yield return new WaitForSeconds(_restAnimationDuration);

            _onUpdateAction = GoAfterTarget;
        }
    }
}