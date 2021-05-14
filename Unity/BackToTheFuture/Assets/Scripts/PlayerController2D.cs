using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : CharacterController2D
{
	private void Update()
	{
		UpdateMovement();
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
	}
}
