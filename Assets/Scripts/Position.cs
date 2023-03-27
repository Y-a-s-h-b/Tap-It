using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
