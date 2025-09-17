using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItemScript : MonoBehaviour
{
   


    //================== 아이템 관련 =======================================
    void 아이템데이터가져오기(int 아이템ID) //씨앗도 당연히 가능
    {
        ItemData itemdata=  ResourceManager.Instance.GetItem(아이템ID);
    }

    void 인벤토리아이템추가(int 아이템ID, int 갯수) //씨앗도 동일하게 이거사용
    {
        MapControl.Instance.player.inventory.AdditemsByID(아이템ID,갯수);
    }

    void 인벤토리아이템갯수차감(int 아이템ID, int 갯수) //씨앗도 동일하게 이거 사용
    {
        MapControl.Instance.player.inventory.SubtractItemQuantity(아이템ID, 갯수);
    }

    void 도구관련기능()
    {
        //현재 플레이어가 장착한 장비를 알고싶어
        Equipment playerEquipment= MapControl.Instance.player.tool.CurrentEquip;

        //====씨앗바구니=====
        //현재 씨앗바구니에 들어있는 씨앗을 알고싶어(씨앗바구니에서 씨앗을 고르는 순간 설정이 되며, 아무것도 안골랐다면 iD가 기본값인 0으로 나올듯)
        int seedID = MapControl.Instance.player.equipment.CheckEquipmentExtra(EquipmentType.SeedBasket);

        //=====물뿌리개======
        //물뿌리개에 대해 남은 물양을 알고싶어
        int waterleft= MapControl.Instance.player.equipment.CheckEquipmentExtra(EquipmentType.WateringCan);

        //물뿌리개의 물을 쓰고싶어 (0이면 물을 못주는 로직은 상호작용쪽에서 추가 필요)
        MapControl.Instance.player.equipment.ChangeEquipmentExtra(EquipmentType.WateringCan, -1);
        //물뿌리개의 물을 끝까지 채우고싶어
        MapControl.Instance.player.equipment.ChangeEquipmentExtra(EquipmentType.WateringCan, 10);



    }




    //================== UI 관련 =======================================

    //=======일반UI(esc로 닫히지 않으며 게임 화면에 계속 있을 UI)=======
    void UI열기() //예를들어 툴바(esc로 닫히지 않는 부분)
    {
        UIManager.Instance.OpenUI<UIToolBar>();
    }

    void UI닫기() //예를들어 툴바(esc로 닫히지 않는 부분)
    {
        UIManager.Instance.CloseUI<UIToolBar>();
    }


    //=========팝업UI(esc로 닫히며, 원한다면 수동으로 닫을수도 있는 팝업ui)=====================
    void 팝업UI열기() //esc로 닫히는 부류만(예를들어 인벤토리)
    {
        UIManager.Instance.OpenPopup<UIInventory>();
    }


    void 특정팝업UI닫기(UIInventory inventory) //언제까지나 인벤토리는 예시일뿐 UIPOpup을 상속받은 것들은 다 가능
    {
        UIManager.Instance.ClosePopupUI(inventory);
    }
   
}
