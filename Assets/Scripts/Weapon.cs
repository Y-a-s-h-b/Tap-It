using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Weapon : MonoBehaviour
{
    public float cooldownSpeed;
    public float fireRate;
    public float recoilCooldown;
    private float accuracy;
    public float maxSpreadAngle;
    public float timetillMaxSpread;
    public GameObject projectile;
    public GameObject bulletPoint;
    public AudioSource peashot;
    public AudioClip singleShot;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        cooldownSpeed += Time.deltaTime * 60f;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("else");
            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Debug.Log("if");
                Shoot();
                peashot.PlayOneShot(singleShot);
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }
        }
        else
        {
            recoilCooldown -= Time.deltaTime;
            if (recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(transform.position, bulletPoint.transform.forward);
        RaycastHit hit;
        Quaternion peaRotation = Quaternion.LookRotation(transform.forward);
        float currentSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timetillMaxSpread);
        peaRotation = Quaternion.RotateTowards(
            peaRotation,
            UnityEngine.Random.rotation,
            UnityEngine.Random.Range(0.0f, currentSpread)
        );
        if (
            Physics.Raycast(
                transform.position,
                peaRotation * Vector3.forward,
                out hit,
                Mathf.Infinity
            )
        )
        {
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.blue, 1);
            GameObject pea = Instantiate(projectile, bulletPoint.transform.position, peaRotation);
            pea.GetComponent<MovePea>().hitPoint = hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.blue, 1);
            GameObject pea = Instantiate(
                projectile,
                bulletPoint.transform.position,
                Quaternion.identity
            );
        }
    }
}
