using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image characterSprite;
    
    public static DialogueManager instance;
    private Queue<string> sentences;
    
    public TextMeshProUGUI dialogueDescriptionText;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueHolder dialogueHolder)
    {
        nameText.text = dialogueHolder.name;
        characterSprite.sprite = dialogueHolder.npcImage;
        
        sentences.Clear();

        foreach (string sentence in dialogueHolder.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (sentences.Count == 0)
        {
            FinishDialogue();
            return; 
        }

        string dialogueText = sentences.Dequeue();
        StartCoroutine(TypingEffect(dialogueText));
    }

    public IEnumerator TypingEffect(string sentence)
    {
        dialogueDescriptionText.text = "";
        foreach (char letter in sentence)
        {
            dialogueDescriptionText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    void FinishDialogue()
    {
        if (FindObjectOfType<GroupedDialogue>().CheckIfListIsEmpty())
        {
            UIManager.instance.SetDialoguePanel();
            GameManager.instance.pauseGame = false;
        }
        else
        {
            sentences.Clear();
            FindObjectOfType<GroupedDialogue>().RemoveIndex();
            FindObjectOfType<GroupedDialogue>().TriggerNextDialogue();
        }
    }
}
