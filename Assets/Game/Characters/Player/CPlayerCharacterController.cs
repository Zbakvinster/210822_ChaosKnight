using UnityEngine;

namespace Game.Characters.Player
{
    public class CPlayerCharacterController : CBaseCharacter
    {
        [SerializeField] private CharacterController _characterController;

        public void MyUpdate(float deltaTime)
        {
            Vector3 direction = Vector3.ClampMagnitude(
                Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right,
                1);

            _characterController.SimpleMove(direction * (_characterConfig.MovementSpeed * deltaTime));
        }
    }
}