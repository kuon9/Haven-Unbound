using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acttiventory : MonoBehaviour
{
    public GameObject Panel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Panel.SetActive(true);
        }
    }
}
