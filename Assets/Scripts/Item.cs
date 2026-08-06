using UnityEngine;

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
    public EquipmentSlotType equipSlotType;

    // 아이템 사용 시 실행될 함수
    public void Use()
    {
        EquipmentMgr.instance.Equip(this);
    }
}