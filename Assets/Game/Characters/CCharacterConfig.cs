using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig")]
    public class CCharacterConfig : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _dmg;
        [SerializeField] private float _damageDelay;
        [SerializeField] private float _restAttackAnimationDuration;

        public float DamageDelay => _damageDelay;
        public float RestAttackAnimationDuration => _restAttackAnimationDuration;
    }
}