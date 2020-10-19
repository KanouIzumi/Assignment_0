using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Player_Controller_29 : MonoBehaviour
{
    float speed = 10.0f;
    float zLimit = 18.30f;
    float xLimit = 18.30f;
    float yLimit = 0.9f;

    float gravityModifier = 2.0f;

    Rigidbody playerRb;

    bool pressSpace = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.Space) && pressSpace)
        {
            playerRB.AddForce(Vector3.up * 15, ForceMode.Impulse);
            pressSpace = false;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        pressSpace = true;
    }



}


