using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTileLoad : TileObjectLoad
{  
    public override TileObject Load()
    {

        return TileControl.Instance.GetTileObjectByType(ObjectInteractionType.House); // 2*2인데 어떻게?
    }
}

