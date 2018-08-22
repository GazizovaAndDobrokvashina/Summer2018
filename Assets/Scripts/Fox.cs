using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.Events;

public class Fox : Player
{
    //находится ли игрок на земле
    [SerializeField] private bool onGround = true;

    //дополнительные жизни
    [SerializeField] private int extraLives;

    //месторасположение последнего чекпоинта
    [SerializeField] private Vector3 lastCheckpoint;

    //список враов в триггере игрока
    private List<Enemy> enemyes;

    //таймер полученного бонуса
    [SerializeField] private float timerBonus;

    //дефолтная скорость
    [SerializeField] private float defaultSpeed;

    //сколько прыжков может совершить игрок
    [SerializeField] private int countOfJumMax;

    //колько прыжков сделал игрок
    [SerializeField] private int countOfJump;

    //событие
    [SerializeField] private UnityEvent _unityEvent;

    [SerializeField] private bool isPressedJump;

    public Animator anim;

    private Transform model;
    private Transform camera;
    public GameObject partHeal;
    private void Start()
    {
        model = transform.GetChild(0);
        camera = transform.GetChild(1);
        
        //здоровье
        maxHealth = 100f;
        health = 100f;
        //мана
        mana = 100f;
        maxMana = 100f;
        //скорость передвижения
        speed = 4f;
        defaultSpeed = speed;
        //сила прыжка
        forceForJump = 6f;
        //rigidbody игрока
        _rigidbody = GetComponent<Rigidbody>();
        //по молчанию одна дополнительная жизнь
        extraLives = 2;
        //инициализация списка врагов в триггере игрока
        enemyes = new List<Enemy>();
        //максимальное количество прыжков
        countOfJumMax = 1;
        countOfJump = 0;



        if (_unityEvent == null)
            _unityEvent = new UnityEvent();

        isPressedJump = false;
        
        
    }

    public void StartSpells()
    {
        
        spells = AllSpells.StarterSpellsForFox();

        currentHealSpell = spells[0];
        currentFirstDamageSpell = spells[1];
        currentSecondDamageSpell = spells[2];
        currentSecondDamageSpell = spells[2];
    }

    private void FixedUpdate()
    {
        //если мана не полная, то восстанавливаем её
        if (mana < maxMana)
            mana += Time.deltaTime;

        //если мана вышла за пределы максимального значения, то приравниваем к максимальному
        if (mana > maxMana)
            mana = maxMana;

        //если бонус ещё активен, то уменьшаем время его действия
        if (timerBonus > 0)
            timerBonus -= Time.deltaTime;

        //если время действия бонуса вышло, то прекращаем его действие
        if (timerBonus <= 0)
        {
            speed = defaultSpeed;
        }

        //перезаряка способности лечения
        if (currentCoolDownHeal > 0)
        {
            currentCoolDownHeal -= Time.deltaTime;
        }

        //перезарядка первой атакующей способности
        if (currentCoolDownFirstDamage > 0)
        {
            currentCoolDownFirstDamage -= Time.deltaTime;
        }

        //перезарядка первой атакующей способности
        if (currentCoolDownSecondDamage > 0)
        {
            currentCoolDownSecondDamage -= Time.deltaTime;
        }

        //движение вперед
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            anim.SetFloat("speed", 1f);
        }


        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("speed", 0f);
        }
        
        //движение назад (скорость снижена вдвое)
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed / 2 * Time.deltaTime;
            anim.SetFloat("speed", 1f);
        }


        //движение влево
        if (Input.GetKey(KeyCode.A))
        {
           // transform.position -= transform.right * speed * Time.deltaTime;
            anim.SetFloat("speed", 1f);
            transform.Rotate(Vector3.down);
        }


        //движение вправо
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("speed", 1f);
            //transform.position += transform.right * speed * Time.deltaTime;
            transform.Rotate(-Vector3.down);
            
        }


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
        if (Input.GetKeyDown(KeyCode.Space) && countOfJumMax > countOfJump && !isPressedJump)
        {
            isPressedJump = true;
            //onGround = false;
            Jump();
        }

        if (!Input.GetKey(KeyCode.Space))
            isPressedJump = false;
    }

    private IEnumerator WaitHealAnimation()
    {
        yield return new WaitUntil(()=> !partHeal.GetComponent<Animation>().isPlaying);
        partHeal.SetActive(false);
    }

    //если игрок коснулся, то отмечаем, что он на земле
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            countOfJump = 0;
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
                    partHeal.SetActive(true);
                    StartCoroutine(WaitHealAnimation());
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
        //onGround = false;
        anim.SetBool("isJump", true);
        countOfJump++;
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
            _unityEvent.Invoke();
        }
    }

    //сбросить параметры лисы к начальным
    public void RestartFox(Vector3 statrLevel)
    {
        health = maxHealth;
        mana = maxMana;
        extraLives = 2;
        currentCoolDownHeal = 0;
        currentCoolDownFirstDamage = 0;
        currentCoolDownSecondDamage = 0;
        transform.position = statrLevel;
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

    //сохранить месторасположение последнего чекпоинта
    public void SaveLastCheckPoint(Vector3 position)
    {
        lastCheckpoint = position;
    }

    //добавить дополнительную жизнь
    public void AddExtraLive()
    {
        extraLives++;
    }

    //добавляем персонажу скорости, убираем двойной прыжок
    public void AddBonusSpeed(float value, float timer)
    {
        speed += value;
        if (timerBonus > 0)
        {
            countOfJumMax = 1;
        }

        timerBonus = timer;
    }

    //добавляем возможность двойного прыжка, убираем доп скорость
    public void AddBonusJump(float value, float timer)
    {
        countOfJumMax = (int) value;
        if (timerBonus > 0)
        {
            speed = defaultSpeed;
        }

        timerBonus = timer;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    public List<Spell> Spells
    {
        get { return spells; }
    }

    public void SetDamageSpell(int number, int id)
    {
        foreach (Spell spell in spells)
        {
            if (spell.Id == id)
            {
                if (number == 1)
                {
                    currentFirstDamageSpell = spell;
                }
                else
                {
                    currentSecondDamageSpell = spell;
                }

                //Debug.Log(currentFirstDamageSpell.NameOfSpell);
                break;
            }
        }
    }

    public float CurrentCoolDownHeal
    {
        get { return currentCoolDownHeal; }
    }

    public float CurrentCoolDownFirstDamage
    {
        get { return currentCoolDownFirstDamage; }
    }

    public float CurrentCoolDownSecondDamage
    {
        get { return currentCoolDownSecondDamage; }
    }

    public Spell CurrentHealSpell
    {
        get { return currentHealSpell; }
    }

    public Spell CurrentFirstDamageSpell
    {
        get { return currentFirstDamageSpell; }
    }

    public Spell CurrentSecondDamageSpell
    {
        get { return currentSecondDamageSpell; }
    }
}