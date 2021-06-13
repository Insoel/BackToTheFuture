using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrapPuzzle : Puzzle
{
    [SerializeField] private HingeJoint2D ropeJoint = default;
    [SerializeField] private DistanceJoint2D anchorPoint = default;

	protected override void OnPuzzleCompleted()
	{
		anchorPoint.connectedBody = null;
		ropeJoint.connectedBody = null;
		base.OnPuzzleCompleted();
	}
}
