using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	// Ускорение игрока
	[Header("Player velocity")]
	// Ось Ox
	public int xVelocity = 3;
	// Ось Oy
	public int yVelocity = 3;

	[SerializeField] private LayerMask ground;

	private bool isFacingRight = true;

	// Физическое поведение (тело) объекта
	private Rigidbody2D rigidBody;
	// Коллайдер, проверка на столкновения
	private Collider2D coll;

	public Transform target;

	float leftEdge, rightEdge;

	public Animator animator;

	private void Start()
	{
		// Получаем доступ к Rigidbody2D объекта Player
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		coll = gameObject.GetComponent<Collider2D>();

		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		

		updatePlayerPosition();

		// Получаем границы камеры по ординате Х
		leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

		// Телепортируем если за границами камеры
		if (transform.position.x < leftEdge)
			transform.position = new Vector3(rightEdge, transform.position.y, transform.position.z);
		else if (transform.position.x > rightEdge)
			transform.position = new Vector3(leftEdge, transform.position.y, transform.position.z);
	}

	// Обновляем местоположение игрока
	private void updatePlayerPosition()
	{
		// Получаем значение ввода горизонтального перемещение
		float moveInput = Input.GetAxis("Horizontal");

		// Значения xVelocity, yVelocity можно задать через инспектор
		if (moveInput < 0)
		{ // Движ влево
			rigidBody.velocity = new Vector2(-xVelocity, rigidBody.velocity.y);
		}
		else if (moveInput > 0)
		{ // Движ вправо
			rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
		}
		else if (moveInput == 0)
		{
			rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
		}
		else if (coll.IsTouchingLayers(ground))
		{
			rigidBody.velocity = Vector2.zero; // Лично меня дико бесит инерция вбок при приземлении персонажа, отключаем
		}

		if (coll.IsTouchingLayers(ground))
		{ //Тип прыгает, если стоит на земле
			animator.SetFloat("Speed", Mathf.Abs(ground));

			rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);
		}
		if (moveInput > 0 && !isFacingRight)
			//отражаем персонажа вправо
			Flip();
		//обратная ситуация. отражаем персонажа влево
		else if (moveInput < 0 && isFacingRight)
			Flip();

	
	}

	private void Flip()
	{
		//меняем направление движения персонажа
		isFacingRight = !isFacingRight;
		transform.Rotate(0f, 180f, 0f);
	}

	
}

//
//if (rigidbody.transform.position.x < -3.5)
//	rigidbody.transform.position = new vector2(transform.position.x + 20f, transform.position.y);
//else if (transform.position.x > 3.5)
//	rigidbody.transform.position = new vector2(transform.position.x - 20f, transform.position.y);