using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoScript : MonoBehaviour {

    public CharacterController player;
    public Text ammoText;
    public Sprite empty;
    public Sprite full;
	
	// Update is called once per frame
	void Update ()
    {
        ammoText.text = player.ammo.ToString();

        if (player.ammo <= 0)
        {
            GetComponent<Image>().sprite = empty;
        }
        else
        {
            GetComponent<Image>().sprite = full;
        }
	}
}
