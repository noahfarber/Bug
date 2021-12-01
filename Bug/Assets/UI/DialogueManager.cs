using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogObject;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;
    public Dialogue dialogue;

    private Queue<string> sentences;
    private void Awake()
    {
        sentences = new Queue<string>();
        DialogSingleton.Instance.Register(this);
    }

    void Start()
    {
        StartDialogue(dialogue);
    }

    public void StartDialogue(Dialogue dialogue)
	{
        Time.timeScale = 1f;

        DialogObject.SetActive(true);

        animator.SetTrigger("Open");

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
        if(sentences.Count == 0)
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
        foreach(char letter in sentence.ToCharArray())
		{
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
		}
	}

    void EndDialogue()
	{
        animator.SetTrigger("Close");
    }
}

public class DialogSingleton : Singleton<DialogSingleton>
{
    public DialogueManager Manager;

    public void Register(DialogueManager manager)
    {
        Manager = manager;
    }

    public void SpawnDialog(string name, string text)
    {
        Manager.StartDialogue(new Dialogue() { name = name, sentences = new string[1] { text } });
    }
}
