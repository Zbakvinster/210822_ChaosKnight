using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public class CFightingAiCharacterController : CBaseCharacter
    {
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        [SerializeField] private float _aggroDistance;
        
        protected CBaseCharacter _target;
        protected Transform _cachedTransform;
        
        private float _cachedStoppingDistanceSqrt;
        protected Vector3 _startingPosition;

        protected override void Start()
        {
            base.Start();
            
            _cachedTransform = transform;
            _cachedStoppingDistanceSqrt = Mathf.Pow(_navMeshAgent.stoppingDistance, 2);

            _startingPosition = _cachedTransform.position;
            
            _onUpdateAction = GoAfterTarget;
        }
        
        private void GoAfterTarget()
        {
            if (_target == null)
                return;

            float targetDistanceSqrt = (_target.transform.position - _cachedTransform.position).sqrMagnitude;
            if (targetDistanceSqrt < _cachedStoppingDistanceSqrt)
            {
                _attackCoroutine = StartCoroutine(Attack(_target, GoAfterTarget));
                _animationController.PlayRun(false);
            }
            else
            {
                if (targetDistanceSqrt < _aggroDistance)
                {
                    _navMeshAgent.SetDestination(_target.transform.position);
                    _animationController.PlayRun(true);
                }
                else
                {
                    _navMeshAgent.SetDestination(_startingPosition);
                    _animationController.PlayRun(
                        (_startingPosition - transform.position).sqrMagnitude > _cachedStoppingDistanceSqrt);
                }
            }
        }
    }
}