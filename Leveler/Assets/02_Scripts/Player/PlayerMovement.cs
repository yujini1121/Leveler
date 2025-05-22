using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
	private Vector2 moveInput;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float moveX = Input.GetAxisRaw("Horizontal");
		moveInput = new Vector2(moveX, 0f).normalized;
	}

	void FixedUpdate()
	{
		Vector2 velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
		rb.velocity = velocity;
	}
}
