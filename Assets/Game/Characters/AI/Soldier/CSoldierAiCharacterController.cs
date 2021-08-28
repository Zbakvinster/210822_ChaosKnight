using UnityEngine;

namespace Game.Characters.AI.Soldier
{
    public class CSoldierAiCharacterController : CFightingAiCharacterController
    {
        [SerializeField] private Collider _collider;
        
        private bool _isDead;

        protected override void Start()
        {
            base.Start();
            
            CGameManager.Instance.AddCitySide(this);

            CGameManager.Instance.OnChaosWin += OnChaosWin_Run;
            CGameManager.Instance.OnCityWin += () =>
            {
                _onUpdateAction = null;
                StopAttackCoroutine();
            };
            _onDeath = () =>
            {
                _isDead = true;
                _collider.enabled = false;
                CGameManager.Instance.OnChaosWin -= OnChaosWin_Run;
                CGameManager.Instance.RemoveCitySide(this);
                _navMeshAgent.SetDestination(_cachedTransform.position);
            };

            void OnChaosWin_Run()
            {
                if (_isDead)
                    return;
                
                StopAttackCoroutine();
                _onUpdateAction = ()
                    => _navMeshAgent.destination
                        = (_cachedTransform.position - CGameManager.Instance.Player.transform.position).normalized
                          * (_navMeshAgent.stoppingDistance + 1)
                          + _cachedTransform.position;
            }
        }

        public override void MyUpdate()
        {
            _target = CGameManager.Instance.GetClosesChaosUnit(_cachedTransform.position);
            
            base.MyUpdate();
        }
    }
}