using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Horizontal Movement")] 
    [SerializeField] private float speedForce;

    [Header("Player Veritcal Movement")] 
    [SerializeField] private float jumpForce;
    public bool isJumping;
    [SerializeField] private float maxFallSpeed;
    public ParticleSystem dust;

    [Header("Player Direction")] 
    public bool turnRight = true;

    [Header("Ground Check")] 
    public float groundRadius;
    public LayerMask groundLayer;
    public Transform groundCheckPos;
    private bool onGround;

    [Header("All Check")] 
    public bool isAttack;
    public bool isHurt;
    public bool isDie;

    [Header("Ref")] 
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimationState playerAnimationState;
    private Rigidbody2D rb;
    private SoundManager _soundManager;

    private void Start()
    {
        isDie = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimationState = GetComponent<PlayerAnimationState>();
        rb = GetComponent<Rigidbody2D>();
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (!isDie)
        {
            onGround = Physics2D.OverlapCircle(groundCheckPos.position, groundRadius, groundLayer);
            CharacterMovement();
            CharacterJump();
        }
        

    }

    private void CharacterMovement()
    {
        Vector3 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        //Debug.Log($"X : {direction.x}, Y: {direction.y}");

        transform.position += direction * speedForce * Time.deltaTime;

        if (direction.x > 0 )
        {
            turnRight = true;
            if (!isJumping && !isAttack && !isHurt)
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Walk;
            }
        }
        else if (direction.x < 0 ) 
        {
            turnRight = false;
            if (!isJumping && !isAttack && !isHurt)
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Walk;
            }
        }
        else
        {
            if (!isJumping && !isAttack && !isHurt)
            {
                PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Idle;
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxFallSpeed);
    }

    private void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && onGround && !isAttack && !isHurt)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            StartCoroutine(JumpSqueeze(0.5f,1.2f,0.1f));
            SoundManager.instace.Play(SoundManager.SoundName.Jump);

        }
        
        if (isJumping && rb.velocity.y > 0 && !isAttack && !isHurt)
        {
            PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Jump;
        }
        else if (isJumping && rb.velocity.y < 0 && !isAttack && !isHurt)
        {
            PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Falling;
        }
        else if (isJumping && rb.velocity.y == 0)
        {
            isJumping = false;
        }

        if (!isJumping && !onGround && !isAttack && !isHurt)
        {
            PlayerAnimationState.instance.State = PlayerAnimationState.PlayerState.Falling;
        }
        
    }

    private void Flip()
    {
        if (turnRight)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        
    }
    
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) 
    {
        CreateDust();
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    
    public void CreateDust()
    {
        dust.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position,groundRadius);
    }

    
}
