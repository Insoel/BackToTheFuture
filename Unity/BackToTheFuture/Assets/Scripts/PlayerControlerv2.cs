using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlerv2 : MonoBehaviour
{
    public float maxSpeed = 2000.0f;
    public float jumpSpeed = 300f;
    public float jumpMaxTime = 0.1f;

    public Transform Checkground;
    public LayerMask groundLayers;

    Rigidbody2D     rb;
    Animator        anim;
    float           jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rb.velocity;

        currentVelocity = new Vector2 (maxSpeed * hAxis, currentVelocity.y);

        if(currentVelocity.x < -0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentVelocity.x > 0.5f)
        {
            transform.rotation = Quaternion.identity;
        }
        
        Collider2D groundCollision = Physics2D.OverlapCircle(Checkground.position, 5, groundLayers);

        bool onGround = groundCollision != null;
        
        if (Input.GetButtonDown("Jump"))
        {
	        if(onGround)
            {    
                currentVelocity.y = jumpSpeed;
                rb.gravityScale = 0.0f;

                jumpTime = Time.time;
            }
        }
        else if ((Input.GetButton("Jump")) && ((Time.time - jumpTime) < jumpMaxTime))
        {
            currentVelocity.y = jumpSpeed - 80.0f;
        }
        else
        {
            rb.gravityScale = 10.0f;
        }

        rb.velocity = currentVelocity;

        anim.SetFloat("VelX", Mathf.Abs(currentVelocity.x));
    }
}
