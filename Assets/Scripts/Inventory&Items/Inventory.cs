using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemDataBase itemData;
    public AbilityTracker abilityTracker;

    public List<Item> PlayerItems = new List<Item> ();
    public List<Item> AbilityItems = new List<Item>();
    public List<Item> EquippedItems = new List<Item>(); 

    public void GiveItem(int id)
    {
        Item item = itemData.GetItem(id);
        PlayerItems.Add(item);
        if(item.Type == ItemType.AbilityItem)
        {
            AbilityItems.Add(item);
            CheckAbilities();
        }
    }
    public Item CheckForItem(int id)
    {
        return PlayerItems.Find(item => item.Item_ID == id);
    }

    public void EquipItem(int id)
    {
        Item item = CheckForItem(id);
        if(item != null && !EquippedItems.Contains(item))
        {
            EquippedItems.Add(item);
        }
    }
    public void UnequipItem(int id)
    {
        Item item = CheckForItem(id);
        if (item != null && !EquippedItems.Contains(item))
        {
            EquippedItems.Remove(item);
        }
    }
    public void RemoveItem(int id)
    {
        Item item = CheckForItem(id);
        if(item != null)
        {
            PlayerItems.Remove(item);
        }
    }
    public void CheckAbilities()
    {
        foreach (Item item in AbilityItems)
        {
            if (item.Item_ID == 1)
            {
                abilityTracker.canDash = true;
            }
            if(item.Item_ID == 2)
            {
                abilityTracker.canDoubleJump = true;
            }
            Debug.Log(item.Name);
        }
    }
}
