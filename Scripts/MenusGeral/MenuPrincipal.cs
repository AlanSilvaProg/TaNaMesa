using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuPrincipal : MonoBehaviour {

    public Animator menu;

    public GameObject characterSelec;

    public Image fade;

    public Image cred, inputCred, inputQuit;

    public void Start()
    {


        if (PlayerPrefs.GetInt("Personagem") == 0)
        {
            
        }
        else
        {
            Color novaCor = new Color(0, 0, 0, 0);
            fade.color = novaCor;
            characterSelec.SetActive(false);
        }

        if(PlayerPrefs.GetString("Book") == "Go")
        {
            gameObject.SendMessage("BookVisit");
            AtivarBonus();
            PlayerPrefs.SetString("Book", " ");
        }
        else  if(PlayerPrefs.GetString("Over") == "Go")
        {
            gameObject.SendMessage("OverVisit");
            PlayerPrefs.SetString("Over", " ");
            AtivarOver();
        }

    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.Save();
        }
    }

    void AtivarOver()
    {
        menu.SetTrigger("toPlay");
        menu.SetTrigger("toMultiPlay");
    }

    void AtivarBonus()
    {
        menu.SetTrigger("toBook");
    }

    public void Personagem1()
    {
        PlayerPrefs.SetInt("Personagem", 1);
        PlayerPrefs.Save();
    }

    public void Personagem2()
    {
        PlayerPrefs.SetInt("Personagem", 2);
        PlayerPrefs.Save();
    }

    public void SettarPais(string str)
    {
        PlayerPrefs.SetString("PaisAtual", str);
    }

    public void EnterCred()
    {
        cred.gameObject.SetActive(true);
        inputQuit.gameObject.SetActive(true);
        inputCred.gameObject.SetActive(false);
    }

    public void QuitCred()
    {
        cred.gameObject.SetActive(false);
        inputQuit.gameObject.SetActive(false);
        inputCred.gameObject.SetActive(true);
    }

}
