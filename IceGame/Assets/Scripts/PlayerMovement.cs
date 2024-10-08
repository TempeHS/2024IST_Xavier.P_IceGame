using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f; 
    public float jumpingPower = 8f; 
    private bool isFacingRight = true;
    //public Vector2 positionToMoveTo;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private bool needStand;
    private bool isCrouching;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    //private float dashingCooldown = 0.4f;
    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform roofCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private CapsuleCollider2D cc2d;
    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        if (isDashing) 
        {
            return;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else 
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && !isCrouching)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if (Input.GetButtonDown("Crouch") /*&& IsGrounded()*/)
        {
            //bc2d.size = new Vector2(bc2d.size.x, 0.25f);
            cc2d.size = new Vector2(cc2d.size.x, 0.75f);
            isCrouching = true;
        }
        if (Input.GetButtonUp("Crouch") && !IsRoof())
        {
            //bc2d.size = new Vector2(bc2d.size.x, 1.46f);
            cc2d.size = new Vector2(cc2d.size.x, 1.96f);
            //StartCoroutine(LerpPosition(positionToMoveTo, 0.05f));
            isCrouching = false;
        } 
        else 
        {
            needStand = true;
        }
        if (needStand == true && !IsRoof() && !Input.GetButton("Crouch"))
        {
            //bc2d.size = new Vector2(bc2d.size.x, 1.46f);
            cc2d.size = new Vector2(cc2d.size.x, 1.96f);
            isCrouching = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }

        Flip();
    }
    /*
    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition; 
    } */

    // Update is called once per frame
    private void FixedUpdate()
    {
        //positionToMoveTo = new Vector2 (transform.position.x, transform.position.y + 0.605f);
        if (isDashing) 
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsRoof()
    {
        return Physics2D.OverlapCircle(roofCheck.position, 0.2f, groundLayer);
    }
    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        
        if (IsGrounded())
        {
            yield return new WaitForSeconds(0.1f);
            canDash = true;
        } else
        {
            yield return new WaitUntil( () => IsGrounded());
            yield return new WaitForSeconds(0.1f);
            canDash = true;
        }
        
    }
}
