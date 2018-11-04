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
    public List<GameObject> enter_left;
    public List<GameObject> enter_right;
    public List<GameObject> exit_left;
    public List<GameObject> exit_right;

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
