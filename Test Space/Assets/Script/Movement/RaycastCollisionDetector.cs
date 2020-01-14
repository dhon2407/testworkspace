using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastCollisionDetector : MonoBehaviour
    {
        [SerializeField] private float raySpacing;
        
        private readonly Vector2 Left = Vector2.left;
        private readonly Vector2 Right = Vector2.right;
        private readonly Vector2 Up = Vector2.up;
        private readonly Vector2 Down = Vector2.down;
        
        private const float SkinWidth = 0.015f;
        private BoxCollider2D _collider;
        private BoxInfo _rayOrigins;

        private int _verticalRayCount;
        private int _horizontalRayCount;

        private float _horizontalRaySpacing;
        private float _verticalRaySpacing;
        
        private Bounds InnerBounds => _collider.bounds.Expand (SkinWidth * -2);
        
        //TEST
        private void Update()
        {
            FilterCollisions(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }

        public CollisionData FilterCollisions(Vector2 velocity)
        {
            CalculateRaySpacing();
            UpdateRaycastOrigins();
            CollisionData data = new CollisionData();

            if (Mathf.Abs(velocity.x) > 0)
                CheckHorizontalCollisions(velocity);

            if (Mathf.Abs(velocity.y) > 0)
                CheckVerticalCollisions(velocity);
            
            return data;
        }

        private void CheckVerticalCollisions(Vector2 velocity)
        {
            Vector2 direction = (Math.Sign(velocity.y) > 0) ? Up : Down;
            Vector2 rayOrigin = (direction == Up) ? _rayOrigins.topLeft : _rayOrigins.bottomLeft;
            float rayLength = Mathf.Abs(velocity.y) + SkinWidth;

            for (int i = 0; i < _verticalRayCount; i++)
                Debug.DrawRay(rayOrigin + (Vector2.right * (i * _verticalRaySpacing)), 
                    direction * rayLength,
                    Color.red);
        }

        private void CheckHorizontalCollisions(Vector2 velocity)
        {
            Vector2 direction = (Math.Sign(velocity.x) > 0) ? Right : Left;
            Vector2 rayOrigin = (direction == Right) ? _rayOrigins.bottomRight : _rayOrigins.bottomLeft;
            float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

            for (int i = 0; i < _horizontalRayCount; i++)
                Debug.DrawRay(rayOrigin + (Vector2.up * (i * _horizontalRaySpacing)), 
                    direction * rayLength,
                    Color.red);
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        public void CalculateRaySpacing() {
            var bounds = _collider.bounds;
            bounds.Expand (SkinWidth * -2);

            var boundsWidth = bounds.size.x;
            var boundsHeight = bounds.size.y;
		
            _horizontalRayCount = Mathf.RoundToInt (boundsHeight / raySpacing);
            _verticalRayCount = Mathf.RoundToInt (boundsWidth / raySpacing);
		
            _horizontalRaySpacing = bounds.size.y / (_horizontalRayCount - 1);
            _verticalRaySpacing = bounds.size.x / (_verticalRayCount - 1);
        }
        
        public void UpdateRaycastOrigins() {
            Bounds bounds = _collider.bounds;
            bounds.Expand (SkinWidth * -2);
		
            _rayOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
            _rayOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
            _rayOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
            _rayOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
        }

        public struct BoxInfo
        {
            public Vector2 bottomLeft;
            public Vector2 topLeft;
            public Vector2 topRight;
            public Vector2 bottomRight;
        }
        
    }

    public struct CollisionData
    {
        public bool collidedUp;
        public bool collidedLeft;
        public bool collidedRight;
        public bool collidedDown;
    }
}
