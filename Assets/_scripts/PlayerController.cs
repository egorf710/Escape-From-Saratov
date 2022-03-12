using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Vector2 animVec;

    [Header("energy")]
    [SerializeField] private float energy_Max = 20;
    [SerializeField] public float energy_Current;
    [SerializeField] private Image energy_IndicatorInHUD;
    [SerializeField] private Color energy_Color;

    [Header("life time")]
    [SerializeField] private float lifeTime_Max = 40;
    [SerializeField] public float lifeTime_Current;
    [SerializeField] private Image lifeTime_IndicatorInHUD;
    [SerializeField] private Color lifeTime_Color;

    [Header("state")]
    [SerializeField] private Player_state state_police;
    [SerializeField] private Player_state state_criminal;
    [SerializeField] private Player_state state_me;
    [SerializeField] private Player_state state_current;

    [Header("player sprite")]
    public SpriteRenderer sprite;
    //public Color spriteColorInCriminal;
    //public Color spriteColorInPolice;
    [Header("player health sprite")]
    public Sprite[] emoji_sprites;
    public Image emoji_image;
    [Header("OtherComponents")]
    [SerializeField] private Animator player_animator;
    private PickUpItem pickUpItem;
    public FightSystem fightSystem;
    private InventoryManager invManager;

    bool lowhp;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pickUpItem = GetComponent<PickUpItem>();
        invManager = FindObjectOfType<InventoryManager>();

        energy_Current = energy_Max;
        lifeTime_Current = lifeTime_Max;

        lifeTime_Color = lifeTime_IndicatorInHUD.color;
    }

    private void Update()
    {
        if (fightSystem.fight) { return; }//что бы во время боя не ходил
        // передвижение
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        // уменьшении энергии
        //if(energy_Current > 0 && moveInput.magnitude != 0)
        //{
        //    energy_Current -= Time.deltaTime;
        //    energy_IndicatorInHUD.fillAmount = energy_Current / energy_Max;
        //}
        // время жизни
        if(lifeTime_Current > 0)
        {
            if(lifeTime_Current > lifeTime_Max) { lifeTime_Current = lifeTime_Max; }
            lifeTime_Current -= Time.deltaTime * 0.5f;
            lifeTime_IndicatorInHUD.fillAmount = lifeTime_Current / lifeTime_Max;
            if(lifeTime_Current > 32)
            {
                emoji_image.sprite = emoji_sprites[0];
            }
            else if(lifeTime_Current > 24)
            {
                emoji_image.sprite = emoji_sprites[1];
            }
            else if(lifeTime_Current > 16)
            {
                emoji_image.sprite = emoji_sprites[2];
            }
            else if(lifeTime_Current > 8)
            {
                emoji_image.sprite = emoji_sprites[3];
            }
            else
            {
                emoji_image.sprite = emoji_sprites[4];
            }
        }
        UpdateAnimator(); // обновляет анимацию у перса по его вектору движения
        // эта строка нужна чтобы если есть какая-то логика в "состоянии" то она выполнялась в Update т.к. Player_state не насладует MonoBehaviour
        if (state_current) state_current.Run();
        // смена состояния (полицейский / преступник)
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(state_current.state_name == "criminal" && FindObjectOfType<InventoryManager>().FindItem("policeForm") > 0)
            {
                player_animator.runtimeAnimatorController = FindObjectOfType<GameComponents>().anim_policeForm;
                FindObjectOfType<InventoryManager>().RemoveItem("policeForm", 1);
                state_set(state_police);
                return;
            }
            if(state_current.state_name == "police" && FindObjectOfType<InventoryManager>().FindItem("zekForm") > 0)
            {
                player_animator.runtimeAnimatorController = FindObjectOfType<GameComponents>().anim_zekForm;
                FindObjectOfType<InventoryManager>().RemoveItem("zekForm", 1);
                state_set(state_criminal);
                return;
            }
            player_animator.runtimeAnimatorController = FindObjectOfType<GameComponents>().anim_meForm;
            state_set(state_me);
        }
        // инвентарь
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpItem.PickUp();
        }
    }
    private void UpdateAnimator()
    {
        animVec = moveInput;
        animVec.x = (float)Math.Round(animVec.x, 1);
        animVec.y = (float)Math.Round(animVec.y, 1);
        if (Mathf.Abs(animVec.x) >= Mathf.Abs(animVec.y))
        {
            animVec.y = 0;
        }
        if (Mathf.Abs(animVec.y) >= Mathf.Abs(animVec.x))
        {
            animVec.x = 0;
        }
        player_animator.SetFloat("x", animVec.x);
        player_animator.SetFloat("y", animVec.y);
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
    
    /// <summary>
    /// Функция для смены состояния у игрока
    /// </summary>
    /// <param name="state">состояние на которое сменится state_current</param>
    public void state_set(Player_state state) 
    {
        Debug.Log(state.name);

        state_current = Instantiate(state);
        state_current._player = this;
        state_current.Init();
    }

    // В случае проигрыша всю логику писать тута!!
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Door>(out Door _door))
        {
            if(invManager.FindItem(_door.keyForUnlock.name) > 0)
            {
                _door.gameObject.SetActive(false);
                invManager.RemoveItem(_door.keyForUnlock);
            }
        }
    }
    public void UpdateIndicatorsUI()
    {
        lifeTime_IndicatorInHUD.fillAmount = lifeTime_Current / lifeTime_Max;
        energy_IndicatorInHUD.fillAmount = energy_Current / energy_Max;
    }
}
