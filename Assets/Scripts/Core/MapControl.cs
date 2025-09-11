using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : Singleton<MapControl>
{
    public Player player;
    public Map map;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
    protected void Start()
    {
        Debug.Log(player);
        Debug.Log(map);
    }
}
