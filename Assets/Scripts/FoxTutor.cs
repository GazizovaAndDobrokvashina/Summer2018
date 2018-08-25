using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTutor : MonoBehaviour
{
    //скорость передвижения
    [SerializeField] protected float speed;

    //сколько прыжков может совершить игрок
    [SerializeField] private int countOfJumMax;

    //Сколько прыжков сделал игрок
    [SerializeField] private int countOfJump;

    //нажата ли клавиша прыжка
    [SerializeField] private bool isPressedJump;

    //аниматор игрока
    public Animator anim;

    //рига персонажа
    private Rigidbody _rigidbody;

    //сила прыжка
    [SerializeField] protected float forceForJump;

    //загрузка лисы
    private void Start()
    {
        //скорость передвижения
        speed = 4f;

        //сила прыжка
        forceForJump = 6f;

        //rigidbody игрока
        _rigidbody = GetComponent<Rigidbody>();

        //максимальное количество прыжков
        countOfJumMax = 1;
        countOfJump = 0;

        //сброс нажатых клавиш
        isPressedJump = false;
    }

    private void FixedUpdate()
    {
        //движение вперед
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            anim.SetFloat("speed", 1f);
        }

        //стоим на месте
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("speed", 0f);
        }

        //движение назад (скорость снижена вдвое)
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed / 4 * Time.deltaTime;
            anim.SetFloat("speed", -1f);
        }


        //движение влево
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.down);
        }


        //движение вправо
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-Vector3.down);
        }

        //eсли поворот на месте
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) &&
            (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            anim.SetFloat("speed", -0.3f);
        }

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && countOfJumMax > countOfJump && !isPressedJump)
        {
            isPressedJump = true;
            Jump();
        }

        //если кнопка не нажата, сбрасываем булин
        if (!Input.GetKey(KeyCode.Space))
            isPressedJump = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        //если игрок коснулся земли, то сбрасываем прыжки 
        if (other.collider.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            countOfJump = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }

    //прыжок
    private void Jump()
    {
        anim.SetBool("isJump", true);
        countOfJump++;
        _rigidbody.AddForce(transform.up * forceForJump, ForceMode.Impulse);
    }
}