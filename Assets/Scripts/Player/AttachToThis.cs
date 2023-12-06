using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToThis : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dog;

    
   
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {   
            player.transform.SetParent(this.transform);
            dog.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
            dog.transform.parent = null;    
        }
    }
}