using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	// ��������� ������
	[Header("Player velocity")]
	// ��� Ox
	public int xVelocity = 3;
	// ��� Oy
	public int yVelocity = 3;

	[SerializeField] private LayerMask ground;

	private bool isFacingRight = true;

	// ���������� ��������� (����) �������
	private Rigidbody2D rigidBody;
	// ���������, �������� �� ������������
	private Collider2D coll;

	public Transform target;

	float leftEdge, rightEdge;

	public Animator animator;

	private void Start()
	{
		// �������� ������ � Rigidbody2D ������� Player
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		coll = gameObject.GetComponent<Collider2D>();

		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		

		updatePlayerPosition();

		// �������� ������� ������ �� �������� �
		leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

		// ������������� ���� �� ��������� ������
		if (transform.position.x < leftEdge)
			transform.position = new Vector3(rightEdge, transform.position.y, transform.position.z);
		else if (transform.position.x > rightEdge)
			transform.position = new Vector3(leftEdge, transform.position.y, transform.position.z);
	}

	// ��������� �������������� ������
	private void updatePlayerPosition()
	{
		// �������� �������� ����� ��������������� �����������
		float moveInput = Input.GetAxis("Horizontal");

		// �������� xVelocity, yVelocity ����� ������ ����� ���������
		if (moveInput < 0)
		{ // ���� �����
			rigidBody.velocity = new Vector2(-xVelocity, rigidBody.velocity.y);
		}
		else if (moveInput > 0)
		{ // ���� ������
			rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
		}
		else if (moveInput == 0)
		{
			rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
		}
		else if (coll.IsTouchingLayers(ground))
		{
			rigidBody.velocity = Vector2.zero; // ����� ���� ���� ����� ������� ���� ��� ����������� ���������, ���������
		}

		if (coll.IsTouchingLayers(ground))
		{ //��� �������, ���� ����� �� �����
			animator.SetFloat("Speed", Mathf.Abs(ground));

			rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);
		}
		if (moveInput > 0 && !isFacingRight)
			//�������� ��������� ������
			Flip();
		//�������� ��������. �������� ��������� �����
		else if (moveInput < 0 && isFacingRight)
			Flip();

	
	}

	private void Flip()
	{
		//������ ����������� �������� ���������
		isFacingRight = !isFacingRight;
		transform.Rotate(0f, 180f, 0f);
	}

	
}

//
//if (rigidbody.transform.position.x < -3.5)
//	rigidbody.transform.position = new vector2(transform.position.x + 20f, transform.position.y);
//else if (transform.position.x > 3.5)
//	rigidbody.transform.position = new vector2(transform.position.x - 20f, transform.position.y);