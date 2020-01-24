using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using UnityEngine;

namespace PlayerDan
{
    public class PlayerController : MonoBehaviour, ICharacterController
    {
        [SerializeField] private int moveSpeed = 10;        
        [SerializeField] private List<AvailableAction> inputActions = null;
            
        private const float AccelerationTimeAirborne = .2f;
        private const float AccelerationTimeGrounded = .1f;
        
        private IMovementController _moveController;
        private ICharacter _player;
        private Vector2 _inputVelocity;

        private float _velocityXSmoothing;

        private readonly float _reducer = 100f;

        public ICharacter Character => _player;
        public Vector2 CurrentVelocity => _inputVelocity;
        
        public float Gravity { get; set; } = -50f;

        public bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);
        public bool OnOverHeadCollision => Collisions.Exists(data => data.Direction == CollisionDirection.Up);
        public List<CollisionData> Collisions => _moveController.Collisions;
        
        public void UpdateVelocity(Vector2 velocity)
        {
            _inputVelocity = velocity;
        }

        private void Update()
        {
            foreach (var input in inputActions)
            {
                if (Input.GetKeyDown(input.code))
                    input.characterAction.Execute(this);
                if (Input.GetKeyUp(input.code))
                    input.characterAction.Cancel(this);
            }
        }

        private void FixedUpdate()
        {
            CalculateVelocity();

            _moveController.Move(_inputVelocity);

            if (OnGround || OnOverHeadCollision)
                _inputVelocity.y = 0;
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = Input.GetAxisRaw("Horizontal") * (moveSpeed / _reducer);

            targetVelocityX *= _player.SpeedMultiplier;
            
            _inputVelocity.x = Mathf.SmoothDamp(_inputVelocity.x, targetVelocityX, ref _velocityXSmoothing,
                OnGround ? AccelerationTimeGrounded : AccelerationTimeAirborne);
            _inputVelocity.y += Gravity * Time.deltaTime;
        }

        private void Awake()
        {
            _moveController = GetComponentInChildren<IMovementController>();
            _player = GetComponent<ICharacter>();
        }

        [System.Serializable]
        private struct AvailableAction
        {
            public KeyCode code;
            public CharacterAction characterAction;

            public AvailableAction(KeyCode keyCode)
            {
                code = KeyCode.None;
                characterAction = null;
            }
        }
    }
}