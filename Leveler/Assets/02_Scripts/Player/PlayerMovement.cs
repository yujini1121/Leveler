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
        Debug.Log(isGrounded);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            soundManager?.PlayJump();
        }

        // 수평 이동
        Vector2 velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = velocity;

        #region 사운드 재생
        // 공격 키 눌렀을 때 공격1 사운드
        if (Input.GetKeyDown(KeyCode.F))
        {
            soundManager?.PlayAttack1(); // 항상 재생됨
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            soundManager?.PlayDefense(); // 항상 재생됨
        }

        if (isGrounded && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            soundManager?.PlayWalk(); // 걷는 중이면 걷는 소리
        }
        else
        {
            soundManager?.StopWalk();
        }
        #endregion
    }
}
