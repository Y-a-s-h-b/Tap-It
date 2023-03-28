using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public float stopDistance;
    public GameObject player;
    public float speed;

    // Update is called once per frame
    private void Update()
    {
        if (player.transform != null)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) > stopDistance)
            {
                this.transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.transform.position,
                    speed * Time.deltaTime
                );
            }
        }
    }
}
