using UnityEngine;

namespace DM2DMovement.Helpers
{
    public static class Collider2DExtensions
    {
        public static Bounds SelfExpand(this Bounds bounds, float amount)
        {
            var expandedBounds = bounds;
            expandedBounds.Expand(amount);

            return expandedBounds;
        }
    }
}