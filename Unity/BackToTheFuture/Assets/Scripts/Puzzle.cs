using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    private bool isPuzzleSolved = false;

    public bool IsPuzzleSolved => isPuzzleSolved;

	public int NumOfTotalPuzzlePieces => puzzlePieces.Count;

	private void Update()
	{
		if (isPuzzleSolved) return;

		foreach (PuzzlePiece piece in puzzlePieces)
		{
			if (!piece.HasBeenInteracted) return;
		}

		isPuzzleSolved = true;
		OnPuzzleCompleted();
	}

	protected virtual void OnPuzzleCompleted()
	{

	}
}
