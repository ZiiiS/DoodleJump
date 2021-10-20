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

	private void Start()
	{
		// �������� ������ � Rigidbody2D ������� Player
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		coll = gameObject.GetComponent<Collider2D>();
	}

	private void Update()
	{
		updatePlayerPosition();
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
		//�������� ������� ���������
		Vector3 theScale = transform.localScale;
		//��������� �������� ��������� �� ��� �
		theScale.x *= -1;
		//������ ����� ������ ���������, ������ �������, �� ��������� ����������
		transform.localScale = theScale;
	}
}

//
//if (rigidbody.transform.position.x < -3.5)
//	rigidbody.transform.position = new vector2(transform.position.x + 20f, transform.position.y);
//else if (transform.position.x > 3.5)
//	rigidbody.transform.position = new vector2(transform.position.x - 20f, transform.position.y);