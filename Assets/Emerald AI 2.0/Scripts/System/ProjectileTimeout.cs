using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimeout : MonoBehaviour {
	public float TimeoutSeconds = 3;
	float Timer;

	void Update () {
		Timer += Time.deltaTime;
		if (Timer >= TimeoutSeconds){
			EmeraldObjectPool.Despawn(gameObject);
		}
	}

	void OnDisable (){
		Timer = 0;
	}
}
