using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public static NpcDialog Instance { get; private set; }

    // 키: NPC ID, 값: 해당 NPC 대사 리스트
    private Dictionary<string, List<DialogueRow>> npcDialogues = new Dictionary<string, List<DialogueRow>>();

    // CSV에서 불러온 모든 대사 리스트
    public List<DialogueRow> allDialogues;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

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
            string npcID = dialogue.ConnectNPC.Trim();

            // 딕셔너리에 해당 NPC 키가 없으면 새 리스트 생성 후 추가
            if (!npcDialogues.ContainsKey(npcID))
            {
                npcDialogues[npcID] = new List<DialogueRow>();
            }

            // NPC 키에 대사 추가
            npcDialogues[npcID].Add(dialogue);
            Debug.Log($"초기화 완료! 딕셔너리 키 개수: {npcDialogues.Count}");
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
        if (npcDialogues.TryGetValue(npcID, out var dialogues))
            return dialogues;
        return new List<DialogueRow>();
    }

    // 확장: npcID별, 플레이어 대사 함께 리턴
    public List<DialogueRow> GetDialoguesByNpcAndPlayer(string npcID)
    {
        return allDialogues
            .Where(d => d.ConnectNPC == npcID || d.ConnectNPC == "P" || d.ConnectNPC == "Player")
            .OrderBy(d => d.DialogIdx)
            .ToList();
    }

    // 퀘스트 필터링 함수 (필요 시 사용)
    public List<DialogueRow> GetDialoguesByQuest(string questID)
    {
        return allDialogues
            .Where(d => d.ConnectQuest != null && d.ConnectQuest.Trim() == questID.Trim())
            .OrderBy(d => d.DialogIdx)
            .ToList();
    }
}
