using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class EmeraldProjectile : MonoBehaviour {

	[HideInInspector]
	public Emerald_AI EmeraldSystem;
	[HideInInspector]
	public Vector3 ProjectileDirection;
	[HideInInspector]
	public float TimeoutTimer;
	[HideInInspector]
	public float CollisionTimer;
	[HideInInspector]
	public bool Collided;
	[HideInInspector]
	public SphereCollider ProjectileCollider;
	GameObject SpawnedEffect;
	GameObject CollisionSound;
	GameObject CollisionSoundObject;
	Vector3 LastDirection;

	//Customizable variables
	[HideInInspector]
	public float TimeoutTime = 4.5f;
	[HideInInspector]
	public float CollisionTime = 0;
	[HideInInspector]
	public bool FollowContinously = false;
	[HideInInspector]
	public int ProjectileSpeed = 30;
	[HideInInspector]
	public enum EffectOnCollision {No = 0, Yes = 1};
	[HideInInspector]
	public EffectOnCollision EffectOnCollisionRef = EffectOnCollision.No;
	[HideInInspector]
	public EffectOnCollision SoundOnCollisionRef = EffectOnCollision.No;
	[HideInInspector]
	public enum HeatSeeking {No = 0, Yes = 1};
	[HideInInspector]
	public HeatSeeking HeatSeekingRef = HeatSeeking.No;
	[HideInInspector]
	public AudioClip ImpactSound;
	[HideInInspector]
	public GameObject CollisionEffect;
	[HideInInspector]
	public Vector3 AdditionalHeight;
	[HideInInspector]
	public float HeatSeekingSeconds = 1;
	[HideInInspector]
	public float HeatSeekingTimer = 0;
	[HideInInspector]
	public float HeatSeekingInitializeTimer = 0;
	[HideInInspector]
	public bool HeatSeekingFinished = false;
	[HideInInspector]
	public Transform ProjectileCurrentTarget;
	[HideInInspector]
	public bool TargetInView = false;
		
	//Setup our AI's projectile once on Awake
	void Awake (){
		gameObject.layer = 2;
		ProjectileCollider = GetComponent<SphereCollider>();
		ProjectileCollider.isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;
		gameObject.isStatic = false;
		CollisionSoundObject = Resources.Load("Emerald Collision Sound") as GameObject;
	}

	void Start (){
		GetHeatSeekingAngle();
	}

	void OnEnable (){
		GetHeatSeekingAngle();
	}

	//Only use the heat seeking feature if your target is within 65 degrees of our AI
	//to prevent the AI from firing behind themselves.
	void GetHeatSeekingAngle (){
		if (HeatSeekingRef == HeatSeeking.Yes && ProjectileCurrentTarget != null && EmeraldSystem != null){
			Vector3 direction = (ProjectileCurrentTarget.position) - EmeraldSystem.transform.position;
			float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), new Vector3(EmeraldSystem.transform.forward.x, 0, EmeraldSystem.transform.forward.z));
			if (angle <= 65){
				TargetInView = true;
			}
			else{
				TargetInView = false;
			}
		}
	}
		
	void Update () {
		//Continue to have our AI projectile follow the direction of its target until it colliders with something
		if (!Collided && HeatSeekingRef == HeatSeeking.No && ProjectileDirection != Vector3.zero || 
			!TargetInView && !Collided && ProjectileDirection != Vector3.zero){
			transform.position = transform.position + ProjectileDirection * Time.deltaTime * ProjectileSpeed;
			transform.rotation = Quaternion.LookRotation(ProjectileDirection);
		}

		//Give our fired projectile a 20th of a second to leave the caster's hand before following the target's position.
		//This is to stop the projectile from firing behind the caster if the target happens to get behind them while they're firing.
		if (!Collided && HeatSeekingRef == HeatSeeking.Yes && TargetInView){
			if(!HeatSeekingFinished){

				HeatSeekingInitializeTimer += Time.deltaTime;

				if (HeatSeekingInitializeTimer < 0.05f){
					transform.position = transform.position + ProjectileDirection * Time.deltaTime * ProjectileSpeed;
				}

				if (HeatSeekingInitializeTimer >= 0.05f){
					transform.Translate(Vector3.Normalize(((ProjectileCurrentTarget.position + AdditionalHeight) - transform.position)) * Time.deltaTime * ProjectileSpeed);
					HeatSeekingTimer += Time.deltaTime;

					if (HeatSeekingTimer >= HeatSeekingSeconds){
						LastDirection = Vector3.Normalize(((ProjectileCurrentTarget.position + AdditionalHeight) - transform.position));
						HeatSeekingFinished = true;
					}
				}
			}
			else if(HeatSeekingFinished && LastDirection != Vector3.zero){
				transform.position = transform.position + LastDirection * Time.deltaTime * ProjectileSpeed;
				transform.rotation = Quaternion.LookRotation(LastDirection);
			}
		}

		//Track our time since instantiation, once the Timeout time has been reachd, despawn
		TimeoutTimer += Time.deltaTime;
		if (TimeoutTimer >= TimeoutTime){
			EmeraldObjectPool.Despawn(gameObject);
		}

		if (Collided){
			CollisionTimer += Time.deltaTime;
			if (CollisionTimer >= CollisionTime){
				EmeraldObjectPool.Despawn(gameObject);
			}
		}
	}

	//Handle all of our collision related calculations here. When this happens, effects and sound can be played before the object is despawned.
	void OnTriggerEnter (Collider C){
		if (!Collided && EmeraldSystem != null && ProjectileCurrentTarget != null && C.gameObject == ProjectileCurrentTarget.gameObject){
			if (EffectOnCollisionRef == EffectOnCollision.Yes){
				if (CollisionEffect != null){
					SpawnedEffect = EmeraldObjectPool.Spawn(CollisionEffect, transform.position, Quaternion.identity);
					SpawnedEffect.transform.SetParent(Emerald_AI.ObjectPool.transform);
				}
			}
			if (ImpactSound != null && SoundOnCollisionRef == EffectOnCollision.Yes){
				CollisionSound = EmeraldObjectPool.Spawn(CollisionSoundObject, transform.position, Quaternion.identity);
				CollisionSound.transform.SetParent(Emerald_AI.ObjectPool.transform);
				AudioSource CollisionAudioSource = CollisionSound.GetComponent<AudioSource>();
				CollisionAudioSource.PlayOneShot(ImpactSound);
			}

			if (EmeraldSystem.TargetTypeRef == Emerald_AI.TargetType.AI && EmeraldSystem.TargetEmerald != null){
				EmeraldSystem.TargetEmerald.Damage(EmeraldSystem.CurrentDamageAmount, Emerald_AI.TargetType.AI);
			}
			else if (EmeraldSystem.TargetTypeRef == Emerald_AI.TargetType.Player){
				EmeraldSystem.DamagePlayer();
			}
			else if (EmeraldSystem.TargetTypeRef == Emerald_AI.TargetType.NonAITarget){
				//Custom code damaging a non-AI object can be added here, if desired.
			}
			Collided = true;
			ProjectileCollider.enabled = false;
		}
		else if (!Collided && EmeraldSystem != null && ProjectileCurrentTarget != null && C.gameObject != ProjectileCurrentTarget.gameObject && C.gameObject != EmeraldSystem.gameObject && C.gameObject.layer != 2){
			Collided = true;
			ProjectileCollider.enabled = false;

			if (EffectOnCollisionRef == EffectOnCollision.Yes){
				if (CollisionEffect != null){
					SpawnedEffect = EmeraldObjectPool.Spawn(CollisionEffect, transform.position, Quaternion.identity);
					SpawnedEffect.transform.SetParent(Emerald_AI.ObjectPool.transform);
				}
			}
			if (ImpactSound != null && SoundOnCollisionRef == EffectOnCollision.Yes){
				CollisionSound = EmeraldObjectPool.Spawn(CollisionSoundObject, transform.position, Quaternion.identity);
				CollisionSound.transform.SetParent(Emerald_AI.ObjectPool.transform);
				AudioSource CollisionAudioSource = CollisionSound.GetComponent<AudioSource>();
				CollisionAudioSource.PlayOneShot(ImpactSound);
			}
		}
	}
}
