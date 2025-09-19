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
    public float interactDistance = 1.3f;
    private Transform player;

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
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO == null)
            {
                Debug.LogWarning("동적 생성된 플레이어 오브젝트를 아직 찾지 못했습니다.");
                return;
            }
            player = playerGO.transform;
        }

        float dist = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(player.position.x, player.position.y)
        );

        if (dist <= interactDistance)
        {
            StartDialogue();
            Debug.Log($"Distance between NPC and Player: {dist}");
        }
        else
        {
            Debug.Log("플레이어가 너무 멀리 있습니다.");
        }
    }
    void StartDialogue()
    {
        ShowNextDialogue();
        DialogueUI dialogueUI = UIManager.Instance.GetUI<DialogueUI>();
        if (dialogueUI != null)
        {
            dialogueUI.OnDialogueScreenClick = ShowNextDialogue; // 대화창 클릭 이벤트 할당
        }
        UIManager.Instance.OpenUI<DialogueUI>();
        Time.timeScale = 0f; // 대화 중 타임스케일 0
    }

    public void ShowNextDialogue()
    {
        if (dialogues == null || currentDialogueIndex >= dialogues.Count)
        {
            UIManager.Instance.CloseUI<DialogueUI>();
            Time.timeScale = 1f;
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