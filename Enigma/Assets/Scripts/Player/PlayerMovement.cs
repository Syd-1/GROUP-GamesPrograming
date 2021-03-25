using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody body;
    private float jumpForce = 10f;
    private float vertical;
    private float horizontal;
    private bool onGround;
    private float movementSpeed = 5f;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);

        // Add force to body if grounded to jump
        if (Input.GetAxis("Jump") > 0 && onGround)
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            onGround = false;
        }
    }
}
