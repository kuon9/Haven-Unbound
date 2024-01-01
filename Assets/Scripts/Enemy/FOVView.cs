using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOV))]
public class FOVView : Editor
{
    void OnSceneGUI()
    {
        EnemyFOV light = (EnemyFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(light.transform.position, Vector3.forward, Vector3.right, 360, light.FOVRadius);
        Vector3 viewAngleA = light.DirFromAngle(-light.FOVAngle / 2, false);
        Vector3 viewAngleB = light.DirFromAngle(light.FOVAngle / 2, false);

        Handles.color = Color.yellow;
        Handles.DrawLine(light.transform.position, light.transform.position + viewAngleA * light.FOVRadius);
        Handles.DrawLine(light.transform.position, light.transform.position + viewAngleB * light.FOVRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTartget in light.visibleTargets)
        {
            Handles.DrawLine(light.transform.position, visibleTartget.position);
        }
    }
}
