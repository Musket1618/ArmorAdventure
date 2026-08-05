using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentSlotType slotType; // Inspector에서 무기/갑옷/투구 등 지정
    public Image icon;                 // 장착 아이콘 표시 UI Image
    public Sprite defaultSprite;        // 빈 슬롯일 때 표시할 실루엣 아이콘 (선택사항)

    private Item currentItem;

    private void Start()
    {
        // 장착 상태 변경 이벤트 구독
        EquipmentMgr.instance.onEquipmentChanged += OnEquipmentChanged;
        ClearSlot();
    }

    // 장착 이벤트 발생 시 내 슬롯 타입에 해당하는 경우 UI 갱신
    void OnEquipmentChanged(EquipmentSlotType changedSlotType, Item newItem)
    {
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
        currentItem = newItem;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        currentItem = null;

        // 실루엣 이미지(defaultSprite)가 있다면 표시, 없으면 이미지 숨김
        if (defaultSprite != null)
        {
            icon.sprite = defaultSprite;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}

