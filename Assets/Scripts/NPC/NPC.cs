using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData npcData;
    private List<DialogueRow> dialogues;
    private int currentDialogueIndex = 0;
    private string npcID;

    void Start()
    {
        string npcID = npcData.NpcID.Trim().ToLower();

        dialogues = NpcDialog.Instance.allDialogues
            .Where(d => !string.IsNullOrEmpty(d.ConnectNPC) &&
                (d.ConnectNPC.Trim().ToLower() == npcID ||
                 d.ConnectNPC.Trim().ToLower() == "p" ||
                 d.ConnectNPC.Trim().ToLower() == "player"))
            .OrderBy(d => d.DialogIdx)
            .ToList();
    }

    void OnMouseDown()
    {
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (dialogues == null || dialogues.Count == 0)
        {
            Debug.Log($"{npcData.NpcName}의 대사가 없습니다.");
            return;
        }

        if (currentDialogueIndex < dialogues.Count)
        {
            var dialogue = dialogues[currentDialogueIndex];
            string speaker = "";
            string connectNPC = dialogue.ConnectNPC?.Trim().ToLower();

            if (connectNPC == npcID)
            {
                speaker = npcData.NpcName;
            }
            else if (connectNPC == "p" || connectNPC == "player")
            {
                speaker = "플레이어";
            }
            else
            {
                speaker = dialogue.ConnectNPC;
            }

            Debug.Log($"{speaker}: {dialogue.DialogCon}");
            currentDialogueIndex++;
        }
        else
        {
            Debug.Log($"{npcData.NpcName}의 모든 대화를 마쳤습니다.");
            currentDialogueIndex = 0;
        }
    }
}