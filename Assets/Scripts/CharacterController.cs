using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;             

    private Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool facingUp = true;

    private float moveHorizontal;
    private float moveVertical;

    private float rotateHorizontal;
    private float rotateVertical;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            moveHorizontal = 1f;
            facingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            moveHorizontal = -1f;
            facingRight = false;
        }
        else
        {
            moveHorizontal = 0f;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            moveVertical = 1f;
            facingUp = true;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            moveVertical = -1f;
            facingUp = false;
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

    void FlipHor()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FlipVer()
    {
        facingUp = !facingUp;
        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            GameObject sourcePortal = collision.gameObject;

            if (sourcePortal.GetComponent<PortalScript>().active)
            {
                List<GameObject> portals = GameObject.FindObjectOfType<GameController>().portals;
                List<GameObject> tempPortals = new List<GameObject>(portals);
                tempPortals.Remove(sourcePortal);
                int index = Random.Range(0, tempPortals.Count);
                GameObject destinationPortal = tempPortals[index];
                PortalScript destScript = destinationPortal.GetComponent<PortalScript>();
                destScript.active = false;
                transform.position = destinationPortal.transform.position;

                if ((destScript.exitRight && moveHorizontal == -1f)
                    || (!destScript.exitRight && moveHorizontal == 1f))
                {
                    FlipHor();
                }

                if ((destScript.exitUp && moveVertical == -1f)
                    || (!destScript.exitUp && moveVertical == 1f))
                {
                    FlipVer();
                }
            }
        }

    }
}
