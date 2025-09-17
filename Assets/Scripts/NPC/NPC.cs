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

    void Start()
    {
        // 전체 대사 리스트에서 현재 NPC와 플레이어의 대사를 담고,
        // DialogIdx 순서로 정렬해서 불러옴
        dialogues = NpcDialog.Instance.allDialogues
            .Where(d => d.ConnectNPC == npcData.NpcID || d.ConnectNPC == "P" || d.ConnectNPC == "Player")
            .OrderBy(d => d.DialogIdx)
            .ToList();
        currentDialogueIndex = 0;
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

            if (dialogue.ConnectNPC == npcData.NpcID)
            {
                speaker = npcData.NpcName;
            }
            else if (dialogue.ConnectNPC == "P" || dialogue.ConnectNPC == "Player")
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