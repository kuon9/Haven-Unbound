using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public PlayerBaseInputs playerControls;
    public int Charge;
    public int MaxCharge;

    private float Radius;
    private float Angle;
}
