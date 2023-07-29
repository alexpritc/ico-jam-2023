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

	WaitForSeconds wait = new WaitForSeconds(0.1f);

	public void StartDialogue (Dialogue dialogue)
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
			EndDialogue();
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
		animator.SetBool("IsOpen", false);
	}

	void ContinueDialogue()
	{

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
