using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Ghost : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer sprite;

    public float LifeTime;

    public float speed;

    Vector2 inputVec;

    public GameObject deathEffect;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(co_DieSlow());
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

    }

    IEnumerator co_DieSlow()
    {
        float timeLeft = LifeTime;

        while(timeLeft >= 0)
        {
            sprite.color = new Color(1, 1, 1, (timeLeft+0.3f) / LifeTime);
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        SoulDeath();
    }

    public void SoulDeath()
    {
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 2.5f);
        SoundManager.Inst.Play("SoulHit");
        GameManager.Inst.Revive();
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb2D.velocity = inputVec * speed;
    }
    void LateUpdate()
    {
        if (inputVec.x != 0)
            sprite.flipX = inputVec.x < 0;
    }

}
