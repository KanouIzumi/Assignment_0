using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_28 : MonoBehaviour
{
    bool isOnGround;

    float jumpForce = 10.0f;
    float gravityModifer = 2.0f;
    float zLimit = 19.50f;
    float xLimit = 19.50f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playermtrls;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;

        Physics.gravity *= gravityModifer;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * jumpForce);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * jumpForce);

        if (transform.position.z < -zLimit)
        {
            //Bottom
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playermtrls[4].color;
        }
        else if (transform.position.z > zLimit)
        {
            //Top
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playermtrls[3].color;
        }
        if (transform.position.x < -xLimit)
        {
            //Left
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playermtrls[2].color;
        }
        else if (transform.position.x > xLimit)
        {
            //Right
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playermtrls[1].color;
        }

        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            playerRdr.material.color = playermtrls[5].color;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isOnGround = false;

            playerRdr.material.color = playermtrls[0].color;
        }
    }
}
