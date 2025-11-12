using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header ("UI Elements")]
    public GameObject dialogueUIParent;
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Image charaSprite1;
    public Image charaSprite2;

    [Header ("Dialogue Parameters")]
    public float typeSpeed = 0.03f;
    public bool isInDialogue;
    private bool isTyping;
    private bool skipTyping;

    public void LaunchDialogue(DialogueData dialogue)
    {
        if (isInDialogue)
        {
            Debug.Log("Is already in dialogue!");
            return;
        }

        isInDialogue = true;
        dialogueUIParent.SetActive(true);
        StartCoroutine(RunDialogue(dialogue));
    }

    private IEnumerator RunDialogue(DialogueData dialogue)
    {
        foreach (var line in dialogue.lines)
        {
            characterNameText.text = line.characterName;
            yield return StartCoroutine(TypeLine(line.dialogueText));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        dialogueUIParent.SetActive(false);
        dialogueText.text = "";
        characterNameText.text = "";
        isInDialogue = false;
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        skipTyping = false;
        dialogueText.text = "";

        foreach (char c in line)
        {
            if (skipTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
            skipTyping = true;
    }
}
