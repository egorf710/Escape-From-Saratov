using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

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

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        energy_Current = energy_Max;
    }

    private void Update()
    {
        // передвижение
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        // уменьшении энергии
        if(energy_Current > 0)
        {
            energy_Current -= Time.deltaTime;
            energy_IndicatorInHUD.fillAmount = energy_Current / energy_Max;
        }
        else
        {
            GameOver();
        }

        // эта строка нужна чтобы если есть какая-то логика в "состоянии" то она выполнялась в Update т.к. Player_state не насладует MonoBehaviour
        if(state_current) state_current.Run();
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
            energy_Current = Mathf.Clamp(energy_Current + 5, 0, energy_Max);
            Destroy(collision.gameObject);
        }
    }
}
