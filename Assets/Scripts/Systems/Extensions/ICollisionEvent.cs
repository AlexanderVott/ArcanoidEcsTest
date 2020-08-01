using UnityEngine;

public interface ICollision2DEvent
{
	Collider2D colliderOwner {get; set; }
	Collider2D colliderOther { get; set; }
	Rigidbody2D rigidBody { get; set; }
	int contactCount { get; set; }
	ContactPoint2D[] contacts { get; set; }
	Vector2 relativeVelocity { get; set; }
}
