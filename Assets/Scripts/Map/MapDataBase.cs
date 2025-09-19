using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataBase : Singleton<MapDataBase>
{
    const string MAPPREFABPATH = "MapData/MapPrefabs";

    public Map[] Maps;

    protected override void Initialize()
    {
        Maps = Resources.LoadAll<Map>(MAPPREFABPATH);
    }
}
