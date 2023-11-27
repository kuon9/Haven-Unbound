using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Flashlight))]
public class FlashRadiusView : Editor
{
    void OnSceneGUI()
    {
        Flashlight light = (Flashlight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(light.transform.position, Vector3.forward, Vector3.right, 360, light.FlashRadius);
        Vector3 viewAngleA = light.DirFromAngle(-light.FlashAngle / 2, false);
        Vector3 viewAngleB = light.DirFromAngle(light.FlashAngle / 2, false);

        Handles.color = Color.yellow;
        Handles.DrawLine(light.transform.position, light.transform.position + viewAngleA * light.FlashRadius);
        Handles.DrawLine(light.transform.position, light.transform.position + viewAngleB * light.FlashRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTartget in light.visibleTargets)
        {
            Handles.DrawLine(light.transform.position, visibleTartget.position);
        }     
    }

}
