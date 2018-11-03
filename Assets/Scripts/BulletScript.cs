using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

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
