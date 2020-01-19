using UnityEngine;

namespace Movement
{
    [AddComponentMenu("Movement/Default Gravity")]
    public class BasicGravity : MonoBehaviour, IMovementModifier, IGravity
    {
        [SerializeField] private float gravity = 0;
        
        public Vector2 Apply(Vector2 velocity)
        {
            var modifiedVelocity = velocity;
            modifiedVelocity.y += -gravity;

            return modifiedVelocity;
        }

        public float Value => gravity;
    }
}