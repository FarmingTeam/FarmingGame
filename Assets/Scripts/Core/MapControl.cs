using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : Singleton<MapControl>
{
    [SerializeField] Player playerPrefab;
    [SerializeField] Map mapPrefab;
    [HideInInspector] public Player player;
    [HideInInspector] public Map map;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
    protected void Start()
    {
        map = Instantiate(mapPrefab);
        player = Instantiate(playerPrefab);
    }
}
