using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzle : Puzzle
{
	[SerializeField] private GameObject boxInteraction = default;
	[SerializeField] private GameObject gearPiece = default;
	protected override void OnPuzzleCompleted()
	{
		boxInteraction.SetActive(false);
		gearPiece.SetActive(true);
		LeanTween.rotateAroundLocal(gearPiece, Vector3.forward, 360f, 5f).setLoopClamp();
	}
}
