using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Animator animator;
    private bool isCrouching = false;
    private bool isWalking = false;
    private bool HasGun = false;

    private float crouchWeight = 0f;
    private float walkWeight = 0f;
    private float rifleWeight = 0f;

    public GameObject gunPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isWalking)
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && !isCrouching)
        {
            isWalking = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isWalking = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            HasGun = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HasGun = false;
        }

        float targetCrouchWeight;
        if (isCrouching)
        {
            targetCrouchWeight = 1f;
        }
        else
        {
            targetCrouchWeight = 0f;
        }

        crouchWeight = Mathf.Lerp(crouchWeight, targetCrouchWeight, Time.deltaTime * 5f);

        float targetWalkWeight;
        if (isWalking)
        {
            targetWalkWeight = 1f;
        }
        else
        {
            targetWalkWeight = 0f;
        }

        walkWeight = Mathf.Lerp(walkWeight, targetWalkWeight, Time.deltaTime * 5f);

        float targetRifleWeight;
        if (HasGun)
        {
            targetRifleWeight = 1f;
        }
        else
        {
            targetRifleWeight = 0f;
        }
        rifleWeight = Mathf.Lerp(rifleWeight, targetRifleWeight, Time.deltaTime * 5f);

        animator.SetLayerWeight(animator.GetLayerIndex("Idle Layer"), 1f - crouchWeight);
        animator.SetLayerWeight(animator.GetLayerIndex("Crouched Layer"), crouchWeight);

        animator.SetLayerWeight(animator.GetLayerIndex("Idle Layer"), 1f - walkWeight);
        animator.SetLayerWeight(animator.GetLayerIndex("Run Layer"), walkWeight);

        animator.SetLayerWeight(animator.GetLayerIndex("Rifle Idle"), rifleWeight);

        if (rifleWeight > 0.9f)
        {
            gunPrefab.SetActive(true);
        }
        else
        {
            gunPrefab.SetActive(false);
        }

        animator.SetBool("IsCrouched", isCrouching);
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("HasGun", HasGun);
    }
}
