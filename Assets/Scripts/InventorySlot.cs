using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;          // 아이템 아이콘 UI (자식 Image)

    Item item; // 현재 슬롯에 들어있는 아이템 데이터

    // 슬롯에 아이템 등록 및 UI 업데이트
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true; // 아이콘 보이기
    }

    // 슬롯 비우기
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false; // 아이콘 숨기기
    }

    // 슬롯(버튼) 클릭 시 아이템 사용
    public void OnSlotClick()
    {
        if (item != null)
        {
            item.Use(); // 아이템 자체 로직 실행

            // 소모품인 경우 사용 후 인벤토리에서 제거
            
                
            
        }
    }
}