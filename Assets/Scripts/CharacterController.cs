using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


    private float TIMER = 5f;
    public int id;
    public float speed;
    public float rotateSpeed;
    public GameObject laserPrefab;
    public GameObject glow;
    public Transform rotateAnchor;
    public int hp = 5;

    public string item = "";    

    private Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool facingUp = true;

    private float moveHorizontal;
    private float moveVertical;

    private bool autoMove = false;
    private float autoMoveX = 0f;
    private float autoMoveY = 0f;

    public int ammo = 100;
    private float cooldown = 1f;

    private float angle = 0f;

    private Animator anim;

    /// 
    /// Items properties
    /// 
    public bool frenzy = false;
    public bool timeCapsule = false;
    public bool reversed = false;
    public bool cottonCandyGun = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        Move();
        Rotate();
        Fire();

    }
    public void DestroyAfterExplode()
    {
        Destroy(gameObject);
    }

    private void Fire()
    {
        if (frenzy && GameObject.FindObjectOfType<GameController>().itemTimer > 0)
        {
            gunFire();
            GameObject.FindObjectOfType<GameController>().itemTimer -= Time.deltaTime;
            Debug.Log("Fire: "+GameObject.FindObjectOfType<GameController>().itemTimer);
        }
        else if (cottonCandyGun && GameObject.FindObjectOfType<GameController>().itemTimer > 0)
        {
            GameObject.FindObjectOfType<GameController>().itemTimer -= Time.deltaTime;
            Debug.Log("Fire: " + GameObject.FindObjectOfType<GameController>().itemTimer);
            gunFire();
            cooldown = 0f;
            ammo--;
        }
        else if (GameObject.FindObjectOfType<GameController>().itemTimer <= 0)
        {
            GameObject.FindObjectOfType<GameController>().hasActiveItem = false;
            frenzy = false;
            cottonCandyGun = false;
        }
        else if (Input.GetButton("Fire" + id) && cooldown > .5 && ammo > 0)
        {
            gunFire();
            cooldown = 0f;
            ammo--;
        }
    }

    private void gunFire()
    {
        glow.SetActive(true);

        double rotationDegrees = rotateAnchor.rotation.eulerAngles.z + 90;
        GameObject Laser = Instantiate(laserPrefab,
                                           transform.position,
                                           rotateAnchor.rotation) as GameObject;

        if (cottonCandyGun)
        {
            Laser.GetComponent<BoxCollider2D>().enabled = false;
        }

        Laser.GetComponent<BulletScript>().player = this.gameObject;

        float projectileSpeed = Laser.GetComponent<BulletScript>().projectileSpeed;

        float xMagnitude = (float)System.Math.Cos((System.Math.PI / 180) * rotationDegrees);
        float yMagnitude = (float)System.Math.Sin((System.Math.PI / 180) * rotationDegrees);
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * xMagnitude, projectileSpeed * yMagnitude);

    }

    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal" + id);
        moveVertical = Input.GetAxis("Vertical" + id);

        if (reversed && GameObject.FindObjectOfType<GameController>().itemTimer > 0){
            float temp = moveHorizontal;
            moveHorizontal = moveVertical;
            moveVertical = temp;
            transport();
            GameObject.FindObjectOfType<GameController>().itemTimer -= Time.deltaTime;
            Debug.Log("reversed: " + GameObject.FindObjectOfType<GameController>().itemTimer);
        }
        else if (timeCapsule && GameObject.FindObjectOfType<GameController>().itemTimer > 0)
        {
            speed = 2f;
            transport();
            GameObject.FindObjectOfType<GameController>().itemTimer -= Time.deltaTime;
            Debug.Log("timeCapsule: " +GameObject.FindObjectOfType<GameController>().itemTimer);
        }
        else if (GameObject.FindObjectOfType<GameController>().itemTimer <= 0){
            transport();
            GameObject.FindObjectOfType<GameController>().hasActiveItem = false;
            reversed = false;
            timeCapsule = false;
            speed = 15f;
            transport();
        }
        else
        {
            transport();
        }
    }


    void transport(){
        if (!autoMove)
        {


            if (moveHorizontal == 0 && moveVertical == 0)
            {
                anim.SetBool("Move", false);
            }
            else
            {
                anim.SetBool("Move", true);
            }

            float deltaX = moveHorizontal * Time.deltaTime * speed;
            float newX = transform.position.x + deltaX;
            float deltaY = moveVertical * Time.deltaTime * speed;
            float newY = transform.position.y + deltaY;

            transform.position = new Vector2(newX, newY);
            if (Input.GetAxis("Horizontal" + id) > 0)
            {
                facingRight = true;
            }
            else if (Input.GetAxis("Horizontal" + id) < 0)
            {
                facingRight = false;
            }


            if (Input.GetAxis("Vertical" + id) > 0)
            {
                facingUp = true;
            }
            else if (Input.GetAxis("Vertical" + id) < 0)
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

    void Rotate()
    {
        float horizontal = Input.GetAxis("RotationX" + id);
        float vertical = Input.GetAxis("RotationY" + id);
        if (horizontal != 0 || vertical != 0)
        {
            angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            rotateAnchor.rotation = Quaternion.Euler(0, 0, angle);            
        }
        else if (horizontal == 0 && vertical == 0)
        {
            rotateAnchor.rotation = Quaternion.Euler(0, 0, angle);
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
            PortalScript sourceScript = sourcePortal.GetComponent<PortalScript>();

            if (sourceScript.active)
            {
                if (transform.position.x < sourcePortal.transform.position.x)
                {
                    sourceScript.enter_left[id - 1].SetActive(true);
                }
                else
                {
                    sourceScript.enter_left[id - 1].SetActive(true);
                }

                sourceScript.bounds.SetActive(false);
                List<GameObject> portals = GameObject.FindObjectOfType<GameController>().portals;
                List<GameObject> tempPortals = new List<GameObject>(portals);
                tempPortals.Remove(sourcePortal);
                int index = Random.Range(0, tempPortals.Count);
                GameObject destinationPortal = tempPortals[index];                
                PortalScript destScript = destinationPortal.GetComponent<PortalScript>();
                destScript.active = false;
                transform.position = destinationPortal.transform.position;

                if (destScript.horizontal)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        destScript.exitRight = true;
                        destScript.exit_right[id - 1].SetActive(true);
                    }
                    else
                    {
                        destScript.exitRight = false;
                        destScript.exit_left[id - 1].SetActive(true);
                    }
                }

                if (destScript.vertical)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        destScript.exitUp = true;
                    }
                    else
                    {
                        destScript.exitUp = false;
                    }
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

                sourceScript.bounds.SetActive(true);
                sourcePortal.GetComponentInParent<Animator>().SetBool("Open", false);
            }
        }

        if (collision.tag == "PickUp")
        {
            item = collision.GetComponent<ItemBehaviour>().itemName;
            ammo += 30;

            Destroy(collision.gameObject);

            GameObject.FindObjectOfType<GameController>().GetComponent<ItemSpawner>().itemsCount--;
        }

        if(collision.tag == "PortalBounds")
        {
            collision.gameObject.GetComponentInParent<Animator>().SetBool("Open", true);
        }
    }

    public void Frenzy()
    {
        Debug.Log("frenzy");
        frenzy = true;

    }

    public void ReverseControl()
    {
        Debug.Log("Reverse");
        reversed = true;
    }

    public void Rehabilitate()
    {
        Debug.Log("HP");
        hp++;
        GameObject.FindObjectOfType<GameController>().hasActiveItem = false;
    }

    public void CottonCandyGun()
    {
        Debug.Log("Cotton");
        cottonCandyGun = true;
    }

    public void TimeCapsule()
    {
        Debug.Log("time");
        timeCapsule = true; 
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PortalBounds" && autoMove)
        {
            autoMove = false;
            autoMoveX = 0f;
            autoMoveY = 0f;
        }
        else if (collision.tag == "PortalBounds")
        {
            collision.gameObject.GetComponentInParent<Animator>().SetBool("Open", false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
    }
}
