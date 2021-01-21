using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject flameThrower;
    float speed = 5f;
    bool flamePickUp = false;
    GameObject pickup;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(horizontalInput * Vector3.right * speed * Time.deltaTime);
        transform.Translate(verticalInput * Vector3.forward * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.E) && flamePickUp)
        {
            flameThrower.SetActive(true);
            Destroy(pickup);
        }

        if (flameThrower.activeInHierarchy)
        {
            Debug.Log("Active");
        }

        if (flameThrower.activeInHierarchy == true && Input.GetKey(KeyCode.Q))
        {
            flameThrower.GetComponent<Flamethrower>().shoot = true;
        } else if(flameThrower.activeInHierarchy == true)
        {
            flameThrower.GetComponent<Flamethrower>().shoot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        flamePickUp = true;
        pickup = other.gameObject;

    }

    private void OnTriggerExit(Collider other)
    {
        flamePickUp = false;
    }
}
