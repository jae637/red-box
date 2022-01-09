using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    int jumpCount = 0;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
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
        if (rigid.position.x > 8.56f || rigid.position.x < -8.56)
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
}
