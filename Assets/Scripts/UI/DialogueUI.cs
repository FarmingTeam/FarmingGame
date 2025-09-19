using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : UIBase
{
    public Image silhouette1;     // NPC 이미지
    public Image silhouette2;     // 플레이어 이미지
    [SerializeField] TextMeshProUGUI textbox;
    [SerializeField] TextMeshProUGUI namebox;

    // 밝고 어두운 색상 미리 준비
    public Color npcBright = Color.white;
    public Color npcDim = new Color(0.4f, 0.4f, 0.4f, 1f);
    public Color playerBright = Color.white;
    public Color playerDim = new Color(0.4f, 0.4f, 0.4f, 1f);
    public System.Action OnDialogueScreenClick;

    public void SetDialogue(string speaker, string content)
    {
        textbox.text = content;
        namebox.text = speaker;

        bool isNpcSpeaking = !IsPlayer(speaker);

        // Silhouette 색상 조절
        if (isNpcSpeaking)
        {
            silhouette1.color = npcBright;
            silhouette2.color = playerDim;
        }
        else
        {
            silhouette1.color = npcDim;
            silhouette2.color = playerBright;
        }
    }

    bool IsPlayer(string speaker)
    {
        // "플레이어", "Player" 등 구분자 기준으로 수정 가능
        return speaker.ToLower().Contains("플레이어") || speaker.ToLower().Contains("player");
    }

    void Update()
    {
        // 대화창이 열려있을 때 아무 곳 클릭 → 다음 대화 진행
        if (Input.GetMouseButtonDown(0) && OnDialogueScreenClick != null)
        {
            OnDialogueScreenClick.Invoke();
        }
    }
}
