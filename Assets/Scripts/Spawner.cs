using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float width = 10f;
    [SerializeField] private float height = 5f;
    [SerializeField] private GameObject tile;
    public float delay = 1f;
    //[SerializeField] private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawner();
    }
   void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (checkForempty())
        {
            spawnUntil();
        }
        /*timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            DestroyImmediate(tile, checkforempty());
        }*/

    }
    void spawner()
    {
        foreach (Transform child in transform)
        {
            GameObject tiles = Instantiate(tile, child.position, Quaternion.identity);
            tiles.transform.parent = child;
        }
    }
    void spawnUntil()
    {
        Transform position = freePosition();
        if (position)
        {
            GameObject tiles = Instantiate(tile,position.transform.position, Quaternion.identity);
            tiles.transform.parent = position;
        }
        if (freePosition())
        {
            Invoke("spawnUntil",delay);
        }
    }
    
    bool checkForempty()
    {
        foreach (Transform child in transform)
        {
            if(child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
    Transform freePosition() {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                return child;
            }
        }
        return null;
    }
}
