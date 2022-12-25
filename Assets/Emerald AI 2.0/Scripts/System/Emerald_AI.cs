using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
[SelectionBase]
[System.Serializable]
public class Emerald_AI : MonoBehaviour {

	Coroutine AttackCoroutine = null;
	public int IdleSoundsSeconds;
	public int IdleSoundsSecondsMin = 5;
	public int IdleSoundsSecondsMax = 10;
	public float IdleSoundsTimer;
	public TotalSoundsEnum IdleSoundsEnumRef = TotalSoundsEnum.None;
	public List<AudioClip> IdleSounds = new List<AudioClip>();
	public AudioClip IdleSound1, IdleSound2, IdleSound3, IdleSound4, IdleSound5;
	public enum DynamicMovement {No = 0, Yes = 1};
	public DynamicMovement DynamicMovementRef = DynamicMovement.Yes;
	public int AttacksToReposition;
	public int CurrentAttacks = 0;
	public int ExpandedDetectionRadius = 40;
	public int StartingDetectionRadius;
	public bool AttackDelay = false;
	public bool IsRunAttack = false;
	public int RandomDirection;
	public bool TargetInView = false;
	public Vector3 ProjectileFirePosition; 
	public float Attack1HeatSeekingSeconds = 1;
	public float Attack2HeatSeekingSeconds = 1;
	public float Attack3HeatSeekingSeconds = 1;
	public float RunAttackHeatSeekingSeconds = 1;
	public enum HeatSeeking {No = 0, Yes = 1};
	public HeatSeeking Attack1HeatSeekingRef = HeatSeeking.No;
	public HeatSeeking Attack2HeatSeekingRef = HeatSeeking.No;
	public HeatSeeking Attack3HeatSeekingRef = HeatSeeking.No;
	public HeatSeeking RunAttackHeatSeekingRef = HeatSeeking.No;

	[System.Serializable]
	public class AnimationClipOverride{
		public string clipNamed;
		public AnimationClip overrideWith;
	}
		
	[SerializeField]
	public int CurrentFaction;
	[SerializeField]
	public static List<string> StringFactionList = new List<string>();
	public enum RelationType {Enemy = 0, Neutral = 1, Friendly = 2};
	public RelationType RelationTypeRef;
	public List<int> FactionRelations = new List<int>();
	public int FactionRelation1;
	public int FactionRelation2;
	public int FactionRelation3;
	public int FactionRelation4;
	public int FactionRelation5;

	public static GameObject ObjectPool;
	public float ProjectileTimeoutTime = 4.5f;
	public float StartingRunSpeed;
	public float StartingRunAnimationSpeed;
	public float StationaryIdleTimer = 0;
	public int StationaryIdleSeconds;
	public int StationaryIdleSecondsMin = 3;
	public int StationaryIdleSecondsMax = 6;
	public GameObject Attack1CollisionEffect;
	public GameObject Attack2CollisionEffect;
	public GameObject Attack3CollisionEffect;
	public GameObject RunAttackCollisionEffect;
	public AudioClip Attack1ImpactSound;
	public AudioClip Attack2ImpactSound;
	public AudioClip Attack3ImpactSound;
	public AudioClip RunAttackImpactSound;
	public int Attack1ProjectileSpeed = 30;
	public int Attack2ProjectileSpeed = 30;
	public int Attack3ProjectileSpeed = 30;
	public int RunAttackProjectileSpeed = 30;
	public bool Attack1DisableOnCollision = true;
	public bool Attack2DisableOnCollision = true;
	public bool Attack3DisableOnCollision = true;
	public bool RunAttackDisableOnCollision = true;
	public bool Projectile1Updated = false;
	public bool Projectile2Updated = false;
	public bool Projectile3Updated = false;
	public bool RunProjectileUpdated = false;
	public bool BloodEffectUpdated = false;
	public float Attack1CollisionSeconds = 0f;
	public float Attack2CollisionSeconds = 0f;
	public float Attack3CollisionSeconds = 0f;
	public float RunAttackCollisionSeconds = 0f;
	public float Effect1TimeoutSeconds = 3f;
	public float Effect2TimeoutSeconds = 3f;
	public float Effect3TimeoutSeconds = 3f;
	public float RunEffectTimeoutSeconds = 3f;
	public float BloodEffectTimeoutSeconds = 3f;
	public float PlayerYOffset = 0;

	public GameObject Attack1Projectile;
	public GameObject Attack2Projectile;
	public GameObject Attack3Projectile;
	public GameObject RunAttackProjectile;
	public GameObject CurrentProjectile;
	public Vector3 ProjectileDirection;

	public List<GameObject> potentialTargets = new List<GameObject>();
	public List<Transform> LineOfSightTargets = new List<Transform>();

	public AnimationClip Idle1, Idle2, Idle3, Walk, Run, IdleAlert, IdleWarning, IdleCombat, Hit1, TurnLeft, TurnRight, RunAttack, Attack1, Attack2, Attack3, DeadAnim;
	[SerializeField]
	public List<AnimationClipOverride> clipOverrides = new List<AnimationClipOverride>();
	public Animator Anim;
	public AnimatorOverrideController overrideController;
	public string FilePath;
	public string EmeraldTag = "Respawn";
	public string UITag = "Player";
	public bool AnimationsUpdated = false;

	float RayCastUpdateSeconds = 0.1f;
	float RayCastUpdateTimer;
	float ObstructionDetectionUpdateSeconds = 0.1f;
	float ObstructionDetectionUpdateTimer;
	public int CurrentHealth;
	public int StartingHealth = 15;
	public int DetectionRadius = 18; 
	public int CurrentDamageAmount = 5;

	public int DamageAmount1 = 5;
	public int DamageAmount2 = 5;
	public int DamageAmount3 = 5;
	public int DamageAmountRun = 5;
	public int WanderRadius = 18; 
	public float TurnSpeed = 3;
	public int AlertWanderRadius = 24; 
	public int MinimumAlertWaitTime = 2; 
	public int MaximumAlertWaitTime = 4; 
	public int TabNumber = 0;
	public int TemperamentTabNumber = 0;
	public int DetectionTagsTabNumber = 0;
	public int AnimationTabNumber = 0;
	public int AttackAnimationNumber = 1;
	public int IdleAnimationNumber = 1;

	public int MinimumWaitTime = 3;
	public int MaximumWaitTime = 6;

	public int MinimumChaseWaitTime = 0;
	public int MaximumChaseWaitTime = 1;

	public int MinimumFollowWaitTime = 1;
	public int MaximumFollowWaitTime = 2;

	public int DeathDelayMin = 1;
	public int DeathDelayMax = 3;

	public float MinimumAttackSpeed = 0;
	public float MaximumAttackSpeed = 1.5f;

	public int MinimumDamageAmount1 = 2;
	public int MaximumDamageAmount1 = 5;
	public int MinimumDamageAmount2 = 2;
	public int MaximumDamageAmount2 = 5;
	public int MinimumDamageAmount3 = 2;
	public int MaximumDamageAmount3 = 5;
	public int MinimumDamageAmountRun = 2;
	public int MaximumDamageAmountRun = 5;

	public float WalkSpeed = 2;
	public float RunSpeed = 5;
	public int ExpandedFieldOfViewAngle = 300;
	public int fieldOfViewAngle = 180;
	public int fieldOfViewAngleRef;
	public float EyeHeight = 0.75f;
	public int MaxChaseDistance = 30;
	public int AngleNeededToTurn = 10;
	public int AngleNeededToTurnRunning = 30;
	public float TooCloseDistance = 2;
	public float AttackDistance = 4;
	public float RunAttackDistance = 3f;
	public int CautiousSeconds = 8;
	public int DeactivateSeconds = 5;
	public float HealthRegRate = 1;
	public int RegenerateAmount = 1;
	public int AILevel = 1;
	public string AIName = "AI Name";
	public string AITitle = "AI Title";

	public float StoppingDistance = 4;
	public float FollowingStoppingDistance = 5;
	public float DistanceOffset = 1;
	public float RunAttackDelay = 0.4f;
	public float Attack1Delay = 0.4f;
	public float Attack2Delay = 0.4f;
	public float Attack3Delay = 0.4f;
	public float DistanceFromTarget;
	public float AgentRadius = 0.5f;
	public float AgentBaseOffset = 0;
	public float AgentTurnSpeed = 2000;
	public float AgentAcceleration = 20;
	public float MaxXAngle = 15;
	public float MaxZAngle = 5;
	public int HealthPercentageToFlee = 10;
	public float AlignSpeed = 2f;

	public float Idle1AnimationSpeed = 1;
	public float Idle2AnimationSpeed = 1;
	public float Idle3AnimationSpeed = 1;
	public float IdleAlertAnimationSpeed = 1;
	public float IdleWarningAnimationSpeed = 1;
	public float IdleCombatAnimationSpeed = 1;
	public float WalkAnimationSpeed = 1;
	public float RunAnimationSpeed = 1;
	public float Attack1AnimationSpeed = 1;
	public float Attack2AnimationSpeed = 1;
	public float Attack3AnimationSpeed = 1;
	public float RunAttackAnimationSpeed = 1;
	public float TurnLeftAnimationSpeed = 1;
	public float TurnRightAnimationSpeed = 1;
	public float HitAnimationSpeed = 1;
	public float DeathAnimationSpeed = 1;

	public int StartingBehaviorRef;
	public int StartingConfidenceRef;

	public Vector3 SingleDestination;

	public Renderer Renderer1;
	public Renderer Renderer2;
	public Renderer Renderer3;
	public Renderer Renderer4;

	public enum CurrentBehavior {Passive = 0, Cautious = 1, Companion = 2, Aggressive = 3, Pet = 4};
	public CurrentBehavior BehaviorRef = CurrentBehavior.Passive;

	public enum DetectionType {Trigger = 0, LineOfSight = 1};
	public DetectionType DetectionTypeRef = DetectionType.Trigger;

	public enum MaxChaseDistanceType {TargetDistance = 0, StartingDistance = 1};
	public MaxChaseDistanceType MaxChaseDistanceTypeRef = MaxChaseDistanceType.TargetDistance;

	public enum ConfidenceType {Coward = 0, Brave = 1, Foolhardy = 2};
	public ConfidenceType ConfidenceRef = ConfidenceType.Brave;

	public enum OptimizedState {Active = 0, Inactive = 1}; //Excluded from Editor
	public OptimizedState OptimizedStateRef = OptimizedState.Active;

	public enum CurrentDetection {Alert = 0, Unaware = 1}; //Excluded from Editor
	public CurrentDetection CurrentDetectionRef = CurrentDetection.Unaware;

	public enum TargetType {Player = 0, AI = 1, NonAITarget = 2}; //Excluded from Editor
	public TargetType TargetTypeRef = TargetType.Player;

	public enum CombatState {NotActive = 0, Active = 1}; //Excluded from Editor
	public CombatState CombatStateRef = CombatState.NotActive;

	public enum CreateHealthBars {No = 0, Yes = 1};
	public CreateHealthBars CreateHealthBarsRef = CreateHealthBars.No;

	public enum UseCombatText {No = 0, Yes = 1};
	public UseCombatText UseCombatTextRef = UseCombatText.No;

	public enum CombatType {Offensive = 0, Defensive = 1};
	public CombatType CombatTypeRef = CombatType.Offensive;

	public enum RandomizeDamage {No = 0, Yes = 1};
	public RandomizeDamage RandomizeDamageRef = RandomizeDamage.Yes;

	public enum CustomizeHealthBar {No = 0, Yes = 1};
	public CustomizeHealthBar CustomizeHealthBarRef = CustomizeHealthBar.No;

	public enum UseCustomFont {No = 0, Yes = 1};
	public UseCustomFont UseCustomFontRef = UseCustomFont.No;

	public enum AnimateCombatText {Stationary = 0, Upwards = 1, Random = 2};
	public AnimateCombatText AnimateCombatTextRef = AnimateCombatText.Stationary;

	public enum DisplayAIName {No = 0, Yes = 1};
	public DisplayAIName DisplayAINameRef = DisplayAIName.No;

	public enum DisplayAITitle {No = 0, Yes = 1};
	public DisplayAITitle DisplayAITitleRef = DisplayAITitle.No;

	public enum DisplayAILevel {No = 0, Yes = 1};
	public DisplayAILevel DisplayAILevelRef = DisplayAILevel.No;

	public enum RefillHealthRTS {No = 0, Yes = 1};
	public RefillHealthRTS RefillHealthRTSRef = RefillHealthRTS.No;

	public enum OpposingFactionsEnum {One = 0, Two = 1, Three = 2, Four = 3, Five = 4};
	public OpposingFactionsEnum OpposingFactionsEnumRef = OpposingFactionsEnum.Two;

	public enum WanderType {Dynamic = 0, Waypoints = 1, Stationary = 2, Destination = 3};
	public WanderType WanderTypeRef = WanderType.Dynamic;

	public enum WaypointType {Loop = 0, Reverse = 1};
	public WaypointType WaypointTypeRef = WaypointType.Loop;

	public enum AlignAIWithGround {No = 0, Yes = 1};
	public AlignAIWithGround AlignAIWithGroundRef = AlignAIWithGround.No;

	public enum WanderAnimation {Walk = 0, Run = 1};
	public WanderAnimation WanderAnimationRef = WanderAnimation.Walk;

	public enum UseBloodEffect {Yes = 0, No = 1};
	public UseBloodEffect UseBloodEffectRef = UseBloodEffect.No;

	public enum AlignmentQuality {Low = 0, Medium = 1, High = 2};
	public AlignmentQuality AlignmentQualityRef = AlignmentQuality.Medium;

	public enum ObstructionDetectionQuality {Low = 0, Medium = 1, High = 2};
	public ObstructionDetectionQuality ObstructionDetectionQualityRef = ObstructionDetectionQuality.Medium;

	public enum TotalSoundsEnum {None = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5};
	public TotalSoundsEnum AttackSoundsEnumRef = TotalSoundsEnum.None;
	public TotalSoundsEnum InjuredSoundsEnumRef = TotalSoundsEnum.None;
	public TotalSoundsEnum WarningSoundsEnumRef = TotalSoundsEnum.None;
	public TotalSoundsEnum DeathSoundsEnumRef = TotalSoundsEnum.None;
	public TotalSoundsEnum FootStepSoundsEnumRef = TotalSoundsEnum.None;

	public enum TotalAttackAnimations {One = 0, Two = 1, Three = 2};
	public TotalAttackAnimations TotalAttackAnimationsRef = TotalAttackAnimations.Three;

	public enum TotalIdleAnimations {One = 0, Two = 1, Three = 2};
	public TotalIdleAnimations TotalIdleAnimationsRef = TotalIdleAnimations.One;

	public enum FollowSpeedType {Dynamic = 0, Manual = 1}; 
	public FollowSpeedType FollowSpeedTypeRef = FollowSpeedType.Dynamic;

	public enum FootstepSoundType {Seconds = 0, AnimationEvent = 1};
	public FootstepSoundType FootstepSoundTypeRef = FootstepSoundType.Seconds;

	public enum ActionAnimation {Eat = 0, Sleep = 1, Talk = 2, Work = 3, Interact = 4};
	public ActionAnimation ActionAnimationRef = ActionAnimation.Talk;

	public enum PickTargetMethod {Closest = 0, FirstDetected = 1};
	public PickTargetMethod PickTargetMethodRef = PickTargetMethod.Closest;

	public enum AIAttacksPlayer {Never = 0, Always = 1, OnlyIfAttacked = 2};
	public AIAttacksPlayer AIAttacksPlayerRef = AIAttacksPlayer.Always;

	public enum UseNonAITag {No = 0, Yes = 1};
	public UseNonAITag UseNonAITagRef = UseNonAITag.No;

	public enum UseRunAttacks {Yes = 0, No = 1};
	public UseRunAttacks UseRunAttacksRef = UseRunAttacks.No;

	public enum UseMagicEffectsPack {No = 0, Yes = 1};
	public UseMagicEffectsPack UseMagicEffectsPackRef = UseMagicEffectsPack.No;

	public enum WeaponType {Melee = 0, Ranged = 1};
	public WeaponType WeaponTypeRef = WeaponType.Melee;

	public enum EffectOnCollision {No = 0, Yes = 1};
	public EffectOnCollision Attack1EffectOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision Attack2EffectOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision Attack3EffectOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision RunAttackEffectOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision Attack1SoundOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision Attack2SoundOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision Attack3SoundOnCollisionRef = EffectOnCollision.No;
	public EffectOnCollision RunAttackSoundOnCollisionRef = EffectOnCollision.No;

	public enum AvoidanceQuality {None = 0, Low = 1, Medium = 2, Good = 3, High = 4};
	public AvoidanceQuality AvoidanceQualityRef = AvoidanceQuality.Medium;

	public Transform RangedAttackTransform;
	public bool DisableProjectileOnCollision = false;

	public Transform CurrentTarget;
	public Transform CurrentFollowTarget;

	public string PlayerTag = "Player";
	public string FollowTag = "Player";
	public string NonAITag = "Water";

	public enum YesOrNo {No = 0, Yes = 1}; 
	public YesOrNo UseAlertAnimationRef = YesOrNo.No;
	public YesOrNo UseRandomRotationOnStartRef = YesOrNo.No;
	public YesOrNo DisableAIWhenNotInViewRef = YesOrNo.No;
	public YesOrNo UseDeactivateDelayRef = YesOrNo.No;
	public YesOrNo AIRegeneratesHealthRef = YesOrNo.No;
	public YesOrNo UseWarningAnimationRef = YesOrNo.No;
	public YesOrNo AlignAIOnStartRef = YesOrNo.No;
	public YesOrNo UseSeparateAlertWaitTimeRef = YesOrNo.No;
	public YesOrNo UseSeparateAlertRadiusRef = YesOrNo.No;

	public enum TotalLODsEnum {Two = 0, Three = 1, Four = 2};
	public TotalLODsEnum TotalLODsRef = TotalLODsEnum.Three;
	public YesOrNo HasMultipleLODsRef = YesOrNo.No;

	public YesOrNo RecycleAIRef = YesOrNo.No;

	public bool ReturningToStartInProgress = false;
	public bool AIReachedDestination = false;

	public bool HealthBarsFoldout = true;
	public bool CombatTextFoldout = true;
	public bool NameTextFoldout = true;
	public bool WaypointsFoldout = true;

	public bool BehaviorFoldout = true;
	public bool ConfidenceFoldout = true;
	public bool WanderFoldout = true;
	public bool CombatStyleFoldout = true;
	public bool CurrentlyPlayingActionAnimation = false;
	public bool DamageSoundInhibitor = false;
	public bool DamageEffectInhibitor = false;

	public List<AudioClip> AttackSounds = new List<AudioClip>();
	public AudioClip AttackSound1, AttackSound2, AttackSound3, AttackSound4, AttackSound5;
	public List<AudioClip> InjuredSounds = new List<AudioClip>();
	public AudioClip InjuredSound1, InjuredSound2, InjuredSound3, InjuredSound4, InjuredSound5;
	public List<AudioClip> WarningSounds = new List<AudioClip>();
	public AudioClip WarningSound1, WarningSound2, WarningSound3, WarningSound4, WarningSound5;
	public List<AudioClip> DeathSounds = new List<AudioClip>();
	public AudioClip DeathSound1, DeathSound2, DeathSound3, DeathSound4, DeathSound5;
	public List<AudioClip> FootStepSounds = new List<AudioClip>();
	public AudioClip FootStepSound1, FootStepSound2, FootStepSound3, FootStepSound4, FootStepSound5;

	[SerializeField]
	public List<int> AIFactionsList = new List<int>(); //was TargetTags
	public int TargetTagsIndex;

	public int OpposingFaction1 = 1;
	public int OpposingFaction2 = 1;
	public int OpposingFaction3 = 1;
	public int OpposingFaction4 = 1;
	public int OpposingFaction5 = 1;

	public Vector3 BloodPosOffset;

	public GameObject BloodEffect;
	public GameObject HealthBarCanvas;
	public Sprite HealthBarImage;
	public Sprite HealthBarBackgroundImage;
	public Font CombatTextFont;
	public GameObject CombatTextObject;
	public Vector3 HealthBarPos = new Vector3(0,1.75f,0);
	public Vector3 CombatTextPos = new Vector3(0,1.75f,0);
	public Color HealthBarColor = new Color32(197,41,41,255);
	public Color HealthBarBackgroundColor = new Color32(36,36,36,255);
	public Color CombatTextColor = new Color32(197,41,41,255);
	public Color NameTextColor = new Color32(255,255,255,255);
	public Color LevelTextColor = new Color32(255,255,255,255);
	public Vector3 HealthBarScale = new Vector3(0.75f,1,1);
	public Vector2 NameTextSize = new Vector2(0.15f,0.15f);
	public GameObject WaypointParent;
	public string WaypointOrigin;
	public Vector3 AINamePos = new Vector3(0,3, 0);

	//Private
	int IdleParameter;
	int IdleCombatParameter;
	int IdleAlertParameter;
	int IdleWarningParameter;
	int WalkParameter;
	int RunParameter;
	int TurnLeftParameter;
	int TurnRightParameter;
	int RunAttackParameter;
	int HitParameter;

	bool WanderingInProgress = false;
	bool AttackingInProgress = false;
	bool ChasingDelay = false;
	bool IsTurning = false;
	bool FirstTimeInCombat = true;
	bool WarningAnimationTriggered = false;
	bool FollowingDelay = false;
	bool HealthRegInProgess = false;
	bool TargetObstructed = false;
	bool IsDead = false;

	float CurrentHealthFloat;
	float CurrentVelocity;
	float AttackSpeed = 1;

	int WaitTime = 5;
	int ChaseWaitTime = 1;
	int FollowWaitTime = 1;
	int CombatTextIndex = 0;
	int DamageReceived;

	Transform LineOfSightRef;
	public Transform PassiveTargetRef;
	public Transform CompanionTargetRef;
	NavMeshPath AIPath;
	public Animator AIAnimator;
	public NavMeshAgent AIAgent;
	Vector3 StartingDestination;
	SphereCollider AICollider;
	float CautiousTimer;
	public BoxCollider AIBoxCollider;
	AudioSource AIAudioSource;
	Renderer AIRenderer;
	Rigidbody AIRigidbody;
	public Emerald_AI TargetEmerald;
	GameObject HealthBar;
	GameObject CombatTextParent;
	List<TextMesh> CombatTextList = new List<TextMesh>();
	Vector3 previous;
	TextMesh TextName;
	TextMesh TextLevel;
	Canvas HealthBarCanvasRef;
	private Quaternion lookRotation;
	Vector3 GroundAngle;
	Quaternion  rot;
	public List<Vector3> WaypointsList = new List<Vector3>();
	private int destPoint = 0;
	int DestPointRef;
	bool DeathDelayActive = false;
	public GameObject WaypointHolder;
	Vector3 TurnDirection;
	Vector3 TurnDirectionOther;
	Quaternion TargetDirection;
	public float FootStepSecondsWalk = 0.5f;
	public float FootStepSecondsRun = 0.3f;
	float FootStepCounter = 0;
	NavMeshHit NMH;
	GameObject HitObject;
	Collider [] CollidersInArea;
	int ReceivedFaction; //Was string and ReceivedEmeraldTag
	Vector3 distanceBetween;
	float WaitForAttackAnimation;
	Vector3 NewPosition;
	bool BackingUp;
	float WanderAngle;
	float angleX;
	float angleZ;
	RaycastHit hit;
	Ray ray;
	float BackUpSeconds;
	bool DestinationUpdated = false;
	bool TargetInRange = false;
	Vector3 RandomOffset;
	float OffsetTimer;
	string StartingTag;
	int StartingLayer;

	public LayerMask DetectionLayerMask;
	public LayerMask ObstructionDetectionLayerMask = 4;

	void Start () {
		Initialize();
	}

	//Initialize all of our Emerald components and settings.
	void Initialize () {
		//If our Animator was somehow missed during the setup process, 
		//or unassigned by the user, get it during Initialization.
		if (AIAnimator == null){
			AIAnimator = GetComponent<Animator>();
		}
		GameObject AIColliderGameObject = new GameObject();
		AIColliderGameObject.name = "AI Collider";
		AIColliderGameObject.transform.SetParent(transform);
		AIColliderGameObject.transform.localPosition = new Vector3(0,0,0);
		AIColliderGameObject.AddComponent<SphereCollider>();
		AICollider = AIColliderGameObject.GetComponent<SphereCollider>();
		AICollider.radius = DetectionRadius;
		AICollider.isTrigger = true;
		AICollider.gameObject.layer = 2;
		AIAgent = GetComponent<NavMeshAgent>();

		AlignSpeed = 2.5f;
		EyeHeight = 0.75f;
		RandomOffset = Random.insideUnitSphere * FollowingStoppingDistance;
		StartingRunSpeed = RunSpeed;
		StartingRunAnimationSpeed = RunAnimationSpeed; 
		TargetObstructed = true;
		StartingTag = gameObject.tag;
		StartingLayer = gameObject.layer;

		if (ObjectPool == null){
			ObjectPool = new GameObject();
			ObjectPool.name = "Emerald Object Pool";
		}

		if (AIAgent == null){
			gameObject.AddComponent<NavMeshAgent>();
			AIAgent = GetComponent<NavMeshAgent>();
		}

		AIAgent.speed = WalkSpeed;
		AIPath = new NavMeshPath();
		AIAgent.CalculatePath(transform.position, AIPath);

		if (BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
			AIAgent.stoppingDistance = StoppingDistance;
		}
		else if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet){
			AIAgent.stoppingDistance = FollowingStoppingDistance;
		}

		AIAgent.radius = AgentRadius;
		AIAgent.baseOffset = AgentBaseOffset;
		AIAgent.angularSpeed = AgentTurnSpeed;
		AIAgent.acceleration = AgentAcceleration;

		if (AvoidanceQualityRef == AvoidanceQuality.None){
			AIAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
		}
		else if (AvoidanceQualityRef == AvoidanceQuality.Low){
			AIAgent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
		}
		else if (AvoidanceQualityRef == AvoidanceQuality.Medium){
			AIAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
		}
		else if (AvoidanceQualityRef == AvoidanceQuality.Good){
			AIAgent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
		}
		else if (AvoidanceQualityRef == AvoidanceQuality.High){
			AIAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
		}

		StartingDetectionRadius = DetectionRadius;
		StartingDestination = transform.position;
		WaitTime = Random.Range(MinimumWaitTime,MaximumWaitTime+1);
		IdleSoundsSeconds = Random.Range(IdleSoundsSecondsMin,IdleSoundsSecondsMax+1);
		StationaryIdleSeconds = Random.Range(StationaryIdleSecondsMin,StationaryIdleSecondsMax+1);
		CurrentHealth = StartingHealth;
		AIBoxCollider = GetComponent<BoxCollider>();
		AIAudioSource = GetComponent<AudioSource>();
		AIRigidbody = GetComponent<Rigidbody>();
		AIRigidbody.isKinematic = true;
		AIAgent.updateRotation = false;

		//Get a reference of the AI's field of view.
		fieldOfViewAngleRef = fieldOfViewAngle;

		//Companion AI cannot use the Line of Sight Detection Type. This is due to them possibly missing targets.
		if (DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight && BehaviorRef == Emerald_AI.CurrentBehavior.Companion 
			|| DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight && BehaviorRef == Emerald_AI.CurrentBehavior.Passive){
			DetectionTypeRef = DetectionType.Trigger;
		}

		if (BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
			if (gameObject.tag != "Untagged"){
				gameObject.tag = "Untagged";
			}
			if (gameObject.layer != 0){
				gameObject.layer = 0;
			}
			AIAttacksPlayerRef = AIAttacksPlayer.Never;
		}

		if (DisableAIWhenNotInViewRef == YesOrNo.Yes){
			AIRenderer = GetComponentInChildren<Renderer>();

			if (UseDeactivateDelayRef == YesOrNo.Yes && HasMultipleLODsRef == YesOrNo.No && AIRenderer != null){
				AIRenderer.gameObject.AddComponent<CheckRendererStatusWithDelay>();
				GetComponentInChildren<CheckRendererStatusWithDelay>().EmeraldComponent = GetComponentInChildren<Emerald_AI>();
			}
			else if (UseDeactivateDelayRef == YesOrNo.No && HasMultipleLODsRef == YesOrNo.No && AIRenderer != null){
				AIRenderer.gameObject.AddComponent<CheckRendererStatusWithoutDelay>();
				GetComponentInChildren<CheckRendererStatusWithoutDelay>().EmeraldComponent = GetComponentInChildren<Emerald_AI>();
			}
			else if (UseDeactivateDelayRef == YesOrNo.Yes || UseDeactivateDelayRef == YesOrNo.No){
				if (HasMultipleLODsRef == YesOrNo.No && AIRenderer == null){
					DisableAIWhenNotInViewRef = YesOrNo.No;
				}
			}
				
			if (!AIRenderer.isVisible && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				OptimizedStateRef = OptimizedState.Inactive;
			}

			if (HasMultipleLODsRef == YesOrNo.Yes){
				if (TotalLODsRef == TotalLODsEnum.Two){
					if(Renderer1 == null || Renderer2 == null){
						DisableAIWhenNotInViewRef = YesOrNo.No;
						HasMultipleLODsRef = YesOrNo.No;
					}
				}
				else if (TotalLODsRef == TotalLODsEnum.Three){
					if(Renderer1 == null || Renderer2 == null || Renderer3 == null){
						DisableAIWhenNotInViewRef = YesOrNo.No;
						HasMultipleLODsRef = YesOrNo.No;
					}
				}
				else if (TotalLODsRef == TotalLODsEnum.Four){
					if(Renderer1 == null || Renderer2 == null || 
						Renderer3 == null || Renderer4 == null){
						DisableAIWhenNotInViewRef = YesOrNo.No;
						HasMultipleLODsRef = YesOrNo.No;
					}
				}
			}
		}
		else if (DisableAIWhenNotInViewRef == YesOrNo.No){
			OptimizedStateRef = OptimizedState.Active;
		}
			
		if (UseRandomRotationOnStartRef == YesOrNo.Yes){
			transform.rotation = Quaternion.AngleAxis(Random.Range(5,360), Vector3.up);
		}

		if (CreateHealthBarsRef == CreateHealthBars.Yes && HealthBarCanvas == null || DisplayAINameRef == DisplayAIName.Yes && HealthBarCanvas == null){
			HealthBarCanvas = Resources.Load("AI Health Bar Canvas") as GameObject;
		}

		if (CreateHealthBarsRef == CreateHealthBars.Yes && HealthBarCanvas != null || DisplayAINameRef == DisplayAIName.Yes && HealthBarCanvas != null){
			HealthBar = Instantiate(HealthBarCanvas, Vector3.zero, Quaternion.identity) as GameObject;
			GameObject HealthBarParent = new GameObject();
			HealthBarParent.name = "HealthBarParent";
			HealthBarParent.transform.SetParent(this.transform);
			HealthBarParent.transform.localPosition = new Vector3(0,0,0);

			HealthBar.transform.SetParent(HealthBarParent.transform);
			HealthBar.transform.localPosition = HealthBarPos;
			HealthBar.AddComponent<EmeraldHealthBar>();
			EmeraldHealthBar HealthBarScript = HealthBar.GetComponent<EmeraldHealthBar>(); //Get a reference to the HealthBar script so it's still only called once.
			HealthBarScript.canvas = HealthBar.GetComponent<Canvas>();
			HealthBarScript.EmeraldComponent = GetComponent<Emerald_AI>();
			HealthBar.name = "AI Health Bar Canvas";

			GameObject HealthBarChild = HealthBar.transform.Find("AI Health Bar Background").gameObject;
			HealthBarChild.transform.localScale = HealthBarScale;

			Image HealthBarRef = HealthBarChild.transform.Find("AI Health Bar").GetComponent<Image>();
			HealthBarRef.color = HealthBarColor;

			Image HealthBarBackgroundImageRef = HealthBarChild.GetComponent<Image>();
			HealthBarBackgroundImageRef.color = HealthBarBackgroundColor;

			HealthBarCanvasRef = HealthBar.GetComponent<Canvas>();

			if (BehaviorRef == CurrentBehavior.Pet || CreateHealthBarsRef == CreateHealthBars.No){
				HealthBarChild.GetComponent<Image>().enabled = false;
				HealthBarRef.gameObject.SetActive(false);
			}

			if (CustomizeHealthBarRef == CustomizeHealthBar.Yes && HealthBarBackgroundImage != null && HealthBarImage != null){
				HealthBarBackgroundImageRef.sprite = HealthBarBackgroundImage;
				HealthBarRef.sprite = HealthBarImage;
			}

			//Displays and colors our AI's name text, if enabled.
			if (DisplayAINameRef == DisplayAIName.Yes){
				TextName = HealthBar.transform.Find("AI Name Text").gameObject.GetComponent<TextMesh>();

				//Reformat the AI's name so the title is diplayed below the AI's name, if this feature is enabled.
				if (DisplayAITitleRef == DisplayAITitle.Yes){
					AIName = AIName + "\\n" + AITitle;
					AIName = AIName.Replace("\\n","\n");
				}
					
				TextName.transform.localPosition = new Vector3(AINamePos.x/10,AINamePos.y/10,AINamePos.z/10);
				TextName.text = AIName;
				TextName.transform.localScale = NameTextSize;
				TextName.color = NameTextColor;
			}

			//Displays and colors our AI's level text, if enabled.
			if (DisplayAILevelRef == DisplayAILevel.Yes){
				TextLevel = HealthBar.transform.Find("AI Level Text").gameObject.GetComponent<TextMesh>();
				TextLevel.text = AILevel.ToString();
				TextLevel.color = LevelTextColor;
			}

			//Add disable to return to start and slight delay
			if (BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				HealthBarCanvasRef.enabled = false;
				if (CreateHealthBarsRef == CreateHealthBars.No){
					HealthBarBackgroundImageRef.gameObject.SetActive(false);
				}
				if (TextName != null && DisplayAINameRef == DisplayAIName.Yes){
					TextName.gameObject.SetActive(false);
				}
				if (TextLevel != null && DisplayAILevelRef == DisplayAILevel.Yes){
					TextLevel.gameObject.SetActive(false);
				}
			}
			else if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet){ 
				SetUI(true);
			}
		}

		if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject == null){
			CombatTextObject = Resources.Load("AI Combat Text") as GameObject;
		}

		if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null){
			CombatTextParent = Instantiate(CombatTextObject, Vector3.zero, Quaternion.identity) as GameObject;
			CombatTextParent.transform.SetParent(this.transform);
			CombatTextParent.transform.localPosition = new Vector3(CombatTextPos.x, CombatTextPos.y/transform.localScale.y, CombatTextPos.z);

			//Store the combat texts into a list so they can be accessed when needed.
			foreach (Transform T in CombatTextParent.transform){
				CombatTextList.Add(T.GetComponent<TextMesh>());
			}
				
			//Control the color and font of the combat text
			foreach (TextMesh Txt in CombatTextList){
				Txt.color = CombatTextColor;
				Txt.GetComponent<CombatTextAnimator>().StartingPos = Vector3.zero;

				//Controls whether or not our AI's combat text is animated.
				if (AnimateCombatTextRef == AnimateCombatText.Stationary){
					Txt.GetComponent<CombatTextAnimator>().AnimateTextType = 0;
				}
				else if (AnimateCombatTextRef == AnimateCombatText.Upwards){
					Txt.GetComponent<CombatTextAnimator>().AnimateTextType = 1;
				}
				else if (AnimateCombatTextRef == AnimateCombatText.Random){
					Txt.GetComponent<CombatTextAnimator>().AnimateTextType = 2;
				}

				if (UseCustomFontRef == UseCustomFont.Yes && CombatTextFont != null){
					Txt.font = CombatTextFont;
				}
			}

			//Set the combat text canvas to false. It will only be enabled when needed.
			CombatTextParent.SetActive(false);
		}

		GetDamageAmount();

		CombatStateRef = CombatState.NotActive;
		StartingBehaviorRef = (int)BehaviorRef;
		StartingConfidenceRef = (int)ConfidenceRef;

		AttackSpeed = Random.Range(MinimumAttackSpeed,MaximumAttackSpeed);
		AttackSpeed = Mathf.Round(AttackSpeed * 10f) / 10f;
		AttacksToReposition = Random.Range(2,4);

		if (AttackSpeed == 0){
			AttackSpeed = 1;
		}

		AIAgent.updateUpAxis = false;
		lookRotation = transform.rotation;

		if (RangedAttackTransform == null){
			RangedAttackTransform = this.transform;
		}

		if (WanderTypeRef == WanderType.Destination){
			AIAgent.autoBraking = false;
		}
		else if (WanderTypeRef == WanderType.Waypoints){
			AIAgent.autoBraking = false;
		}

		//Due to limitations in Unity, lists cannot be serialized. So, tags are stored
		//in individual serialized strings and added to the Tag list on Start.
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.One){
			AIFactionsList.Add(OpposingFaction1);
			FactionRelations.Add(FactionRelation1);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Two){
			AIFactionsList.Add(OpposingFaction1);
			AIFactionsList.Add(OpposingFaction2);
			FactionRelations.Add(FactionRelation1);
			FactionRelations.Add(FactionRelation2);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Three){
			AIFactionsList.Add(OpposingFaction1);
			AIFactionsList.Add(OpposingFaction2);
			AIFactionsList.Add(OpposingFaction3);
			FactionRelations.Add(FactionRelation1);
			FactionRelations.Add(FactionRelation2);
			FactionRelations.Add(FactionRelation3);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Four){
			AIFactionsList.Add(OpposingFaction1);
			AIFactionsList.Add(OpposingFaction2);
			AIFactionsList.Add(OpposingFaction3);
			AIFactionsList.Add(OpposingFaction4);
			FactionRelations.Add(FactionRelation1);
			FactionRelations.Add(FactionRelation2);
			FactionRelations.Add(FactionRelation3);
			FactionRelations.Add(FactionRelation4);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Five){
			AIFactionsList.Add(OpposingFaction1);
			AIFactionsList.Add(OpposingFaction2);
			AIFactionsList.Add(OpposingFaction3);
			AIFactionsList.Add(OpposingFaction4);
			AIFactionsList.Add(OpposingFaction5);
			FactionRelations.Add(FactionRelation1);
			FactionRelations.Add(FactionRelation2);
			FactionRelations.Add(FactionRelation3);
			FactionRelations.Add(FactionRelation4);
			FactionRelations.Add(FactionRelation5);
		}

		//Due to limitations in Unity, lists cannot be serialized. So, sounds are stored
		//in individual serialized AudioClips and added to the sounds list on Start.
		//Attack
		if (AttackSoundsEnumRef == TotalSoundsEnum.One){
			AttackSounds.Add(AttackSound1);
		}
		if (AttackSoundsEnumRef == TotalSoundsEnum.Two){
			AttackSounds.Add(AttackSound1);
			AttackSounds.Add(AttackSound2);
		}
		if (AttackSoundsEnumRef == TotalSoundsEnum.Three){
			AttackSounds.Add(AttackSound1);
			AttackSounds.Add(AttackSound2);
			AttackSounds.Add(AttackSound3);
		}
		if (AttackSoundsEnumRef == TotalSoundsEnum.Four){
			AttackSounds.Add(AttackSound1);
			AttackSounds.Add(AttackSound2);
			AttackSounds.Add(AttackSound3);
			AttackSounds.Add(AttackSound4);
		}
		if (AttackSoundsEnumRef == TotalSoundsEnum.Five){
			AttackSounds.Add(AttackSound1);
			AttackSounds.Add(AttackSound2);
			AttackSounds.Add(AttackSound3);
			AttackSounds.Add(AttackSound4);
			AttackSounds.Add(AttackSound5);
		}

		//Injured
		if (InjuredSoundsEnumRef == TotalSoundsEnum.One){
			InjuredSounds.Add(InjuredSound1);
		}
		if (InjuredSoundsEnumRef == TotalSoundsEnum.Two){
			InjuredSounds.Add(InjuredSound1);
			InjuredSounds.Add(InjuredSound2);
		}
		if (InjuredSoundsEnumRef == TotalSoundsEnum.Three){
			InjuredSounds.Add(InjuredSound1);
			InjuredSounds.Add(InjuredSound2);
			InjuredSounds.Add(InjuredSound3);
		}
		if (InjuredSoundsEnumRef == TotalSoundsEnum.Four){
			InjuredSounds.Add(InjuredSound1);
			InjuredSounds.Add(InjuredSound2);
			InjuredSounds.Add(InjuredSound3);
			InjuredSounds.Add(InjuredSound4);
		}
		if (InjuredSoundsEnumRef == TotalSoundsEnum.Five){
			InjuredSounds.Add(InjuredSound1);
			InjuredSounds.Add(InjuredSound2);
			InjuredSounds.Add(InjuredSound3);
			InjuredSounds.Add(InjuredSound4);
			InjuredSounds.Add(InjuredSound5);
		}

		//Warning
		if (WarningSoundsEnumRef == TotalSoundsEnum.One){
			WarningSounds.Add(WarningSound1);
		}
		if (WarningSoundsEnumRef == TotalSoundsEnum.Two){
			WarningSounds.Add(WarningSound1);
			WarningSounds.Add(WarningSound2);
		}
		if (WarningSoundsEnumRef == TotalSoundsEnum.Three){
			WarningSounds.Add(WarningSound1);
			WarningSounds.Add(WarningSound2);
			WarningSounds.Add(WarningSound3);
		}
		if (WarningSoundsEnumRef == TotalSoundsEnum.Four){
			WarningSounds.Add(WarningSound1);
			WarningSounds.Add(WarningSound2);
			WarningSounds.Add(WarningSound3);
			WarningSounds.Add(WarningSound4);
		}
		if (WarningSoundsEnumRef == TotalSoundsEnum.Five){
			WarningSounds.Add(WarningSound1);
			WarningSounds.Add(WarningSound2);
			WarningSounds.Add(WarningSound3);
			WarningSounds.Add(WarningSound4);
			WarningSounds.Add(WarningSound5);
		}

		//Death
		if (DeathSoundsEnumRef == TotalSoundsEnum.One){
			DeathSounds.Add(DeathSound1);
		}
		if (DeathSoundsEnumRef == TotalSoundsEnum.Two){
			DeathSounds.Add(DeathSound1);
			DeathSounds.Add(DeathSound2);
		}
		if (DeathSoundsEnumRef == TotalSoundsEnum.Three){
			DeathSounds.Add(DeathSound1);
			DeathSounds.Add(DeathSound2);
			DeathSounds.Add(DeathSound3);
		}
		if (DeathSoundsEnumRef == TotalSoundsEnum.Four){
			DeathSounds.Add(DeathSound1);
			DeathSounds.Add(DeathSound2);
			DeathSounds.Add(DeathSound3);
			DeathSounds.Add(DeathSound4);
		}
		if (DeathSoundsEnumRef == TotalSoundsEnum.Five){
			DeathSounds.Add(DeathSound1);
			DeathSounds.Add(DeathSound2);
			DeathSounds.Add(DeathSound3);
			DeathSounds.Add(DeathSound4);
			DeathSounds.Add(DeathSound5);
		}

		//Footstep
		if (FootStepSoundsEnumRef == TotalSoundsEnum.One){
			FootStepSounds.Add(FootStepSound1);
		}
		if (FootStepSoundsEnumRef == TotalSoundsEnum.Two){
			FootStepSounds.Add(FootStepSound1);
			FootStepSounds.Add(FootStepSound2);
		}
		if (FootStepSoundsEnumRef == TotalSoundsEnum.Three){
			FootStepSounds.Add(FootStepSound1);
			FootStepSounds.Add(FootStepSound2);
			FootStepSounds.Add(FootStepSound3);
		}
		if (FootStepSoundsEnumRef == TotalSoundsEnum.Four){
			FootStepSounds.Add(FootStepSound1);
			FootStepSounds.Add(FootStepSound2);
			FootStepSounds.Add(FootStepSound3);
			FootStepSounds.Add(FootStepSound4);
		}
		if (FootStepSoundsEnumRef == TotalSoundsEnum.Five){
			FootStepSounds.Add(FootStepSound1);
			FootStepSounds.Add(FootStepSound2);
			FootStepSounds.Add(FootStepSound3);
			FootStepSounds.Add(FootStepSound4);
			FootStepSounds.Add(FootStepSound5);
		}

		if (IdleSoundsEnumRef == TotalSoundsEnum.One){
			IdleSounds.Add(IdleSound1);
		}
		if (IdleSoundsEnumRef == TotalSoundsEnum.Two){
			IdleSounds.Add(IdleSound1);
			IdleSounds.Add(IdleSound2);
		}
		if (IdleSoundsEnumRef == TotalSoundsEnum.Three){
			IdleSounds.Add(IdleSound1);
			IdleSounds.Add(IdleSound2);
			IdleSounds.Add(IdleSound3);
		}
		if (IdleSoundsEnumRef == TotalSoundsEnum.Four){
			IdleSounds.Add(IdleSound1);
			IdleSounds.Add(IdleSound2);
			IdleSounds.Add(IdleSound3);
			IdleSounds.Add(IdleSound4);
		}
		if (IdleSoundsEnumRef == TotalSoundsEnum.Five){
			IdleSounds.Add(IdleSound1);
			IdleSounds.Add(IdleSound2);
			IdleSounds.Add(IdleSound3);
			IdleSounds.Add(IdleSound4);
			IdleSounds.Add(IdleSound5);
		}

		//Only update our animation speed multipliers if the value is different than the default value.
		if (RunAnimationSpeed != 1){AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);}
		if (Idle1AnimationSpeed != 1){AIAnimator.SetFloat("Idle 1 Speed", Idle1AnimationSpeed);}
		if (Idle2AnimationSpeed != 1){AIAnimator.SetFloat("Idle 2 Speed", Idle2AnimationSpeed);}
		if (Idle3AnimationSpeed != 1){AIAnimator.SetFloat("Idle 3 Speed", Idle3AnimationSpeed);}
		if (IdleAlertAnimationSpeed != 1){AIAnimator.SetFloat("Idle Alert Speed", IdleAlertAnimationSpeed);}
		if (IdleWarningAnimationSpeed != 1){AIAnimator.SetFloat("Idle Warning Speed", IdleWarningAnimationSpeed);}
		if (IdleCombatAnimationSpeed != 1){AIAnimator.SetFloat("Idle Combat Speed", IdleCombatAnimationSpeed);}
		if (WalkAnimationSpeed != 1){AIAnimator.SetFloat("Walk Speed", WalkAnimationSpeed);}
		if (Attack1AnimationSpeed != 1){AIAnimator.SetFloat("Attack 1 Speed", Attack1AnimationSpeed);}
		if (Attack2AnimationSpeed != 1){AIAnimator.SetFloat("Attack 2 Speed", Attack2AnimationSpeed);}
		if (Attack3AnimationSpeed != 1){AIAnimator.SetFloat("Attack 3 Speed", Attack3AnimationSpeed);}
		if (RunAttackAnimationSpeed != 1){AIAnimator.SetFloat("Run Attack Speed", RunAttackAnimationSpeed);}
		if (TurnLeftAnimationSpeed != 1){AIAnimator.SetFloat("Turn Left Speed", TurnLeftAnimationSpeed);}
		if (TurnRightAnimationSpeed != 1){AIAnimator.SetFloat("Turn Right Speed", TurnRightAnimationSpeed);}
		if (HitAnimationSpeed != 1){AIAnimator.SetFloat("Hit Speed", HitAnimationSpeed);}
		if (DeathAnimationSpeed != 1){AIAnimator.SetFloat("Death Speed", DeathAnimationSpeed);}

		//Get the animator hash values because they are quicker than comparing a string
		GetAnimatorHashValues();

		if (AlignAIOnStartRef == YesOrNo.Yes && AlignAIWithGroundRef == AlignAIWithGround.Yes){
			AlignOnStart();
		}

		if (AlignmentQualityRef == AlignmentQuality.Low){
			RayCastUpdateSeconds = 0.3f;
		}
		else if (AlignmentQualityRef == AlignmentQuality.Medium){
			RayCastUpdateSeconds = 0.2f;
		}
		else if (AlignmentQualityRef == AlignmentQuality.High){
			RayCastUpdateSeconds = 0.1f;
		}

		if (ObstructionDetectionQualityRef == ObstructionDetectionQuality.Low){
			RayCastUpdateSeconds = 0.75f;
		}
		else if (ObstructionDetectionQualityRef == ObstructionDetectionQuality.Medium){
			RayCastUpdateSeconds = 0.5f;
		}
		else if (ObstructionDetectionQualityRef == ObstructionDetectionQuality.High){
			RayCastUpdateSeconds = 0.25f;
		}
			
		CheckEditorUpdateButtons();
	}

	void CheckEditorUpdateButtons (){
		if (AnimationsUpdated){
			Debug.Log("The AI " + gameObject.name + "'s animations have been modified, but not updated. To update, go to the Animations tab in its Emerald Editor and press the 'Update Animator Controller' button.");
		}
		if (Projectile1Updated){
			Debug.Log("The AI " + gameObject.name + "'s Projectile 1 has been modified, but not updated. To update, go to the AI's Settings tab under the Combat section, in its Emerald Editor, and press the 'Update Projectile 1' button.");
		}
		if (Projectile2Updated){
			Debug.Log("The AI " + gameObject.name + "'s Projectile 2 has been modified, but not updated. To update, go to the AI's Settings tab under the Combat section, in its Emerald Editor, and press the 'Update Projectile 2' button.");
		}
		if (Projectile3Updated){
			Debug.Log("The AI " + gameObject.name + "'s Projectile 3 has been modified, but not updated. To update, go to the AI's Settings tab under the Combat section, in its Emerald Editor, and press the 'Update Projectile 3' button.");
		}
		if (RunProjectileUpdated){
			Debug.Log("The AI " + gameObject.name + "'s Run Projectile has been modified, but not updated. To update, go to the AI's Settings tab under the Combat section, in its Emerald Editor, and press the 'Update Run Projectile' button.");
		}
		if (BloodEffectUpdated){
			Debug.Log("The AI " + gameObject.name + "'s Hit Effect has been modified, but not updated. To update, go to the AI's Settings tab under the Combat section, in its Emerald Editor, and press the 'Update Hit Effect' button.");
		}
	}

	void GetAnimatorHashValues (){
		IdleParameter = Animator.StringToHash("Idle 1");
		IdleCombatParameter = Animator.StringToHash("Idle (Combat)");
		IdleAlertParameter = Animator.StringToHash("Idle (Alert)");
		IdleWarningParameter = Animator.StringToHash("Idle (Warning)");
		WalkParameter = Animator.StringToHash("Walk");
		RunParameter = Animator.StringToHash("Run");
		TurnLeftParameter = Animator.StringToHash("Turn Left");
		TurnRightParameter = Animator.StringToHash("Turn Right");
		RunAttackParameter = Animator.StringToHash("Run Attack");
		HitParameter = Animator.StringToHash("Hit 1");
	}

	void GetDamageAmount (){
		if (RandomizeDamageRef == RandomizeDamage.Yes){
			if (AttackAnimationNumber == 1){
				CurrentDamageAmount = Random.Range(MinimumDamageAmount1, MaximumDamageAmount1+1);
			}
			else if (AttackAnimationNumber == 2){
				CurrentDamageAmount = Random.Range(MinimumDamageAmount2, MaximumDamageAmount2+1);
			}
			else if (AttackAnimationNumber == 3){
				CurrentDamageAmount = Random.Range(MinimumDamageAmount3, MaximumDamageAmount3+1);
			}
		}
		else if (RandomizeDamageRef == RandomizeDamage.No){
			if (AttackAnimationNumber == 1){
				CurrentDamageAmount = DamageAmount1;
			}
			else if (AttackAnimationNumber == 2){
				CurrentDamageAmount = DamageAmount2;
			}
			else if (AttackAnimationNumber == 3){
				CurrentDamageAmount = DamageAmount3;
			}
		}
	}

	void DetectTarget (GameObject C){
		if (DetectionTypeRef == DetectionType.Trigger){
			if (C.CompareTag(PlayerTag) && AIAttacksPlayerRef != AIAttacksPlayer.Never){
				TargetTypeRef = TargetType.Player;
				CurrentDetectionRef = CurrentDetection.Alert;

				if (BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Passive && BehaviorRef != CurrentBehavior.Pet){
					CombatStateRef = CombatState.Active;

					//Pick our target depending on the AI's options
					if (PickTargetMethodRef == PickTargetMethod.FirstDetected){
						CurrentTarget = C.transform;
					}
					else if (PickTargetMethodRef == PickTargetMethod.Closest){
						SearchForTarget();
					}
				}
				else if (BehaviorRef == CurrentBehavior.Companion){
					if (CombatTypeRef == CombatType.Defensive){
						CompanionTargetRef = C.transform;
					}
					else if (CombatTypeRef == CombatType.Offensive){
						CompanionTargetRef = C.transform;
					}
				}
				else if (BehaviorRef == CurrentBehavior.Passive){
					PassiveTargetRef = C.transform;
				}
			}
			else if (C.CompareTag(EmeraldTag)){
				if (C.GetComponent<Emerald_AI>() != null){
					ReceivedFaction = C.GetComponent<Emerald_AI>().CurrentFaction;
				}
				if (AIFactionsList.Contains(ReceivedFaction) && FactionRelations[AIFactionsList.IndexOf(ReceivedFaction)] == 0){
					if (C.GetComponent<Emerald_AI>() != null){
						TargetEmerald = C.GetComponent<Emerald_AI>();
						TargetTypeRef = TargetType.AI;
					}
					CurrentDetectionRef = CurrentDetection.Alert;

					if (BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Passive && BehaviorRef != CurrentBehavior.Pet){
						CombatStateRef = CombatState.Active;

						//Pick our target depending on the AI's options
						if (PickTargetMethodRef == PickTargetMethod.FirstDetected){
							CurrentTarget = C.transform;
						}
						else if (PickTargetMethodRef == PickTargetMethod.Closest){
							SearchForTarget();
						}
					}
					else if (BehaviorRef == CurrentBehavior.Companion){
						if (CombatTypeRef == CombatType.Defensive){
							CompanionTargetRef = C.transform;
						}
						else if (CombatTypeRef == CombatType.Offensive){
							CompanionTargetRef = C.transform;
						}
					}
					else if (BehaviorRef == CurrentBehavior.Passive){
						PassiveTargetRef = C.transform;
					}
				}
			}
			else if (C.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
				if (C.GetComponent<Emerald_AI>() == null){
					TargetTypeRef = TargetType.NonAITarget;
				}
				CurrentDetectionRef = CurrentDetection.Alert;

				if (BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Passive && BehaviorRef != CurrentBehavior.Pet){
					CombatStateRef = CombatState.Active;

					//Pick our target depending on the AI's options
					if (PickTargetMethodRef == PickTargetMethod.FirstDetected){
						CurrentTarget = C.transform;
					}
					else if (PickTargetMethodRef == PickTargetMethod.Closest){
						SearchForTarget();
					}
				}
				else if (BehaviorRef == CurrentBehavior.Companion){
					if (CombatTypeRef == CombatType.Defensive){
						CompanionTargetRef = C.transform;
					}
					else if (CombatTypeRef == CombatType.Offensive){
						CompanionTargetRef = C.transform;
					}
				}
				else if (BehaviorRef == CurrentBehavior.Passive){
					PassiveTargetRef = C.transform;
				}
			}
		}
		else if (DetectionTypeRef == DetectionType.LineOfSight){
			if (C.CompareTag(PlayerTag) && !LineOfSightTargets.Contains(C.transform) && AIAttacksPlayerRef == AIAttacksPlayer.Always){
				TargetTypeRef = TargetType.Player;
				CurrentDetectionRef = CurrentDetection.Alert;
				LineOfSightTargets.Add(C.transform);
			}
			else if (C.CompareTag(EmeraldTag)){
				if (C.GetComponent<Emerald_AI>() != null){
					ReceivedFaction = C.GetComponent<Emerald_AI>().CurrentFaction;
				}
				if (AIFactionsList.Contains(ReceivedFaction) && FactionRelations[AIFactionsList.IndexOf(ReceivedFaction)] == 0 && !LineOfSightTargets.Contains(C.transform)){
					if (C.GetComponent<Emerald_AI>() != null){
						TargetEmerald = C.GetComponent<Emerald_AI>();
						TargetTypeRef = TargetType.AI;
					}
					CurrentDetectionRef = CurrentDetection.Alert;
					LineOfSightTargets.Add(C.transform);
				}
			}
			else if (C.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes && !LineOfSightTargets.Contains(C.transform)){
				if (C.GetComponent<Emerald_AI>() == null){
					TargetTypeRef = TargetType.NonAITarget;
				}
				CurrentDetectionRef = CurrentDetection.Alert;
				LineOfSightTargets.Add(C.transform);
			}
		}
	}

	//This is responsible for all of our detection mechanics. This OnTriggerEnter function will only set
	//targets whose tag exists in the AI's target tags.
	void OnTriggerEnter(Collider C) {
		if (CurrentTarget == null){
			if (C.CompareTag(EmeraldTag) || C.CompareTag(PlayerTag)  && AIAttacksPlayerRef != AIAttacksPlayer.Never || C.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
				if (DetectionTypeRef == DetectionType.Trigger){
					DetectTarget(C.gameObject);
				}
				else if (DetectionTypeRef == DetectionType.LineOfSight && CurrentTarget == null){
					DetectTarget(C.gameObject);
				}
			}
		}

		if (C.CompareTag(FollowTag) && C.tag != "Untagged" && CurrentFollowTarget == null){
			if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet){
				CurrentFollowTarget = C.transform;
				if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null){
					CombatTextParent.SetActive(true);
				}
			}
		}

		if (C.CompareTag(UITag)){
			if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				SetUI(true);
			}

			if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				CombatTextParent.SetActive(true);
			}
		}
	}

	void OnTriggerExit(Collider C) {
		if (BehaviorRef != CurrentBehavior.Pet && C.GetComponent<Emerald_AI>() != null){
			if (C.CompareTag(EmeraldTag)){
				ReceivedFaction = C.GetComponent<Emerald_AI>().CurrentFaction;
			}

			if (AIFactionsList.Contains(ReceivedFaction) && FactionRelations[AIFactionsList.IndexOf(ReceivedFaction)] == 0 
				|| C.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always 
				|| C.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
				if (DetectionTypeRef == DetectionType.LineOfSight && CurrentTarget == null && LineOfSightTargets.Contains(C.transform)){
					if (LineOfSightTargets.Count <= 1){
						StopCoroutine("Attack");
						CurrentDetectionRef = CurrentDetection.Unaware;
						CombatStateRef = CombatState.NotActive;
						FirstTimeInCombat = true;
						AttackingInProgress = false;
						AttackDelay = false;
					}
						
					LineOfSightTargets.Remove(C.transform);
					TargetEmerald = null;
				}
				fieldOfViewAngle = fieldOfViewAngleRef;
			}

			if (DetectionTypeRef == DetectionType.Trigger && DistanceFromTarget >= DetectionRadius && CombatStateRef == CombatState.NotActive){
				StopCoroutine("Attack");
				AttackingInProgress = false;
				AttackDelay = false;
				WarningAnimationTriggered = false;
				WanderingInProgress = false;
				FirstTimeInCombat = true;
				ReturningToStartInProgress = true;
				CautiousTimer = 0;

				if (CombatStateRef == CombatState.NotActive){
					CurrentDetectionRef = CurrentDetection.Unaware;
					CompanionTargetRef = null;
					TargetEmerald = null;
					CurrentTarget = null;
				}
			}
				
			if (BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward){
				if (DetectionTypeRef == DetectionType.LineOfSight && CurrentTarget != null && LineOfSightTargets.Contains(C.transform)){
					LineOfSightTargets.Remove(C.transform);
					TargetEmerald = null;
				}

				StopCoroutine("Attack");
				AttackingInProgress = false;
				AttackDelay = false;
				WarningAnimationTriggered = false;
				WanderingInProgress = false;
				FirstTimeInCombat = true;
				ReturningToStartInProgress = true;
				CautiousTimer = 0;
				CombatStateRef = CombatState.NotActive;
				TargetEmerald = null;
				CurrentTarget = null;
				CurrentDetectionRef = CurrentDetection.Unaware;
				LineOfSightRef = null;
				fieldOfViewAngle = fieldOfViewAngleRef;
				IsTurning = false;
			}
		}

		if (C.CompareTag(UITag)){
			if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				SetUI(false);
			}

			if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				CombatTextParent.SetActive(false);
			}
		}
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.O)){
			GameObject AI = GameObject.Find("Skeleton Aggressive - Ranged");
			AI.GetComponent<Emerald_AI>().UpdateTargetTags("Creature", "Undead", "NPC", "Animal", null, null);
		}

		if (HasMultipleLODsRef == YesOrNo.Yes){
			if (TotalLODsRef == TotalLODsEnum.Two){
				if(Renderer1.isVisible || Renderer2.isVisible){
					OptimizedStateRef = OptimizedState.Active;
				}
				else if(!Renderer1.isVisible && !Renderer2.isVisible){
					OptimizedStateRef = OptimizedState.Inactive;
				}
			}
			else if (TotalLODsRef == TotalLODsEnum.Three){
				if(Renderer1.isVisible || Renderer2.isVisible || Renderer3.isVisible){
					OptimizedStateRef = OptimizedState.Active;
				}
				else if(!Renderer1.isVisible && !Renderer2.isVisible && !Renderer3.isVisible){
					OptimizedStateRef = OptimizedState.Inactive;
				}
			}
			else if (TotalLODsRef == TotalLODsEnum.Four){
				if(Renderer1.isVisible || Renderer2.isVisible || Renderer3.isVisible || Renderer4.isVisible){
					OptimizedStateRef = OptimizedState.Active;
				}
				else if(!Renderer1.isVisible && !Renderer2.isVisible && !Renderer3.isVisible && !Renderer4.isVisible){
					OptimizedStateRef = OptimizedState.Inactive;
				}
			}
		}

			
		//Only run the system if the OptimizedState is set to active. This is to help 
		//with performance by not running AI that are currently not visible by the camera or whose
		//LOD Group is currently Culled.
		if (OptimizedStateRef == OptimizedState.Active){
			//Default Action - If there is no target, Wander. Play our AI's walk and Idle animations depending on
			//if our AI is moving or stationary, but only if they are currently alive and visible.
			if (CurrentTarget == null && CurrentHealth > 0 && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				//If our CurrentTarget is null, search for another target before returning to wandering.
				if (CombatStateRef == CombatState.Active){
					SearchForTarget();
				}
				if (WanderTypeRef == WanderType.Dynamic && !WanderingInProgress && !DeathDelayActive){
					if (AIAgent.remainingDistance <= AIAgent.stoppingDistance){
						StartCoroutine("Wander");
					}
				}
				else if (WanderTypeRef == WanderType.Waypoints){
					if (AIAgent.remainingDistance < 1f && !DeathDelayActive && !AIAgent.pathPending){ 
						AIAgent.stoppingDistance = 0;
						NextWaypoint();
					}
					AIAgent.stoppingDistance = 0;
					StartingDestination = transform.position;
				}
				else if (WanderTypeRef == WanderType.Stationary){
					
				}
				else if (WanderTypeRef == WanderType.Destination && !AIReachedDestination && !DeathDelayActive && !IsTurning){
					if (AIAgent.destination != SingleDestination && !DestinationUpdated){
						AIAgent.SetDestination(SingleDestination);
						DestinationUpdated = true;
					}
					if (AIAgent.remainingDistance <= AIAgent.stoppingDistance && !AIAgent.pathPending){
						AIReachedDestination = true;
					}
					StartingDestination = transform.position;
				}
					
				if (AIAgent.remainingDistance <= AIAgent.stoppingDistance && !IsTurning){
					AnimationIdle();

					IdleSoundsTimer += Time.deltaTime;

					if (IdleSoundsTimer >= IdleSoundsSeconds){
						if (IdleSounds.Count != 0){
							AIAudioSource.PlayOneShot(IdleSounds[Random.Range(0, IdleSounds.Count)]);
							IdleSoundsSeconds = Random.Range(IdleSoundsSecondsMin,IdleSoundsSecondsMax+1);
							IdleSoundsTimer = 0;
						}
					}
				}
				else if (AIAgent.remainingDistance > AIAgent.stoppingDistance && !AIAgent.isStopped && !IsTurning){ //USed to be just else
					if (WanderAnimationRef == WanderAnimation.Walk){
						AIAgent.speed = WalkSpeed;
						AnimationWalk();
					}
					else if (WanderAnimationRef == WanderAnimation.Run){
						AIAgent.speed = RunSpeed;
						AnimationRun();
					}
				}

				//Regnerate our AI's health when not in battle, if enabled.
				if (AIRegeneratesHealthRef == YesOrNo.Yes && !HealthRegInProgess && CurrentHealth < StartingHealth && CombatStateRef == CombatState.NotActive){
					HealthRegInProgess = true;
					StartCoroutine("HealthRegeneration");
				}
			}
			//Cautious - Cautious AI react differently to targets. Depending on their Confidence, an AI can
			//either run from their target or attack them if their Cautious timer has been exceeded. The cautious timer
			//allows AI to act territorial by playing a warning animation before they will attack their target. Once a Cautious AI
			//has exceeded its Cautious Seconds, it will turn hostile towrds the target. If a Cautious AI is attacked first,
			//it will instantly turn hostile towrds its attacker.
			else if (BehaviorRef == CurrentBehavior.Cautious && CurrentTarget != null && CurrentHealth > 0){
				//Flee - If the AI is set to Cautious, Flee.
				if (ConfidenceRef == ConfidenceType.Coward){
					Flee();
				}
				else if (ConfidenceRef == ConfidenceType.Brave || ConfidenceRef == ConfidenceType.Foolhardy){
					CautiousTimer += Time.deltaTime;

					if (AIAgent.hasPath){
						StopCoroutine("Wander");
						WanderingInProgress = false;
						AIAgent.ResetPath();
					}
						
					if (!IsTurning && UseWarningAnimationRef == YesOrNo.Yes){
						if (CautiousTimer >= CautiousSeconds/CautiousTimer && !WarningAnimationTriggered){
							AnimationIdleWarning();
							WarningAnimationTriggered = true;
							if (WarningSounds.Count != 0){
								AIAudioSource.PlayOneShot(WarningSounds[Random.Range(0, WarningSounds.Count)]);
							}
						}
						else{
							AnimationIdleCombat();
						}
					}
					else if (!IsTurning && UseWarningAnimationRef == YesOrNo.No){
						AnimationIdleCombat();
					}

					if (CautiousTimer >= CautiousSeconds){
						BehaviorRef = CurrentBehavior.Aggressive;
						CombatStateRef = CombatState.Active;
					}
				}
			}
			//Aggressive - Aggressive AI will wander around until a target gets within their trigger radius, if they 
			//exist on in their target tags list.
			else if (BehaviorRef == CurrentBehavior.Aggressive && CurrentTarget != null && CurrentHealth > 0){
				if (!TargetObstructed && !BackingUp){
					AIAgent.stoppingDistance = AttackDistance;
				}

				//When using Dynamic Movement, change reposition around the AI's target afer a random amount of attacks.
				if (DynamicMovementRef == DynamicMovement.Yes && CurrentAttacks >= AttacksToReposition && !AttackingInProgress && !BackingUp && !IsTurning){ //Removed && !ChasingDelay
					if (!AttackingInProgress && CurrentVelocity <= 0 && DistanceFromTarget > TooCloseDistance){
						if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run Attack")){
							if (WeaponTypeRef == WeaponType.Melee){
								AttacksToReposition = Random.Range(2,5);
							}
							else if (WeaponTypeRef == WeaponType.Ranged){
								AttacksToReposition = Random.Range(1,4);
							}
							CurrentAttacks = 0;

							StartCoroutine(TooClose());
						}
					}
				}

				if (FirstTimeInCombat){
					StopCoroutine("Wander");
					WanderingInProgress = false;
				}

				if (CurrentTarget != null){
					DistanceFromTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(CurrentTarget.position.x, 0, CurrentTarget.position.z));
					Chase();
				}

				if (CurrentTarget != null){
					if (HealthBarCanvas != null && CombatStateRef == CombatState.Active && !HealthBarCanvasRef.enabled){
						SetUI(true);
					}

					if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null 
						&& CombatStateRef == CombatState.Active && !CombatTextParent.activeSelf){
						CombatTextParent.SetActive(true);
					}
				}

				if (TargetTypeRef == TargetType.AI && TargetEmerald != null && TargetEmerald.CurrentHealth <= 0){
					AIAnimator.ResetTrigger(HitParameter);
					if (AttackCoroutine != null){
						StopCoroutine(AttackCoroutine);
					}
					AttackingInProgress = false;
					AttackDelay = false;
					StopCoroutine("TooClose");
					BackingUp = false;
					AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);
					AIAgent.stoppingDistance = AttackDistance;
					LineOfSightTargets.Clear();
					TargetEmerald = null;
					WarningAnimationTriggered = false;
					CautiousTimer = 0;

					if (WanderTypeRef == WanderType.Dynamic){
						StartCoroutine("DeathDelay");
						AnimationIdleCombat(); 
						AIAgent.ResetPath();
					}
					else if (WanderTypeRef == WanderType.Waypoints){
						StartCoroutine("DeathDelay");
						AnimationIdleCombat(); 
						AIAgent.ResetPath();
					}
					else if (WanderTypeRef == WanderType.Stationary){
						StartCoroutine("DeathDelay");
						AnimationIdleCombat(); 
						AIAgent.ResetPath();
					}
					else if (WanderTypeRef == WanderType.Destination){
						StartCoroutine("DeathDelay");
						AnimationIdleCombat(); 
						AIAgent.ResetPath();
						StartingDestination = transform.position;
					}

					if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
						SetUI(false);
					}

					if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
						CombatTextParent.SetActive(false);
					}

					AICollider.enabled = false;
					AICollider.enabled = true;

					SearchForTarget();
				}

				//Chase - Target is too far away to attack so it will continue to pursue until it's within range.
				if (AIAgent.remainingDistance >= AIAgent.stoppingDistance){
					if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") 
						&& !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
					AnimationRun();
					AIAgent.speed = RunSpeed;
					}
				}
				//Attack - Target is close enough to attack.
				if (AIAgent.remainingDistance <= AIAgent.stoppingDistance){
					FirstTimeInCombat = false;

					if (!IsTurning){
						AnimationIdleCombat();
					}

					if (!ChasingDelay && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && !DeathDelayActive){ 
						if (!BackingUp){
							StartCoroutine("ChaseDelay");
						}
					}
				}
				else {
					if (CurrentVelocity <= 0 && !FirstTimeInCombat){
						AnimationIdleCombat();
					}
				}

				if (DistanceFromTarget > AIAgent.stoppingDistance && DistanceFromTarget < (StoppingDistance+RunAttackDistance) && CurrentVelocity >= RunSpeed && UseRunAttacksRef == UseRunAttacks.Yes || 
					DistanceFromTarget <= AIAgent.stoppingDistance && CurrentVelocity <= 0){
					if (!AttackingInProgress && CurrentTarget != null && !IsTurning && !TargetObstructed && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && 
						!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && !BackingUp && !AttackDelay){
						if (DistanceFromTarget <= AIAgent.stoppingDistance && CurrentVelocity <= 0 && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit 1")){ 
							if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Turn Left") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Turn Right") && ChasingDelay){ 
								IsRunAttack = false;
								AttackCoroutine = StartCoroutine(Attack());
							}
						}
						else if (UseRunAttacksRef == UseRunAttacks.Yes && DistanceFromTarget > AIAgent.stoppingDistance && DistanceFromTarget < (StoppingDistance+RunAttackDistance) && CurrentVelocity >= RunSpeed){
							IsRunAttack = true;
							AttackCoroutine = StartCoroutine(Attack());
						}
					}
				}

				if (ConfidenceRef == ConfidenceType.Brave && (float)CurrentHealth/StartingHealth <= (HealthPercentageToFlee*0.01f)){
					RunSpeed = RunSpeed/1.25f;
					AIAnimator.SetFloat("Run Speed", RunAnimationSpeed/1.25f);

					BehaviorRef = CurrentBehavior.Cautious;
					ConfidenceRef = ConfidenceType.Coward;
				}
			}
			//Companion - Companion AI will follow around their Follow Target until they are dead. They will continously fight
			//for their follower by assisting them in combat. Depending on their Combat Type, they will either attack any AI in sight, or
			//wait for their follower to be engaged in combat. 
			else if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet){
				if (CurrentHealth > 0 && CurrentFollowTarget != null){
					if (CompanionTargetRef != null && CurrentTarget == null && TargetEmerald != null){
						if (TargetTypeRef == TargetType.AI && CombatTypeRef == CombatType.Defensive && BehaviorRef == CurrentBehavior.Companion && TargetEmerald.CombatStateRef == CombatState.Active 
							&& TargetEmerald.BehaviorRef == CurrentBehavior.Aggressive && TargetEmerald.CurrentDetectionRef == CurrentDetection.Alert && !TargetEmerald.ReturningToStartInProgress){
							CurrentDetectionRef = CurrentDetection.Alert;
							CombatStateRef = CombatState.Active;
							AIAgent.stoppingDistance = AttackDistance; 
							SearchForTarget();

							RunAnimationSpeed = StartingRunAnimationSpeed;
							AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);
						}
						else if (CombatTypeRef == CombatType.Offensive && BehaviorRef == CurrentBehavior.Companion && TargetEmerald.BehaviorRef == CurrentBehavior.Aggressive 
							&& TargetEmerald.CurrentDetectionRef == CurrentDetection.Alert && !TargetEmerald.ReturningToStartInProgress){
							CurrentDetectionRef = CurrentDetection.Alert;
							CombatStateRef = CombatState.Active;
							AIAgent.stoppingDistance = AttackDistance; 
							SearchForTarget();

							RunAnimationSpeed = StartingRunAnimationSpeed;
							AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);
						}
					}

					//When there is no threat, dynamically adjust our Companion's run speed and animation speed according to the leader's velocity.
					if (CurrentTarget == null && !FollowingDelay){
						if (FollowSpeedTypeRef == FollowSpeedType.Dynamic){
							float velocity = Vector3.Distance(transform.position, CurrentFollowTarget.position);

							if (velocity <= FollowingStoppingDistance*2){
								RunSpeed = velocity*0.5f;
								AIAnimator.SetFloat("Run Speed", (RunSpeed*RunAnimationSpeed)*0.15f);
							}
							else if (velocity > FollowingStoppingDistance*2 && velocity <= 25){
								RunSpeed = velocity*0.75f;
								AIAnimator.SetFloat("Run Speed", (RunSpeed*RunAnimationSpeed)*0.15f);
							}
						}

						OffsetTimer += Time.deltaTime;

						if (OffsetTimer > 3 && AIAgent.remainingDistance >= AIAgent.stoppingDistance){
							RandomOffset = new Vector3 (Random.Range(-FollowingStoppingDistance, FollowingStoppingDistance),Random.Range(-FollowingStoppingDistance, FollowingStoppingDistance),0);
							OffsetTimer = 0;
						}
						else if (AIAgent.remainingDistance < AIAgent.stoppingDistance){
							RandomOffset = Vector3.zero;
						}

						Destination(CurrentFollowTarget.localPosition + RandomOffset);
					}
						
					if (CurrentTarget != null && !FollowingDelay && BehaviorRef != CurrentBehavior.Pet){ //Added
						if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
							if (!BackingUp && !IsTurning){
								RunSpeed = StartingRunSpeed;
								Destination(CurrentTarget.position);
							}
						}
						else if (AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
							if (!BackingUp){
								AIAgent.speed = 0; 
							}
						}

						DistanceFromTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(CurrentTarget.position.x, 0, CurrentTarget.position.z));

						if (TargetTypeRef == TargetType.AI && TargetEmerald.CurrentHealth <= 0 || TargetTypeRef == TargetType.AI && TargetEmerald.ReturningToStartInProgress){
							if (AttackCoroutine != null){
								StopCoroutine(AttackCoroutine);
							}
							AttackingInProgress = false;
							AttackDelay = false;
							AIAgent.stoppingDistance = FollowingStoppingDistance;
							CurrentTarget = null;
							CompanionTargetRef = null;

							if (TargetEmerald.CurrentHealth <= 0){
								TargetEmerald = null;
								SearchForTarget();
							}
						}
							
						if (DistanceFromTarget > AIAgent.stoppingDistance && DistanceFromTarget < (StoppingDistance+RunAttackDistance) && CurrentVelocity >= RunSpeed && UseRunAttacksRef == UseRunAttacks.Yes || 
							DistanceFromTarget <= AIAgent.stoppingDistance && CurrentVelocity <= 0){
							if (!AttackingInProgress && CurrentTarget != null && !IsTurning && !TargetObstructed && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && 
								!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && !BackingUp && !AttackDelay){
								if (DistanceFromTarget <= AIAgent.stoppingDistance && CurrentVelocity <= 0 && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit 1")){ 
									if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Turn Left") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Turn Right")){ 
										IsRunAttack = false;
										AttackCoroutine = StartCoroutine(Attack());
									}
								}
								else if (UseRunAttacksRef == UseRunAttacks.Yes && DistanceFromTarget > AIAgent.stoppingDistance && DistanceFromTarget < (StoppingDistance+RunAttackDistance) && CurrentVelocity >= RunSpeed){
									IsRunAttack = true;
									AttackCoroutine = StartCoroutine(Attack());
								}
							}
						}
					}
						
					//Regnerate our AI's health when not in battle, if enabled.
					if (AIRegeneratesHealthRef == YesOrNo.Yes && !HealthRegInProgess && CurrentHealth < StartingHealth && CombatStateRef == CombatState.NotActive){
						HealthRegInProgess = true;
						StartCoroutine("HealthRegeneration");
					}

					if (AIAgent.remainingDistance > AIAgent.stoppingDistance){
						if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") 
							&& !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run Attack")){
							AnimationRun();
							AIAgent.speed = RunSpeed;
						}
					}

					if (AIAgent.remainingDistance < AIAgent.stoppingDistance){
						if (!IsTurning && CurrentVelocity <= 0){
							AnimationIdleCombat();
						}
					}
				}
			}

			//Our AI has successfully reached its return spot. Double check that there are
			//no new targets before returning to wandering.
			if (ReturningToStartInProgress && CurrentHealth > 0){
				if (AIAgent.remainingDistance <= AIAgent.stoppingDistance){
					if (AttackCoroutine != null){
						StopCoroutine(AttackCoroutine);
					}
					AttackingInProgress = false;
					AttackDelay = false;
					AICollider.enabled = false;
					AICollider.enabled = true;
					ConfidenceRef = (ConfidenceType)StartingConfidenceRef;
					BehaviorRef = (CurrentBehavior)StartingBehaviorRef;
					ReturningToStartInProgress = false;
				}
			}
				
			if (FootStepSounds.Count > 0 && FootstepSoundTypeRef == FootstepSoundType.Seconds){
				FootStepCounter += Time.deltaTime;
				if (AIAgent.speed == WalkSpeed && FootStepCounter >= FootStepSecondsWalk && AIAgent.velocity.magnitude > 0.1f){
					AIAudioSource.PlayOneShot(FootStepSounds[Random.Range(0,FootStepSounds.Count)]);
					FootStepCounter = 0;
				}
				else if (AIAgent.speed == RunSpeed && FootStepCounter >= FootStepSecondsRun && AIAgent.velocity.magnitude > 0.1f){
					AIAudioSource.PlayOneShot(FootStepSounds[Random.Range(0,FootStepSounds.Count)]);
					FootStepCounter = 0;
				}
			}

			if (OptimizedStateRef == OptimizedState.Active){
				CurrentVelocity = AIAgent.velocity.magnitude;

				if (CurrentVelocity > 0.1f || CombatStateRef == CombatState.Active || BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward || CombatStateRef == CombatState.NotActive){
					if (!IsDead){
						AlignWithGround();
					}
				}
			}
				
			if (CombatStateRef == CombatState.Active && !TargetObstructed && BehaviorRef == CurrentBehavior.Aggressive){
				if (DistanceFromTarget <= TooCloseDistance && !BackingUp && !IsTurning && !TargetObstructed){
					if (!AttackingInProgress && CurrentVelocity <= 0){
						if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && 
							!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run Attack")){
							if (CurrentAttacks >= AttacksToReposition){
								CurrentAttacks--;
							}
							StartCoroutine(TooClose());
						}
					}
				}
			}
		}
	}

	IEnumerator DeathDelay (){
		DeathDelayActive = true;
		int RandomWait = Random.Range(DeathDelayMin,DeathDelayMax+1);
		yield return new WaitForSeconds(RandomWait);
		if (WanderTypeRef == WanderType.Dynamic){
			StartCoroutine("Wander");
		}
		else if (WanderTypeRef == WanderType.Waypoints && WaypointsList.Count > 0){
			Destination(WaypointsList[destPoint]);
		}
		else if (WanderTypeRef == WanderType.Destination){
			Destination(SingleDestination);
		}
		else if (WanderTypeRef == WanderType.Stationary){
			Destination(StartingDestination);
		}
		DeathDelayActive = false;
	}

	//Regenerates our AI's health when not in combat, if it's enabled.
	IEnumerator HealthRegeneration (){
		CurrentHealth += RegenerateAmount;
		yield return new WaitForSeconds(HealthRegRate);
		HealthRegInProgess = false;
	}

	void AlignOnStart (){
		RaycastHit hit;
		Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), -transform.up); //Didn't have +0.25f

		if (Physics.Raycast(ray, out hit)){ //Add LM.value when ready
			GroundAngle = new Vector3(hit.normal.x,hit.normal.y,hit.normal.z);
		}

		TurnDirection = transform.forward;
		Quaternion qLook = Quaternion.LookRotation(TurnDirection, Vector3.up);
		Quaternion qNorm = Quaternion.FromToRotation(Vector3.up, GroundAngle);
		lookRotation =  qNorm * qLook;

		transform.rotation = lookRotation;
	}

	void AlignWithGround (){
		RayCastUpdateTimer += Time.deltaTime;

		ObstructionDetectionUpdateTimer += Time.deltaTime;

		if (!IsTurning && ObstructionDetectionUpdateTimer >= ObstructionDetectionUpdateSeconds && CombatStateRef == CombatState.Active){
			CheckForObstructions();
			ObstructionDetectionUpdateTimer = 0;
		}

		if (RayCastUpdateTimer >= RayCastUpdateSeconds && AlignAIWithGroundRef == AlignAIWithGround.Yes){
			ray = new Ray(new Vector3(transform.position.x+(transform.up.x), transform.position.y+(transform.up.y), transform.position.z+(transform.up.z)), -transform.up); 

			if (Physics.Raycast(ray, out hit, 3)){
				if (hit.collider.gameObject != this.gameObject){
					GroundAngle = new Vector3(hit.normal.x,hit.normal.y,hit.normal.z);
				}
			}
		}
			
		if (BehaviorRef != CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward || BehaviorRef == CurrentBehavior.Cautious && CurrentTarget == null || ConfidenceRef == ConfidenceType.Coward){
			if (!BackingUp){
				TurnDirection = AIAgent.steeringTarget - transform.position;
				TurnDirection.y = 0; 
			}
			else if (BackingUp && CurrentTarget != null && !IsTurning && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
				if (AIPath.status == NavMeshPathStatus.PathComplete && AIPath.status != NavMeshPathStatus.PathPartial && AIPath.status != NavMeshPathStatus.PathInvalid){

					if (AttackingInProgress){
						StopCoroutine(AttackCoroutine);
						AttackingInProgress = false;
						AttackDelay = false;
						TargetInView = false;
						TargetInRange = false;
					}

					//Move to the destination generated with the TooClose Coroutine. Once there, use combat idle animation.
					AIAgent.speed = RunSpeed;
					AIAgent.stoppingDistance = 0.5f;
					TurnDirection = AIAgent.steeringTarget - transform.position;
					TurnDirection.y = 0;

					if (AIAgent.remainingDistance <= 0.5f && !IsTurning){
						StopCoroutine("TooClose");
						BackingUp = false;
					}
				}
				if (AIPath.status == NavMeshPathStatus.PathPartial || AIPath.status == NavMeshPathStatus.PathInvalid || AIAgent.isPathStale){ //Edited had  || !AIAgent.hasPath
					StopCoroutine(TooClose());
					BackingUp = false;
				}
			}
		}

		if (BehaviorRef == CurrentBehavior.Cautious && CurrentTarget != null && ConfidenceRef != ConfidenceType.Coward || BehaviorRef == CurrentBehavior.Aggressive && CurrentTarget != null){
			if (!BackingUp && AIAgent.remainingDistance <= AIAgent.stoppingDistance){
				TurnDirection = CurrentTarget.position - transform.position;
				TurnDirection.y = 0;
				TurnDirection.Normalize();
			}
		}
			
		if (CurrentTarget != null){
			TurnDirectionOther = CurrentTarget.position - transform.position;
			TurnDirectionOther.y = 0;
			TargetDirection = Quaternion.LookRotation(TurnDirectionOther, Vector3.up);
		}
			
		if(!AIAgent.pathPending && ConfidenceRef == ConfidenceType.Coward 
			|| ConfidenceRef != ConfidenceType.Coward) {
			if (CurrentVelocity > 0.1f || CombatStateRef == CombatState.Active || CombatStateRef == CombatState.NotActive){
					if (TurnDirection.magnitude > 0.1f){
						Quaternion qLook = Quaternion.LookRotation(TurnDirection, Vector3.up);
						Quaternion qNorm = Quaternion.FromToRotation(Vector3.up, GroundAngle);
						lookRotation =  qNorm * qLook;
					}
			}
		}

		if (CurrentTarget == null && !DeathDelayActive || AIAgent.enabled && AIAgent.remainingDistance > AIAgent.stoppingDistance  || BackingUp || CombatStateRef == CombatState.NotActive){
			float angle = Vector3.Angle(TurnDirection, new Vector3(transform.forward.x, 0, transform.forward.z));
			Vector3 perp = Vector3.Cross(transform.forward, TurnDirection);
			float dir = Vector3.Dot(perp, transform.up);

			if (angle > AngleNeededToTurnRunning && angle != 0 || CurrentVelocity > 0.1f || CombatStateRef == CombatState.Active){
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*AlignSpeed);
			}
				
			if (angle > AngleNeededToTurnRunning && angle != 0){
				if (dir > 0.0f) {
					dir = 1.0f;
					AnimationTurnRight();
					AIAgent.speed = 0;
					IsTurning = true;
				} else if (dir < 0.0f) {
					dir = -1.0f;
					AnimationTurnLeft();
					AIAgent.speed = 0;
					IsTurning = true;
				}
			}
			else{
				IsTurning = false;
			}
				
			if (CurrentVelocity > 0.1f && AlignAIWithGroundRef == AlignAIWithGround.Yes){
				if (RayCastUpdateTimer >= RayCastUpdateSeconds){
					angleX = transform.localEulerAngles.x;
					angleZ = transform.localEulerAngles.z;
					angleX = (angleX > 180) ? angleX - 360 : angleX;
					angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;
				}

				if (angleX >= MaxXAngle){
					transform.localEulerAngles = new Vector3(MaxXAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
				}
				if (angleX <= -MaxXAngle){
					transform.localEulerAngles = new Vector3(-MaxXAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
				}

				if (angleZ >= MaxZAngle){
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, MaxZAngle);
				}
				if (angleZ <= -MaxZAngle){
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -MaxZAngle);
				}
			}

			if (RayCastUpdateTimer >= RayCastUpdateSeconds){
				RayCastUpdateTimer = 0;
			}
		}
		if (CurrentTarget != null && AIAgent.enabled && AIAgent.remainingDistance <= AIAgent.stoppingDistance || CurrentTarget != null && BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward){
			//Get the angle between our current target and the AI negating the angle of the height to calculate our rotation angle.
			float angle = Vector3.Angle(TurnDirectionOther, new Vector3(transform.forward.x, 0, transform.forward.z));
			Vector3 perp = Vector3.Cross(transform.forward, TurnDirectionOther);
			float dir = Vector3.Dot(perp, CurrentTarget.up);

			if (angle > AngleNeededToTurn && angle != 0 || CurrentVelocity > 0.1f){
				//Stops our AI from rotating while attacking
				if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookRotation.eulerAngles.x, TargetDirection.eulerAngles.y, lookRotation.eulerAngles.z), Time.deltaTime*TurnSpeed);
				}
				else{
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookRotation.eulerAngles.x, transform.eulerAngles.y, lookRotation.eulerAngles.z), Time.deltaTime*TurnSpeed);
				}
			}

			//Edited
			/*
			ObstructionDetectionUpdateTimer += Time.deltaTime;
				
			if (!IsTurning && ObstructionDetectionUpdateTimer >= ObstructionDetectionUpdateSeconds && CombatStateRef == CombatState.Active){
				CheckForObstructions();
				ObstructionDetectionUpdateTimer = 0;
			}
			*/

			if (angle > AngleNeededToTurn && angle != 0){
				if (dir > 0.0f) {
					dir = 1.0f;
					AnimationTurnRight();
					IsTurning = true;
				} else if (dir < 0.0f) {
					dir = -1.0f;
					AnimationTurnLeft();
					IsTurning = true;
				}
			}
			else{
				IsTurning = false;
			}
		}
			
		if (CurrentVelocity > 0.1f || CombatStateRef == CombatState.Active){
			if (RayCastUpdateTimer >= RayCastUpdateSeconds && AlignAIWithGroundRef == AlignAIWithGround.Yes){
				angleX = transform.localEulerAngles.x;
				angleZ = transform.localEulerAngles.z;
				angleX = (angleX > 180) ? angleX - 360 : angleX;
				angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;
			}

			if (angleX >= MaxXAngle){
				transform.localEulerAngles = new Vector3(MaxXAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
			}
			if (angleX <= -MaxXAngle){
				transform.localEulerAngles = new Vector3(-MaxXAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
			}

			if (angleZ >= MaxZAngle){
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, MaxZAngle);
			}
			if (angleZ <= -MaxZAngle){
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -MaxZAngle);
			}
		}

		if (RayCastUpdateTimer >= RayCastUpdateSeconds){
			RayCastUpdateTimer = 0;
		}
	}

	//Calculates our AI's line of sight mechanics.
	//For each target that is within the AI's trigger radius, and they are within the AI's
	//line of sight, set the first seen target as the CurrentTarget.
	void LineOfSightDetection (){
		if (CurrentDetectionRef == CurrentDetection.Alert && CurrentTarget == null && CurrentHealth > 0){
			foreach (Transform T in LineOfSightTargets.ToArray()){
				Vector3 direction = new Vector3(T.position.x, T.position.y, T.position.z) - transform.position;
				float angle = Vector3.Angle(new Vector3(direction.x, 0 ,direction.z), transform.forward);
				//Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.up, direction.normalized * AICollider.radius);
				if (angle < fieldOfViewAngle * 0.5f){
					RaycastHit hit;
					if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.up, direction.normalized, out hit, AICollider.radius)){
						if (hit.collider.CompareTag(EmeraldTag) || hit.collider.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always || hit.collider.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
							if (hit.collider.CompareTag(EmeraldTag)){
								ReceivedFaction = hit.collider.GetComponent<Emerald_AI>().CurrentFaction;
							}
							if (AIFactionsList.Contains(ReceivedFaction) && FactionRelations[AIFactionsList.IndexOf(ReceivedFaction)] == 0 || hit.collider.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always || hit.collider.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
								if (AIFactionsList.Contains(ReceivedFaction)){
									TargetTypeRef = TargetType.AI;
									TargetEmerald = hit.collider.GetComponent<Emerald_AI>();
								}
								else if (hit.collider.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always){
									TargetTypeRef = TargetType.Player;
								}
								else if (hit.collider.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
									TargetTypeRef = TargetType.NonAITarget;
								}

								SetLineOfSightTarget(hit.collider.transform);
							}
						}
					}
				}
			}
		}
	}
		
	void CheckForObstructions (){
		if (CurrentTarget != null){
			Vector3 direction = (CurrentTarget.position) - transform.position;
			float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), new Vector3(transform.forward.x, 0 ,transform.forward.z));
			RaycastHit hit;

			direction = Quaternion.Euler(CurrentTarget.rotation.x,CurrentTarget.rotation.y, CurrentTarget.rotation.z) * direction;

			Debug.DrawRay(new Vector3(transform.position.x, transform.position.y+EyeHeight, transform.position.z)+transform.forward, direction);

			//Check for obstructions and incrementally lower our AI's stopping distance until one is found. If none are found when the distance has reached 5 or below, search for a new target to see if there is a better option
			if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y+EyeHeight, transform.position.z), (direction), out hit, (AttackDistance+RunAttackDistance), ~ObstructionDetectionLayerMask)){
				if (CurrentTarget != null && angle > 45 && hit.collider.gameObject != this.gameObject && hit.collider.gameObject != CurrentTarget.gameObject || CurrentTarget != null && hit.collider.gameObject != CurrentTarget.gameObject && hit.collider.gameObject != this.gameObject){
					TargetObstructed = true;

					if (AIAgent.stoppingDistance > StoppingDistance+5 && !BackingUp && !IsTurning && WeaponTypeRef == WeaponType.Ranged){
						if (!AttackingInProgress && hit.collider.tag != EmeraldTag){
							AIAgent.stoppingDistance -= 5;
						}

					}
					if (DistanceFromTarget <= TooCloseDistance && !BackingUp && !IsTurning && WeaponTypeRef == WeaponType.Ranged && BehaviorRef != CurrentBehavior.Companion || DistanceFromTarget <= TooCloseDistance && !BackingUp && !IsTurning && WeaponTypeRef == WeaponType.Melee){
						if (!AttackingInProgress){
							StartCoroutine(TooClose());
						}
					}
				} 
				//If our AI's view becomes obstructed, and the object is another potential target, switch targets to the said AI it is most likely obstructing our AI's view of its original target.
				if (!BackingUp && CurrentTarget != null && angle <= 45 && TargetObstructed && hit.collider.gameObject != this.gameObject && hit.collider.gameObject != CurrentTarget.gameObject && hit.collider.tag == EmeraldTag){
					if (!AttackingInProgress){
						StartCoroutine(TooClose());
					}
				}
				if (CurrentTarget != null && angle <= 45 && hit.collider.gameObject == CurrentTarget.gameObject && !IsTurning){
					TargetObstructed = false;
				}
			}
		}
	}

	//Controls our AI's fleeing mechanics. If our AI's path becomes obstructed or unreachable, 
	//generate a new path for our AI to take. Note: An AI should not be able to reach the end of the
	//NavMesh. Leave a decent amount of distance for the AI to use for fleeing. This can be accomplished
	//by blocking your player off before they can reach the end of the terrain.
	void Flee (){
		StopCoroutine("Attack");
		StopCoroutine("Wander");
		WanderingInProgress = false;
		AttackingInProgress = false;
		AttackDelay = false;
		AIAgent.speed = RunSpeed;

		if (AIAgent.remainingDistance <= StoppingDistance+2){
			Vector3 direction = (CurrentTarget.position - transform.position).normalized;
			Vector3 GeneratedDestination = transform.position + -direction * 30 + Random.insideUnitSphere * 10;
			GeneratedDestination.y = GeneratedDestination.y+20;

			if (AIPath.status == NavMeshPathStatus.PathPartial){
				AnimationIdle();
				Destination(new Vector3(CurrentTarget.position.x + 15, CurrentTarget.position.y, CurrentTarget.position.z + 15));
			}
			else if (AIPath.status != NavMeshPathStatus.PathPartial && !AIAgent.pathPending && !IsTurning){
				RaycastHit hit;
				if (Physics.Raycast(GeneratedDestination, -transform.up, out hit, 40)){
					GeneratedDestination.y = hit.point.y;
				}
				Destination(GeneratedDestination);
			}
		}
	
		if (AIAgent.remainingDistance <= AIAgent.stoppingDistance){
			AnimationIdle();
		}
		else{
			AnimationRun();
		}

		if (CurrentTarget != null){
			DistanceFromTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(CurrentTarget.position.x, 0, CurrentTarget.position.z));

			if (DistanceFromTarget > MaxChaseDistance){
				StartingDestination = transform.position;
				ReturnToStart();
			}
		}
	}

	//Randomly pick a destination within the AI's wandering radius that's around the AI's starting position.
	//Randomly choose how long to pause before picking a new destination.
	IEnumerator Wander () {
		if (BehaviorRef == CurrentBehavior.Passive || BehaviorRef == CurrentBehavior.Cautious){
			WanderingInProgress = true;
			yield return new WaitForSeconds(WaitTime);
			Vector3 Dest = StartingDestination + Random.insideUnitSphere * WanderRadius;
			if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Dest.x, 0, Dest.z)) > StoppingDistance){
				Destination(Dest);
			}
			WaitTime = Random.Range(MinimumWaitTime,MaximumWaitTime+1);
			WanderingInProgress = false;
		}
		else if (BehaviorRef == CurrentBehavior.Aggressive && CurrentDetectionRef == CurrentDetection.Unaware){
			WanderingInProgress = true;
			yield return new WaitForSeconds(WaitTime);

			Vector3 NewDestination = StartingDestination + Random.insideUnitSphere * WanderRadius;
			NewDestination.y = NewDestination.y+20;

			RaycastHit hit;
			if (Physics.Raycast(NewDestination, -transform.up, out hit, 40)){
				NewDestination.y = hit.point.y;
				Destination(NewDestination);
			}
				
			WaitTime = Random.Range(MinimumWaitTime,MaximumWaitTime+1);
			WanderingInProgress = false;
		}
		else if (BehaviorRef == CurrentBehavior.Aggressive && CurrentDetectionRef == CurrentDetection.Alert){
			WanderingInProgress = true;
			yield return new WaitForSeconds(WaitTime);

			Vector3 NewDestination = StartingDestination + Random.insideUnitSphere * AlertWanderRadius;
			NewDestination.y = NewDestination.y+20;

			RaycastHit hit;
			if (Physics.Raycast(NewDestination, -transform.up, out hit, 40)){
				NewDestination.y = hit.point.y;
				Destination(NewDestination);
			}

			WaitTime = Random.Range(MinimumAlertWaitTime,MaximumAlertWaitTime+1);
			WanderingInProgress = false;
		}
	}

	void NextWaypoint (){
		if (WaypointsList.Count == 0)
			return;

		AIAgent.destination = WaypointsList[destPoint];
		destPoint++;

		if (destPoint == WaypointsList.Count){
			destPoint = 0;

			if (WaypointTypeRef == WaypointType.Reverse){
				WaypointsList.Reverse();
			}
		}
	}

	//Chase our target if they are not within attack distance.
	void Chase (){
		if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")){
			
			Vector3 direction = (CurrentTarget.position) - transform.position;
			float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), new Vector3(transform.forward.x, 0, transform.forward.z));

			if (!ChasingDelay && !BackingUp && angle < 45){

				//Stop our attack coroutine if it managed to trigger while our AI is attempting to move to its target.
				if (AttackingInProgress && DistanceFromTarget > AIAgent.stoppingDistance && AttackCoroutine != null){
					StopCoroutine(AttackCoroutine);
					TargetInRange = false;
					TargetInView = false;
					AttackingInProgress = false;
					AttackDelay = false;
				}

				Destination(CurrentTarget.position);
			}
		}
		else if (AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") || ChasingDelay || AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit 1")){
			if (!BackingUp){
				AIAgent.speed = 0; 
			}
		}

		if (MaxChaseDistanceTypeRef == MaxChaseDistanceType.TargetDistance && DistanceFromTarget > MaxChaseDistance){
			ReturnToStart();
		}
		else if (MaxChaseDistanceTypeRef == MaxChaseDistanceType.StartingDistance && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(StartingDestination.x, 0, StartingDestination.z)) > MaxChaseDistance){
			ReturnToStart();
		}
	}
		
	//Delay our chase by a random amount of seconds before pursuing.
	//This allows for more realistic combat mechanics. 
	IEnumerator ChaseDelay () {
		ChasingDelay = true;

		if (AIAgent.enabled){
			AIAgent.isStopped = true;
		}

		yield return new WaitForSeconds(ChaseWaitTime);
		ChaseWaitTime = Random.Range(MinimumChaseWaitTime,MaximumChaseWaitTime+1);
		ChasingDelay = false;

		if (AIAgent.enabled){
			AIAgent.isStopped = false;
		}
	}

	//Delay our follow by a random amount of seconds before following.
	//This allows for more realistic following mechanics. 
	IEnumerator FollowDelay () {
		FollowingDelay = true;
		yield return new WaitForSeconds(FollowWaitTime);
		FollowWaitTime = Random.Range(MinimumFollowWaitTime,MaximumFollowWaitTime+1);
		FollowingDelay = false;
	}

	//Attack our target, depending on how close the target is, use either 
	//stationary attack or a running attack. Once completed, generate a new attack time. 
	IEnumerator Attack (){
		AttackingInProgress = true;
		AttackDelay = true;

		if (CurrentTarget != null){
			if (!IsRunAttack){
				if (AttackAnimationNumber == 1){
					yield return new WaitForSeconds(0.25f);

					AnimationAttack();

					//Grab our AI's target position halfway through the attack delay so that the projectile fires straight. 
					//This is to stop the projectile from firing behind the AI if their target happens to move after an attack animation has been triggered
					ProjectileFirePosition = CurrentTarget.position;
					yield return new WaitForSeconds(Attack1Delay);
					CurrentAttacks++;

					if (WeaponTypeRef == WeaponType.Ranged && Attack1Projectile != null){
						CurrentProjectile = EmeraldObjectPool.Spawn(Attack1Projectile, RangedAttackTransform.position, Quaternion.identity);
						CurrentProjectile.transform.SetParent(ObjectPool.transform);
						CalculateRangedProjectile(CurrentProjectile);
					}
				}
				else if (AttackAnimationNumber == 2){
					yield return new WaitForSeconds(0.25f);
					AnimationAttack();
					//Grab our AI's target position halfway through the attack delay so that the projectile fires straight. 
					//This is to stop the projectile from firing behind the AI if their target happens to move after an attack animation has been triggered
					ProjectileFirePosition = CurrentTarget.position;
					yield return new WaitForSeconds(Attack2Delay);
					CurrentAttacks++;

					if (WeaponTypeRef == WeaponType.Ranged && Attack2Projectile != null){
						CurrentProjectile = EmeraldObjectPool.Spawn(Attack2Projectile, RangedAttackTransform.position, Quaternion.identity);
						CurrentProjectile.transform.SetParent(ObjectPool.transform);
						CalculateRangedProjectile(CurrentProjectile);
					}
				}
				else if (AttackAnimationNumber == 3){
					yield return new WaitForSeconds(0.25f);
					AnimationAttack();
					//Grab our AI's target position halfway through the attack delay so that the projectile fires straight. 
					//This is to stop the projectile from firing behind the AI if their target happens to move after an attack animation has been triggered
					ProjectileFirePosition = CurrentTarget.position;
					yield return new WaitForSeconds(Attack3Delay);
					CurrentAttacks++;

					if (WeaponTypeRef == WeaponType.Ranged && Attack3Projectile != null){
						CurrentProjectile = EmeraldObjectPool.Spawn(Attack3Projectile, RangedAttackTransform.position, Quaternion.identity);
						CurrentProjectile.transform.SetParent(ObjectPool.transform);
						CalculateRangedProjectile(CurrentProjectile);
					}
				}
			}
			else if (IsRunAttack && UseRunAttacksRef == UseRunAttacks.Yes){ //Edited
				if (RunAttack != null){
					yield return new WaitForSeconds(AttackSpeed+RunAttack.length*(1/RunAttackAnimationSpeed)-RunAttackDelay);
					AnimationRunAttack();
				}

				if (RunAttack == null){
					yield return new WaitForSeconds(AttackSpeed-RunAttackDelay);
				}

				if (RandomizeDamageRef == RandomizeDamage.Yes){
					CurrentDamageAmount = Random.Range(MinimumDamageAmountRun, MaximumDamageAmountRun+1);
				}
				else if (RandomizeDamageRef == RandomizeDamage.No){
					CurrentDamageAmount = DamageAmountRun;
				}

				ProjectileFirePosition = CurrentTarget.position;
				yield return new WaitForSeconds(RunAttackDelay);

				if (WeaponTypeRef == WeaponType.Ranged && RunAttackProjectile != null){
					CurrentProjectile = EmeraldObjectPool.Spawn(RunAttackProjectile, RangedAttackTransform.position, Quaternion.identity);
					CurrentProjectile.transform.SetParent(ObjectPool.transform);
					CalculateRangedProjectile(CurrentProjectile);
				}
			}

			//Prevent our AI from triggering attacks when not in range and if a target's angle is greater than the user set amount when using melee attacks
			if (WeaponTypeRef == WeaponType.Melee){
				if (DistanceFromTarget <= AIAgent.stoppingDistance){
					TargetInRange = true;
				}
				else {
					TargetInRange = false;
				}

				//Get the angle of our AI and its target, only apply damage if the angle is equal to or less than the needed amount.
				Vector3 direction = (CurrentTarget.position) - transform.position;
				float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), new Vector3(transform.forward.x, 0, transform.forward.z));

				if (angle <= 75){
					TargetInView = true;
				}
				else{
					TargetInView = false;
				}
			}
				
				
			if (TargetInRange && WeaponTypeRef == WeaponType.Melee){
				if (TargetTypeRef == TargetType.Player){
					if (TargetInView){
						DamagePlayer();
					}
				}
				else if (TargetTypeRef == TargetType.AI){
					if (TargetEmerald != null){
						if (TargetEmerald.CombatStateRef == Emerald_AI.CombatState.NotActive){
							TargetEmerald.CurrentTarget = this.transform;
							TargetEmerald.CombatStateRef = Emerald_AI.CombatState.Active;
							if (TargetEmerald.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
								TargetEmerald.TargetEmerald = GetComponent<Emerald_AI>();
							}
						}
						if (TargetInView){
							TargetEmerald.Damage(CurrentDamageAmount, Emerald_AI.TargetType.AI);
						}
					}
					else{
						TargetTypeRef = TargetType.Player;
					}
				}
				else if (TargetTypeRef == TargetType.NonAITarget){
					//Custom code can be added here.
				}
			}
		}

		GetDamageAmount();

		//Round our generated attack speed so they aren't too close to the previously generated amount
		AttackSpeed = Random.Range(MinimumAttackSpeed,MaximumAttackSpeed);
		AttackSpeed = Mathf.Round(AttackSpeed * 10f) / 10f;

		AIAnimator.ResetTrigger(HitParameter); 

		if (AttackAnimationNumber == 1 && Attack1 != null){
			AttackSpeed = AttackSpeed+Attack1.length*(1/Attack1AnimationSpeed-Attack1Delay);
		}
		else if (AttackAnimationNumber == 2 && Attack2 != null){
			AttackSpeed = AttackSpeed+Attack2.length*(1/Attack2AnimationSpeed-Attack2Delay);
		}
		else if (AttackAnimationNumber == 3 && Attack3 != null){
			AttackSpeed = AttackSpeed+Attack3.length*(1/Attack3AnimationSpeed-Attack3Delay);
		}

		AttackAnimationNumber++;
		if (TotalAttackAnimationsRef == TotalAttackAnimations.One){
			if (AttackAnimationNumber == 2){
				AttackAnimationNumber = 1;
			}
		}
		else if (TotalAttackAnimationsRef == TotalAttackAnimations.Two){
			if (AttackAnimationNumber == 3){
				AttackAnimationNumber = 1;
			}
		}
		else if (TotalAttackAnimationsRef == TotalAttackAnimations.Three){
			if (AttackAnimationNumber == 4){
				AttackAnimationNumber = 1;
			}
		}
			
		yield return new WaitForSeconds(AttackSpeed);

		AIAnimator.ResetTrigger(HitParameter); 
		TargetInView = false;
		TargetInRange = false;
		AttackingInProgress = false;

		yield return new WaitForSeconds(0.25f);
		AttackDelay = false;
	}

	void CalculateRangedProjectile (GameObject SentProjectile){
		if (SentProjectile.GetComponent<EmeraldProjectile>() != null){
			EmeraldProjectile Projectile = SentProjectile.GetComponent<EmeraldProjectile>();
			Projectile.TimeoutTimer = 0;
			Projectile.HeatSeekingFinished = false;
			Projectile.HeatSeekingTimer = 0;
			Projectile.ProjectileCurrentTarget = CurrentTarget;
			Projectile.HeatSeekingInitializeTimer = 0;
			Projectile.CollisionTimer = 0;
			Projectile.TimeoutTime = ProjectileTimeoutTime;
			Projectile.Collided = false;
			Projectile.ProjectileCollider.enabled = true;
			Projectile.EmeraldSystem = GetComponent<Emerald_AI>();

			if (TargetTypeRef == Emerald_AI.TargetType.AI && TargetEmerald != null){
				Projectile.ProjectileDirection = ((ProjectileFirePosition+TargetEmerald.AIBoxCollider.center) - Projectile.transform.position).normalized;
				Projectile.AdditionalHeight = TargetEmerald.AIBoxCollider.center;
			}
			else if (TargetTypeRef == Emerald_AI.TargetType.Player){
				Projectile.ProjectileDirection = ((ProjectileFirePosition) - Projectile.transform.position).normalized;
				Projectile.ProjectileDirection = new Vector3(Projectile.ProjectileDirection.x, Projectile.ProjectileDirection.y+PlayerYOffset/10, Projectile.ProjectileDirection.z); 
				Projectile.AdditionalHeight = new Vector3(0, PlayerYOffset/10, 0);
			}
			else if (TargetTypeRef == Emerald_AI.TargetType.NonAITarget){
				Projectile.ProjectileDirection = ((ProjectileFirePosition) - Projectile.transform.position).normalized;
			}
		}
		else{
			Debug.Log("There is no EmeraldProjectile script attached to your " + SentProjectile.name + " GameObject. Please use the projectile settings located in the AI's Settings under the Combat section. Here, Emerald will be able to automatically apply said script.");
		}
	}

	//Handles the Combat Text system, if enabled. If an AI has a combat text enabled before
	//a previous text is fully faded, immediately disable it and enable a new one.
	void CombatText (){
		foreach (TextMesh T in CombatTextList){
			if (T.gameObject.activeSelf){
				T.gameObject.SetActive(false);
			}
		}
		CombatTextList[CombatTextIndex].gameObject.SetActive(true);
		CombatTextList[CombatTextIndex].text = DamageReceived.ToString();
		CombatTextIndex++;
		if (CombatTextIndex == CombatTextList.Count){
			CombatTextIndex = 0;
		}
	}

	void Destination (Vector3 AIDestination){
		if (AIAgent.enabled){
			AIAgent.destination = AIDestination;
			AIAgent.CalculatePath(AIDestination, AIPath);
		}
	}

	/// <summary>
	/// Return our AI to its starting position. Once an AI successfully makes it to its starting position,
	/// it will check to ensure there are no more targets within its trigger radius. If there are, it will
	/// react according to its behavior.
	/// </summary>
	public void ReturnToStart (){
		StopCoroutine("Attack");
		AttackingInProgress = false; 
		AttackDelay = false;
		WarningAnimationTriggered = false;
		WanderingInProgress = false;
		FirstTimeInCombat = true;
		ReturningToStartInProgress = true;
		CurrentDetectionRef = CurrentDetection.Unaware;
		CombatStateRef = CombatState.NotActive;
		LineOfSightTargets.Clear();
		CurrentTarget = null;
		TargetEmerald = null;
		CautiousTimer = 0;

		if (WanderTypeRef == WanderType.Dynamic){
			Destination(StartingDestination + Random.insideUnitSphere * WanderRadius);
		}
		else if (WanderTypeRef == WanderType.Waypoints){
			Destination(WaypointsList[destPoint]);
		}
		else if (WanderTypeRef == WanderType.Destination){
			Destination(SingleDestination);
		}
		else if (WanderTypeRef == WanderType.Stationary){
			Destination(StartingDestination);
		}

		if (RefillHealthRTSRef == RefillHealthRTS.Yes){
			CurrentHealth = StartingHealth;
		}
			
		if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
			SetUI(false);
		}
		if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion){
			CombatTextParent.SetActive(false);
		}
	}

	/// <summary>
	/// Clears an AI's current targets.
	/// </summary>
	public void ClearTargets (){
		CurrentTarget = null;
		LineOfSightTargets.Clear();
		LineOfSightRef = null;
		CombatStateRef = CombatState.NotActive;
		CurrentDetectionRef = CurrentDetection.Unaware;
		AttackingInProgress = false; 
		TargetEmerald = null;

		if (WanderTypeRef == WanderType.Dynamic){
			StartCoroutine("Wander");
		}
		else if (WanderTypeRef == WanderType.Waypoints && WaypointsList.Count > 0){
			Destination(WaypointsList[destPoint]);
		}
		else if (WanderTypeRef == WanderType.Destination){
			Destination(SingleDestination);
		}
		else if (WanderTypeRef == WanderType.Stationary){
			Destination(StartingDestination);
		}
	}
		
	/// <summary>
	/// Sets the current target to the Target parameter. Depending on your AI's
	/// behavior, you may need to modify its behavior in order for the AI to properly react.
	/// </summary>
	/// <param name="SetTarget">SetTarget.</param>
	public void SetTarget (Transform Target){
		CurrentTarget = Target;
	}

	/// <summary>
	/// Plays a random footstep from the user's Footstep sounds when called. This is useful for calling with an Animation Event.
	/// </summary>
	public void PlayFootstepSound (){
		if (FootStepSounds.Count > 0){
			AIAudioSource.PlayOneShot(FootStepSounds[Random.Range(0,FootStepSounds.Count)]);
		}
	}

	/// <summary>
	/// Sets the follower target to the FollowerTarget parameter. When using this setting,
	/// Emerald will automatically set your AI's behavior to Companion.
	/// </summary>
	public void SetFollowerCompanion (Transform FollowerTarget){
		AIAttacksPlayerRef = AIAttacksPlayer.Never;
		DetectionTypeRef = DetectionType.Trigger;
		SetBehavior(Emerald_AI.CurrentBehavior.Companion);
		ClearTargets();
		CurrentFollowTarget = FollowerTarget;
	}
		
	/// <summary>
	/// Sets the follower target to the FollowerTarget parameter. When using this setting,
	/// Emerald will automatically set your AI's behavior to Pet.
	/// </summary>
	public void SetFollowerPet (Transform FollowerTarget){
		AIAttacksPlayerRef = AIAttacksPlayer.Never;
		DetectionTypeRef = DetectionType.Trigger;
		SetBehavior(Emerald_AI.CurrentBehavior.Pet);
		ClearTargets();
		CurrentFollowTarget = FollowerTarget;
	}
		
	/// <summary>
	/// Clears an AI's current factions and updates them to the ones in the parameters.
	/// Factions that you don't need can be given the value of null. This function also allows users to update an
	/// AI's Faction as it may be needed due to updating its Opposing Factions.
	/// </summary>
	public void UpdateTargetTags (string NewFaction, string UpdateOpposingFaction1, string UpdateOpposingFaction2, string UpdateOpposingFaction3, string UpdateOpposingFaction4, string UpdateOpposingFaction5){

		AIFactionsList.Clear();
		TextAsset FactionData = Resources.Load("EmeraldAIFactions") as TextAsset;

		if (FactionData != null){
			string[] textLines =  FactionData.text.Split (',');

			foreach (string s in textLines){
				if (!Emerald_AI.StringFactionList.Contains(s) && s != ""){
					Emerald_AI.StringFactionList.Add(s);
				}
			}
		}

		ClearTargets();
			
		CurrentFaction = Emerald_AI.StringFactionList.IndexOf(NewFaction);

		if (OpposingFactionsEnumRef == OpposingFactionsEnum.One){
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1));
			OpposingFaction1 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Two){
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2));
			OpposingFaction1 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1);
			OpposingFaction2 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Three){
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3));
			OpposingFaction1 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1);
			OpposingFaction2 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2);
			OpposingFaction3 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Four){
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction4));
			OpposingFaction1 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1);
			OpposingFaction2 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2);
			OpposingFaction3 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3);
			OpposingFaction4 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction4);
		}
		if (OpposingFactionsEnumRef == OpposingFactionsEnum.Five){
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction4));
			AIFactionsList.Add(Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction5));
			OpposingFaction1 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction1);
			OpposingFaction2 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction2);
			OpposingFaction3 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction3);
			OpposingFaction4 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction4);
			OpposingFaction5 = Emerald_AI.StringFactionList.IndexOf(UpdateOpposingFaction5);
		}
	}

	/// <summary>
	/// Change the behavior of the targeted AI. For example, if you wanted to set an AI to aggressive, you would use
	/// SetBehavior(Emerald_AI.CurrentBehavior.Agressive). The available behaviors are Cautious, Passive, and Aggressive.
	/// An AI can be changed to a Companion or Pet by using the SetFollowerCompanion() or SetFollowerPet() functions.
	/// </summary>
	public void SetBehavior (CurrentBehavior Behavior){
		BehaviorRef = Behavior;
	}

	/// <summary>
	/// Resets the AI to its default settings. This is useful for object pooling systems. Simply call ResetAI() when you'd like to do so.
	/// </summary>
	public void ResetAI (){
		StopCoroutine("Dead");
		GetComponent<Emerald_AI>().enabled = true;
		ClearTargets();
		OptimizedStateRef = OptimizedState.Active;
		BehaviorRef = (CurrentBehavior)StartingBehaviorRef;
		CurrentHealth = StartingHealth;
		gameObject.tag = StartingTag;
		this.gameObject.layer = StartingLayer;
		FirstTimeInCombat = true;
		AIAudioSource.enabled = true;
		AIAnimator.enabled = true;
		AIAgent.enabled = true;
		AICollider.enabled = true;
		AIBoxCollider.enabled = true;
		IsDead = false;
		AttackingInProgress = false;
		ChasingDelay = false;
		AnimationIdle();
		AIAgent.isStopped = false;
		StartCoroutine("Wander");
	}

	void OnEnable (){
		if (IsDead && RecycleAIRef == YesOrNo.Yes){
			ResetAI ();
		}
	}

	/// <summary>
	/// Pauses the AI's current actions and position. This is useful for implementing dialogue or quests.
	/// </summary>
	public void Pause(){
		AIAgent.isStopped = true;
	}

	/// <summary>
	/// Resumes the AI's current actions and position. This is useful after a quest or dialogue has been given.
	/// </summary>
	public void Resume(){
		AIAgent.isStopped = false;
	}

	/// <summary>
	/// Changes the AI's Wander Type to Destination and sets a single destination for the AI to move to. The AI will still react to targets using its Behavior Type.
	/// This can be updated as many times as needed and can even be used for custom schedules.
	/// </summary>
	/// <param name="AIDestination">AI destination.</param>
	public void SetDestination (Vector3 AIDestination){
		if (CurrentTarget == null && CurrentHealth > 0){
			AIReachedDestination = false;
			WanderTypeRef = WanderType.Destination;
			SingleDestination = AIDestination;
			AIAgent.destination = AIDestination;
			AIAgent.CalculatePath(AIDestination, AIPath);
			DestinationUpdated = false;
		}
	}

	/// <summary>
	/// Tells a Companion or Pet AI to stay at its current position. (AI must have a current follow target)
	/// </summary>
	public void Stay(){
		AIAgent.isStopped = true;
	}

	/// <summary>
	/// Tells a Companion or Pet AI to resume following its follower. (AI must have a current follow target)
	/// </summary>
	public void Follow(){
		AIAgent.isStopped = false;
	}

	/// <summary>
	/// Damages an AI according to the DamageAmount parameter. If your custom damage amount is a float, 
	/// simply use (int)YourDamageVariable to convert it to be useable with this function.
	/// </summary>
	public void Damage (int DamageAmount, TargetType TypeOfTarget){	
		if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null){
			DamageReceived = DamageAmount;
			CombatText();
		}

		//Add object pooling here
		if (UseBloodEffectRef == UseBloodEffect.Yes && BloodEffect != null && !DamageEffectInhibitor){
			GameObject SpawnedBlood = EmeraldObjectPool.Spawn(BloodEffect, Vector3.zero, Quaternion.identity) as GameObject;
			SpawnedBlood.transform.SetParent(ObjectPool.transform);
			SpawnedBlood.transform.localPosition = transform.position+BloodPosOffset;
		}

		DamageEffectInhibitor = false;

		if (TargetEmerald != null && TargetEmerald.ConfidenceRef == Emerald_AI.ConfidenceType.Coward){
			SearchForTarget();
		}

		//If our AI is damaged, and they are cautious, check their confidence. If it's higher than
		//coward, attack the target immediately.
		if (BehaviorRef == CurrentBehavior.Cautious && CurrentTarget != null || BehaviorRef == CurrentBehavior.Aggressive && CurrentTarget != null){
			if (ConfidenceRef == ConfidenceType.Brave || ConfidenceRef == ConfidenceType.Foolhardy){
				ReturningToStartInProgress = false;

				if (HealthBarCanvas != null && CombatStateRef == CombatState.Active && !HealthBarCanvasRef.enabled){
					SetUI(true);
				}

				if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null 
					&& CombatStateRef == CombatState.Active && !CombatTextParent.activeSelf){
					CombatTextParent.SetActive(true);
				}

				if (DetectionTypeRef == DetectionType.LineOfSight && LineOfSightRef == null){
					LineOfSightRef = CurrentTarget;
				}

				CurrentDetectionRef = CurrentDetection.Alert;
				CombatStateRef = CombatState.Active;
				BehaviorRef = CurrentBehavior.Aggressive;
			}
		}
		//If our AI is damaged, and they are passive, check their behavior. If it's higher than
		//coward, attack the target immediately. If it's lower, flee.
		else if (BehaviorRef == CurrentBehavior.Passive && PassiveTargetRef != null){
			if (ConfidenceRef == ConfidenceType.Brave || ConfidenceRef == ConfidenceType.Foolhardy){
				ReturningToStartInProgress = false;
				CurrentTarget = PassiveTargetRef;
				CurrentDetectionRef = CurrentDetection.Alert;
				CombatStateRef = CombatState.Active;
				BehaviorRef = CurrentBehavior.Aggressive;

				if (TypeOfTarget == Emerald_AI.TargetType.Player && AIAttacksPlayerRef == AIAttacksPlayer.OnlyIfAttacked){
					AIAttacksPlayerRef = AIAttacksPlayer.Always;
				}

				SearchForTarget();
			}
			else if (ConfidenceRef == ConfidenceType.Coward){
				ReturningToStartInProgress = false;
				CurrentTarget = PassiveTargetRef;
				CombatStateRef = CombatState.Active;
				CurrentDetectionRef = CurrentDetection.Alert;
				BehaviorRef = CurrentBehavior.Cautious;

				if (HealthBarCanvas != null && CombatStateRef == CombatState.Active && !HealthBarCanvasRef.enabled){
					SetUI(true);
				}

				if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null 
					&& CombatStateRef == CombatState.Active && !CombatTextParent.activeSelf){
					CombatTextParent.SetActive(true);
				}
			}
		}
		//If for some reason a target isn't added, and an AI
		//is attacked by one, search for potetial targets and attack if one is found.
		else if (CurrentTarget == null){
			if (BehaviorRef == CurrentBehavior.Cautious || BehaviorRef == CurrentBehavior.Aggressive || BehaviorRef == CurrentBehavior.Passive || BehaviorRef == CurrentBehavior.Companion){
				if (ConfidenceRef != ConfidenceType.Coward){
					if (TypeOfTarget == Emerald_AI.TargetType.Player && AIAttacksPlayerRef == AIAttacksPlayer.OnlyIfAttacked){
						AIAttacksPlayerRef = AIAttacksPlayer.Always;
					}

					//If our AI is using the trigger Detection Type, and our AI is damaged, search the surrounding area for an attacker.
					if (DetectionTypeRef == DetectionType.Trigger){
						CombatStateRef = CombatState.Active;
						StopCoroutine("DeathDelay");
						DeathDelayActive = false;
						StopCoroutine("Wander");
						SearchForTarget();
					}
				}
				else if (ConfidenceRef == ConfidenceType.Coward){
					if (TypeOfTarget == Emerald_AI.TargetType.Player && AIAttacksPlayerRef == AIAttacksPlayer.OnlyIfAttacked){
						AIAttacksPlayerRef = AIAttacksPlayer.Always;
						CombatStateRef = CombatState.Active;
						StopCoroutine("Wander");
						SearchForTarget();
					}
				}
				else if (BehaviorRef == CurrentBehavior.Companion){
					CurrentDetectionRef = CurrentDetection.Alert;
					CombatStateRef = CombatState.Active;
					SearchForTarget();
				}

				//If our AI no target is detected after receiving damage, expand the AI's radius according to the expanded radii to look for an attacker.
				if (CurrentTarget == null && CombatStateRef != CombatState.Active){
					DetectionRadius = ExpandedDetectionRadius;
					AICollider.radius = ExpandedDetectionRadius;
					fieldOfViewAngle = ExpandedFieldOfViewAngle;
				}
			}
		}

		if (InjuredSounds.Count != 0){
			AIAudioSource.PlayOneShot(InjuredSounds[Random.Range(0, InjuredSounds.Count)]);
		}

		CurrentHealth -= DamageAmount;

		if (CurrentHealth > 0 && CurrentVelocity <= 0 && !BackingUp){ 
			if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && 
				!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") && !AIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run Attack")){
				AnimationGetHit();
			}
		}

		if (CurrentHealth <= 0){
			IsDead = true;
			AIBoxCollider.enabled = false;
			this.gameObject.layer = 0;
			StopCoroutine("TooClose");
			BackingUp = false;
			StopCoroutine("ChaseDelay");
			if (AttackCoroutine != null){
				StopCoroutine(AttackCoroutine);
			}
			AttackingInProgress = false;
			StopCoroutine("Wander");
			StopCoroutine("DeactivateDelay");
			ChasingDelay = false;
			if (DeathSounds.Count != 0){
				AIAudioSource.PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.Count)]);
			}
				
			OptimizedStateRef = OptimizedState.Inactive;
			AIAgent.enabled = false;
			AICollider.enabled = false;
			LineOfSightRef = null;
			LineOfSightTargets.Clear();
	
			AIAnimator.SetTrigger("Dead");
			AIAnimator.SetBool(TurnRightParameter, false);
			AIAnimator.SetBool(TurnLeftParameter, false);
			AIAnimator.SetBool(WalkParameter, false);
			AIAnimator.SetBool(IdleParameter, false);
			AIAnimator.SetBool(IdleCombatParameter, false);
			AIAnimator.SetBool(RunParameter, false);
			AIAnimator.SetBool(IdleAlertParameter, false);
			StartCoroutine("Dead");
		}
	}

	//Our AI has died. Play its death animation and sound then deactivate all the AI's components.
	IEnumerator Dead () {
		if (HealthBarCanvas != null){
			if (HealthBar.activeSelf){
				HealthBar.GetComponent<EmeraldHealthBar>().FadeOut();
			}
			else{
				//If an AI's health bar gameobject is disabled, skip the fading process and disabling it instantly.
				HealthBar.SetActive(false);
			}
		}
		yield return new WaitForSeconds(0.5f);
		this.gameObject.tag = "Untagged";
		yield return new WaitForSeconds(2f);
		if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null){
			CombatTextParent.SetActive(false);
		}
		yield return new WaitForSeconds(2f);
		AIAudioSource.enabled = false;
		AIAnimator.enabled = false;
		yield return new WaitForSeconds(0.5f);
		GetComponent<Emerald_AI>().enabled = false;
	}
		
	//Controls when an AI is deactivated while using the optimization system
	public void Deactivate (){
		if (CurrentDetectionRef != CurrentDetection.Alert && CurrentTarget == null && !ReturningToStartInProgress && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
			AICollider.enabled = false;
			AIBoxCollider.enabled = false;
			AnimationIdle();
			OptimizedStateRef = OptimizedState.Inactive;
			StopCoroutine("Wander");
			WanderingInProgress = false;

			if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				SetUI(false);
			}

			if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet){
				CombatTextParent.SetActive(false);
			}
		}
	}

	//Controls when an AI is activated while using the optimization system
	public void Activate (){
		if (CurrentHealth > 0){
			AICollider.enabled = true;
			AIBoxCollider.enabled = true;
			OptimizedStateRef = OptimizedState.Active;
		}
	}

	public void DamagePlayer(){
		//If you'd like AI to damage RFPS, simple uncomment this and comment out the PlayerHealth if statement.
		/*
		if (CurrentTarget != null && CurrentTarget.GetComponent<FPSPlayer>() != null){
			CurrentTarget.GetComponent<FPSPlayer>().ApplyDamage((float)CurrentDamageAmount, transform, true);
		}
		*/
		if (CurrentTarget != null && CurrentTarget.GetComponent<PlayerHealth>() != null){
			CurrentTarget.GetComponent<PlayerHealth>().DamagePlayer((float)CurrentDamageAmount);
		}
	}

	void SetLineOfSightTarget (Transform LineOfSightTarget){
		StopCoroutine("Wander");
		LineOfSightRef = LineOfSightTarget;
		//Pick our target depending on the AI's options
		if (PickTargetMethodRef == PickTargetMethod.FirstDetected || BehaviorRef == CurrentBehavior.Cautious){
			CurrentTarget = LineOfSightRef;
		}
		else if (PickTargetMethodRef == PickTargetMethod.Closest){
			SearchForTarget();
		}

		CombatStateRef = CombatState.Active;

		if (BehaviorRef == CurrentBehavior.Companion){
			AIAgent.stoppingDistance = StoppingDistance;
		}
	}

	void SetUI (bool Enabled){
			HealthBarCanvasRef.enabled = Enabled;
		if (CreateHealthBarsRef == CreateHealthBars.Yes){
			HealthBar.SetActive(Enabled);

			if (DisplayAILevelRef == DisplayAILevel.Yes){
				TextLevel.gameObject.SetActive(Enabled);
			}
		}

		if (DisplayAINameRef == DisplayAIName.Yes){
			TextName.gameObject.SetActive(Enabled);
		}
	}
		
	//Controls all of our AI's animations.
	void AnimationIdle (){
		if (BehaviorRef == CurrentBehavior.Passive || BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef == ConfidenceType.Coward && UseAlertAnimationRef == YesOrNo.No || 
			BehaviorRef == CurrentBehavior.Aggressive && UseAlertAnimationRef == YesOrNo.No || BehaviorRef == CurrentBehavior.Cautious && UseAlertAnimationRef == YesOrNo.No){

			if (WanderTypeRef == WanderType.Stationary){
				StationaryIdleTimer += Time.deltaTime;
			}

			if (WanderTypeRef != WanderType.Stationary && !AIAnimator.GetBool(IdleParameter) || WanderTypeRef == WanderType.Stationary && StationaryIdleTimer >= StationaryIdleSeconds){
				if (IdleAnimationNumber == 1){
					AIAnimator.SetBool(IdleParameter, false);
					IdleParameter = Animator.StringToHash("Idle 1");
				}
				else if (IdleAnimationNumber == 2){
					if (TotalIdleAnimationsRef == TotalIdleAnimations.Two || TotalIdleAnimationsRef == TotalIdleAnimations.Three){
						AIAnimator.SetBool(IdleParameter, false);
						IdleParameter = Animator.StringToHash("Idle 2");
					}
				}
				else if (IdleAnimationNumber == 3){
					if (TotalIdleAnimationsRef == TotalIdleAnimations.Three){
						AIAnimator.SetBool(IdleParameter, false);
						IdleParameter = Animator.StringToHash("Idle 3");
					}
				}

				AIAnimator.SetBool(IdleParameter, true);
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
				AIAnimator.SetBool(TurnRightParameter, false);
				AIAnimator.SetBool(TurnLeftParameter, false);

				IdleAnimationNumber++;

				if (TotalIdleAnimationsRef == TotalIdleAnimations.One){
					if (IdleAnimationNumber == 2){
						IdleAnimationNumber = 1;
					}
				}
				else if (TotalIdleAnimationsRef == TotalIdleAnimations.Two){
					if (IdleAnimationNumber == 3){
						IdleAnimationNumber = 1;
					}
				}
				else if (TotalIdleAnimationsRef == TotalIdleAnimations.Three){
					if (IdleAnimationNumber == 4){
						IdleAnimationNumber = 1;
					}
				}

				StationaryIdleTimer = 0;
				StationaryIdleSeconds = Random.Range(StationaryIdleSecondsMin,StationaryIdleSecondsMax+1);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Aggressive && CurrentDetectionRef == CurrentDetection.Alert && UseAlertAnimationRef == YesOrNo.Yes){
			if (!AIAnimator.GetBool(IdleAlertParameter)){
				AIAnimator.SetBool(IdleAlertParameter, true);
				AIAnimator.SetBool(IdleParameter, false);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Aggressive && CurrentDetectionRef == CurrentDetection.Unaware && UseAlertAnimationRef == YesOrNo.Yes){
			if (!AIAnimator.GetBool(IdleParameter)){
				AIAnimator.SetBool(IdleParameter, true);
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Cautious && CurrentDetectionRef == CurrentDetection.Alert && UseAlertAnimationRef == YesOrNo.Yes){
			if (!AIAnimator.GetBool(IdleAlertParameter)){
				AIAnimator.SetBool(IdleAlertParameter, true);
				AIAnimator.SetBool(IdleParameter, false);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Cautious && CurrentDetectionRef == CurrentDetection.Alert && UseAlertAnimationRef == YesOrNo.No){
			if (ConfidenceRef == ConfidenceType.Brave || ConfidenceRef == ConfidenceType.Foolhardy){
				if (!AIAnimator.GetBool(IdleParameter)){
					AIAnimator.SetBool(IdleAlertParameter, false);
					AIAnimator.SetBool(IdleParameter, true);
					AIAnimator.SetBool(IdleCombatParameter, false);
					AIAnimator.SetBool(RunParameter, false);
					AIAnimator.SetBool(WalkParameter, false);
				}
			}
		}
		else if (BehaviorRef == CurrentBehavior.Cautious && CurrentDetectionRef == CurrentDetection.Unaware && UseAlertAnimationRef == YesOrNo.Yes){
			if (!AIAnimator.GetBool(IdleParameter)){
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(IdleParameter, true);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef == ConfidenceType.Coward && UseAlertAnimationRef == YesOrNo.Yes && CurrentDetectionRef == CurrentDetection.Unaware){
			if (!AIAnimator.GetBool(IdleParameter)){
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(IdleParameter, true);
				AIAnimator.SetBool(IdleCombatParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
			}
		}
	}
	void AnimationIdleCombat (){
		if (BehaviorRef != CurrentBehavior.Pet){
			if (!AIAnimator.GetBool(IdleCombatParameter)){
				AIAnimator.SetBool(IdleCombatParameter, true);
				AIAnimator.SetBool(IdleParameter, false);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(TurnRightParameter, false);
				AIAnimator.SetBool(TurnLeftParameter, false);
			}
		}
		else if (BehaviorRef == CurrentBehavior.Pet){
			if (!AIAnimator.GetBool(IdleParameter)){
				AIAnimator.SetBool(IdleParameter, true);
				AIAnimator.SetBool(RunParameter, false);
				AIAnimator.SetBool(WalkParameter, false);
				AIAnimator.SetBool(IdleAlertParameter, false);
				AIAnimator.SetBool(TurnRightParameter, false);
				AIAnimator.SetBool(TurnLeftParameter, false);
			}
		}
	}
	void AnimationIdleWarning (){
		AIAnimator.SetTrigger(IdleWarningParameter);
		AIAnimator.SetBool(IdleCombatParameter, false);
		AIAnimator.SetBool(IdleParameter, false);
		AIAnimator.SetBool(RunParameter, false);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(IdleAlertParameter, false);
		AIAnimator.SetBool(TurnRightParameter, false);
		AIAnimator.SetBool(TurnLeftParameter, false);
	}
	void AnimationWalk (){
		if (!AIAnimator.GetBool(WalkParameter)){
			AIAnimator.ResetTrigger(HitParameter);
			AIAnimator.SetBool(WalkParameter, true);
			AIAnimator.SetBool(IdleParameter, false);
			AIAnimator.SetBool(IdleCombatParameter, false);
			AIAnimator.SetBool(RunParameter, false);
			AIAnimator.SetBool(IdleAlertParameter, false);
			AIAnimator.SetBool(TurnRightParameter, false);
			AIAnimator.SetBool(TurnLeftParameter, false);
		}
	}
	void AnimationRun (){
		if (!AIAnimator.GetBool(RunParameter)){
			AIAnimator.ResetTrigger("Attack 1");
			AIAnimator.ResetTrigger("Attack 2");
			AIAnimator.ResetTrigger("Attack 3");

			AIAnimator.SetBool(RunParameter, true);
			AIAnimator.ResetTrigger(HitParameter); 
			AIAnimator.SetBool(IdleParameter, false);
			AIAnimator.SetBool(IdleCombatParameter, false);
			AIAnimator.SetBool(WalkParameter, false);
			AIAnimator.SetBool(IdleAlertParameter, false);
			AIAnimator.SetBool(TurnRightParameter, false);
			AIAnimator.SetBool(TurnLeftParameter, false);
		}
	}
	void AnimationTurnLeft (){
		AIAnimator.SetBool(TurnLeftParameter, true);
		AIAnimator.SetBool(TurnRightParameter, false);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(IdleParameter, false);
		AIAnimator.SetBool(IdleCombatParameter, false);
		AIAnimator.SetBool(RunParameter, false);
		AIAnimator.SetBool(IdleAlertParameter, false);
	}
	void AnimationTurnRight (){
		AIAnimator.SetBool(TurnRightParameter, true);
		AIAnimator.SetBool(TurnLeftParameter, false);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(IdleParameter, false);
		AIAnimator.SetBool(IdleCombatParameter, false);
		AIAnimator.SetBool(RunParameter, false);
		AIAnimator.SetBool(IdleAlertParameter, false);
	}
	void AnimationAttack (){
		if (AttackSounds.Count != 0){
			AIAudioSource.PlayOneShot(AttackSounds[Random.Range(0, AttackSounds.Count)]);
		}

		AIAnimator.ResetTrigger(HitParameter);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(TurnRightParameter, false);
		AIAnimator.SetBool(TurnLeftParameter, false);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(IdleParameter, false);
		AIAnimator.SetBool(IdleCombatParameter, false);
		AIAnimator.SetBool(RunParameter, false);
		AIAnimator.SetBool(IdleAlertParameter, false);

		if (AttackAnimationNumber == 1){
			AIAnimator.SetTrigger("Attack 1");
		}
		else if (AttackAnimationNumber == 2){
			if (TotalAttackAnimationsRef == TotalAttackAnimations.Two || TotalAttackAnimationsRef == TotalAttackAnimations.Three){
				AIAnimator.SetTrigger("Attack 2");
			}
		}
		else if (AttackAnimationNumber == 3){
			if (TotalAttackAnimationsRef == TotalAttackAnimations.Three){
				AIAnimator.SetTrigger("Attack 3");
			}
		}


	}
	void AnimationRunAttack (){
		if (AttackSounds.Count != 0){
			AIAudioSource.PlayOneShot(AttackSounds[Random.Range(0, AttackSounds.Count)]);
		}

		AIAnimator.SetTrigger(RunAttackParameter);
		AIAnimator.ResetTrigger(HitParameter); 
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(TurnRightParameter, false);
		AIAnimator.SetBool(TurnLeftParameter, false);
		AIAnimator.SetBool(WalkParameter, false);
		AIAnimator.SetBool(IdleParameter, false);
		AIAnimator.SetBool(IdleCombatParameter, false);
		AIAnimator.SetBool(RunParameter, false);
		AIAnimator.SetBool(IdleAlertParameter, false);
	}
	void AnimationGetHit (){
		if (!AIAnimator.GetBool(WalkParameter)){
			AIAnimator.SetTrigger(HitParameter);
			AIAnimator.SetBool(WalkParameter, false);
			AIAnimator.SetBool(TurnRightParameter, false);
			AIAnimator.SetBool(TurnLeftParameter, false);
			AIAnimator.SetBool(WalkParameter, false);
			AIAnimator.SetBool(IdleParameter, false);
			AIAnimator.SetBool(IdleCombatParameter, false);
			AIAnimator.SetBool(RunParameter, false);
			AIAnimator.SetBool(IdleAlertParameter, false);
		}
		else if (CombatStateRef == CombatState.Active){
			AIAnimator.SetBool(IdleCombatParameter, true);
		}
	}
	void AnimationAction (){
		if (!AIAnimator.GetBool("Action")){
			AIAnimator.SetBool("Action", true);
			AIAnimator.SetBool(RunParameter, false); 
			AIAnimator.SetBool(IdleParameter, false);
			AIAnimator.SetBool(IdleCombatParameter, false);
			AIAnimator.SetBool(WalkParameter, false);
			AIAnimator.SetBool(IdleAlertParameter, false);
			AIAnimator.SetBool(TurnRightParameter, false);
			AIAnimator.SetBool(TurnLeftParameter, false);
		}
	}
		
	void FixedUpdate (){
		if (DetectionTypeRef == DetectionType.LineOfSight && OptimizedStateRef == OptimizedState.Active && CombatStateRef == CombatState.NotActive){
			LineOfSightDetection();
		}
	}

	//Sets up the AI's animations
	#if UNITY_EDITOR
	public void SetupAnimations (Animator animator){
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[0].clipNamed = "Idle 1";
		clipOverrides[0].overrideWith = Idle1;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[1].clipNamed = "Walk";
		clipOverrides[1].overrideWith = Walk;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[2].clipNamed = "Run";
		clipOverrides[2].overrideWith = Run;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[3].clipNamed = "Idle (Alert)";
		clipOverrides[3].overrideWith = IdleAlert;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[4].clipNamed = "Idle (Warning)";
		clipOverrides[4].overrideWith = IdleWarning;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[5].clipNamed = "Idle (Combat)";
		clipOverrides[5].overrideWith = IdleCombat;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[6].clipNamed = "Hit 1";
		clipOverrides[6].overrideWith = Hit1;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[7].clipNamed = "Turn Left";
		clipOverrides[7].overrideWith = TurnLeft;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[8].clipNamed = "Turn Right";
		clipOverrides[8].overrideWith = TurnRight;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[9].clipNamed = "Run Attack";
		clipOverrides[9].overrideWith = RunAttack;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[10].clipNamed = "Attack 1";
		clipOverrides[10].overrideWith = Attack1;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[11].clipNamed = "Attack 2";
		clipOverrides[11].overrideWith = Attack2;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[12].clipNamed = "Attack 3";
		clipOverrides[12].overrideWith = Attack3;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[13].clipNamed = "Dead";
		clipOverrides[13].overrideWith = DeadAnim;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[14].clipNamed = "Idle 2";
		clipOverrides[14].overrideWith = Idle2;
		clipOverrides.Add(new AnimationClipOverride());
		clipOverrides[15].clipNamed = "Idle 3";
		clipOverrides[15].overrideWith = Idle3;

		foreach(AnimationClipOverride clipOverride in clipOverrides){
			if (clipOverride.overrideWith != null){
				overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
			}
		}

		animator.runtimeAnimatorController = overrideController;
		clipOverrides.Clear();
	}
	#endif


	//Find colliders within range using a Physics.OverlapSphere. Mask the Physics.OverlapSphere to only 2 layers. 
	//One for Emerald AI objects and one for the Player. This will allow the Physics.OverlapSphere to only get relevent colliders.
	//Once found, use Emerald's custom tag system to find matches for potential targets. Once found, apply them to a list for potential targets.
	//Finally, search through each target in the list and set the nearest one as our current target.
	public void SearchForTarget (){
		StopCoroutine("Attack");
		AttackingInProgress = false;
		AttackDelay = false;

		Collider[] Col = Physics.OverlapSphere(transform.position, DetectionRadius, DetectionLayerMask);
		CollidersInArea = Col;

		foreach (Collider C in CollidersInArea){
			if (C.gameObject != this.gameObject && !potentialTargets.Contains(C.gameObject)){
				if (C.gameObject.GetComponent<Emerald_AI>() != null){
					Emerald_AI EmeraldRef = C.gameObject.GetComponent<Emerald_AI>();
					if (AIFactionsList.Contains(EmeraldRef.CurrentFaction) && FactionRelations[AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && BehaviorRef != CurrentBehavior.Companion){
						potentialTargets.Add(C.gameObject);
					}
					else if (AIFactionsList.Contains(EmeraldRef.CurrentFaction) && FactionRelations[AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && BehaviorRef == CurrentBehavior.Companion && EmeraldRef.CombatStateRef == Emerald_AI.CombatState.Active){
						potentialTargets.Add(C.gameObject);
					}
					else if (AIFactionsList.Contains(EmeraldRef.CurrentFaction) && FactionRelations[AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && BehaviorRef == CurrentBehavior.Companion && EmeraldRef.CombatStateRef == Emerald_AI.CombatState.NotActive){
						CompanionTargetRef = C.transform;
						TargetTypeRef = TargetType.AI;
						TargetEmerald = C.gameObject.GetComponent<Emerald_AI>();
					}
				}
				else if (C.gameObject.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always){
					potentialTargets.Add(C.gameObject);
				}
				else if (C.gameObject.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
					potentialTargets.Add(C.gameObject);
				}
			}
		}
			
		float previousDistance = Mathf.Infinity; 
		float currentDistance;  

		//No targets were found within the AI's trigger radius. Set AI to an inactive combat state and wander.
		if (potentialTargets.Count == 0){
			CurrentDetectionRef = CurrentDetection.Unaware;
			CombatStateRef = CombatState.NotActive;
			AIAgent.stoppingDistance = StoppingDistance;
			CurrentTarget = null;
		}

		foreach (GameObject target in potentialTargets.ToArray()){
			if (target != null){ 
				if (BehaviorRef != CurrentBehavior.Companion){
					distanceBetween = target.transform.position - transform.position;
				}
				else if (BehaviorRef == CurrentBehavior.Companion){
					distanceBetween = target.transform.position - CurrentFollowTarget.position;
				}
				currentDistance = distanceBetween.sqrMagnitude;

				if (currentDistance < previousDistance){
					CurrentTarget = target.transform;
					StopCoroutine("DeathDelay");
					WanderingInProgress = false;
					DeathDelayActive = false;
					AIAgent.stoppingDistance = AttackDistance;
					potentialTargets.Clear();

					//Once a target has been found, reduce the Detection Radius back to the defaul value.
					DetectionRadius = StartingDetectionRadius;
					AICollider.radius = StartingDetectionRadius;
					fieldOfViewAngle = fieldOfViewAngleRef;

					if (CurrentTarget.tag == EmeraldTag){
						ReceivedFaction = CurrentTarget.GetComponent<Emerald_AI>().CurrentFaction;
					}

					if (AIFactionsList.Contains(ReceivedFaction) && FactionRelations[AIFactionsList.IndexOf(ReceivedFaction)] == 0){
						TargetTypeRef = TargetType.AI;
						TargetEmerald = CurrentTarget.GetComponent<Emerald_AI>();
					}
					if (CurrentTarget.tag == PlayerTag && AIAttacksPlayerRef == AIAttacksPlayer.Always){
						TargetTypeRef = TargetType.Player;
					}
					if (CurrentTarget.tag == NonAITag && UseNonAITagRef == UseNonAITag.Yes){
						TargetTypeRef = TargetType.NonAITarget;
					}

					previousDistance = currentDistance;
				}
			}
		}
	}

	public void SearchForDifferentTarget (){
		Collider[] Col = Physics.OverlapSphere(transform.position, DetectionRadius, DetectionLayerMask);
		CollidersInArea = Col;

		foreach (Collider C in CollidersInArea){
			if (C.gameObject != this.gameObject && !potentialTargets.Contains(C.gameObject) && !potentialTargets.Contains(CurrentTarget.gameObject)){
				if (C.gameObject.GetComponent<Emerald_AI>() != null){
					if (AIFactionsList.Contains(C.gameObject.GetComponent<Emerald_AI>().CurrentFaction)){
						potentialTargets.Add(C.gameObject);
					}
				}
				else if (C.gameObject.CompareTag(PlayerTag) && AIAttacksPlayerRef == AIAttacksPlayer.Always){
					potentialTargets.Add(C.gameObject);
				}

				if (potentialTargets.Count > 1){
					CurrentTarget = potentialTargets[Random.Range(0, potentialTargets.Count)].transform;
					AIAgent.stoppingDistance = AttackDistance;
					potentialTargets.Clear();
				}
			}
		}
	}

	IEnumerator TooClose (){
		BackingUp = true;

		if (AttackCoroutine != null){
			StopCoroutine(AttackCoroutine);
			AttackingInProgress = false;
			TargetInRange = false;
			TargetInView = false;
			AttackDelay = false;
		}

		AIAnimator.ResetTrigger("Attack 1");
		AIAnimator.ResetTrigger("Attack 2");
		AIAnimator.ResetTrigger("Attack 3");

		//AnimationRun();
		BackUpSeconds = 3f;
		RandomDirection = Random.Range(0,2);

		if (RandomDirection == 0){
			Destination(CurrentTarget.position + transform.right * AttackDistance);
		}
		else if (RandomDirection == 1){
			Destination(CurrentTarget.position + transform.right * -AttackDistance);
		}

		if (CurrentTarget == null){
			BackingUp = false;
			AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);
			StopCoroutine("TooClose");
		}
			
		yield return new WaitForSeconds(BackUpSeconds);
		//AnimationRun();
		AIAgent.stoppingDistance = AttackDistance;
		AIAnimator.SetFloat("Run Speed", RunAnimationSpeed);
		BackingUp = false;
	}
}