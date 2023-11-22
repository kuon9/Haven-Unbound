using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void StartGame ()
    {
        SceneManager.LoadScene("Hotel");
    }
}
