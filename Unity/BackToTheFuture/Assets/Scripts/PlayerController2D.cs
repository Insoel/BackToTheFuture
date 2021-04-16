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

	// Gets the user input and moves the player along that axis.
	private void UpdateMovement()
	{
		Vector2 movement = new Vector2();
		movement.x = Input.GetAxis("Horizontal") * moveSpeed;
		movement.y = rb.velocity.y;
		if (Input.GetAxis("Vertical") > 0f) rb.AddForce(new Vector2(0f, jumpForce));

		if (movement != Vector2.zero) Move(movement);
	}

	protected override void Move(Vector2 movement)
	{
		Debug.Log("Moved");
		rb.velocity = movement;
	}
}
