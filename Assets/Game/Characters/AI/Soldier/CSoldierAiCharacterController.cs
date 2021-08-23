namespace Game.Characters.AI.Soldier
{
    public class CSoldierAiCharacterController : CFightingAiCharacterController
    {
        protected override void Start()
        {
            base.Start();
            
            CGameManager.Instance.AddCitySide(this);
        }

        protected override void Update()
        {
            _target = CGameManager.Instance.GetClosesChaosUnit(_cachedTransform.position);
            
            base.Update();
        }
    }
}