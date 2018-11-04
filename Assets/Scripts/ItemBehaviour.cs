using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public List<Sprite> sprites;
    public int type = 5;
    public string itemName = "";
    public bool self = false;
    // Use this for initialization
    void Start()
    {
        if (Random.Range(0, 2) == 0) self = true;
        type = Random.Range(1,5);

        switch (type){
            case 1:
                itemName = "timeCapsule";
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            //case 2:
                //itemName = "cottonCandyGun";
                //GetComponent<SpriteRenderer>().sprite = sprites[1];
                //break;
            case 2:
                itemName = "rehabilitator";
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 3:
                itemName = "reverseControl";
                GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case 4:
                itemName = "frenzy";
                GetComponent<SpriteRenderer>().sprite = sprites[4];
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
                                //case "cottonCandyGun":
                                    //player.CottonCandyGun();
                                    //break;
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
                        //case "cottonCandyGun":
                            //currPlayer.CottonCandyGun();
                            //break;
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
