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
    public string CurrentQuestID;

    void Start()
    {
        npcID = npcData.NpcID.Trim().ToLower();
        CurrentQuestID = "Q001";
        UpdateQuestID(CurrentQuestID);
        foreach (var d in dialogues)
        {
            Debug.Log($"{d.DialogIdx} | {d.DialogCon} | {d.ConnectNPC} | {d.ConnectQuest}");
        }

    }
    public void UpdateDialogues()
    {
        dialogues = NpcDialog.Instance.allDialogues
            .Where(d => d.ConnectNPC != null && d.ConnectQuest != null &&
                        d.ConnectNPC.Trim().ToLower() == npcID &&
                        d.ConnectQuest.Trim().ToLower() == CurrentQuestID.Trim().ToLower())
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
        if (dialogues == null || currentDialogueIndex >= dialogues.Count)
        {
            UIManager.Instance.CloseUI<DialogueUI>();
            currentDialogueIndex = 0;
            return;
        }

        var dialogue = dialogues[currentDialogueIndex];

        string speaker = "";
        string connectNPC = dialogue.ConnectNPC?.Trim().ToLower();
        if (connectNPC == npcID)
            speaker = npcData.NpcName;
        else if (connectNPC == "p" || connectNPC == "player")
            speaker = "플레이어";
        else
            speaker = dialogue.ConnectNPC;

        var dialogUI = UIManager.Instance.GetUI<DialogueUI>();
        dialogUI?.SetDialogue(speaker, dialogue.DialogCon);
        UIManager.Instance.OpenUI<DialogueUI>();

        currentDialogueIndex++;
    }

    public void UpdateQuestID(string newQuestID)
    {
        CurrentQuestID = newQuestID;

        dialogues = NpcDialog.Instance.allDialogues
    .Where(d => !string.IsNullOrEmpty(d.ConnectNPC) &&
                (d.ConnectNPC.Trim().ToLower() == npcID ||
                 d.ConnectNPC.Trim().ToLower() == "p" ||
                 d.ConnectNPC.Trim().ToLower() == "player") &&
                d.ConnectQuest != null &&
                d.ConnectQuest.Trim() == CurrentQuestID)
    .OrderBy(d => d.DialogIdx)
    .ToList();
        currentDialogueIndex = 0;
    }
}