using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActionsTracker : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager = default;
    [SerializeField] private TimeSwitchManager timeSwitchManager = default;
    [SerializeField] private PlayerController2D marty = default;

    [SerializeField] private float distanceToMove = 10f;

    [SerializeField] private InteractableObject treeInteractableObject = default;

    [SerializeField] private PuzzlePiece saplingPuzzlePiece = default;

    [SerializeField] private InteractableObject bridgeGapObject = default;

    [SerializeField] private PuzzlePiece woodPuzzpePiece = default;

    [SerializeField] private PuzzlePiece gearPuzzlePiece = default;

    [SerializeField] private List<Dialogue> dialogueOptions = new List<Dialogue>();

    private int dialogueCounter = 0;
    private float distanceMoved = 0f;
    private Vector3 lastPos;

    private float dialogueCooldown = 0f;

    private bool usedTimeSwap = false;

    private bool foundTree = false;

    private bool removedSapling = false;

    private bool foundGap = false;

    private bool woodSent = false;

    private bool gearCollected = false;

	// Start is called before the first frame update
	void Start()
    {
        NextDialogue();
        lastPos = marty.transform.position;
        timeSwitchManager.enabled = false;
        marty.enabled = false;
        LeanTween.rotateAroundLocal(gearPuzzlePiece.gameObject, Vector3.forward, 360f, 5f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueCounter == 2)
		{
            distanceMoved += Vector3.Distance(marty.transform.position, lastPos);
            lastPos = marty.transform.position;
		}
        dialogueCooldown -= Time.deltaTime;
        dialogueCooldown = Mathf.Clamp(dialogueCooldown, 0f, 1f);
        if (dialogueManager.IsWriting || dialogueCooldown > 0f) return;
        if (dialogueCounter == 1)
		{
            NextDialogue();
            marty.enabled = true;
        }
        else if (dialogueCounter == 2 && distanceMoved >= distanceToMove)
		{
            NextDialogue();
            marty.enabled = false;

        }
        else if (dialogueCounter == 3)
		{
            NextDialogue();
        }
        else if (dialogueCounter == 4)
		{
            NextDialogue();
            timeSwitchManager.enabled = true;
            
            timeSwitchManager.OnTimeSwap += TimeSwapTrack;
        }
        else if (dialogueCounter == 5 && usedTimeSwap)
		{
            NextDialogue();
            timeSwitchManager.enabled = false;
            treeInteractableObject.OnInteraction += TreeFoundTrack;
        }
        else if (dialogueCounter == 6 && foundTree)
		{
            NextDialogue();
            timeSwitchManager.enabled = true;
            usedTimeSwap = false;
            timeSwitchManager.OnTimeSwap += TimeSwapTrack;
        }
        else if (dialogueCounter == 7 && usedTimeSwap)
		{
            treeInteractableObject.gameObject.SetActive(false);
            NextDialogue();
            marty.enabled = true;
            saplingPuzzlePiece.OnActionInteraction += SaplingRemoved;
            timeSwitchManager.enabled = false;
        }
        else if (dialogueCounter == 8 && removedSapling)
		{
            saplingPuzzlePiece.gameObject.SetActive(false);
            NextDialogue();
            bridgeGapObject.OnInteraction += GapFound;
        }
        else if (dialogueCounter == 9 && foundGap)
		{
            NextDialogue();
            woodPuzzpePiece.OnActionInteraction += BridgePlacingEnabled;
        }
        else if (dialogueCounter == 10 && woodSent)
		{
            bridgeGapObject.gameObject.SetActive(false);
            woodSent = false;
            gearPuzzlePiece.OnActionInteraction += TutorialCompleted;
		}
        else if (dialogueCounter == 10 && gearCollected)
		{
            NextDialogue();
            StartCoroutine(TutorialCompletedCoroutine());
		}
    }

    private void NextDialogue()
	{
        dialogueManager.TriggerDialogue(dialogueOptions[dialogueCounter], null);
        dialogueCounter++;
        dialogueCooldown = 1f;
    }

    private void TimeSwapTrack()
	{
        usedTimeSwap = true;
        timeSwitchManager.OnTimeSwap -= TimeSwapTrack;
    }

    private void TreeFoundTrack(Dialogue dialogue, GameObject go)
	{
        foundTree = true;
        treeInteractableObject.OnInteraction -= TreeFoundTrack;
    }

    private void SaplingRemoved()
	{
        removedSapling = true;
        saplingPuzzlePiece.OnActionInteraction -= SaplingRemoved;
    }

    private void GapFound(Dialogue dialog, GameObject go)
	{
        foundGap = true;
        timeSwitchManager.enabled = true;
        bridgeGapObject.OnInteraction -= GapFound;
    }

    private void BridgePlacingEnabled()
	{
        woodSent = true;
        woodPuzzpePiece.OnActionInteraction -= BridgePlacingEnabled;
    }

    private void TutorialCompleted()
	{
        gearCollected = true;
        gearPuzzlePiece.OnActionInteraction -= TutorialCompleted;
    }

    IEnumerator TutorialCompletedCoroutine()
	{
        yield return new WaitForSeconds(5f);
        // Load victory screen
        Debug.Log("Tutorial Finished!!!!");
	}
}
