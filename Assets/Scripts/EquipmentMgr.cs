using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMgr : MonoBehaviour
{
    public static EquipmentMgr instance;

    // 부위별 현재 장착된 아이템 저장 dictionary
    public Dictionary<EquipmentSlotType, Item> currentEquipment = new Dictionary<EquipmentSlotType, Item>();

    // 장착 변경 시 UI에 알리는 이벤트
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

        // 3. UI 갱신 이벤트 호출
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(targetSlot, newItem);
    } 
}
