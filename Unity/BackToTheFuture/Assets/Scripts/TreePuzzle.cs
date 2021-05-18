using UnityEngine;

public class TreePuzzle : Puzzle
{
	[SerializeField] private SpriteRenderer sprite = default;
	[SerializeField] private Collider2D col = default;

	protected override void OnPuzzleCompleted()
	{
		sprite.enabled = false;
		col.enabled = false;
	}
}
