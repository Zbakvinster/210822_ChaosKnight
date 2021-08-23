using System;
using UnityEngine;

namespace Game.Characters.Player
{
    public class CPlayerCharacterController : CBaseCharacter
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cachedGraphicsTransform;
        [SerializeField] private Transform _cameraFollowTarget;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private const float GRAVITY = 9.8f;
        private float _fallSpeed;

        protected override void Start()
        {
            base.Start();

            CGameManager.Instance.AddChaosSide(this);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            if (_characterController.isGrounded)
                _fallSpeed = 0;

            _fallSpeed -= GRAVITY * deltaTime;

            Vector3 moveDirection = Vector3.ClampMagnitude(
                Input.GetAxis("Vertical") * _cameraFollowTarget.forward
                    + Input.GetAxis("Horizontal") * _cameraFollowTarget.right,
                1);
            Vector3 direction = moveDirection + new Vector3(0, _fallSpeed, 0);

            if (moveDirection != Vector3.zero)
                _cachedGraphicsTransform.forward = moveDirection;
            
            _characterController.Move(direction * (_movementSpeed * deltaTime));
        }

        private void LateUpdate()
        {
            _cameraFollowTarget.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime);
        }
    }
}