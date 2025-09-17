using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DialogueRow
{
    public string DialogIdx;
    public string DialogCon;
    public string ConnectNPC;
    public string ConnectQuest;
    public string Comment;
}

public class NpcDialog : MonoBehaviour
{
    // 키: NPC ID, 값: 해당 NPC 대사 리스트
    private Dictionary<string, List<DialogueRow>> npcDialogues = new Dictionary<string, List<DialogueRow>>();

    // CSV에서 불러온 모든 대사 리스트
    public List<DialogueRow> allDialogues;

    void Start()
    {
        LoadCsvFromResources("DialogData/Test");
        // 딕셔너리 초기화 및 분류 함수 호출
        InitializeNpcDialogues();
    }

    public void LoadCsvFromResources(string fileName)
    {
        allDialogues.Clear();

        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        if (csvData == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + fileName);
            return;
        }

        string[] lines = csvData.text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++) // 첫 줄 헤더 스킵
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] values = ParseCsvLine(line); //parseCsvLine 메서드 사용 쉼표 구분 매서드

            if (values.Length < 5) continue;

            DialogueRow row = new DialogueRow();
            row.DialogIdx = values[0];
            row.DialogCon = values[1];
            row.ConnectNPC = values[2];
            row.ConnectQuest = values[3];
            row.Comment = values[4];

            allDialogues.Add(row);
        }
    }

    void InitializeNpcDialogues()
    {
        // 딕셔너리를 비워 초기화
        npcDialogues.Clear();

        // 모든 대사를 순회하며
        foreach (DialogueRow dialogue in allDialogues)
        {
            // 각 대사의 ConnectNPC 필드를 키로 사용
            string npcID = dialogue.ConnectNPC;

            // 딕셔너리에 해당 NPC 키가 없으면 새 리스트 생성 후 추가
            if (!npcDialogues.ContainsKey(npcID))
            {
                npcDialogues[npcID] = new List<DialogueRow>();
            }

            // NPC 키에 대사 추가
            npcDialogues[npcID].Add(dialogue);
        }
    }
    string[] ParseCsvLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes; // 쌍따옴표 내부/외부 토글
            }
            else if (c == ',' && !inQuotes)
            {
                // 따옴표 밖에서 쉼표 만나면 컬럼 구분
                result.Add(current);
                current = "";
            }
            else
            {
                current += c; // 하나씩 문자 추가
            }
        }
        result.Add(current); // 마지막 컬럼 추가

        return result.ToArray();
    }


    // 특정 NPC ID의 대사 리스트를 반환
    public List<DialogueRow> GetDialoguesByNpc(string npcID)
    {
        if (npcDialogues.TryGetValue(npcID, out List<DialogueRow> dialogues))
        {
            return dialogues;
        }
        return new List<DialogueRow>(); // 없으면 빈 리스트 반환
    }

}
