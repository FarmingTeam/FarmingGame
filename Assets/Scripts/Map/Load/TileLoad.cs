using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileLoad : MonoBehaviour
{
    [SerializeField] public TileObject[] tileObjects;

    readonly public Dictionary<ObjectInteractionType, TileObjectLoad> OBJECTACTIONPAIR = new Dictionary<ObjectInteractionType, TileObjectLoad>
    {
        { ObjectInteractionType.None, null},
        { ObjectInteractionType.Tree, new TreeTileLoad()},
        { ObjectInteractionType.Rock, new RockTileLoad()},
        { ObjectInteractionType.House, new HouseTileLoad()},
    };

    public TileObject GetTileObjectLoad(ObjectInteractionType type)
    {
        TileObject result = tileObjects.First(state => state.objectType == type);
        return result;
    }
}
