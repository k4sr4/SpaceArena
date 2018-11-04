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
        Debug.Log(currScene);
        if (currScene == 0)
        {
            StartCoroutine(Wait());
            NextScene();
        }
        if(currScene == 1 && GameObject.FindGameObjectsWithTag("Player").Length == 1){
            SceneManager.LoadScene(currScene);

        }
    }

    public void NextScene(){
        currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene + 1);
    }

    public void PrevScene(){
        currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene - 1);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
    }

}
