using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public int type = 5;
    public string itemName = "";
    // Use this for initialization
    void Start()
    {
        type = 5;
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
                itemName = "revereseControl";
                break;
            case 5:
                itemName = "frenzy";
                break;
        }
    }
   
}
