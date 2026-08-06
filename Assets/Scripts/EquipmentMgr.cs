using System.Collections.Generic;
using UnityEngine;

public class EquipmentMgr : MonoBehaviour
{
    public static EquipmentMgr instance;

    // 현재 장착된 아이템 저장
    public Dictionary<EquipmentSlotType, Item> currentEquipment = new Dictionary<EquipmentSlotType, Item>();

    // UI 갱신용 이벤트
    public delegate void OnEquipmentChanged(EquipmentSlotType slotType, Item newItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // 아이템 장착
    public void Equip(Item newItem)
    {
        EquipmentSlotType targetSlot = newItem.equipSlotType;
        if (targetSlot == EquipmentSlotType.Gambeson)
        {
            print("dwa");
        }
        

        // 3. 장착 처리
        currentEquipment[targetSlot] = newItem;

        // 인벤토리에서 제거
        Inventory.instance.Remove(newItem);

        // UI 업데이트 이벤트 호출
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(targetSlot, newItem);

        // ★ 장착 후 활성화된 모든 슬롯이 찼는지 검사
        CheckAllSlotsEquipped();
    }

    // 활성화된 모든 슬롯에 아이템이 장착되었는지 확인하는 함수
    void CheckAllSlotsEquipped()
    {
        // 씬에 있는 모든 EquipmentSlot을 가져옴
        EquipmentSlot[] allSlots = FindObjectsOfType<EquipmentSlot>();

        foreach (EquipmentSlot slot in allSlots)
        {
            // 비활성화(isLocked)된 슬롯은 검사에서 제외
            if (slot.isLocked) continue;

            // 활성화된 슬롯인데 장착된 아이템이 없다면 함수 종료
            if (!currentEquipment.ContainsKey(slot.slotType) || currentEquipment[slot.slotType] == null)
            {
                return; // 하나라도 비어있으면 풀 장착이 아님
            }
        }

        // 반복문을 무사히 통과했다면 활성화된 모든 슬롯이 찼다는 뜻!
        GameMgr.I.Nextscene();
    }

    // 해당 부위 슬롯의 잠금 상태 확인 함수
    bool IsSlotLocked(EquipmentSlotType slotType)
    {
        EquipmentSlot[] slots = FindObjectsOfType<EquipmentSlot>();
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.slotType == slotType)
            {
                return slot.isLocked;
            }
        }
        return false;
    }
}