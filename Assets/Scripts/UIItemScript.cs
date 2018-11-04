using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemScript : MonoBehaviour {

    public CharacterController player;
    public Sprite frenzy;
    public Sprite timeCapsule;
    public Sprite reverse;

    // Update is called once per frame
    void Update ()
    {
        if (player.frenzy)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GetComponent<Image>().sprite = frenzy;            
        }
        else if (player.timeCapsule)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GetComponent<Image>().sprite = timeCapsule;            
        }
        else if (player.reversed)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GetComponent<Image>().sprite = reverse;            
        }
        else
        {
            GetComponent<Image>().color = new Color32(135, 147, 129, 255);
            GetComponent<Image>().sprite = null;
        }
    }
}
