using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Execute : MonoBehaviour {

    [SerializeField]
    private UnityEvent @event;

    void Start () {

        @event.Invoke();
		
	}
	

	void Update () {
		
	}
}
