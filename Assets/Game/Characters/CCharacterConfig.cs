using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig")]
    public /*abstract*/ class CCharacterConfig : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _dmg;
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed => _movementSpeed;
    }
}