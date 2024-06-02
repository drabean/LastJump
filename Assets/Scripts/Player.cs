using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    float jumpGravity = 1f;
    [SerializeField]
    float fallGravity = 4f;   

    float inputValue;

    [SerializeField] Transform[] groundCheckPoints;
    Rigidbody2D rb2D;
    SpriteRenderer sprite;
    Animator anim;
    HitModule hit;

    [SerializeField]
    LayerMask groundLayer;


    #region 상태변수
    bool isGround;
    bool isIdle = true;
    float lastGroundTime;

    bool canJump;

    #endregion

    public GameObject deathEffect;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hit = GetComponent<HitModule>();
    }

    void FixedUpdate()
    {
        //좌우 이동 처리
        rb2D.velocityX = inputValue * speed;

        if (rb2D.velocityY > 0) rb2D.gravityScale = jumpGravity;
        else rb2D.gravityScale = fallGravity;

        checkGround();

        syncAnimator();
    }



    void LateUpdate()
    {
        if (inputValue != 0)
            sprite.flipX = inputValue < 0;
    }

    #region PlayerInput 함수

    void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>().x;
        isIdle = (inputValue == 0);
    }

    float coyoteTime = 0.1f;
    void OnJump()
    {
        if (!canJump) return;
        SoundManager.Inst.Play("Jump");
        rb2D.velocityY = 0;
        rb2D.AddForceY(jumpPower, ForceMode2D.Impulse);
        //SendMessage("SetVertical", power);
    }

    public void Revive()
    {
        hit.FlashWhite(0.5f);
        SoundManager.Inst.Play("Revive");
        rb2D.AddForceY(jumpPower, ForceMode2D.Impulse);
    }
    bool isDead;

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        SoundManager.Inst.Play("SkulHit");
        GameManager.Inst.SpawnGhost(transform.position);
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 2.5f);
        Destroy(gameObject);
    }


    #endregion



    void checkGround()
    {
        isGround = Physics2D.CircleCast(transform.position + Vector3.down * 0.41f, 0.2f, Vector2.zero, 0.05f, groundLayer);

        bool groundLeft = Physics2D.CircleCast(groundCheckPoints[0].position, 0.15f, Vector2.zero, 0.05f, groundLayer);
        bool groundRight = Physics2D.CircleCast(groundCheckPoints[1].position, 0.15f, Vector2.zero, 0.05f, groundLayer);

        isGround = groundLeft || groundRight;
        if (isGround) lastGroundTime = Time.time;

        if (Time.time - lastGroundTime < coyoteTime) canJump = true;
        else canJump = false;



    }
    void syncAnimator()
    {
        anim.SetBool("isGround", isGround);
        anim.SetBool("isIdle", isIdle);

        anim.SetFloat("VelocityX", rb2D.velocityX);
        anim.SetFloat("VelocityY", rb2D.velocityY);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoints[0].position, 0.15f);
        Gizmos.DrawWireSphere(groundCheckPoints[1].position, 0.15f);
    }

}

