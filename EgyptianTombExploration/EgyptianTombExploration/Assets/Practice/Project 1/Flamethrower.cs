using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public bool shoot = false;
    public GameObject flame;

    // Update is called once per frame
    void Update()
    {
        if(shoot)
        {
            GameObject newFlame = Instantiate(flame, transform.position, transform.rotation);
            Destroy(newFlame, 1);
        }
    }
}
