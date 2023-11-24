using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    KeyItem,
    AbilityItem,
    baseItem
}
public class Item
{
    public int Item_ID;
    public string Name;
    public ItemType Type;

    public Item(int item_ID, string name, ItemType type)
    {
        this.Item_ID = item_ID;
        this.Name = name;
        this.Type = type;
    }
    public Item(Item item)
    {
        this.Item_ID=item.Item_ID;
        this.Name = item.Name;
        this.Type = item.Type;
    }
}