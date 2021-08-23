using System;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CBaseCharacter : MonoBehaviour
    {
        [SerializeField] private float _maxHp;
        [SerializeField] protected float _damage;
        
        private float _actualHp;

        private void Start()
        {
            _actualHp = _maxHp;
        }

        public void ApplyDamage(float damage)
        {
            if ((_actualHp -= damage) <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log($"{name} says: PIČI, já umřel ...");
        }
    }
}