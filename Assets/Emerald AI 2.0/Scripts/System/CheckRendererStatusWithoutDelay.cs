using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRendererStatusWithoutDelay : MonoBehaviour {

	public Emerald_AI EmeraldComponent;

	void OnBecameInvisible (){
		EmeraldComponent.Deactivate();
	}

	void OnBecameVisible (){
		EmeraldComponent.Activate();
	}
}
