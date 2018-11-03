using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public bool active = true;
    public bool horizontal;
    public bool vertical;
    public bool exitRight;
    public bool exitUp;
    public GameObject bounds;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = true;
        }
        if (collision.tag == "Bullet")
        {
            active = true;
        }
    }
}
