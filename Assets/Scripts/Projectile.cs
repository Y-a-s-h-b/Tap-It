using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    private float angle;
    private float ang;
    private Vector2 directionVec;
    public int damage;

    // Start is called before the first frame update
    public void SetAngle(float anglep)
    {
        ang = anglep;
        Debug.Log(ang);
    }

    void Start()
    {
        directionVec = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionVec * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
