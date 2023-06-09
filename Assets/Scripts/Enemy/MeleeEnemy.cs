using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public float stopDistance;

    //public GameObject player;

    private float attackTime;
    public float attackSpeed;

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            gameObject.transform.LookAt(player);
            if (Vector3.Distance(this.transform.position, player.position) > stopDistance)
            {
                this.transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );
            }
            else if (Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Movement>().TakeDamage(damage);

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
