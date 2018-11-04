using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInstructionScript : MonoBehaviour {

    public GameObject[] players;
    public Sprite secondInstruction;
    public Sprite[] winSprites;

    bool acceptInput = false;
    bool first = true;

	// Use this for initialization
	void Awake ()
    {
        GameObject.FindObjectOfType<ItemSpawner>().instruction = true;
        StartCoroutine(FirstInstruction());

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
                    acceptInput = false;
                }
                else
                {
                    foreach (CharacterController player in GameObject.FindObjectsOfType<CharacterController>())
                    {
                        player.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                    }
                    acceptInput = false;
                }

            }
        }
	}

    public void CheckIfFinished()
    {
        int killed = 0;
        int winnedID = 0;

        foreach(CharacterController player in GameObject.FindObjectsOfType<CharacterController>())
        {
            if (player.killed)
            {
                killed++;
            }
            else
            {
                winnedID = player.id;
            }
        }

        if (killed == 1 && GameObject.FindObjectsOfType<CharacterController>().Length == 2)
        {
            StartCoroutine(WinnerAnounce(winnedID));
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
        yield return new WaitForSeconds(20f);
        StartCoroutine(SecondInstruction());
    }

    IEnumerator SecondInstruction()
    {
        GetComponent<Image>().sprite = secondInstruction;
        GetComponent<Image>().enabled = true;
        foreach (CharacterController player in GameObject.FindObjectsOfType<CharacterController>())
        {
            player.gameObject.GetComponent<Rigidbody2D>().Sleep();
        }
        acceptInput = false;
        yield return new WaitForSeconds(2f);
        acceptInput = true;
        GameObject.FindObjectOfType<ItemSpawner>().instruction = false;
    }

    IEnumerator WinnerAnounce(int i)
    {
        GetComponent<Image>().sprite = winSprites[i-1];
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
