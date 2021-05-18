using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PuzzlePiece : MonoBehaviour
{
	[SerializeField] private Color interactColor = default;

    private bool hasBeenInteracted = false;

	private Color originalColor = default;

	private SpriteRenderer sprite;

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
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			sprite.color = originalColor;
		}
	}
}
