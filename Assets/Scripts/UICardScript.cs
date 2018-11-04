using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardScript : MonoBehaviour {

    public CharacterController player;
    public Image hp;
    public Image ammo;
    public Image item;
	
	// Update is called once per frame
	void Update ()
    {
        if (player.hp <= 0)
        {
            GetComponent<Image>().color = new Color32(255, 85, 0, 255);
            hp.GetComponent<Image>().color = new Color32(255, 85, 0, 255);
            ammo.GetComponent<Image>().color = new Color32(255, 85, 0, 255);
            item.GetComponent<Image>().enabled = false;
        }		
	}
}
