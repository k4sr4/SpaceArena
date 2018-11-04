using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthScript : MonoBehaviour {

    public Sprite[] hpSprites;
    public CharacterController player;
	
	void Update ()
    {
        GetComponent<Image>().sprite = hpSprites[player.hp];
	}
}
