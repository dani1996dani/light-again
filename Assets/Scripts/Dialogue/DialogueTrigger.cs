using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DialogueTrigger : MonoBehaviour
{
    private bool didStartDialogue = false;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!didStartDialogue && collision.gameObject.tag == Settings.TagPlayer)
        {
            TriggerDialogue();
            didStartDialogue = true;
        }
    }
}