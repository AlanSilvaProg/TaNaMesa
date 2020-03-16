using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleToCurve : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve curveX, curveY;

    [SerializeField]
    private float MaxTime;

    private float timer = 0;

    private Vector3 originalSize;

    [SerializeField]
    private Sprite ativado;

    private Sprite padrao;

    private Image spr;

    [SerializeField]
    private AnimationCurve idleCurveX, idleCurveY;

    private bool inIdle = false;

    [SerializeField]
    private bool shouldIde = true;

    [SerializeField]
    private float idleModifier;

    private float idleTimer = 0;

    private float timeModifier = 0;
    // Use this for initialization
    void Start()
    {
        originalSize = transform.localScale;
        spr = GetComponent<Image>();
        padrao = spr.sprite;
        timeModifier = 1 / MaxTime;
        inIdle = shouldIde;
        /*
         0.5 = 2

         1 = 1  1 * 1/a
         2 = 0.5
         4 = 0.25
         */
    }

    // Update is called once per frame
    void Update()
    {
        if (inIdle)
        {
            transform.localScale = new Vector3(originalSize.x * idleCurveX.Evaluate(idleTimer), originalSize.y * idleCurveY.Evaluate(idleTimer), originalSize.z);
            idleTimer += Time.deltaTime * idleModifier;
            if (idleTimer > 1)
            {
                idleTimer = 0;
            }
        }
        else
        {
            idleTimer = 0;
        }


    }

    public void StartScaling()
    {
        StartCoroutine(ScaleCorotine());
        if (ativado != null)
        {
            spr.sprite = ativado;
        }
        else
        {
            spr.sprite = padrao;
        }

    }

    IEnumerator ScaleCorotine()
    {
        inIdle = false;
        while (true)
        {
            transform.localScale = new Vector3(originalSize.x * curveX.Evaluate(timer), originalSize.y * curveY.Evaluate(timer), originalSize.z);
            timer += Time.deltaTime * timeModifier;

            if (timer > 1)
            {
                timer = 0;
                spr.sprite = padrao;
                transform.localScale = originalSize;
                yield break;
            }
            inIdle = shouldIde;
            yield return null;
        }
    }


}