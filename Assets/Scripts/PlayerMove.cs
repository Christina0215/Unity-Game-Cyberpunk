using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rig;
    public LayerMask ground;
    private CircleCollider2D col;
    public float jumpForce;
    public float speed;
    private Animator anim;
    private bool jump;
    private bool onGround;
    private bool isDashing;
    private int jumpTimes = 1;
    private float fallMultiplier = 3f;
    private float lowJumpMultiplier = 2f;
    public ParticleSystem jumpParticle;
    // Start is called before the first frame update
    public float dashTime;
    private float dashTimeLeft;
    public float dashSpeed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        Jump();
        if(Input.GetKeyDown(KeyCode.J)&&Input.GetAxis("Horizontal")!=0)
        {
            ReadyToDash();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(col.IsTouchingLayers(ground)&&!onGround)
        {
            onGround = true;
            jumpParticle.Play();
        }
        if (!col.IsTouchingLayers(ground) && onGround)
        {
            onGround = false;
        }
        Dash();
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        if (isDashing)
            return;
        float horizontal = Input.GetAxis("Horizontal");
        float toward = Input.GetAxisRaw("Horizontal");

        anim.SetBool("isRunning", false);

        if (horizontal != 0)
        {
            rig.velocity = new Vector2(horizontal * speed * Time.deltaTime, rig.velocity.y);
            anim.SetBool("isRunning", true);
        }

        if (toward != 0)
            transform.localScale = new Vector3(toward*0.8f, 0.8f, 0.8f);

    }
    void Jump()
    {
        if (isDashing)
            return;
        if (onGround)
        {
            jumpTimes = 1;
        }
        if (Input.GetButtonDown("Jump")&&jumpTimes>0)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce*1.4f);
            anim.SetBool("isJumping", true);
            jumpTimes--;
            jumpParticle.Play();
        }
        if(Input.GetButtonDown("Jump") && jumpTimes == 0 && onGround)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            anim.SetBool("isJumping", true);
            jumpParticle.Play();
        }
    }
    void SwitchAnim()
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        anim.SetBool("isIdleing", false);
        if (rig.velocity.y < 0 && !onGround)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning",false);
            anim.SetBool("isIdleing", false);
            anim.SetBool("FalltoIdle", false);
            anim.SetBool("isFalling", true);
        }
        if (anim.GetBool("isJumping"))
        {
            if(rig.velocity.y<0)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
                rig.velocity += Vector2.up * Physics2D.gravity.y *10* (fallMultiplier - 1) * Time.deltaTime;
            }
            if (rig.velocity.y > 0 )
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isFalling", false);
                rig.velocity += Vector2.up * Physics2D.gravity.y *0.8f* (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        else if(col.IsTouchingLayers(ground))
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("FalltoIdle", true);
        }
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("Fall_to_Idle")))
        {
            anim.SetBool("isIdleing", true);
            anim.SetBool("FalltoIdle", false);
        }
    }
    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
    }
    void Dash()
    {
        float toward = Input.GetAxisRaw("Horizontal");
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                rig.velocity = new Vector2(dashSpeed * toward, rig.velocity.y);
                dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFromPool();
            }
            if(dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }
}
