using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [Header("Куда будет идти")]
    public Vector2 toMoveVector;
    [Header("Минимальное и максимальное время того\nсколько он буде ждать перед тем как идти")]
    public Vector2 timeRate;
    [Header("Дуга направления")]
    public Transform duga;
    [Header("Скорость")]
    public float speed = 1f;
    float curspeed;
    [Header("Components")]
    public Animator anim;
    public Rigidbody2D rb;
    [Header("Разница во времени того сколько он стоит и сколько идёт, чем больше тем темньше он ходит и больше стоит")]
    [Range(1, 5f)]
    public float baffTime;
    PoliceBrain policeBrain;

    private void Start()
    {
        policeBrain = GetComponent<PoliceBrain>();
        curspeed = speed;
        StartCoroutine(UpdateVector());
    }
    private void Update()
    {
        if (policeBrain.inDialog)
        {
            curspeed = 0;
        }
        rb.velocity = duga.right * curspeed * Time.deltaTime;
        anim.SetFloat("x", rb.velocity.x);
        anim.SetFloat("y", rb.velocity.y);
    }
    IEnumerator UpdateVector()
    {
        while (true)
        {
            if (!policeBrain.inDialog)
            {
                curspeed = speed;
                toMoveVector = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
                float angle = Mathf.Atan2(toMoveVector.y - transform.position.y, toMoveVector.x - transform.position.x) * Mathf.Rad2Deg;
                duga.transform.rotation = Quaternion.Euler(0, 0, angle);
                yield return new WaitForSeconds(Random.Range(timeRate.x, timeRate.y) / baffTime);
                curspeed = 0;
            }
            yield return new WaitForSeconds(Random.Range(timeRate.x, timeRate.y) * baffTime);
        }
    }
}
