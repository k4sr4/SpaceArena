using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float projectileSpeed = 50f;

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
            Destroy(gameObject);
        }

        //if (collision.tag == "Player" && collision.gameObject != this.gameObject)
        //{
        //    Destroy(gameObject);
        //}
    }
}
