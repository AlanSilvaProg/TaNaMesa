using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    [SerializeField]
    private Image[] fadeObject;
    
    [SerializeField]
    private float fadeSpeed, alphaValue;

    void Start ()
    {
        if (fadeObject == null)
        {
            fadeObject[0] = GetComponent<Image>();
        }
    }
	
	
	void Update () {
		
	}

    public void Fade()
    {
        StartCoroutine(FadeTo(alphaValue,fadeSpeed));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        foreach (Image obj in fadeObject)
        {
            float alpha = obj.color.a;
            for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / aTime)
            {
                
                Color newColor = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.Lerp(alpha, aValue, t));
                obj.color = newColor;
                yield return null;
            }
           obj.color = new Color(obj.color.r, obj.color.g, obj.color.b,alphaValue);
        }

        
    }
}
