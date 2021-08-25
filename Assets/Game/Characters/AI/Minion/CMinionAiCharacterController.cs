namespace Game.Characters.AI.Minion
{
    public class CMinionAiCharacterController : CFightingAiCharacterController
    {
        protected override void Start()
        {
            base.Start();
            
            CGameManager.Instance.AddChaosSide(this);
            CGameManager.Instance.OnCityWin += () => _die?.Invoke();
            CGameManager.Instance.OnChaosWin += () =>
            {
                _onUpdateAction = null;
                StopAttackCoroutine();
            };
            _onDeath = () =>
            {
                CGameManager.Instance.RemoveChaosSide(this);
                _navMeshAgent.SetDestination(_cachedTransform.position);
            };
        }

        protected override void Update()
        {
            _target = CGameManager.Instance.GetClosesCityUnit(_cachedTransform.position);
            
            base.Update();
        }
    }
}