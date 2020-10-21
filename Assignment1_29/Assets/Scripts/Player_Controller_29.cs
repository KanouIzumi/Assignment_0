using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_29 : MonoBehaviour
{
    float speed = 10.0f;
    float zLimit = 18.30f;
    float xLimit = 18.30f;
    float yLimit = 0.9f;

    bool isOnFloor = false;
    float DoubleJump = 0f;
    float PlayersJump = 0f;

    float gravityModifier = 2.0f;
    private RaycastHit hit;

    

    Rigidbody playerRb;

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

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1))
            { // can jump
                playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
        */

        //This allow me to jump when the ball is on the floor
        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor == true && DoubleJump == 0)
        {
            DoubleJump = 1;
            ++PlayersJump;
            isOnFloor = false;
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);

        }
        //This allow me to jump another time on the air
        else if (Input.GetKeyDown(KeyCode.Space) && DoubleJump == 1)
        {
            DoubleJump = 0;
            ++PlayersJump;
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        Debug.Log("Jump Count:" + PlayersJump);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }


}

