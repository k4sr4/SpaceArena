using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;             

    private Rigidbody2D rb2d;       

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal;
        float moveVertical;

        float rotateHorizontal;
        float mrotateVertical;

        //if (!Input.anyKey)
        //{
        //    rb2d.velocity = new Vector2(0f, 0f);
        //    moveHorizontal = 0f;
        //    moveVertical = 0f;
        //}

        if (Input.GetAxis("Horizontal") > 0)
        {
            moveHorizontal = 1f;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            moveHorizontal = -1f;
        }
        else
        {
            moveHorizontal = 0f;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            moveVertical = 1f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            moveVertical = -1f;
        }
        else
        {
            moveVertical = 0f;
        }

        if (!Input.anyKey)
        {
            rb2d.velocity = new Vector2(0f, 0f);
            moveHorizontal = 0f;
            moveVertical = 0f;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //rb2d.AddForce(movement * speed);
        rb2d.velocity = movement * speed;
    }
}
