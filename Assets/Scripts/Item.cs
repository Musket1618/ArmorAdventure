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
    Legarmor,
    Shoes,
    Surcoat,

}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // 아이템 이름
    public Sprite icon;     // UI에 표시할 아이콘
    public ItemType type;   // 아이템 타입

    // 아이템 사용 시 실행될 함수
    public void Use()
    {
        Debug.Log($"{itemName} ({type}) 아이템 사용");

        switch (type)
        {
            case ItemType.Gambeson:
                
                break;

            case ItemType.Chainmail:
                
                break;
            case ItemType.Helmet:
                
                break;

            case ItemType.Cuirass:
                
                break;

            case ItemType.Coif:

                break;

            case ItemType.Chaincoif:

                break;

            case ItemType.Armarmor:

                break;

            case ItemType.Legarmor:

                break;

            case ItemType.Shoes:

                break;

            case ItemType.Surcoat:

                break;
        }
    }
}