using UnityEngine;

namespace Game.Characters.Player
{
    public class CPlayerCharacterController : CBaseCharacter
    {
        [SerializeField] private CPlayerCharacterConfig _characterConfig;
        [SerializeField] private CharacterController _characterController;

        private const float GRAVITY = 9.8f;
        private float _fallSpeed;

        public override void OnUpdate(float deltaTime)
        {
            if (_characterController.isGrounded)
                _fallSpeed = 0;

            _fallSpeed -= GRAVITY * deltaTime;
            
            Vector3 direction = Vector3.ClampMagnitude(
                    Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right,
                    1)
                + new Vector3(0, _fallSpeed, 0);

            _characterController.Move(direction * (_characterConfig.MovementSpeed * deltaTime));
        }
    }
}