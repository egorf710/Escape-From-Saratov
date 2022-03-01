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
    [SerializeField] private float maxEnergy = 20;
    [SerializeField] float _energy;
    [SerializeField] private Image energyIndicatorInHUD;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        _energy = maxEnergy;
    }

    private void Update()
    {
        // передвижение
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        // уменьшении энергии
        if(_energy > 0)
        {
            _energy -= Time.deltaTime;
            energyIndicatorInHUD.fillAmount = _energy / maxEnergy;
        }
        else
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }

    // ¬ случае проигрыша всю логику писать тута!!
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "soap")
        {
            _energy = Mathf.Clamp(_energy + 5, 0, maxEnergy);
            Destroy(collision.gameObject);
        }
    }
}
