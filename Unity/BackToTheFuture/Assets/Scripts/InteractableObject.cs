using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractableDialogue
{
	[SerializeField] private Dialogue dialogue;

	public Dialogue Dialogue { get => dialogue; }

	public event Action<Dialogue, GameObject> OnInteraction;
	public event Action<Dialogue> OnStopInteraction;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			OnInteraction?.Invoke(Dialogue, collision.gameObject);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			OnStopInteraction?.Invoke(Dialogue);
		}
	}
}
