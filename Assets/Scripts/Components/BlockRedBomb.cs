using UnityEngine;

namespace RedDev.Game {
	public struct BlockRedBomb: ICollision2DEvent
    {
        public Collider2D colliderOwner { get; set; }
        public Collider2D colliderOther { get; set; }
        public Rigidbody2D rigidBody { get; set; }
        public int contactCount { get; set; }
        public ContactPoint2D[] contacts { get; set; }
        public Vector2 relativeVelocity { get; set; }
    }
}