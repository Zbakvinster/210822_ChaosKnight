using Game.Characters.Player;
using UnityEngine;

namespace Game.Characters.AI.King
{
    public class CKingAiCharacterController : CFightingAiCharacterController
    {
        [SerializeField] private CPlayerCharacterController _player;

        protected override void Start()
        {
            base.Start();

            _target = _player;
            CGameManager.Instance.AddCitySide(this);
        }
    }
}