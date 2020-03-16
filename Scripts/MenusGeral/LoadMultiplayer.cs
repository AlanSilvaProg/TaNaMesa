using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiplayer : MonoBehaviour {

	public void LoadMultiRapido()
    {
        PlayerPrefs.SetString("Modo", "Rapido");
        SceneManager.LoadScene(1);
    }

}
