using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; // SlotHolder의 Transform
    public GameObject inventoryUI; // 인벤토리 전체 창 패널

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;

        // Inventory의 데이터 변경 이벤트에 UI 업데이트 함수 연결
        inventory.onItemChangedCallback += UpdateUI;

        // SlotHolder 하위에 있는 모든 InventorySlot 컴포넌트를 가져옴
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    void Update()
    {
        // I 키를 누르면 인벤토리 창 열기/닫기 (토글)
        
    }

    // 인벤토리 데이터에 맞춰 슬롯 UI를 새로고침
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 인벤토리에 들어있는 아이템 개수 범위 내일 때
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}