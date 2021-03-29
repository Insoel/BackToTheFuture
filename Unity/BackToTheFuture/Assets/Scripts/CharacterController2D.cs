using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController2D : MonoBehaviour
{
	protected Rigidbody2D rb;

	[SerializeField] protected float moveSpeed = 2f;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	protected abstract void Move(Vector2 newPos);
}
