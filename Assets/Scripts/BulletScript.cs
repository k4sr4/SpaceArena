using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float projectileSpeed = 50f;

    public GameObject player;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position.x > 40f || transform.position.x < -40f || transform.position.y > 30f || transform.position.y < -30f)
        {
            Destroy(this.gameObject);
        }
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

                if (destScript.horizontal)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        destScript.exitRight = true;
                    }
                    else
                    {
                        destScript.exitRight = false;
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

                double rotationDegrees = 0;

                if (destScript.exitRight && destScript.horizontal)
                {
                    rotationDegrees = 0;
                }
                if (!destScript.exitRight && destScript.horizontal)
                {
                    rotationDegrees = 180;
                }

                if (destScript.exitUp && destScript.vertical)
                {
                    rotationDegrees = 90;
                }
                if (!destScript.exitUp && destScript.vertical)
                {
                    rotationDegrees = 270;
                }

                float xMagnitude = (float)System.Math.Cos((System.Math.PI / 180) * rotationDegrees);
                float yMagnitude = (float)System.Math.Sin((System.Math.PI / 180) * rotationDegrees);

                GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * xMagnitude, projectileSpeed * yMagnitude);
            }
        }

        if (collision.tag == "Wall")
        {
            ExplodeAnimation();
        }

        if (collision.tag == "Player" && collision.gameObject != player)
        {
            Destroy(gameObject);
            collision.GetComponent<CharacterController>().hp--;
        }
    }

    void ExplodeAnimation()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        anim.SetTrigger("Explode");
    }

    public void DestroyAfterExplode()
    {
        Destroy(gameObject);
        player.GetComponent<CharacterController>().glow.SetActive(false);
    }
}
