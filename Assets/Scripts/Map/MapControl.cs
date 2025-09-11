using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : Singleton<MapControl>
{
    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
}
