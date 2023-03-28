using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float delay;
    private float shotTime;
    public Sprite[] spriteList;
    private float index;
    private int spriteCount = 8;

    //public GameObject shotPointObject;
    public float ang;
    public Projectile pea;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float ang = Mathf.Atan2(direction.y, direction.x);

        //Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        //shotPointObject.rotation = rotation;

        if (angle < 0)
            angle += 360;
        index = (Mathf.Floor(angle / (360 / spriteCount)));
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[(int)index];
        float shotX = origin.x + (0.6f * Mathf.Cos(ang));
        float shotY = origin.y + (0.6f * Mathf.Sin(ang));

        Vector2 shotPointVec = new Vector2(shotX, shotY);
        //(origin.y + (1.6 * Mathf.Sin(angle)))))

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > shotTime)
            {
                Projectile peaO = Instantiate(pea, shotPointVec, transform.rotation);
                peaO.SetAngle(ang);
                shotTime = Time.time + delay;
            }
        }
    }
}
