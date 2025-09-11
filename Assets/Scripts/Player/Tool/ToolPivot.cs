using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPivot : PlayerController
{
    [Header("Tool")]
    public GameObject ToolSprite;
    public Equipment CurrentEquip;

    public Equipment[] QuickSlots = new Equipment[6];

    protected override void HandleQuickslot(int slot)
    {
        base.HandleQuickslot(slot);

        int idx = slot - 1;
        Equipment next = null;
        if (idx >= 0 && idx < QuickSlots.Length)
            next = QuickSlots[idx];

        if (slot == 1) next = null;

        ApplyEquip(next);
    }

    private void ApplyEquip(Equipment eq)
    {
        CurrentEquip = eq;

        if (ToolSprite) ToolSprite.SetActive(CurrentEquip != null); // 빈손이면 ToolSprite 비활성화
    }

}
