using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelCamp : MonoBehaviour {

	public void LoadLevel()
    {
        
        PlayerPrefs.SetString("Modo", "Campanha");
        SceneManager.LoadScene(1);

    }

    public void SaveLocal(string nomePais)
    {

        PlayerPrefs.SetString("PaisAtual", nomePais);

    }

    public void LoadLevelSingle()
    {

        PlayerPrefs.SetString("Modo", "Campanha");
        SceneManager.LoadScene(2);

    }

}
