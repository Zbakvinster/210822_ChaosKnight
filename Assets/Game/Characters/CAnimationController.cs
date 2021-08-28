using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    public class CAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int TakeHit = Animator.StringToHash("TakeHit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public void PlayRun(bool play) => _animator.SetBool(IsRunning, play);

        public void PlayTakeHit()
        {
            if (!_animator.GetBool(IsAttacking))
                _animator.SetTrigger(TakeHit);
        }

        public void PlayDie()
        {
            _animator.SetTrigger(Die);
            _animator.SetBool(IsDead, true);
        }

        public void PlayAttack(float attackTime)
        {
            _animator.SetTrigger(Attack);
            _animator.SetBool(IsAttacking, true);

            StartCoroutine(StopAttack());

            IEnumerator StopAttack()
            {
                yield return new WaitForSeconds(attackTime);
                
                _animator.SetBool(IsAttacking, false);
            }
        }
    }
}