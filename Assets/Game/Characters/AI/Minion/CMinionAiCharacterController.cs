namespace Game.Characters.AI.Minion
{
    public class CMinionAiCharacterController : CFightingAiCharacterController
    {
        protected override void Start()
        {
            base.Start();
            
            CGameManager.Instance.AddChaosSide(this);
            CGameManager.Instance.OnCityWin += () => _die?.Invoke();
            _onDeath = () =>
            {
                CGameManager.Instance.RemoveChaosSide(this);
                _navMeshAgent.SetDestination(_cachedTransform.position);
            };
        }

        public override void MyUpdate()
        {
            _target = CGameManager.Instance.GetClosesCityUnit(_cachedTransform.position);
            _startingPosition = CGameManager.Instance.Player.transform.position;

            base.MyUpdate();
        }
    }
}