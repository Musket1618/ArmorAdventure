using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float horizontalInput;
    [SerializeField] private float jumpingPower = 4f;

    [Header("감속 설정")]
    [Tooltip("값이 클수록 빨리 멈추고, 작을수록 더 미끄러집니다.")]
    [SerializeField] private float deceleration = 10f;

    [Header("지면 감지 설정")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // 이동 가능할 때만 점프 입력 받기
        if (GameMgr.I.isCanMove)
        {
            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        // 1. 지면 감지
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2. 이동 제한 상태이거나 입력이 없을 때: 부드럽게 감속 (Lerp)
        if (!GameMgr.I.isCanMove)
        {
            float targetVelocityX = Mathf.Lerp(rb.velocity.x, 0f, Time.fixedDeltaTime * deceleration);
            rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
            return;
        }

        // 3. Normal 이동 처리
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            // 입력이 있을 때는 목표 속도로 이동
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);       
        }
        else
        {
            // 키를 뗐을 때도 자연스럽게 감속
            float targetVelocityX = Mathf.Lerp(rb.velocity.x, 0f, Time.fixedDeltaTime * deceleration);
            rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}