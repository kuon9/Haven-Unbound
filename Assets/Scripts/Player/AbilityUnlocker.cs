using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AbilityType
{
    dash,
    doubleJump
}
public class AbilityUnlocker : MonoBehaviour
{
   // [SerializeField] bool unlockDash, unlockDoubleJump;
    [SerializeField] AbilityType type;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] TMP_Text unlockedText;
    public string unlockMessage;
    private float startY;
    private float speed = 3f;
    
    
    
    void Start()
    {
        startY = transform.position.y;     
    }
    
    
    void Update()
    {
        Vector3 position = transform.position;
        position = new Vector3(position.x, startY + Mathf.Sin(Time.time * speed) * 0.1f, position.z);
        transform.position = position;
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //AbilityTracker player = other.GetComponentInParent<AbilityTracker>();
            Inventory inventory = other.GetComponent<Inventory>();

            if(type == AbilityType.dash)
            {
                //player.canDash = true;
                inventory.GiveItem(1);
            }

            if(type == AbilityType.doubleJump)
            {
                //player.canDoubleJump = true;
                inventory.GiveItem(2);
            }
            
             Instantiate(pickupEffect, transform.position, Quaternion.identity);
            unlockedText.transform.parent.SetParent(null);
            unlockedText.transform.parent.position = transform.position;
            unlockedText.text = unlockMessage;
            unlockedText.gameObject.SetActive(true);
            Destroy(unlockedText.transform.parent.gameObject, 2f);
            Destroy(gameObject);
        
        }
    }

}
