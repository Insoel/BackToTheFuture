using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController2D : MonoBehaviour
{
	protected Rigidbody2D rb;


	[Header("Movement Values")]
	[SerializeField] protected float moveSpeed = 2f;
	[SerializeField] protected float jumpForce = 10f;

	[Header("Collider Settings")]
	[SerializeField] protected Transform groundChecker;
	[Tooltip("This is the layer the ground uses.")]
	[SerializeField] protected LayerMask groundLayer;

	protected bool isGrounded = true;

	const float GROUNDED_SIZE_Y = 0.1f;

	private Collider2D col;

	private Vector2 groundedSize;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		groundedSize = new Vector2(col.bounds.size.x, GROUNDED_SIZE_Y);
	}

	protected virtual void FixedUpdate()
	{
		UpdateGroundCollision();
	}

	protected virtual void Move(Vector2 movement)
	{
		rb.velocity = movement;
	}

	private void UpdateGroundCollision()
	{
		isGrounded = false;
		
		Collider2D[] colliders = Physics2D.OverlapBoxAll(groundChecker.position, groundedSize, 0f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject && rb.velocity.y < 0.1f)
				isGrounded = true;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(groundChecker.position, groundedSize);
	}
}
