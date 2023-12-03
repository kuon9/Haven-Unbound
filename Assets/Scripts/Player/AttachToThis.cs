using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToThis : MonoBehaviour
{
    [SerializeField] GameObject player;
    

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;    
        }
    }

}