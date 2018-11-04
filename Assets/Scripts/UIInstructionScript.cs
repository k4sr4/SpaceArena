using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInstructionScript : MonoBehaviour {

    public GameObject[] players;
    public Sprite secondInstruction;

    bool acceptInput = false;
    bool first = true;

	// Use this for initialization
	void Awake ()
    {
        StartCoroutine(FirstInstruction());
        GameObject.FindObjectOfType<GameController>().hasActiveItem = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (acceptInput)
        {
            if (Input.anyKey)
            {
                GetComponent<Image>().enabled = false;

                if (first)
                {
                    foreach(GameObject player in players)
                    {
                        player.SetActive(true);
                    }
                    first = false;
                }
                else
                {
                    foreach (GameObject player in players)
                    {
                        player.GetComponent<Rigidbody2D>().WakeUp();
                    }
                }
            }
        }
	}

    IEnumerator FirstInstruction()
    {
        yield return new WaitForSeconds(2f);
        acceptInput = true;
        StartCoroutine(PlayTilSecondInstruction());
    }

    IEnumerator PlayTilSecondInstruction()
    {
        yield return new WaitForSeconds(30f);
        StartCoroutine(SecondInstruction());
    }

    IEnumerator SecondInstruction()
    {
        GetComponent<Image>().sprite = secondInstruction;
        GetComponent<Image>().enabled = true;
        foreach (GameObject player in players)
        {
            player.GetComponent<Rigidbody2D>().Sleep();
        }
        acceptInput = false;
        yield return new WaitForSeconds(2f);
        acceptInput = true;
        GameObject.FindObjectOfType<GameController>().hasActiveItem = false;
    }
}
