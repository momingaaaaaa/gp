using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<ItemSO> itemList = new List<ItemSO>();
    [SerializeField] private ItemSO defaultWeapon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }



    public void AddItem(ItemSO item)
    {
        itemList.Add(item);
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.AddItem(item);
            MessageUI.Instance.Show("你获得了一个：" + item.name);
        }
        else
        {
            Debug.LogWarning("InventoryUI 尚未初始化，无法更新UI。");
        }
    }

    public void RemoveItem(ItemSO item)
    {
        if (itemList.Remove(item))
        {
            // 这里也可以调用InventoryUI.Instance.RemoveItem(item); 同步更新UI
        }
        else
        {
            Debug.LogWarning("试图移除不存在的物品：" + item.name);
        }
    }
}