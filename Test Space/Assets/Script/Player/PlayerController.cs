using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using UnityEngine;

namespace PlayerDan
{
    public class PlayerController : MonoBehaviour, ICharacterController
    {
        [SerializeField] private List<AvailableAction> inputActions = null;
            
        private const float AccelerationTimeAirborne = .2f;
        private const float AccelerationTimeGrounded = .1f;
        
        private IMovementController _moveController;
        private ICharacter _player;
        private Vector2 _velocity;

        private float _velocityXSmoothing;

        private readonly float _reducer = 100f;

        public ICharacter Character => _player;
        public Vector2 CurrentVelocity => _velocity;
        
        public float Gravity { get; set; } = -50f;

        public bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);
        public bool OnOverHeadCollision => Collisions.Exists(data => data.Direction == CollisionDirection.Up);
        public List<CollisionData> Collisions => _moveController.Collisions;
        
        public void UpdateVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        private void Update()
        {
            foreach (var availableAction in inputActions)
                availableAction.CheckInputs(this);
            
            UpdateInputVelocity();
        }

        private void FixedUpdate()
        {
            ApplyGravity();

            _moveController.Move(_velocity);

            if (OnGround || OnOverHeadCollision)
                _velocity.y = 0;
        }

        private void ApplyGravity()
        {
            _velocity.y += Gravity * Time.deltaTime;
        }

        private void UpdateInputVelocity()
        {
            float targetVelocityX = Input.GetAxisRaw("Horizontal") * (_player.Movespeed / _reducer);

            _velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref _velocityXSmoothing,
                OnGround ? AccelerationTimeGrounded : AccelerationTimeAirborne);
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

            public void CheckInputs(ICharacterController controller)
            {
                if (Input.GetKeyDown(code))
                    characterAction.Execute(controller);
                
                if (Input.GetKey(code))
                    characterAction.Hold(controller);
                
                if (Input.GetKeyUp(code))
                    characterAction.Cancel(controller);
            }
        }
    }
}