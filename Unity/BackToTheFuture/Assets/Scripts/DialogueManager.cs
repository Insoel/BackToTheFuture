using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> interactableDialogues = new List<GameObject>();

    [Header("Chat Bubble Settings")]
    [SerializeField] private Image chatBubbleImage = default;
    [SerializeField] private TextMeshProUGUI chatBubbleTMP = default;
    [SerializeField] private Vector3 chatBubbleOffset;

    [Header("Conversation Box Settings")]
    [SerializeField] private Image conversationBoxImage = default;
    [SerializeField] private TextMeshProUGUI conversationBoxTMP = default;
    [SerializeField] private float conversationBoxFadeTimer = 2f;
    [Range(10, 30)][SerializeField] private int charactersPerSecond = 1;
    private float writingSpeed = 1f;

    private Transform playerTransform;

    public bool IsWriting { get; private set; }

    public bool IsChatBubbleActive => chatBubbleImage.gameObject.activeSelf;

    // Start is called before the first frame update
    void Start()
    {
        writingSpeed /= charactersPerSecond;
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
                dialogue.OnInteraction += TriggerDialogue;
                dialogue.OnStopInteraction += StopDialogue;
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
                dialogue.OnInteraction -= TriggerDialogue;
                dialogue.OnStopInteraction -= StopDialogue;
            }
        }
    }

	public void TriggerDialogue(Dialogue dialogue, GameObject player)
	{
        Debug.Log("Dialogue Triggered");
        
        switch(dialogue.dialogueType)
		{
            case DialogueType.ChatBubble:
                chatBubbleImage.gameObject.SetActive(true);
                playerTransform = player.transform;
                SetupChatBubble(dialogue.sentence);
                break;
            case DialogueType.ConversationBox:
                conversationBoxImage.gameObject.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(WriteSentence(dialogue));
                break;
        }
	}

    private void StopDialogue(Dialogue dialogue)
	{
        switch (dialogue.dialogueType)
        {
            case DialogueType.ChatBubble:
                chatBubbleImage.gameObject.SetActive(false);
                break;
            case DialogueType.ConversationBox:
                conversationBoxImage.gameObject.SetActive(false);
                break;
        }
        
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

    private void SetupConversationBox(string text)
	{
        conversationBoxTMP.SetText(text);
        conversationBoxTMP.ForceMeshUpdate();
        Vector2 conversationBoxTextSize = conversationBoxTMP.GetRenderedValues(false);
        conversationBoxTextSize.x = Mathf.Clamp(conversationBoxTextSize.x, 160f, 1000f);
        conversationBoxTextSize.y = Mathf.Clamp(conversationBoxTextSize.y, 80f, 400f);

        Vector2 padding = new Vector2(40f, 20f);
        //conversationBoxTMP.rectTransform.sizeDelta = conversationBoxTextSize;
        conversationBoxImage.rectTransform.sizeDelta = conversationBoxTextSize + padding;
    }
    IEnumerator WriteSentence(Dialogue dialogue)
	{
        string text = "";
        IsWriting = true;
        foreach(char letter in dialogue.sentence.ToCharArray())
		{
            text += letter;
            SetupConversationBox(text);
            yield return new WaitForSeconds(writingSpeed);
        }
        yield return new WaitForSeconds(conversationBoxFadeTimer);
        IsWriting = false;
        StopDialogue(dialogue);
    }
}


public enum DialogueType
{
    ChatBubble,
    ConversationBox
}

public interface IInteractableDialogue
{
    Dialogue Dialogue { get; }

    event Action<Dialogue, GameObject> OnInteraction;
    event Action<Dialogue> OnStopInteraction;
}

[System.Serializable]
public class Dialogue
{
    public DialogueType dialogueType;
    public string sentence;
}
