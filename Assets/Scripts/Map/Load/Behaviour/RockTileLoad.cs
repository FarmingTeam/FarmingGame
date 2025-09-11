using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTileLoad : TileObjectLoad
{
    public override TileObject Load()
    {
        return TileControl.Instance.GetTileObjectByType(ObjectInteractionType.Rock);
    }
}

