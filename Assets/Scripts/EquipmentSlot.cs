using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentSlotType slotType; // 무기, 갑옷 등 부위 지정
    public Image icon;                 // 장착 아이콘 표시용 Image 컴포넌트

    [Header("슬롯 설정")]
    public bool isLocked = false;      // ★ 체크하면 인스펙터에서 슬롯 비활성화

    private void Start()
    {
        if (EquipmentMgr.instance != null)
        {
            EquipmentMgr.instance.onEquipmentChanged += OnEquipmentChanged;
        }

        // 인스펙터에서 비활성화 체크가 되어 있다면 붉은색/반투명 등으로 시각적 표시 (선택사항)
        if (isLocked && icon != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            ClearSlot();
        }
    }

    void OnEquipmentChanged(EquipmentSlotType changedSlotType, Item newItem)
    {
        // 잠긴(비활성화된) 슬롯이면 UI를 업데이트하지 않음
        if (isLocked) return;

        if (changedSlotType == slotType)
        {
            if (newItem != null)
                AddItem(newItem);
            else
                ClearSlot();
        }
    }

    public void AddItem(Item newItem)
    {
        if (isLocked) return; // 비활성화 상태면 추가 거부

        if (icon != null && newItem.icon != null)
        {
            icon.sprite = newItem.icon;
            icon.color = Color.white;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}