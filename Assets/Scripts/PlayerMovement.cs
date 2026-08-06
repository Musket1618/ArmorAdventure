using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    float horizontalInput;
    [SerializeField] private float jumpingPower = 4f;

    private Rigidbody2D rb;

    private bool grounded;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        if (GameMgr.I.isCanMove)
        {           
            rb.velocity = new Vector2 (horizontalInput * moveSpeed, rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    Jump();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        if (horizontalInput < 0)
        {
            gameObject.transform.localRotation = new Quaternion(0, -180, 0, 0);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    
    
}
