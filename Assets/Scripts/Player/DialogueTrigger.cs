using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public bool playerinRange;
    public GameObject popUpText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerinRange) {return;}
        {
            BeginDialogue();    
        }    
    }

    void BeginDialogue()
    {
        if(playerinRange && Keyboard.current.eKey.wasPressedThisFrame)
        {       
            Debug.Log("Dialogue Starting");
            Dialogue.isTriggered = true;    
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("In Range");
            popUpText.SetActive(true);
            playerinRange = true;    
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            popUpText.SetActive(false);
            playerinRange = false;
            
        }
    }

}
