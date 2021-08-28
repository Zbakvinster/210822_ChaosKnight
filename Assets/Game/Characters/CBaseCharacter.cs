using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters
{
    public abstract class CBaseCharacter : MonoBehaviour
    {
        [SerializeField] protected CAnimationController _animationController;
        [SerializeField] private CHpBarController _hpBarController;
        [SerializeField] protected AudioSource _impactAudioSource;
        [SerializeField] protected List<AudioClip> _attackImpactSfx;
        [SerializeField] protected List<AudioClip> _deathSfx;
        [SerializeField] private float _maxHp;
        [SerializeField] protected float _damage;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _restAnimationDuration;
        [SerializeField] private float _deathDelay;
        
        private float _actualHp;
        protected Action _onUpdateAction;
        protected Action _die;
        protected Action _onDeath;
        protected Coroutine _attackCoroutine;

        public void ApplyDamage(float damage, AudioClip impactSfx)
        {
            _impactAudioSource.clip = impactSfx;
            _impactAudioSource.Play();
            
            if ((_actualHp -= damage) <= 0)
                _die?.Invoke();
            else
                _animationController.PlayTakeHit();

            _hpBarController.UpdateUi(_actualHp / _maxHp);
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
        {
            return Attack(
                () =>
                {
                    target.ApplyDamage(_damage, _attackImpactSfx[Random.Range(0, _attackImpactSfx.Count)]);
                    
                    Vector3 forwardVec = target.transform.position - transform.position;
                    forwardVec.y = 0;
                    transform.forward = forwardVec;
                },
                onUpdateAfterAttack);
        }

        protected IEnumerator Attack(Action attackAction, Action onUpdateAfterAttack)
        {
            _onUpdateAction = null;

            _animationController.PlayAttack(_attackDelay + _restAnimationDuration);

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
            StopAttackCoroutine();

            _impactAudioSource.clip = _deathSfx[Random.Range(0, _deathSfx.Count)];
            _impactAudioSource.Play();

            _animationController.PlayDie();

            yield return new WaitForSeconds(_deathDelay);

            Destroy(gameObject);
        }

        protected void StopAttackCoroutine()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }
}