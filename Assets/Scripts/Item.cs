using UnityEngine;

// 아이템 유형 정의
public enum ItemType
{
    Gambeson,  
    Chainmail, 
    Cuirass,      
    Helmet,
    Coif,
    Chaincoif,
    Armarmor,
    Gloves,
    Pants,
    Legarmor,
    Shoes,
    Surcoat,
    RHand,
    LHand,
    Shoulder,
}

public enum EquipmentSlotType
{
    Gambeson,
    Chainmail,
    Cuirass,
    Helmet,
    Coif,
    Chaincoif,
    Armarmor,
    Gloves,
    Pants,
    Legarmor,
    Shoes,
    Surcoat,
    RHand,
    LHand,
    Shoulder,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public EquipmentSlotType equipSlotType;

    // 아이템 사용 시 실행될 함수
    public void Use()
    {
        Debug.Log($"{itemName} ({type}) 아이템 사용");

        switch (type)
        {
            case ItemType.Gambeson:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Chainmail:
                EquipmentMgr.instance.Equip(this);
                break;
            case ItemType.Helmet:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Cuirass:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Coif:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Chaincoif:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Armarmor:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Pants:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Legarmor:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Shoes:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Surcoat:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.RHand:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.LHand:
                EquipmentMgr.instance.Equip(this);
                break;

            case ItemType.Shoulder:
                EquipmentMgr.instance.Equip(this);
                break;
        }
    }
}