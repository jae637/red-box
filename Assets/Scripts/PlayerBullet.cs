using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int dmg;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnermyAI enemy = other.gameObject.GetComponent<EnermyAI>();
            enemy.onHit(dmg);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
    }
}
