using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> interactableDialogues = new List<GameObject>();


    [SerializeField] private Image chatBubbleImage = default;
    [SerializeField] private TextMeshProUGUI chatBubbleTMP = default;
    [SerializeField] private Vector3 chatBubbleOffset;

    private Transform playerTransform;

    public bool IsChatBubbleActive => chatBubbleImage.gameObject.activeSelf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChatBubbleActive)
		{
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position + chatBubbleOffset);
            chatBubbleImage.transform.position = screenPos;
        }
    }

	private void OnEnable()
	{
		foreach(GameObject go in interactableDialogues)
		{
            if (go != null)
			{
                IInteractableDialogue dialogue = go.GetComponent<IInteractableDialogue>();
                dialogue.OnInteraction += TriggerChatBubbleDialogue;
                dialogue.OnStopInteraction += StopChatBubbleDialogue;
            }
		}
	}

	private void OnDisable()
	{
        foreach (GameObject go in interactableDialogues)
        {
            if (go != null)
			{
                IInteractableDialogue dialogue = go.GetComponent<IInteractableDialogue>();
                dialogue.OnInteraction -= TriggerChatBubbleDialogue;
                dialogue.OnStopInteraction -= StopChatBubbleDialogue;
            }
        }
    }

	private void TriggerChatBubbleDialogue(string textDialogue, GameObject player)
	{
        Debug.Log("Dialogue Triggered");
        chatBubbleImage.gameObject.SetActive(true);
        playerTransform = player.transform;
        SetupChatBubble(textDialogue);
	}

    private void StopChatBubbleDialogue()
	{
        chatBubbleImage.gameObject.SetActive(false);
    }

    private void SetupChatBubble(string text)
	{
        chatBubbleTMP.SetText(text);
        chatBubbleTMP.ForceMeshUpdate();
        Vector2 chatBubbleTextSize = chatBubbleTMP.GetRenderedValues(false);
        chatBubbleTextSize.x = Mathf.Clamp(chatBubbleTextSize.x, 160f, 1600f);
        chatBubbleTextSize.y = Mathf.Clamp(chatBubbleTextSize.y, 80f, 800f);

        Vector2 padding = new Vector2(40f, 20f);
        chatBubbleTMP.rectTransform.sizeDelta = chatBubbleTextSize;
        chatBubbleImage.rectTransform.sizeDelta = chatBubbleTextSize + padding;

    }
}


public enum DialogueType
{
    ChatBubble,
    ConversationBox
}

public interface IInteractableDialogue
{
    string Dialogue { get; }

    event Action<string, GameObject> OnInteraction;
    event Action OnStopInteraction;
}
