using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    public static bool onTuto;
    [Header("Desativar apenas na versão final de build")]
    public bool[] tutoTest;
    private bool tutoPermission;
    [Header("indice 0 e 1 para menu, 2 e 3 para game play, 4 para overworld 2.")]
    public GameObject[] tutoScreen;

    private bool[] testPermission = new bool[5];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (tutoPermission && PlayerPrefs.GetInt("OverVisit") == 1 || tutoTest[0] && testPermission[0]) 
        {
            tutoTest[0] = false;
            testPermission[0] = false;
            tutoPermission = false;
            tutoScreen[0].SetActive(true);
            onTuto = true;
        }

        if (tutoPermission && PlayerPrefs.GetInt("BookVisit") == 1 || tutoTest[1] && testPermission[1])
        {
            tutoTest[1] = false;
            testPermission[1] = false;
            tutoPermission = false;
            tutoScreen[1].SetActive(true);
            onTuto = true;
        }

        if (tutoPermission && PlayerPrefs.GetInt("IngreVisit") == 1 || tutoTest[2] && testPermission[2])
        {
           
            tutoTest[2] = false;
            testPermission[2] = false;
            tutoPermission = false;
            tutoScreen[2].SetActive(true);
            onTuto = true;
        }

        if (tutoPermission && PlayerPrefs.GetInt("AdiviVisit") == 1 || tutoTest[3] && testPermission[3])
        {

            tutoTest[3] = false;
            testPermission[3] = false;
            tutoPermission = false;
            tutoScreen[3].SetActive(true);
            onTuto = true;
        }

        if (tutoPermission && PlayerPrefs.GetInt("Over2Visit") == 1 || tutoTest[4] && testPermission[4])
        {
            tutoTest[4] = false;
            testPermission[4] = false;
            tutoPermission = false;
            tutoScreen[4].SetActive(true);
            onTuto = true;
        }
    }

    public void OverVisit()
    {
        
        testPermission[0] = true;
        int valor = PlayerPrefs.GetInt("OverVisit");
        valor++;
        PlayerPrefs.SetInt("OverVisit", valor);
        if(PlayerPrefs.GetInt("OverVisit") <= 1)
        {
            tutoPermission = true;
        }
    }

    public void DesativeOverTuto()
    {
        tutoScreen[0].SetActive(false);
        onTuto = false;
        testPermission[0] = false;
        PlayerPrefs.SetInt("OverVisit", 2);
    }

    public void BookVisit()
    {
        testPermission[1] = true;
        int valor = PlayerPrefs.GetInt("BookVisit");
        valor++;
        PlayerPrefs.SetInt("BookVisit", valor);
        if (PlayerPrefs.GetInt("BookVisit") <= 1)
        {
            tutoPermission = true;
        }
    }

    public void DesativeBookTuto()
    {
        tutoScreen[1].SetActive(false);
        onTuto = false;
        testPermission[1] = false;
        PlayerPrefs.SetInt("BookVisit", 2);


    }

    public void IngreVisit()
    {
        
        testPermission[2] = true;
        int valor = PlayerPrefs.GetInt("IngreVisit");
        valor++;
        PlayerPrefs.SetInt("IngreVisit", valor);
        if (PlayerPrefs.GetInt("IngreVisit") <= 1)
        {
            tutoPermission = true;
        }
    }

    public void DesativeIngreTuto()
    {
        tutoScreen[2].SetActive(false);
        onTuto = false;
        testPermission[2] = false;
        PlayerPrefs.SetInt("IngreVisit", 2);


    }

    public void AdiviVisit()
    {
        testPermission[3] = true;
        int valor = PlayerPrefs.GetInt("AdiviVisit");
        valor++;
        PlayerPrefs.SetInt("AdiviVisit", valor);
        if (PlayerPrefs.GetInt("AdiviVisit") <= 1)
        {
            tutoPermission = true;
        }
    }

    public void DesativeAdviTuto()
    {
        tutoScreen[3].SetActive(false);
        onTuto = false;
        testPermission[3] = false;
        PlayerPrefs.SetInt("AdiviVisit", 2);

    }

    public void OverAAAVisit()
    {
        testPermission[4] = true;
        int valor = PlayerPrefs.GetInt("Over2Visit");
        valor++;
        PlayerPrefs.SetInt("Over2Visit", valor);
        if (PlayerPrefs.GetInt("Over2Visit") <= 1)
        {
            tutoPermission = true;
        }
    }

    public void DesativeOver2Tuto()
    {
        
        tutoScreen[4].SetActive(false);
        onTuto = false;

        testPermission[4] = false;
        PlayerPrefs.SetInt("Over2Visit", 2);

    }

}
