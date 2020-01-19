using System;
using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using UnityEngine;
using Action = System.Action;

namespace PlayerDan
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int moveSpeed = 10;        
        
        [SerializeField] private List<AvailableAction> availableActions;
            
        [Space, Header("Jump Parameters")]
        [SerializeField] private float maxJumpHeight = 4;
        [SerializeField] private float minJumpHeight = 1;
        [SerializeField] private float timeToJumpApex = .4f;
        
        private IMovementController _moveController;
        private Vector2 _inputVelocity;
        private float _accelerationTimeAirborne = .2f;
        private float _accelerationTimeGrounded = .1f;
        private float _maxJumpVelocity;
        private float _minJumpVelocity;
        private IGravity _gravityController;
        private float _gravity;

        private float _velocityXSmoothing;

        private bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);
        private List<CollisionData> Collisions => _moveController.Collisions;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnJump();
            if (Input.GetKeyUp(KeyCode.Space))
                OnJumpRelease();
            
            CalculateVelocity();
            
            _moveController.Move(_inputVelocity);
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = Input.GetAxisRaw("Horizontal") * (moveSpeed/10f);
            
            _inputVelocity.x = Mathf.SmoothDamp(_inputVelocity.x, targetVelocityX, ref _velocityXSmoothing,
                OnGround ? _accelerationTimeGrounded : _accelerationTimeAirborne);
            _inputVelocity.y += _gravity * Time.deltaTime;
        }

        private void OnJump()
        {
            if (OnGround)
                _inputVelocity.y = _maxJumpVelocity;
        }
        
        private void OnJumpRelease()
        {
            if (_inputVelocity.y > _minJumpVelocity)
                _inputVelocity.y = minJumpHeight;
        }

        private void Awake()
        {
            _moveController = GetComponentInChildren<IMovementController>();
            _gravityController = GetComponentInChildren<IGravity>();
        }

        private void Start()
        {
            SetupGravity();
        }

        private void SetupGravity()
        {
            _gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);

            _maxJumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
            _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(_gravity) * minJumpHeight);
        }

        [System.Serializable]
        private struct AvailableAction
        {
            public ActionCode Code;
            public Action Action;
        }
    }
}