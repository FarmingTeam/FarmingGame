using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTileLoad : TileObjectLoad
{
    public override TileObject Load()
    {
        return TileControl.Instance.GetTileObjectByType(ObjectInteractionType.Tree);
    }
}
