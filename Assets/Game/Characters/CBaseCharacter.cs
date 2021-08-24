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
        [SerializeField] private float _deathDelay;
        
        private float _actualHp;
        protected Action _onUpdateAction;
        private Action _die;
        protected Action _onDeath;
        protected Coroutine _attackCoroutine;

        public void ApplyDamage(float damage)
        {
            if ((_actualHp -= damage) <= 0)
                _die?.Invoke();
        }

        protected virtual void Start()
        {
            _actualHp = _maxHp;
            _die = () => StartCoroutine(OnDie());
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
            _attackCoroutine = null;
        }

        private IEnumerator OnDie()
        {
#if UNITY_EDITOR
            if (TryGetComponent(out MeshRenderer renderer))
                renderer.material.color = Color.blue;
#endif
            
            
            _die = null;
            _onUpdateAction = null;
            _onDeath?.Invoke();
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }

            // Play death anim

            yield return new WaitForSeconds(_deathDelay);

            Destroy(gameObject);
        }
    }
}