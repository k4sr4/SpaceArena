using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenelaoder : MonoBehaviour {

    int currScene = -1;
    // Use this for initialization
    void Start()
    {
        currScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update(){
        if (currScene == 0)
        {
            StartCoroutine(Wait());
        }
        if(currScene == 1 )
        {
            if (Input.anyKeyDown) SceneManager.LoadScene(currScene + 1);
        }
    }

    public void NextScene(){
        currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene + 1);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        NextScene();
    }

}
