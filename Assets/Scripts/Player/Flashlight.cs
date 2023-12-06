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
    public bool canFlash;
    public float flashCooldown;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
    public UnityEngine.Rendering.Universal.Light2D light;

    public float FlashRadius;
    [Range(0, 360)]
    public float FlashAngle;

    private void Awake()
    {
        playerControls = new PlayerBaseInputs();
        canFlash = true;
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
        if (context.started && canFlash)
        {
            if(Charge > 0)
            {
                //Debug.Log("Fire");
                FindHitTargets();
                Charge--;
            }
        }
    }

    public void FindHitTargets()
    {
        light.enabled = true;
        StartCoroutine(Flash());
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, FlashRadius,targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector2 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < FlashAngle / 2)
            {
                float disToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    if(target.tag == "Enemy")
                    {
                    Debug.Log(target);
                    visibleTargets.Add(target);

                    }
                }
            }
        }
    }
    private IEnumerator Flash()
    {
        canFlash = false;
        yield return new WaitForSeconds(.3f);
        light.enabled = false;
        yield return new WaitForSeconds(flashCooldown);
        canFlash = true;

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
