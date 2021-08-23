using UnityEngine;

namespace Game.Characters
{
    public class CAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }
    }
}