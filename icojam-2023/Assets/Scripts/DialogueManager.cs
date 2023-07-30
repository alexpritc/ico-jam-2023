using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerClickHandler {

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	public Animator animator;

	private Queue<string> sentences = new Queue<string>();

    string currentSentence;

	bool isTyping = false;

	WaitForSeconds wait = new WaitForSeconds(0.025f);

    private Queue<Dialogue> conversations = new Queue<Dialogue>();


    // --------------------------
    public bool isIntro;
    public bool isUseArrowsClue;
    public bool canNowSelect;

    public IEnumerator StartConversation(Dialogue[] conversation)
	{
        conversations.Clear();

        foreach (Dialogue dialogue in conversation) {	
			conversations.Enqueue(dialogue);
		}

        StartNextConversation();
		yield return null;
    }

	public void StartNextConversation()
	{
        if (conversations.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = conversations.Dequeue();

        StartNextDialogue(currentDialogue);
    }

	public void StartNextDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

        sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

        DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
            StartNextConversation();
			return;
		}

		currentSentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(currentSentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		isTyping = true;

		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			if (dialogueText.text == currentSentence)
			{
				isTyping = false;
			}
			yield return wait;
		}
	}

	void EndDialogue()
	{
		StopAllCoroutines();

        animator.SetBool("IsOpen", false);
		
		if (isIntro)
		{
			GameManager.Instance.EndFirstScene();
			isIntro = false;
		}

		if (isUseArrowsClue)
		{
            GameManager.Instance.EnableFirstButton();
            isUseArrowsClue = false;
		}

		if (canNowSelect)
		{
			GameManager.Instance.EnableSelectGameState();
			canNowSelect = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
		if (isTyping)
		{
            FinishTypingSentence();
        }
		else
		{
			DisplayNextSentence();
		}
    }

    public void FinishTypingSentence()
    {
		StopAllCoroutines();
		dialogueText.text = currentSentence;
		isTyping = false;
    }
}
