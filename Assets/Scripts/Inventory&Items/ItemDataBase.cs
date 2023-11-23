using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> Items = new List<Item> ();

    private void Awake()
    {
        BuildDataBase();
    }
    public Item GetItem(int id)
    {
        return Items.Find(item => item.Item_ID == id);
    }
    private void BuildDataBase()
    {
        Items = new List<Item>()
        {
            new Item(0,"Item Name",ItemType.baseItem), // this is what to add when adding a new item
            new Item(1,"dash Ability",ItemType.AbilityItem),
            new Item(2,"double jump Ability",ItemType.AbilityItem)
        };
    }
}
