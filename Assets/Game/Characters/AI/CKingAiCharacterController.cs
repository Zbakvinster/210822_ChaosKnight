using Game.Characters.Player;
using UnityEngine;

namespace Game.Characters.AI
{
    public class CKingAiCharacterController : CBaseAiCharacter
    {
        private CPlayerCharacterController _player;
        private Transform _playerTransformCached;

        public void Init(CPlayerCharacterController player)
        {
            _player = player;
            _playerTransformCached = _player.transform;
        }
        
        public override void OnUpdate(float deltaTime)
        {
            _navMeshAgent.SetDestination(_playerTransformCached.position);
        }
    }
}