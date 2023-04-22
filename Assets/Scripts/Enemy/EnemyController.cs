using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float health = 10;
    public float timeBetweenAttacks;
    public int damage;
    public float speed;
    private float rotateSpeed = 5f;

    [HideInInspector]
    public Transform player;
    Vector3 direction;

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
        Debug.Log(player.transform.position);
        Vector3 targetPos = player.transform.position;
        direction = targetPos - transform.position;
        transform.up = direction;
    }
}
