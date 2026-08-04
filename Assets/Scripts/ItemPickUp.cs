using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item; // 이 필드 아이템이 가지고 있을 Item 데이터

    private void OnTriggerStay2D(Collider2D other)
    {
        // 충돌한 대상이 플레이어일 때만 작동
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        // 인벤토리에 아이템 추가 시도
        bool wasPickedUp = Inventory.instance.Add(item);

        // 인벤토리에 들어가는 데 성공했다면 필드의 오브젝트 삭제
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}