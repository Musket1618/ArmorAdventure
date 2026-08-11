using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMgr : MonoBehaviour
{
    public static EquipmentMgr instance;

    [Header("UI / 오버레이 오브젝트")]
    [Tooltip("장착 시 켜질 부위별 오브젝트들이 들어있는 부모 오브젝트")]
    public Transform equipmentShow;

    // 현재 장착된 아이템 저장
    public Dictionary<EquipmentSlotType, Item> currentEquipment = new Dictionary<EquipmentSlotType, Item>();

    // UI 갱신용 이벤트
    public delegate void OnEquipmentChanged(EquipmentSlotType slotType, Item newItem);
    public OnEquipmentChanged onEquipmentChanged;

    [Header("선행 장착 여부")]
    public bool isGambesonWorn = false;
    public bool isCoifWorn = false;
    public bool isPantsWorn = false;

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
        if (newItem == null) return;

        EquipmentSlotType targetSlot = newItem.equipSlotType;

        // 1. 선행 장착 상태 업데이트
        if (targetSlot == EquipmentSlotType.Gambeson) isGambesonWorn = true;
        if (targetSlot == EquipmentSlotType.Coif) isCoifWorn = true;
        if (targetSlot == EquipmentSlotType.Pants) isPantsWorn = true;

        // 2. 선행 조건 체크
        if (targetSlot == EquipmentSlotType.Helmet || targetSlot == EquipmentSlotType.Chaincoif)
        {
            if (!isCoifWorn) return;
            
        }
        if (targetSlot == EquipmentSlotType.Chainmail || targetSlot == EquipmentSlotType.Cuirass || targetSlot == EquipmentSlotType.Armarmor || targetSlot == EquipmentSlotType.Shoulder)
        {
            if (!isGambesonWorn) return;
        }
        if (targetSlot == EquipmentSlotType.Legarmor)
        {
            if (!isPantsWorn) return;
        }

        // 3. 장착 처리
        currentEquipment[targetSlot] = newItem;

        // 인벤토리에서 제거
        if (Inventory.instance != null)
        {
            Inventory.instance.Remove(newItem);
        }

        // UI 업데이트 이벤트 호출
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(targetSlot, newItem);

        // ★ EquipmentShow 하위의 동일한 이름을 가진 오브젝트 활성화
        SetEquipmentShowActive(targetSlot, true);

        // ★ 장착 후 활성화된 모든 슬롯이 찼는지 검사
        CheckAllSlotsEquipped();
    }

    // EquipmentShow 하위에서 이름이 동일한 오브젝트를 찾아 활성화하는 함수
    private void SetEquipmentShowActive(EquipmentSlotType slotType, bool isActive)
    {
        if (equipmentShow == null)
        {
            Debug.LogWarning("EquipmentShow 부모 Transform이 인스펙터에 설정되지 않았습니다!");
            return;
        }

        // Enum 이름을 문자열로 변환 (예: EquipmentSlotType.Gambeson -> "Gambeson")
        string slotName = slotType.ToString();

        // EquipmentShow 하위에서 해당 이름을 가진 자식 오브젝트 검색
        Transform targetObject = equipmentShow.Find(slotName);

        if (targetObject != null)
        {
            targetObject.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"EquipmentShow 하위에서 '{slotName}' 이름의 오브젝트를 찾을 수 없습니다.");
        }
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
        GameMgr.I.CutSceneActived = true;
        GameMgr.I.StopTimer();
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