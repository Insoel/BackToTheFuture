using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	private void Update()
	{
		Move(transform.position + new Vector3(0.1f, 0f));
	}

	protected virtual void Move(Vector2 newPos)
	{
		transform.position = newPos;
	}
}
