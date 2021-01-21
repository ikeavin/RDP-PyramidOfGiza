using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public GameObject puddle;
    bool meltIce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (meltIce == true) { 
            
        //}
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != puddle) {
            transform.localScale *= .99f;
            puddle.transform.localScale *= 1.01f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != puddle)
        {
            transform.localScale *= .99f;
            puddle.transform.localScale *= 1.01f;
        }
    }
}
