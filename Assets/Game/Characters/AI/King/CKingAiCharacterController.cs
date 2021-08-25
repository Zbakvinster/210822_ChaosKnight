using Game.Characters.Player;
using UnityEngine;

namespace Game.Characters.AI.King
{
    public class CKingAiCharacterController : CFightingAiCharacterController
    {
        [SerializeField] private CPlayerCharacterController _player;
        [SerializeField] private float _playerAggroDistance;
        private float _sqrPlayerAggroDistance;
        private bool _isGameOver;

        protected override void Start()
        {
            base.Start();

            _sqrPlayerAggroDistance = _playerAggroDistance * _playerAggroDistance;
            
            CGameManager.Instance.AddCitySide(this);
            CGameManager.Instance.OnCityWin += () =>
            {
                _onUpdateAction = null;
                StopAttackCoroutine();
                _isGameOver = true;
            };
            _onDeath = () =>
            {
                CGameManager.Instance.RemoveCitySide(this);
                CGameManager.Instance.ChaosWin();
                _navMeshAgent.SetDestination(_cachedTransform.position);
            };
        }

        protected override void Update()
        {
            if (!_isGameOver)
                _target = (_player.transform.position - _cachedTransform.position).sqrMagnitude
                          <= _sqrPlayerAggroDistance
                    ? _player
                    : CGameManager.Instance.GetClosesChaosUnit(_cachedTransform.position);
            
            base.Update();
        }
    }
}