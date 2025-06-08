using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 5f;

    [Header("점프")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private PlayerSoundManager soundManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        soundManager = GetComponent<PlayerSoundManager>();
    }

    void Update()
    {
        // 이동 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0f).normalized;

        // 바닥 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        #region 사운드 재생
        // 점프 키 누르면 점프 사운드 재생
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundManager.PlayJump();
        }

        // 공격 키 눌렀을 때 공격1 사운드
        if (Input.GetKeyDown(KeyCode.F))
        {
            soundManager.PlayAttack1();
        }

        // 왼쪽 쉬프트 누르면 방어 사운드
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            soundManager.PlayDefense();
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded)
        {
            soundManager.PlayWalk();
        }
        else
        {
            soundManager.StopWalk();
        }
        #endregion
    }

    void FixedUpdate()
    {
        // 수평 이동
        Vector2 velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = velocity;
    }
}
