using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldButtonUnlocker : MonoBehaviour {

    private int isUnlocked;

    private BetterButton better;
    private ScaleToCurve scaler;

    private Image button;

    [SerializeField]
    private Image flagImg;

    [SerializeField]
    private string country;

    [SerializeField]
    private Sprite whiteButton;

    private Color redOverlay;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("Brasil",0) == 0)
        {
            PlayerPrefs.SetInt("Brasil", 1);
        }

        better = GetComponent<BetterButton>();
        scaler = GetComponent<ScaleToCurve>();
        button = GetComponent<Image>();

        redOverlay = new Color32(255,100,100,255);

        isUnlocked = PlayerPrefs.GetInt(country,0);
        if (isUnlocked == 0)
        {
            better.enabled = false;
            scaler.enabled = false;
            button.color = redOverlay;
            flagImg.color = redOverlay;
            button.sprite = whiteButton;

        }
       
        //0 = locked 
        //1 = unlocked
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
