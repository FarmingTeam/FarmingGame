using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcID;

    private NpcDialog dialogManager;

    void Start()
    {
        dialogManager = FindObjectOfType<NpcDialog>();
    }

    public void Talk()
    {
        var dialogues = dialogManager.GetDialoguesByNpc(npcID);
        foreach (var dialogue in dialogues)
        {
            Debug.Log(dialogue.DialogCon);
            // 실제론 UI 출력, 사운드 재생 등으로 대사 처리
        }
    }
}
