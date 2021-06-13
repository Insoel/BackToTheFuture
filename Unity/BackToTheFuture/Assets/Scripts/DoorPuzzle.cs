using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : Puzzle
{
	[SerializeField] private GameObject doorInteraction = default;
	[SerializeField] private GameObject unlockDoor = default;
	protected override void OnPuzzleCompleted()
	{
		doorInteraction.SetActive(false);
		unlockDoor.SetActive(true);
	}
}
