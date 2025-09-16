using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData",menuName = "NpcData") ]
public class NPCData : ScriptableObject
{
    public int NpcID;
    public string NpcName;
    public int NpcAge;
    public bool NpcGender;

    public Sprite sprite;

  
}
