using System;
using System.Collections.Generic;
using UnityEngine;
using DM2DMovement.Helpers;

namespace DM2DMovement.Collisions
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastCollisionDetector : MonoBehaviour, ICollisionDetector
    {
        [SerializeField] private float raySpacing = 0.05f;
        [SerializeField] private LayerMask collisionMask = 0;

        public List<CollisionData> GetCollisions(Vector2 velocity)
        {
            UpdateRaycastOrigins();
            _data.Clear();

            if (Mathf.Abs(velocity.x) > 0)
                CheckHorizontalCollisions(velocity);

            if (Mathf.Abs(velocity.y) > 0)
                CheckVerticalCollisions(velocity);
            
            return _data;
        }

        public void CheckVerticalCollisions(Vector2 velocity)
        {
            Vector2 direction = (Math.Sign(velocity.y) > 0) ? Up : Down;
            Vector2 rayOrigin = (direction == Up) ? _rayOrigins.topLeft : _rayOrigins.bottomLeft;
            float rayLength = Mathf.Abs(velocity.y) + SkinWidth;

            for (int i = 0; i < _verticalRayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin + (Vector2.right * (i * _verticalRaySpacing)), direction,
                    rayLength, collisionMask);

                if (hit)
                {
                    if (direction == Up)
                    {
                        if (_data.Exists(data => data.Direction == CollisionDirection.Up))
                            _data.Find(data => data.Direction == CollisionDirection.Up).Distance =
                                hit.distance - SkinWidth;
                        else
                            _data.Add(new CollisionData(CollisionDirection.Up, hit.distance - SkinWidth));
                    }
                    else
                    {
                        if (_data.Exists(data => data.Direction == CollisionDirection.Down))
                            _data.Find(data => data.Direction == CollisionDirection.Down).Distance =
                                hit.distance - SkinWidth;
                        else
                            _data.Add(new CollisionData(CollisionDirection.Down, hit.distance - SkinWidth));
                    }

                    rayLength = hit.distance;
                }
                
                Debug.DrawRay(rayOrigin + (Vector2.right * (i * _verticalRaySpacing)),
                    direction * rayLength,
                    Color.red);
            }
        }

        public void CheckHorizontalCollisions(Vector2 velocity)
        {
            Vector2 direction = (Math.Sign(velocity.x) > 0) ? Right : Left;
            Vector2 rayOrigin = (direction == Right) ? _rayOrigins.bottomRight : _rayOrigins.bottomLeft;
            float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

            for (int i = 0; i < _horizontalRayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin + (Vector2.up * (i * _horizontalRaySpacing)), direction,
                    rayLength, collisionMask);

                if (hit)
                {
                    if (direction == Right)
                    {
                        if (_data.Exists(data => data.Direction == CollisionDirection.Right))
                            _data.Find(data => data.Direction == CollisionDirection.Right).Distance =
                                hit.distance - SkinWidth;
                        else
                            _data.Add(new CollisionData(CollisionDirection.Right, hit.distance - SkinWidth));
                    }
                    else
                    {
                        if (_data.Exists(data => data.Direction == CollisionDirection.Left))
                            _data.Find(data => data.Direction == CollisionDirection.Left).Distance =
                                hit.distance - SkinWidth;
                        else
                            _data.Add(new CollisionData(CollisionDirection.Left, hit.distance - SkinWidth));
                    }

                    rayLength = hit.distance;
                }
                
                
                Debug.DrawRay(rayOrigin + (Vector2.up * (i * _horizontalRaySpacing)),
                    direction * rayLength,
                    Color.red);
            }
        }
        
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

        private List<CollisionData> _data;
        
        private Bounds InnerBounds => _collider.bounds.SelfExpand(SkinWidth * -2);

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
            _data = new List<CollisionData>();
        }

        private void CalculateRaySpacing()
        {
            var innerBounds = InnerBounds;

            var boundsWidth = innerBounds.size.x;
            var boundsHeight = innerBounds.size.y;
		
            _horizontalRayCount = Mathf.RoundToInt (boundsHeight / raySpacing);
            _verticalRayCount = Mathf.RoundToInt (boundsWidth / raySpacing);
		
            _horizontalRaySpacing = innerBounds.size.y / (_horizontalRayCount - 1);
            _verticalRaySpacing = innerBounds.size.x / (_verticalRayCount - 1);
        }

        private void UpdateRaycastOrigins()
        {
            var innerBounds = InnerBounds;
		
            _rayOrigins.bottomLeft = new Vector2 (innerBounds.min.x, innerBounds.min.y);
            _rayOrigins.bottomRight = new Vector2 (innerBounds.max.x, innerBounds.min.y);
            _rayOrigins.topLeft = new Vector2 (innerBounds.min.x, innerBounds.max.y);
            _rayOrigins.topRight = new Vector2 (innerBounds.max.x, innerBounds.max.y);
        }

        private struct BoxInfo
        {
            public Vector2 bottomLeft;
            public Vector2 topLeft;
            public Vector2 topRight;
            public Vector2 bottomRight;
        }
        
    }
}
