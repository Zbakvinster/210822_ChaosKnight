using UnityEngine;

namespace Game.Characters
{
    public abstract class CBaseCharacter : MonoBehaviour
    {
        [SerializeField] protected CCharacterConfig _characterConfig;

        public abstract void OnUpdate(float deltaTime);
    }
}