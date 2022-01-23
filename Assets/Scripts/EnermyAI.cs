using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAI : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    public int nextMove;

    public int health;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Pattern();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (rigid.position.x > 7.8)
            rigid.position = new Vector2(7.8f, rigid.position.y);
        if (rigid.position.x < -7.8)
            rigid.position = new Vector2(-7.8f, rigid.position.y);
    }

    void Pattern()
    {
        nextMove = 0;
        while (true)
        {
            int a = Random.Range(-1, 2);
            if (a != 0)
            {
                nextMove = a * 5;
                break;
            }
        }

        Invoke("Pattern", 1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("hit");
        else if (other.gameObject.tag == "PlayerBullet")
        {
            PlayerBullet bullet = other.gameObject.GetComponent<PlayerBullet>();
            onHit(bullet.dmg);
        }
    }

    public void onHit(int dmg)
    {
        health -= dmg;
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("finish");
            Destroy(gameObject);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
