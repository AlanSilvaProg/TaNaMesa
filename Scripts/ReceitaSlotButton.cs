using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceitaSlotButton : MonoBehaviour {

    private Canvas canvas;
    
    public Image icone;
    public Text theName;
	// Use this for initialization
	void Start () {
        canvas = FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void atualizarReceita()
    {
        FindObjectOfType<ReceitaContentScript>().AtualizarTexto(GetComponent<ReceitasBonus>());
        canvas.GetComponent<Animator>().SetTrigger("ToOpenRecipe");
        Debug.Log("Hm");

    }
}
