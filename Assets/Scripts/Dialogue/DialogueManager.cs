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
    public CanvasGroup chara1CanvasGroup;
    public CanvasGroup chara2CanvasGroup;

    [Header ("Dialogue Parameters")]
    public float typeSpeed = 0.03f;
    public bool isInDialogue;
    private bool isTyping;
    private bool skipTyping;
    private float previousTimeScale;

    public void LaunchDialogue(DialogueData dialogue)
    {
        if (isInDialogue)
        {
            Debug.Log("Is already in dialogue!");
            return;
        }

        isInDialogue = true;

        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        dialogueUIParent.SetActive(true);
        StartCoroutine(RunDialogue(dialogue));
    }
    private IEnumerator RunDialogue(DialogueData dialogue)
    {
        charaSprite1.sprite = dialogue.startingSpriteChara1;
        charaSprite2.sprite = dialogue.startingSpriteChara2;

        if(dialogue.startingSpriteChara2 == null)
        {
            chara2CanvasGroup.alpha = 0f;
        }
        
        foreach (var line in dialogue.lines)
        {
            characterNameText.text = line.characterName;
            if(line.characterName == "Georges")
            {
                charaSprite1.sprite = line.charaSprite;
                chara1CanvasGroup.alpha = 1f;
                chara2CanvasGroup.alpha = 0.25f;
            }
            else
            {
                charaSprite2.sprite = line.charaSprite;
                chara2CanvasGroup.alpha = 1f;
                chara1CanvasGroup.alpha = 0.25f;
            }
            yield return StartCoroutine(TypeLine(line.dialogueText));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        dialogueUIParent.SetActive(false);
        dialogueText.text = "";
        characterNameText.text = "";
        isInDialogue = false;

        Time.timeScale = previousTimeScale;
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
            yield return new WaitForSecondsRealtime(typeSpeed);
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
