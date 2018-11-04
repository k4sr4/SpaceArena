using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public int type = 5;
    public string itemName = "";
    public bool self = false;
    // Use this for initialization
    void Start()
    {
        if (Random.Range(0, 2) == 0) self = true;
        type = Random.Range(1,6);
        switch (type){
            case 1:
                itemName = "timeCapsule";
                break;
            case 2:
                itemName = "cottonCandyGun";
                break;
            case 3:
                itemName = "rehabilitator";
                break;
            case 4:
                itemName = "reverseControl";
                break;
            case 5:
                itemName = "frenzy";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!GameObject.FindObjectOfType<GameController>().hasActiveItem)
            {
                GameObject.FindObjectOfType<GameController>().hasActiveItem = true;
                GameObject.FindObjectOfType<GameController>().itemTimer = 5f;

                CharacterController currPlayer = collision.gameObject.GetComponent<CharacterController>();

                if (!self)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                    for (int i = 0; i < players.Length; i++)
                    {
                        CharacterController player = players[i].GetComponent<CharacterController>();
                        if (player != currPlayer)
                        {
                            switch (itemName)
                            {
                                case "timeCapsule":
                                    player.TimeCapsule();
                                    break;
                                case "cottonCandyGun":
                                    player.CottonCandyGun();
                                    break;
                                case "rehabilitator":
                                    player.Rehabilitate();
                                    break;
                                case "reverseControl":
                                    player.ReverseControl();
                                    break;
                                case "frenzy":
                                    player.Frenzy();
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    switch (itemName)
                    {
                        case "timeCapsule":
                            currPlayer.TimeCapsule();
                            break;
                        case "cottonCandyGun":
                            currPlayer.CottonCandyGun();
                            break;
                        case "rehabilitator":
                            currPlayer.Rehabilitate();
                            break;
                        case "reverseControl":
                            currPlayer.ReverseControl();
                            break;
                        case "frenzy":
                            currPlayer.Frenzy();
                            break;
                    }
                }
            }
        }
    }
}
