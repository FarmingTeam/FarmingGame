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
}
