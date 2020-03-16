using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceitaContentScript : MonoBehaviour {

    [SerializeField]
    private Text contextText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AtualizarTexto(ReceitasBonus receita)
    {
        contextText.text = receita.info.receitaEscrita;
    }
}
