using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PuzzlePiece : MonoBehaviour, IInteractableDialogue
{
	[SerializeField] private Color interactColor = default;
	[SerializeField] private string dialogue;

    private bool hasBeenInteracted = false;

	private Color originalColor = default;

	private SpriteRenderer sprite;

	public event Action<string, GameObject> OnInteraction;
	public event Action OnStopInteraction;

	public string Dialogue { get => dialogue; }

	public bool HasBeenInteracted
	{
        get => hasBeenInteracted;

        set
		{
            if (hasBeenInteracted != value && !hasBeenInteracted)
			{
				hasBeenInteracted = value;
				GetComponent<Collider2D>().enabled = false;
				sprite.enabled = false;
			}
		}
	}

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		originalColor = sprite.color;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			sprite.color = interactColor;
			OnInteraction?.Invoke(Dialogue, collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			sprite.color = originalColor;
			OnStopInteraction?.Invoke();
		}
	}
}
