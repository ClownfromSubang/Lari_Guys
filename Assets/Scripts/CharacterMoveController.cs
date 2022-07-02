﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;
    private Rigidbody2D rig;
    private Animator anim;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    void FixedUpdate ()
    {
        Vector2 velocityVector = rig.velocity;

        if (isJumping)
        {
            velocityVector.y += jumpAccel;
            isJumping = false;
        }

        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
        rig.velocity = velocityVector;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
            if (hit)
            {
                if (!isOnGround && rig.velocity.y <= 0)
                {
                  isOnGround = true;
                }
            }
            else
            {
               isOnGround = false;
            }
    }

    [Header("Jump")]
    public float jumpAccel;

    private bool isJumping;
    private bool isOnGround;

    private void OnDrawGizmos ()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            if (isOnGround)
            {
                isJumping = true;
            }
        }
        anim.SetBool("isOnGround", isOnGround);
    }
}
