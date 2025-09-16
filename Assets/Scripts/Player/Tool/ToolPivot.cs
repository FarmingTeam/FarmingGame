using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolPivot : MonoBehaviour
{
    [Header("Tool")]
    public GameObject ToolSprite;
    public Equipment CurrentEquip;

    public Equipment[] QuickSlots = new Equipment[6];

    private Action onSelectionChange;
    //만약 특정 번호를 누를경우 여기로 신호가 감
    //그냥 그럼 퀵슬롯 배열에 미리 도구를 할당해주면 되겠네




    public void SelectQuickslot(int slot)
    {
        int idx = slot - 1;
        //플레이어의 현재 장비 리스트를 받아와서 이 번호에 맞는걸 ApplyEquip한다
        
        QuickSlots=MapControl.Instance.player.equipment.equipList.ToArray();
        


        ApplyEquip(QuickSlots[idx]);
    }

    private void ApplyEquip(Equipment eq)
    {
        CurrentEquip = eq;

        if (ToolSprite) ToolSprite.SetActive(CurrentEquip != null); // 빈손이면 ToolSprite 비활성화
        onSelectionChange?.Invoke();
        Debug.Log($"장착템: {CurrentEquip.equipmentName}");
    }

    public void SubscribeToSelectionChange(Action action)
    {
        onSelectionChange += action;
    }

    public void UnsubscribeToSelectionChange(Action action)
    {
        onSelectionChange -= action;    
    }

}
