using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
            gameObject.GetComponent<Body>().nullifyForce();
        if (Input.GetButton("Fire2"))
            gameObject.GetComponent<Body>().stop();
        if (Input.GetButton("Fire3"))
            gameObject.GetComponent<Body>().reset();
        if (Input.GetAxis("Horizontal") != 0)
            gameObject.GetComponent<Body>().force.x += Input.GetAxis("Horizontal") * Time.deltaTime;
        if (Input.GetAxis("Vertical") != 0)
            gameObject.GetComponent<Body>().force.y += Input.GetAxis("Vertical") * Time.deltaTime;
    }
}
