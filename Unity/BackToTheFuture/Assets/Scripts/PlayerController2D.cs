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
		movement.y = Input.GetAxis("Vertical") * moveSpeed;				

		if (movement != Vector2.zero) Move(movement);
	}

	protected override void Move(Vector2 movement)
	{
		Debug.Log("Moved");
		rb.velocity = movement;
	}
}
