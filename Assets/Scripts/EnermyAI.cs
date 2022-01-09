using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAI : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (rigid.position.x > 7.9)
            rigid.position = new Vector2(7.9f, rigid.position.y);
        if (rigid.position.x < -7.9)
            rigid.position = new Vector2(-7.9f, rigid.position.y);
    }

    void Think()
    {
        nextMove = Random.Range(-5, 5);
        // if (rigid.velocity.x == 0)
        //     Think();
        Invoke("Think", 5);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("hit");
    }
}
