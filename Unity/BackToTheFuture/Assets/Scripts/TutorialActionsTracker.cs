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

    [SerializeField] private List<Dialogue> dialogueOptions = new List<Dialogue>();

    private int dialogueCounter = 0;
    private float distanceMoved = 0f;
    private Vector3 lastPos;

    private float dialogueCooldown = 0f;

	// Start is called before the first frame update
	void Start()
    {
        dialogueManager.TriggerDialogue(dialogueOptions[0], null);
        dialogueCounter++;
        dialogueCooldown = 1f;
        lastPos = marty.transform.position;
        timeSwitchManager.enabled = false;
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
            dialogueManager.TriggerDialogue(dialogueOptions[dialogueCounter], null);
            dialogueCounter++;
            dialogueCooldown = 1f;
        }
        else if (dialogueCounter == 2 && distanceMoved >= distanceToMove)
		{
            dialogueManager.TriggerDialogue(dialogueOptions[dialogueCounter], null);
            dialogueCounter++;
            dialogueCooldown = 1f;
            timeSwitchManager.enabled = true;
		}
        else if (dialogueCounter == 3)
		{
            dialogueManager.TriggerDialogue(dialogueOptions[dialogueCounter], null);
            dialogueCounter++;
            dialogueCooldown = 1f;
        }
        else if (dialogueCounter == 4)
		{
            dialogueManager.TriggerDialogue(dialogueOptions[dialogueCounter], null);
            dialogueCounter++;
            dialogueCooldown = 1f;
        }
    }
}
