using System;
using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using UnityEngine;
using Action = System.Action;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private IMovementController _moveController;
        private List<CollisionData> _currentCollisions;

        [SerializeField] private List<AvailableAction> availableActions;

        private bool OnGround => _currentCollisions.Exists(data => data.Direction == CollisionDirection.Down);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && OnGround)
                Debug.Log("JUMP!!");
        }


        private void FixedUpdate()
        {
            var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _moveController.Move(inputVector);

            _currentCollisions = _moveController.Collisions;
        }

        private void Awake()
        {
            _moveController = GetComponentInChildren<IMovementController>();
        }

        [System.Serializable]
        private struct AvailableAction
        {
            public ActionCode Code;
            public Action Action;
        }
    }
}