using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    private Rigidbody2D rig;
    public LayerMask ground;
    private CircleCollider2D col;
    public float jumpForce;
    public float speed;
    private Animator anim;
    private bool onGround;
    private bool isDashing;
    private float fallMultiplier = 3f;
    private float lowJumpMultiplier = 2f;
    public ParticleSystem flyParticle;
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        Fly();
        Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (col.IsTouchingLayers(ground) && !onGround)
        {
            onGround = true;
        }
        if (!col.IsTouchingLayers(ground) && onGround)
        {
            onGround = false;
        }
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        anim.SetBool("isRunning", false);

        if (Input.GetKey(KeyCode.A))
        {
            rig.velocity = new Vector2(-1 * speed * Time.deltaTime, rig.velocity.y);
            anim.SetBool("isRunning", true);
            transform.localScale = new Vector3(-1 * 3f, 3f, 0.8f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rig.velocity = new Vector2(1 * speed * Time.deltaTime, rig.velocity.y);
            anim.SetBool("isRunning", true);
            transform.localScale = new Vector3(1 *3f, 3f, 0.8f);
        }
            

    }
    void Fly()
    {
        if (Input.GetButton("Jump"))
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce * 1.6f);
            anim.SetBool("isFlying", true);
            flyParticle.Play();
            Light.SetActive(true);
        }
        if (Input.GetButton("Jump") && onGround)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce * 1.6f);
            anim.SetBool("isFlying", true);
            flyParticle.Play();
            Light.SetActive(true);
        }
    }
    void Fire()
    {
        anim.SetBool("isAttacking", false);
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("isAttacking", true);
        }
    }
    void SwitchAnim()
    {
        anim.SetBool("isIdleing", false);
        if (rig.velocity.y < 0 && !onGround)
        {
            anim.SetBool("isFlying", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdleing", false);
            anim.SetBool("isFalling", true);
        }
        if (anim.GetBool("isFlying"))
        {
            if (rig.velocity.y < 0)
            {
                anim.SetBool("isFlying", false);
                anim.SetBool("isFalling", true);
                rig.velocity += Vector2.up * Physics2D.gravity.y * 10 * (fallMultiplier - 1) * Time.deltaTime;
                Light.SetActive(false);
            }
            if (rig.velocity.y > 0)
            {
                anim.SetBool("isFlying", true);
                anim.SetBool("isFalling", false);
                rig.velocity += Vector2.up * Physics2D.gravity.y * 0.8f * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        if(onGround)
        {
            anim.SetBool("isFalling", false);
            Light.SetActive(false);
        }
        if(onGround && !anim.GetBool("isFalling") && !anim.GetBool("isFlying"))
        {
            anim.SetBool("isIdleing", true);
            Light.SetActive(false);
        }
    }
}
