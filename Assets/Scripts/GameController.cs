using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> portals;
    public List<GameObject> players;
    public bool hasActiveItem = false;
    public float itemTimer = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hasActiveItem){
            itemTimer -= Time.deltaTime;
            if (itemTimer <= 0.1f)
            {
                hasActiveItem = false;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject p in players){
                    p.GetComponent<CharacterController>().frenzy = false;
                    p.GetComponent<CharacterController>().timeCapsule = false;
                    p.GetComponent<CharacterController>().reversed = false;
                    p.GetComponent<CharacterController>().cottonCandyGun = false;
                }
            }
        }
	}
}
