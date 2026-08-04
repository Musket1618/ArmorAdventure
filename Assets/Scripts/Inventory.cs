using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // 싱글톤 인스턴스

    public int space = 20; // 인벤토리 슬롯 최대 개수
    public List<Item> items = new List<Item>(); // 획득한 아이템 목록

    // UI 자동 갱신용 이벤트
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // 아이템 획득 시 호출
    public bool Add(Item item)
    {
        // 인벤토리 공간이 꽉 차면 줍지 못함
        if (items.Count >= space)
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return false;
        }

        items.Add(item);

        // UI에 변경 사항 알림
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    // 아이템 버리거나 사용 후 삭제 시 호출
    public void Remove(Item item)
    {
        items.Remove(item);

        // UI에 변경 사항 알림
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}