using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ITileInteraction
{
    public abstract TileState Interaction(int tool);
}
