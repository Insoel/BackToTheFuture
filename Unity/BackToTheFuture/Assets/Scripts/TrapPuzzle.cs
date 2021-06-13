using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPuzzle : Puzzle
{
	[SerializeField] private GameObject ropeTrap = default;
	[SerializeField] private GameObject activateTrap = default;

	protected override void OnPuzzleCompleted()
	{
		ropeTrap.SetActive(true);
		activateTrap.SetActive(true);
	}
}
