using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�̵�")]
    public float moveSpeed = 5f;

    [Header("����")]
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
        // �̵� �Է�
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, 0f).normalized;

        // �ٴ� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ���� �Է�
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        #region ���� ���
        // ���� Ű ������ ���� ���� ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundManager.PlayJump();
        }

        // ���� Ű ������ �� ����1 ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            soundManager.PlayAttack1();
        }

        // ���� ����Ʈ ������ ��� ����
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
        // ���� �̵�
        Vector2 velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = velocity;
    }
}
