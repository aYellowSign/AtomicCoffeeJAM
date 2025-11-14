using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Serializable]
    public class DialogueLine
    {
        public string characterName;
        [TextArea(2,5)] public string dialogueText;
        public Sprite charaSprite;
    }

    public Sprite startingSpriteChara1;
    public Sprite startingSpriteChara2;
    public List<DialogueLine> lines = new List<DialogueLine>();
}
