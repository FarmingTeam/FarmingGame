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
        OnLoad();
    }
    protected void Start()
    {
        
    }

    public void OnLoad()
    {
        map = Instantiate(mapPrefab);
        player = Instantiate(playerPrefab);
        player.InitPos(new Vector3(1, 1, 0));
    }

    //Refactor : 수정 필요
    public void ReLoader()
    {
        Destroy(map.gameObject);
        map = Instantiate (mapPrefab);
        player.InitPos(new Vector3(1, 1, 0));
    }
}
