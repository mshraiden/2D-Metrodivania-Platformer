using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkspeed = 1; //so that we can edit it in unity and that too in the field of player
    private int jumpBufferCounter = 0;

    private int airJumpCounter = 0; //counts air jumps
    private float coyoteTimeCounter = 0;
    private Rigidbody2D rb;
    private float xaxis, yAxis; //to get directional input
    Animator anim;
    private float gravity;
    PlayerStateList pState;
    private bool canDash;
    private bool dashed;

    [Header("Vertical Movement Settings")]
    [SerializeField] private float jumpforce = 35;
    [SerializeField] private int jumpBufferFrames;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int maxAirJumps;
    [Space(5)]

    [Header("Groundcheck Settings")]
    [SerializeField] private Transform groundcheckpoint;
    [SerializeField] private float groundcheckY = 0.2f;
    [SerializeField] private float groundcheckX = 0.5f;
    [SerializeField] private LayerMask WhatIsGround;
    [Space(5)]

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] GameObject dashEffect;
    [Space(5)]

    [Header("Attack Settings")]
    bool attack = false;
    float timeBetweenAttack;
    float timeSinceAttck;
    [SerializeField] Transform SideAttackTransform, UpAttackTransform, DownAttackTransform; // to get the position of the attack
    [SerializeField] Vector2 SideAttackArea, UpAttackArea, DownAttackArea;
    [SerializeField] LayerMask attackableLayer; // to get the layer of the attackable objects
    [SerializeField] float damage;  //can be changed later


    public static PlayerController Instance; // so that we can access it from other scrips as well
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pState = GetComponent<PlayerStateList>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        gravity = rb.gravityScale;
        canDash = true;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // color for attack areas
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }
    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpVariables();
        if (pState.dashing) return;
        Flip();
        Move();
        Jump();
        StartDash();
        Attack();


    }

    void GetInputs()
    {
        xaxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical"); 
        attack = Input.GetMouseButtonDown(0); // using Left mouse button for attack
    }

    void Flip()
    {
        if (xaxis < 0)
        {
            transform.localScale = new Vector2(-5, transform.localScale.y);
        }
        else if (xaxis > 0)
        {
            transform.localScale = new Vector2(5, transform.localScale.y);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkspeed * xaxis, rb.velocity.y); //horizontal changes without affecting vertical for the player
        anim.SetBool("walking", rb.velocity.x != 0 && Grounded()); //to animate walking
        
    }

    void StartDash()
    {
        if (Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash()); //so that player cant dash more than once in air
            dashed = true;

        }
        if(Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        anim.SetTrigger("dashing");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        if (Grounded()) Instantiate(dashEffect, transform);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Attack()
    {
        timeSinceAttck += Time.deltaTime;
        if (attack && timeSinceAttck >= timeBetweenAttack)
        {
            timeSinceAttck = 0;


            if (yAxis == 0 || yAxis < 0 && Grounded())
            {
                anim.SetTrigger("Attacking");
                Hit(SideAttackTransform, SideAttackArea); // horizontal attack
            }
            else if (yAxis > 0)
            {
                anim.SetTrigger("AttackUp"); // trigger upward attack animation
                Hit(UpAttackTransform, UpAttackArea); // upward attack
            }
            else if (yAxis < 0 && !Grounded())
            {

                Hit(DownAttackTransform, DownAttackArea); // downward attack
            }
            else if (!Grounded())
            {
                anim.SetTrigger("AttackJump"); 
                Hit(SideAttackTransform, SideAttackArea); 
            }
        }
    }

    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackableLayer);
        if(objectsToHit.Length > 0)
        {
            Debug.Log("Hit");
        }
        for(int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].GetComponent<Enemy>() != null)
            {
                objectsToHit[i].GetComponent<Enemy>().EnemyHit(damage, (objectsToHit[i].transform.position - transform.position).normalized.normalized, 100); // assuming 1 damage for now
            }
           

        }
        
    }


    public bool Grounded() //to confirm when player is standing on ground
    {
        if (Physics2D.Raycast(groundcheckpoint.position, Vector2.down, groundcheckY, WhatIsGround) 
            || Physics2D.Raycast(groundcheckpoint.position + new Vector3(groundcheckX, 0, 0), Vector2.down, groundcheckY, WhatIsGround)
            || Physics2D.Raycast(groundcheckpoint.position + new Vector3(-groundcheckX, 0, 0), Vector2.down, groundcheckY, WhatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        // VARIABLE JUMP HEIGHT — cut jump short if released early
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            pState.jumping = false;
        }

        // SINGLE JUMP with COYOTE TIME + JUMP BUFFER
        if (!pState.jumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                pState.jumping = true;
                anim.SetBool("jumping", true); // for general jump animation
                return;
            }

            // DOUBLE JUMP
            if (!Grounded() && airJumpCounter < maxAirJumps && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                airJumpCounter++;
                pState.jumping = true;

                // Trigger double jump animation
                if (airJumpCounter == 1)
                {

                }

                return;
            }
        }

        // Update jump animation status
        anim.SetBool("jumping", !Grounded());
    }

    void UpdateJumpVariables()
    {
        if(Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;

        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }
    }
}
