using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemText : MonoBehaviour {

    public Sprite[] itemTexts;
	
	public void Activate (int index)
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = itemTexts[index];

    }

    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Image>().enabled = false;
    }
}
