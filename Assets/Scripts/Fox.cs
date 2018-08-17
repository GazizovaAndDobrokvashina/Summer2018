using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Player
{
    //находится ли игрок на земле
    [SerializeField] private bool onGround = true;

    //дополнительные жизни
    [SerializeField] private int extraLives;

    private List<Enemy> enemyes;

    private void Start()
    {
        //здоровье
        maxHealth = 100f;
        health = 100f;
        //мана
        mana = 100f;
        maxMana = 100f;
        //скорость передвижения
        speed = 15f;
        //сила прыжка
        forceForJump = 6f;
        //rigidbody игрока
        _rigidbody = GetComponent<Rigidbody>();
        //по молчанию одна дополнительная жизнь
        extraLives = 1;
        //инициализация списка врагов в триггере игрока
        enemyes = new List<Enemy>();

        //ТЕСТОВЫЕ ДАННЫЕ
        spells = new Spell[3];
        spells[0] = new Spell("Heal", 1f, 20f, 10f);
        spells[1] = new Spell("Fire", 4f, 20f, 20f);
        spells[2] = new Spell("Lightning", 3f, 15f, 15f);

        currentHealSpell = spells[0];
        currentFirstDamageSpell = spells[1];
        currentSecondDamageSpell = spells[2];
    }

    private void FixedUpdate()
    {
        if (mana < maxMana)
            mana += Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;

        //перезаряка способности лечения
        if (currentCoolDownHeal > 0)
            currentCoolDownHeal -= Time.deltaTime;

        //перезарядка первой атакующей способности
        if (currentCoolDownFirstDamage > 0)
            currentCoolDownFirstDamage -= Time.deltaTime;

        //перезарядка первой атакующей способности
        if (currentCoolDownSecondDamage > 0)
            currentCoolDownSecondDamage -= Time.deltaTime;

        //движение вперед
        if (Input.GetKey(KeyCode.W))

            transform.position += transform.forward * speed * Time.deltaTime;

        //движение назад (скорость снижена вдвое)
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * speed / 2 * Time.deltaTime;

        //движение влево
        if (Input.GetKey(KeyCode.A))

            transform.position -= transform.right * speed * Time.deltaTime;

        //движение вправо
        if (Input.GetKey(KeyCode.D))

            transform.position += transform.right * speed * Time.deltaTime;

        //использование скилла лечения
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseHealSpell();
        }

        //использования скила первой атаки
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseFirstDamgeSpell();
        }

        //использование скила второй атаки
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseSecondDamgeSpell();
        }

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
            Jump();
    }

    //если игрок коснулся, то отмечаем, что он на земле
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyes.Add(other.gameObject.GetComponent<Enemy>());
            Debug.Log("I add! " + enemyes.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyes.Remove(other.gameObject.GetComponent<Enemy>());
            Debug.Log("I remove! " + enemyes.Count);
        }
    }

    //использование первой атакующей способности
    protected override void UseFirstDamgeSpell()
    {
        if (currentCoolDownFirstDamage <= 0 && enemyes.Count > 0 && mana >= spells[1].ManaValue)
        {
            mana -= spells[1].ManaValue;
            currentCoolDownFirstDamage = spells[1].Cooldown;
            foreach (Enemy enemy in enemyes)
            {
                enemy.TakeDamage(spells[1].Value);
            }

            Debug.Log("Damage 1");
        }
        else
        {
            Debug.Log("Cooldown Damage 1");
        }
    }

    //использование второй атакующей способности
    protected override void UseSecondDamgeSpell()
    {
        if (currentCoolDownSecondDamage <= 0 && enemyes.Count > 0 && mana >= spells[2].ManaValue)
        {
            mana -= spells[2].ManaValue;
            currentCoolDownSecondDamage = spells[2].Cooldown;
            foreach (Enemy enemy in enemyes)
            {
                enemy.TakeDamage(spells[2].Value);
            }

            Debug.Log("Damage 2");
        }
        else
        {
            Debug.Log("Cooldown Damage 2");
        }
    }

    //использование хилки
    protected override void UseHealSpell()
    {
        if (currentCoolDownHeal <= 0 && mana >= spells[0].ManaValue)
        {
            mana -= spells[0].ManaValue;
            currentCoolDownHeal = spells[0].Cooldown;
            health += spells[0].Value;
            if (health > maxHealth)
                health = maxHealth;
            Debug.Log("Heal");
        }
        else
        {
            Debug.Log("Cooldown heal");
        }
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    //прыжок
    protected override void Jump()
    {
        onGround = false;
        _rigidbody.AddForce(transform.up * forceForJump, ForceMode.Impulse);
    }

    //смерть персонажа
    protected override void Death()
    {
        //если игрок погиб и имеет дополнительные жизни, то начинает с чекпоинта; а если не имеет доп жизней, то с начала уровня
    }

    public float Health
    {
        get { return health; }
    }

    public float Mana
    {
        get { return mana; }
    }
}