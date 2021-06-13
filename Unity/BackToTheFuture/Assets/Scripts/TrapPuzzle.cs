using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPuzzle : Puzzle
{
	[SerializeField] private GameObject ropeTrap = default;

	protected override void OnPuzzleCompleted()
	{
		ropeTrap.SetActive(true);
	}
}
