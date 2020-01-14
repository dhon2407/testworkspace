using System;
using UnityEngine;

namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private int movespeed = 10;
        
        private ICollisionDetector _collisionDetector;

        private void Update()
        {
            var inputVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move(inputVelocity * (movespeed * Time.deltaTime));
            
        }

        public void Move(Vector2 velocity)
        {
            var collisions =_collisionDetector.GetCollisions(velocity);
            
                //TODO : Implement collision check to apply velocity changes.
            
            transform.Translate(velocity);
        }

        private void Awake()
        {
            _collisionDetector = GetComponent<ICollisionDetector>();
        }
    }
}