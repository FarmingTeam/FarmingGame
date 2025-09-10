using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileBehaviour : ITileInteraction
{
    public override TileState Interaction(int tool)
    {
        //만약 괭이를 들고있다면으로 수정
        if (tool == 0)
        {
            return TileControl.Instance.GetTileStateByType(TileInteractionType.Dirt);
        }
        return null;
    }
}
