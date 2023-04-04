using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : EnemyController
{
    public float minX;
    public float maxY;
    public float minY;
    public float maxX;

    private Vector2 targetPosition;
    private Animator anim;
    private float summonTime;
    public EnemyController enemyToSummon;
    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                );
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenAttacks;
                    anim.SetTrigger("summon");
                }
            }
            if (Vector2.Distance(this.transform.position, player.position) < stopDistance)
            {
                this.transform.position = Vector2.MoveTowards(
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

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Movement>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
