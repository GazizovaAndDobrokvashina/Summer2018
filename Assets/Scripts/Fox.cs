using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Fox : Player
{
    //находится ли игрок на земле
    [SerializeField] private bool onGround = true;

    //дополнительные жизни
    [SerializeField] private int extraLives;
    
    //месторасположение последнего чекпоинта
    [SerializeField] private Vector3 lastCheckpoint;

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
        extraLives = 2;
        //инициализация списка врагов в триггере игрока
        enemyes = new List<Enemy>();

        //ТЕСТОВЫЕ ДАННЫЕ
        spells = new Spell[3];
        spells[0] = new Spell("HEAL", 1f, 20f, 10f);
        spells[1] = new Spell("ALLDAMAGE", 4f, 20f, 20f);
        spells[2] = new Spell("SINGLEDAMAGE", 3f, 15f, 15f);

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
            //UseHealSpell();
            UseSpell(currentHealSpell, 0);
        }

        //использования скила первой атаки
        if (Input.GetKeyDown(KeyCode.E))
        {
            //UseFirstDamgeSpell();
            UseSpell(currentFirstDamageSpell, 1);
        }

        //использование скила второй атаки
        if (Input.GetKeyDown(KeyCode.F))
        {
            //UseSecondDamgeSpell();
            UseSpell(currentSecondDamageSpell, 2);
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
           // Debug.Log("I add! " + enemyes.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyes.Remove(other.gameObject.GetComponent<Enemy>());
           // Debug.Log("I remove! " + enemyes.Count);
        }
    }

    protected override void UseSpell(Spell spell, int numberOfAttackSpell)
    {
        //проверяем тип способности
        switch (spell.Type)
        {
            //если это лечение, то применяем на игрока
            case "HEAL":
                //если способность перезаряжена, маны достаточно и здоровье не полное, то применяем
                if (currentCoolDownHeal <= 0 && mana >= spell.ManaValue && health < maxHealth)
                {
                    mana -= spell.ManaValue;
                    currentCoolDownHeal = spell.Cooldown;
                    health += spell.Value;
                    if (health > maxHealth)
                        health = maxHealth;
                    Debug.Log("Heal");
                }
                else
                {
                    Debug.Log("Can't use heal");
                }

                break;

            //Если тип способности "атакующая по всем врагам"
            case "ALLDAMAGE":

                //если она сохранена на первый спелл игрока или на второй спелл  и достаточно маны, в радиусе поражения есть враги
                if ((numberOfAttackSpell == 1 && currentCoolDownFirstDamage <= 0 ||
                     numberOfAttackSpell == 2 && currentCoolDownSecondDamage <= 0) && mana >= spell.ManaValue &&
                    enemyes.Count > 0)
                {
                    //тратим ману
                    mana -= spell.ManaValue;
                    //наносим урон всем врагам
                    foreach (Enemy enemy in enemyes)
                    {
                        enemy.TakeDamage(spell.Value);
                    }

                    //отправляем на перезарядку использованный скилл
                    if (numberOfAttackSpell == 1)
                    {
                        currentCoolDownFirstDamage = spell.Cooldown;
                        Debug.Log("Used 1 attack");
                    }
                    else
                    {
                        currentCoolDownSecondDamage = spell.Cooldown;
                        Debug.Log("Used 2 attack");
                    }
                }
                else
                {
                    Debug.Log("Can't use all attack");
                }

                break;

            //Если тип способности "атакующая по одному врагу"  
            case "SINGLEDAMAGE":

                //если она сохранена на первый спелл игрока или на второй спелл  и достаточно маны, в радиусе поражения есть враги
                if ((numberOfAttackSpell == 1 && currentCoolDownFirstDamage <= 0 ||
                     numberOfAttackSpell == 2 && currentCoolDownSecondDamage <= 0) && mana >= spell.ManaValue &&
                    enemyes.Count > 0)
                {
                    //тратим ману
                    mana -= spell.ManaValue;

                    //наносим урон врагу
                    if (enemyes.Count == 1)
                    {
                        enemyes[0].TakeDamage(spell.Value);
                    }
                    else
                    {
                        Enemy nearEnemy = FindNearlierEnemy();
                        if (nearEnemy != null)
                        {
                            nearEnemy.TakeDamage(spell.Value);
                        }
                    }

                    //отправляем на перезарядку использованный скилл
                    if (numberOfAttackSpell == 1)
                    {
                        currentCoolDownSecondDamage = spell.Cooldown;
                        Debug.Log("Used 1 attack");
                    }
                    else
                    {
                        currentCoolDownSecondDamage = spell.Cooldown;
                        Debug.Log("Used 2 attack");
                    }
                }
                else
                {
                    Debug.Log("Can't use single attack");
                }

                break;
        }
    }


    private Enemy FindNearlierEnemy()
    {
        float distance;
        Enemy nearlierEnemy = null;
        if (enemyes.Count != 0)
        {
            distance = Vector3.Distance(transform.position, enemyes[0].gameObject.transform.position);
            nearlierEnemy = enemyes[0];

            foreach (Enemy enemy in enemyes)
            {
                if (distance > Vector3.Distance(transform.position, enemy.gameObject.transform.position))
                {
                    distance = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                    nearlierEnemy = enemy;
                }
            }
        }

        return nearlierEnemy;
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
        //если игрок погиб и имеет дополнительные жизни, то начинает с чекпоинта; 
        if (extraLives > 0)
        {
            extraLives--;
            transform.position = lastCheckpoint;

        }
        else
        {
            //предложить игроку начать уровень заново (все объекты возвращаются на свои места) либо вернуться в главное меню
        }
    }

    public float Health
    {
        get { return health; }
    }

    public float Mana
    {
        get { return mana; }
    }

    public int ExtraLives
    {
        get { return extraLives; }
    }

    public void SaveLastCheckPoint(Vector3 position)
    {
        lastCheckpoint = position;
    }
}