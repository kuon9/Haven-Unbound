using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> UIItems = new List<ItemUI>();
    public Transform ItemPanel;
    public GameObject slotPrefab;

    public int SlotAmount;

    private void Awake()
    {
        for (int i = 0; i < SlotAmount; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(ItemPanel);
            instance.transform.localScale = new Vector3(1, 1, 1);
            UIItems.Add(instance.GetComponentInChildren<ItemUI>());
        }
       // AddNewItem(new Item(0, "Item Name", ItemType.baseItem));
    }
    public void UpdateSlot(int slot, Item item)
    {
        UIItems[slot].UpdateItem(item);
    }
    public void AddNewItem(Item item)
    {
        UpdateSlot(UIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(UIItems.FindIndex(i => i.item == item), null);
    }
}
