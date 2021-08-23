using UnityEngine;

namespace Game.Characters
{
    public abstract class CBaseCharacter : MonoBehaviour
    {
        [SerializeField] protected CAnimationController _animationController;

        public abstract void OnUpdate(float deltaTime);
    }
}