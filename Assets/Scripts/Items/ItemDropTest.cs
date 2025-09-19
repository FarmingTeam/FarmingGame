using System.Collections;
using UnityEngine;

public class ItemDropTest : MonoBehaviour
{
    // 드롭할 아이템 프리팹 (ItemToPlayer가 붙어있는 오브젝트)
    public GameObject itemPrefab;

    public int runtimeItemID = 9001;
    public string runtimeItemName = "Test Item";
    [TextArea] public string runtimeItemDescription = "For runtime test";
    public string runtimeItemPath = "Items/Test";
    public ItemType runtimeItemType = ItemType.Others;
    public Sprite runtimeItemIcon;
    public int runtimeMaxQuantity = 99;


    // 드롭 주기 (10초)
    public float dropInterval = 10f;

    // 맵 범위 (랜덤 스폰 위치 제한)
    public Vector2 dropAreaMin = new Vector2(0, 0);
    public Vector2 dropAreaMax = new Vector2(5, 5);

    private void Start()
    {
        // 주기적으로 아이템 드롭 실행
        StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropInterval);
            DropItem();
        }
    }

    private void DropItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("ItemPrefab이 할당되지 않았습니다.");
            return;
        }

        // 맵 내 랜덤 좌표 생성
        float x = Random.Range(dropAreaMin.x, dropAreaMax.x);
        float y = Random.Range(dropAreaMin.y, dropAreaMax.y);
        Vector3 dropPos = new Vector3(x, y, 0);

        var go = Instantiate(itemPrefab, dropPos, Quaternion.identity);
        var itp = go.GetComponent<ItemToPlayer>();

        var runtimeData = ScriptableObject.CreateInstance<ItemData>();
        runtimeData.itemID = runtimeItemID;
        runtimeData.itemName = runtimeItemName;
        runtimeData.itemDescription = runtimeItemDescription;
        runtimeData.itemPath = runtimeItemPath;
        runtimeData.itemType = runtimeItemType;
        runtimeData.itemIcon = runtimeItemIcon;
        runtimeData.maxQuantity = Mathf.Max(1, runtimeMaxQuantity);
        runtimeData.isStackable = runtimeData.maxQuantity > 1;

        itp.data = runtimeData;

        // 아이템 생성
        Instantiate(itemPrefab, dropPos, Quaternion.identity);
        Debug.Log($"아이템 드롭됨: {dropPos}");
    }
}
