using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRendererStatusWithDelay : MonoBehaviour {

	public Emerald_AI EmeraldComponent;
	int DeactivateSeconds;
	public enum CurrentBehavior {Passive = 1, Cautious = 2, Companion = 3, Aggresive = 4};

	void Start (){
		DeactivateSeconds = EmeraldComponent.DeactivateSeconds;
	}
		
	 IEnumerator OnBecameInvisible (){
		if (EmeraldComponent.CurrentDetectionRef != Emerald_AI.CurrentDetection.Alert && EmeraldComponent.CurrentTarget == null && !EmeraldComponent.ReturningToStartInProgress && EmeraldComponent.BehaviorRef != Emerald_AI.CurrentBehavior.Companion){
			yield return new WaitForSeconds(DeactivateSeconds);
			EmeraldComponent.Deactivate();
		}
	}

	void OnBecameVisible (){
		StopCoroutine("OnBecameInvisible");
		EmeraldComponent.Activate();
	}
}
