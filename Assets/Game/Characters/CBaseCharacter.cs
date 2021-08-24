using System;
using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CBaseCharacter : MonoBehaviour
    {
        [SerializeField] private float _maxHp;
        [SerializeField] protected float _damage;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _restAnimationDuration;
        
        private float _actualHp;
        protected Action _onUpdateAction;

        public void ApplyDamage(float damage)
        {
            if ((_actualHp -= damage) <= 0)
                Die();
        }

        protected virtual void Start()
        {
            _actualHp = _maxHp;
        }
        
        protected virtual void Update()
        {
            _onUpdateAction?.Invoke();
        }

        protected IEnumerator Attack(CBaseCharacter target, Action onUpdateAfterAttack)
            => Attack(() => target.ApplyDamage(_damage), onUpdateAfterAttack);

        protected IEnumerator Attack(Action attackAction, Action onUpdateAfterAttack)
        {
            _onUpdateAction = null;
            
            // Play attack anim

            yield return new WaitForSeconds(_attackDelay);

            attackAction();

            yield return new WaitForSeconds(_restAnimationDuration);

            _onUpdateAction = onUpdateAfterAttack;
        }

        private void Die()
        {
            Debug.Log($"{name} says: PIČI, já umřel ...");
        }
    }
}