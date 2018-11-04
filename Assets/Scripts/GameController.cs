using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> portals;
    public List<GameObject> players;    
    public bool hasActiveItem = false;
    public float itemTimer = 5f;
    private List<GameObject> items = new List<GameObject>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hasActiveItem){
            if (items.Count == 0)
            {
                GameObject[] tempItem = GameObject.FindGameObjectsWithTag("PickUp");

                foreach (GameObject item in tempItem)
                {
                    items.Add(item);
                    item.SetActive(false);
                }
            }
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
                foreach (GameObject item in items)
                {
                    item.SetActive(true);
                }
                items.Clear();
            }
        }
	}
}
