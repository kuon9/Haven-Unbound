using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image spriteImage;
    public void UpdateItem(Item item)
    {
        this.item = item;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            Debug.Log(item.Name);
        }
        else
        {
            Debug.Log("empty");
        }
    }
}