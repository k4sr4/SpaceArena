using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;
    public GameObject laserPrefab;
    public float projectileSpeed = 10f;    

    private Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool facingUp = true;

    private float moveHorizontal;
    private float moveVertical;

    private bool autoMove = false;
    private float autoMoveX = 0f;
    private float autoMoveY = 0f;

    private int ammo = 10;
    private float cooldown = 1f;

    private float rotateHorizontal;
    private float rotateVertical;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && cooldown > .5 && ammo > 0)
        {
            GameObject Laser = Instantiate(laserPrefab,
                                           transform.position,
                                           transform.rotation) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            cooldown = 0f;
            ammo--;
        }
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        Fire();
        Move();
    }

    void Move()
    {
        if (!autoMove)
        {
            float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float newX = transform.position.x + deltaX;
            float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float newY = transform.position.y + deltaY;

            transform.position = new Vector2(newX, newY);
            if (Input.GetAxis("Horizontal") > 0)
            {
                facingRight = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                facingRight = false;
            }


            if (Input.GetAxis("Vertical") > 0)
            {
                facingUp = true;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                facingUp = false;
            }
        }
        else
        {
            float newX = transform.position.x + autoMoveX;
            float newY = transform.position.y + autoMoveY;

            transform.position = new Vector2(newX, newY);
        }
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
                sourcePortal.GetComponent<PortalScript>().bounds.SetActive(false);
                List<GameObject> portals = GameObject.FindObjectOfType<GameController>().portals;
                List<GameObject> tempPortals = new List<GameObject>(portals);
                tempPortals.Remove(sourcePortal);
                int index = Random.Range(0, tempPortals.Count);
                GameObject destinationPortal = tempPortals[index];                
                PortalScript destScript = destinationPortal.GetComponent<PortalScript>();
                destScript.active = false;
                transform.position = destinationPortal.transform.position;

                if (destScript.exitRight && destScript.horizontal && moveHorizontal == -1f){
                    FlipHor();
                }
                if (!destScript.exitRight && destScript.horizontal && moveHorizontal == 1f)
                {
                    FlipHor();
                }

                if (destScript.exitUp && destScript.vertical && moveVertical == -1f)
                {
                    FlipVer();
                }
                if (!destScript.exitUp && destScript.vertical && moveVertical == 1f)
                {
                    FlipVer();
                }

                autoMove = true;

                if (destScript.horizontal)
                {
                    if (destScript.exitRight)
                    {
                        autoMoveX = 1f;
                    }

                    if (!destScript.exitRight)
                    {
                        autoMoveX = -1f;
                    }
                }

                if (destScript.vertical)
                {
                    if (destScript.exitUp)
                    {
                        autoMoveY = 1f;
                    }

                    if (!destScript.exitUp)
                    {
                        autoMoveY = -1f;
                    }
                }

                sourcePortal.GetComponent<PortalScript>().bounds.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PortalBounds" && autoMove)
        {
            autoMove = false;
            autoMoveX = 0f;
            autoMoveY = 0f;
        }
    }
}
