using Game.Characters.Player;
using UnityEngine;

namespace Game.Characters.AI.King
{
    public class CKingAiCharacterController : CFightingAiCharacterController
    {
        [SerializeField] private CPlayerCharacterController _player;
        [SerializeField] private float _playerAggroDistance;
        private float _sqrPlayerAggroDistance;

        protected override void Start()
        {
            base.Start();

            _sqrPlayerAggroDistance = _playerAggroDistance * _playerAggroDistance;
            
            CGameManager.Instance.AddCitySide(this);
        }

        protected override void Update()
        {
            _target = (_player.transform.position - _cachedTransform.position).sqrMagnitude <= _sqrPlayerAggroDistance
                ? _player
                : CGameManager.Instance.GetClosesChaosUnit(_cachedTransform.position);
            
            base.Update();
        }
    }
}