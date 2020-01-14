using UnityEngine;

namespace Helpers
{
    public static class Collider2DExtensions
    {
        public static Bounds SelfExpand(this Bounds bounds, float amount)
        {
            var expandedBounds = bounds;
            bounds.Expand(amount);

            return expandedBounds;
        }
    }
}