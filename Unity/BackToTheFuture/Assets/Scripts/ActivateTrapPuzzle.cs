using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrapPuzzle : Puzzle
{
    [SerializeField] private HingeJoint2D ropeJoint = default;
    [SerializeField] private DistanceJoint2D anchorPoint = default;
	[SerializeField] private GameObject gearPiece = default;

	protected override void OnPuzzleCompleted()
	{
		anchorPoint.connectedBody = null;
		ropeJoint.connectedBody = null;
		LeanTween.rotateAroundLocal(gearPiece, Vector3.forward, 360f, 5f).setLoopClamp();
		LeanTween.move(gearPiece, gearPiece.transform.position + new Vector3(3f, 0f, 0f), 1f);
		gearPiece.transform.GetChild(0).gameObject.SetActive(false);
	}
}
