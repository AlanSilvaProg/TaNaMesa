using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class BetterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float holdTimer;
    private float invokeTimer;
    private float invokeTimerD;
    private float delayTimer;

    [SerializeField]
    private bool useOnPress;
    [SerializeField]
    private UnityEvent OnPressEvent;
    [Space]
    [SerializeField]
    private bool useOnPressDelayed;
    [SerializeField]
    private float delay;
    [SerializeField]
    private UnityEvent OnPressDelayedEvent;
    [Space]
    [SerializeField]
    private bool useLongPress;
    [SerializeField]
    private float holdMinDuration;
    [SerializeField]
    public UnityEvent longPressEvent;
    [SerializeField]
    private bool isFillable;
    [SerializeField]
    private Image fillImage;

    [Space]
    [SerializeField]
    private bool repeatWhileHold;
    [SerializeField]
    private float timeBetweenInvoke = 0.5f;

    private bool switcher;

    private bool hasLongPressedOnce = false;


    public void Start()
    {
        if (fillImage == null && isFillable)
        {
            fillImage = GetComponent<Image>();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;

         if (useOnPressDelayed)
        {
            StartCoroutine(OnPressDelayed());
        }

        if (useOnPress)
        {
            if (OnPressEvent != null)
            {
                OnPressEvent.Invoke();
            }
        }


        if (!useLongPress && !useOnPress && !useOnPressDelayed)
        {
            Debug.LogError("nenhum tipo de evento selecionado");
            return;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        pointerDown = false;
        holdTimer = 0;
        delayTimer = 0;
        if (isFillable)
        {
            fillImage.fillAmount = holdTimer / holdMinDuration;
        }
        hasLongPressedOnce = false;

    }

    private void Update()
    {

        if (pointerDown)
        {
            
            if (useLongPress)
            {
                holdTimer += Time.deltaTime;

                fillImage.fillAmount = holdTimer / holdMinDuration;


                if (repeatWhileHold)
                {
                    invokeTimer += Time.deltaTime;
                    invokeTimerD += Time.deltaTime;
                }


                if (holdTimer >= holdMinDuration)
                {


                    if (repeatWhileHold)
                    {
                        if (invokeTimer >= timeBetweenInvoke)
                        {
                            if (longPressEvent != null)
                            {
                                longPressEvent.Invoke();
                                invokeTimer = 0;
                            }

                        }
                    }

                    else
                    {
                        if (longPressEvent != null && !hasLongPressedOnce )
                        {
                            longPressEvent.Invoke();
                            hasLongPressedOnce = true;


                        }


                    }


                }


            }


        }

        

    }

    IEnumerator OnPressDelayed()
    {
        yield return new WaitForSeconds(delay);
        if (OnPressDelayedEvent != null)
        {
            OnPressDelayedEvent.Invoke();
        }
    }
}



