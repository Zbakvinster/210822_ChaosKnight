using UnityEngine;

namespace Game.Characters.Player
{
    public class CPlayerCharacterController : CBaseCharacter
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private const float GRAVITY = 9.8f;
        private float _fallSpeed;
        private Transform _cachedTransform;

        protected override void Start()
        {
            base.Start();

            _cachedTransform = transform;
            CGameManager.Instance.AddChaosSide(this);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            _cachedTransform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _rotationSpeed * deltaTime);
            
            if (_characterController.isGrounded)
                _fallSpeed = 0;

            _fallSpeed -= GRAVITY * deltaTime;

            Vector3 direction = Vector3.ClampMagnitude(
                    Input.GetAxis("Vertical") * _cachedTransform.forward
                        + Input.GetAxis("Horizontal") * _cachedTransform.right,
                    1)
                + new Vector3(0, _fallSpeed, 0);

            _characterController.Move(direction * (_movementSpeed * deltaTime));
        }
    }
}