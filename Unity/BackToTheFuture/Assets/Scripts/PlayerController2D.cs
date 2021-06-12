using UnityEngine;

public class PlayerController2D : CharacterController2D
{
	[SerializeField] private KeyCode interactKey = KeyCode.E;

	protected override void Update()
	{
		UpdateMovement();
		base.Update();
	}

	// Gets the user input and moves/jumps the player along that axis.
	private void UpdateMovement()
	{
		Vector2 movement = new Vector2();
		movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
		movement.y = rb.velocity.y;

		Move(movement);
		
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			isGrounded = false;
			rb.AddForce(new Vector2(0f, jumpForce));
		}

		if (Input.GetKeyDown(interactKey))
		{
			Collider2D[] colliders = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size, 0f);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].CompareTag("PuzzlePiece"))
				{
					PuzzlePiece piece = colliders[i].GetComponent<PuzzlePiece>();
					piece.HasBeenInteracted = true;
					break;
				}
			}
		}
		
	}

	private void OnDisable()
	{
		Move(new Vector2(0, rb.velocity.y));
		animator.SetFloat("VelX", 0f);
	}
}
