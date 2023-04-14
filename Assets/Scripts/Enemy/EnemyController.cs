using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 10;
    public float timeBetweenAttacks;
    public int damage;
    public float speed;

    [HideInInspector]
    public Transform player;
    Vector2 direction;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector2 targetPos = player.transform.position;
        direction = targetPos - (Vector2)transform.position;
        transform.up = direction;
    }
}
