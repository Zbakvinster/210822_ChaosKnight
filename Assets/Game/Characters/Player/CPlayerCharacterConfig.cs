using UnityEngine;

namespace Game.Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerCharacterConfig", menuName = "ScriptableObjects/PlayerCharacterConfig")]
    public class CPlayerCharacterConfig : CCharacterConfig
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed => _movementSpeed;
    }
}