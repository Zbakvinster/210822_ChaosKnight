using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public class CFightingAiCharacterController : CBaseCharacter
    {
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        
        protected CBaseCharacter _target;
        protected Transform _cachedTransform;
        
        private float _cachedStoppingDistanceSqrt;

        protected override void Start()
        {
            base.Start();
            
            _cachedTransform = transform;
            _cachedStoppingDistanceSqrt = Mathf.Pow(_navMeshAgent.stoppingDistance, 2);
            
            _onUpdateAction = GoAfterTarget;
        }
        
        private void GoAfterTarget()
        {
            if (_target == null)
                return;

            if ((_target.transform.position - _cachedTransform.position).sqrMagnitude < _cachedStoppingDistanceSqrt)
            {
                _attackCoroutine = StartCoroutine(Attack(_target, GoAfterTarget));
                _animationController.PlayRun(false);
            }
            else
            {
                _navMeshAgent.SetDestination(_target.transform.position);
                _animationController.PlayRun(true);
            }
        }
    }
}