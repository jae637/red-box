using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public int hp;
    int jumpCount = 0;

    public float curShotDelay, maxShotDelay;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public GameObject playerBullet;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        jump();
        fire();
        move();
        reload();
    }

    void jump()
    {
        // double jump flag
        if (rigid.velocity.y == 0.0f)
            jumpCount = 0;

        //double jump
        if (jumpCount < 2)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpCount == 1)
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower * 3 / 4);
                else
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                jumpCount++;
            }
        }
    }

    void reload()
    {
        curShotDelay += Time.deltaTime;
    }

    // 총알 발사 로직
    void fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;
        GameObject bullet = Instantiate(playerBullet, transform.position, transform.rotation);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        int filped = spriteRenderer.flipX ? -1 : 1;
        bulletRigid.AddForce(Vector2.right * filped * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    // 이동 로직
    void move()
    {
        //
        if (Input.GetButton("Horizontal"))
        {
            if (rigid.velocity.x != 0)
                spriteRenderer.flipX = rigid.velocity.x < 0;
        }

        //stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        //range limit
        if (rigid.position.x > 8.46f || rigid.position.x < -8.46)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (rigid.position.x < 8.5f && h > 0)
        {
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
            // rigid.position = new Vector2(8.4f, rigid.position.y);
        }
        if (rigid.position.x > -8.5 && h < 0)
        {
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
            // rigid.position = new Vector2(-8.4f, rigid.position.y);
        }
        if (rigid.position.x > 8.56)
            rigid.position = new Vector2(8.56f, rigid.position.y);
        if (rigid.position.x < -8.56)
            rigid.position = new Vector2(-8.56f, rigid.position.y);
    }

    // 적 피격 액션
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnermyAI enemy = other.gameObject.GetComponent<EnermyAI>();
            onHit();
        }
    }

    //사용자 피격시
    public void onHit()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
