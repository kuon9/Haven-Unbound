using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public PlayerBaseInputs playerControls;
    private InputAction fire;
    public int Charge;
    public int MaxCharge;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();

    public float FlashRadius;
    [Range(0, 360)]
    public float FlashAngle;

    private void Awake()
    {
        playerControls = new PlayerBaseInputs();
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;

        fire.Enable();

        fire.performed += Fire;
    }
    private void OnDisable()
    {
        fire.Disable();
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Fire");
            FindHitTargets();
        }
    }

    public void FindHitTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, FlashRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < FlashAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    Debug.Log(target);
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
