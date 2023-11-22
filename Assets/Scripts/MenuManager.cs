using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        StartCoroutine(ActivateCanvas());
    }

    //Activate Canvas after Cutscene
    IEnumerator ActivateCanvas ()
    {
        yield return new WaitForSeconds(22f);
        canvas.enabled = true;
    }
}
