using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePea : MonoBehaviour
{
    public Vector3 hitPoint;
    public GameObject explode;
    public GameObject splash;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>()
            .AddForce((hitPoint - this.transform.position).normalized * speed);
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().health -= 5f;
            GameObject blood = Instantiate(
                splash,
                this.transform.position,
                this.transform.rotation
            );
            blood.transform.parent = col.transform;
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(explode, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
