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
    [SerializeField] float energy_Current;
    [SerializeField] private Image energy_IndicatorInHUD;

    [Header("state")]
    [SerializeField] private Player_state state_police;
    [SerializeField] private Player_state state_criminal;
    [SerializeField] private Player_state state_current;

    [Header("player sprite")]
    public SpriteRenderer sprite;
    public Color spriteColorInCriminal;
    public Color spriteColorInPolice;

    [Header("OtherComponents")]
    [SerializeField] private Animator player_animator;
    private PickUpItem pickUpItem;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pickUpItem = GetComponent<PickUpItem>();

        energy_Current = energy_Max;
    }

    private void Update()
    {
        // передвижение
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        // уменьшении энергии
        if(energy_Current > 0 && moveInput.magnitude != 0)
        {
            energy_Current -= Time.deltaTime;
            energy_IndicatorInHUD.fillAmount = energy_Current / energy_Max;
        }
        UpdateAnimator(); // обновляет анимацию у перса по его вектору движения
        // эта строка нужна чтобы если есть какая-то логика в "состоянии" то она выполнялась в Update т.к. Player_state не насладует MonoBehaviour
        if (state_current) state_current.Run();
        // смена состояния (полицейский / преступник)
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(state_current.state_name == "criminal")
            {
                state_set(state_police);
            }
            else
            {
                state_set(state_criminal);
            }
        }
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
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "soap")
        {
            return;
            energy_Current = Mathf.Clamp(energy_Current + 5, 0, energy_Max);
            Destroy(collision.gameObject);
        }
    }
}
