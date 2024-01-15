using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    public GameObject [] flickeringLights;
    
    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < flickeringLights.Length; i++)    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject lights in flickeringLights)
            {
                // access each of the gameobject in array and setting their boolean to true.
                lights.GetComponent<Lights>().Triggered = true;
            }
        }    
    }
}
