namespace Game.Characters.AI.Soldier
{
    public class CSoldierAiCharacterController : CFightingAiCharacterController
    {
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
                CGameManager.Instance.OnChaosWin -= OnChaosWin_Run;
                CGameManager.Instance.RemoveCitySide(this);
                _navMeshAgent.SetDestination(_cachedTransform.position);
            };

            void OnChaosWin_Run()
            {
                StopAttackCoroutine();
                _onUpdateAction = () => _navMeshAgent.destination = (_cachedTransform.position - CGameManager.Instance.Player.transform.position).normalized * (_navMeshAgent.stoppingDistance + 1) + _cachedTransform.position;
            }
        }

        protected override void Update()
        {
            _target = CGameManager.Instance.GetClosesChaosUnit(_cachedTransform.position);
            
            base.Update();
        }
    }
}