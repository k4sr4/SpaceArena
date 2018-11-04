using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingGameControl : MonoBehaviour {


    public static UndyingGameControl Instance;

    private void Awake()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop =true;

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
