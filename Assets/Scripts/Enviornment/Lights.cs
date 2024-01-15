using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lights : MonoBehaviour
{
    [SerializeField] Light2D Light;
    [SerializeField] float maxWait = 1f;
    [SerializeField] float maxFlicker = 0.2f;
    [SerializeField] float minFlicker = 0.1f;
    float timer;
    float interval;
    public bool Triggered;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!Triggered) {return;}
        timer += Time.deltaTime;
        if(timer > interval)
        {
            ToggleLight();
        }
    }

    void ToggleLight() // this is for single light
    {
        Light.enabled = !Light.enabled;
        if(Light.enabled)
        {
            interval = Random.Range(0, maxWait);
        }
        else
        {
            interval = Random.Range(0, maxFlicker);
        }

        timer = 0;
    }
}
