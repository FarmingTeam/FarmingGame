using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTileBehaviour : TileObjectInteraction
{
    public override bool Interaction(Equipment tool, Tile tile, out ChunkData chunkData)
    {
        switch ((int)tile.chunkData.chunkType % 10)
        {
            //Farm -> House
            case 0:
                SceneChangeManager.Instance.ChangeScene(SceneName.HouseScene); break;
            //House -> Farm
            case 1:
                SceneChangeManager.Instance.ChangeScene(SceneName.FarmScene); break;
            default:
                break;
        }
        chunkData = null;
        return false;
    }
}
