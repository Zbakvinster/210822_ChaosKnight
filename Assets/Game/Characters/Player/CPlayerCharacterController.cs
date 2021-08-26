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
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _attackAngle;

        private const float GRAVITY = 9.8f;
        private float _fallSpeed;
        private readonly RaycastHit[] _targets = new RaycastHit[20];
        private float _dotAngle;

        protected override void Start()
        {
            base.Start();

            CGameManager.Instance.AddChaosSide(this);
            CGameManager.Instance.OnChaosWin += () =>
            {
                _onUpdateAction = null;
                StopAttackCoroutine();
            };
            _dotAngle = Mathf.Cos(_attackAngle * Mathf.Deg2Rad);
            _onUpdateAction = OnUpdate;
            _onDeath = () =>
            {
                CGameManager.Instance.RemoveChaosSide(this);
                CGameManager.Instance.CityWin();
            };

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = !Cursor.visible;
                if (Cursor.visible)
                {
                    Cursor.lockState = CursorLockMode.None;
                    _onUpdateAction = null;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    _onUpdateAction = OnUpdate;
                }
                
            }
        }

        private void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            // MOVEMENT
            if (_characterController.isGrounded)
                _fallSpeed = 0;

            _fallSpeed -= GRAVITY * deltaTime;

            Vector3 moveDirection = Vector3.ClampMagnitude(
                Input.GetAxis("Vertical") * _cameraFollowTarget.forward
                    + Input.GetAxis("Horizontal") * _cameraFollowTarget.right,
                1);
            Vector3 direction = moveDirection + new Vector3(0, _fallSpeed, 0);

            if (moveDirection != Vector3.zero)
            {
                _cachedGraphicsTransform.forward = moveDirection;
                _animationController.PlayRun(true);
            }
            else
                _animationController.PlayRun(false);
            
            _characterController.Move(direction * (_movementSpeed * deltaTime));

            // ATTACK
            if (Input.GetMouseButtonDown(0))
            {
                _animationController.PlayRun(false);
                
                int targetCount = Physics.CapsuleCastNonAlloc(
                    _cachedGraphicsTransform.position + Vector3.down,
                    _cachedGraphicsTransform.position + Vector3.up,
                    _attackRadius,
                    Vector3.forward,
                    _targets,
                    0,
                    (1 << 9));

                _attackCoroutine = StartCoroutine(Attack(
                    () =>
                    {
                        for (int i = 0; i < targetCount; i++)
                        {
                            if (Vector3.Dot(
                                    _cachedGraphicsTransform.forward,
                                    _targets[i].transform.position - _cachedGraphicsTransform.position)
                                > _dotAngle)
                                _targets[i].collider.GetComponent<CBaseCharacter>().ApplyDamage(_damage);
                        }
                    },
                    OnUpdate));
            }
        }

        private void LateUpdate()
        {
            if (!Cursor.visible)
                _cameraFollowTarget.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime);
        }
    }
}