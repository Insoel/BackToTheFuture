using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePuzzle : Puzzle
{
	[SerializeField] private GameObject bridge = default;

	protected override void OnPuzzleCompleted()
	{
		bridge.SetActive(true);
	}
}
