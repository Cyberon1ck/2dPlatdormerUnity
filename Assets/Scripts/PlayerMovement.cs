using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D BoxColl2d;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirsas = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForse = 14f;

    [SerializeField] private AudioSource jumpSound;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BoxColl2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirsas = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirsas * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForse);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirsas > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirsas < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("StateAnim", (int)state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(BoxColl2d.bounds.center, BoxColl2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
