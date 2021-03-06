﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

    private void Update()
    {
        if (Settings.isInStoryMode)
        {
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
			{
				DisplayNextSentence();
			}
		}
    }

    public void StartDialogue(Dialogue dialogue)
	{
		Settings.isGamePaused = true;
		Settings.isInStoryMode = true;
		animator.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
	}

	void EndDialogue()
	{
		animator.SetBool("isOpen", false);
		StartCoroutine("ContinueGame");
	}

	IEnumerator ContinueGame()
    {
		yield return new WaitForSeconds(0.2f);
		Settings.isInStoryMode = false;
		Settings.isGamePaused = false;
	}

}