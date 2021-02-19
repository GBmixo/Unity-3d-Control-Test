using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandler : MonoBehaviour
{
    PlayerInputs controls;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerInputs();
        controls.Player.Attack.started += ctx =>
        {
            Debug.Log("Attack button pressed");
            Attack();
        };
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        Debug.Log("Attack animation");
        animator.SetTrigger("Attack");
    }

    //string name = animator.GetLayerName(layerNumber) + "." + animatorStateName;
    //string animationFullNameHash = Animator.StringToHash(name);
}
