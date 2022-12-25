using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEditorInternal;

[System.Serializable]
[CustomEditor(typeof(Emerald_AI))] 
[CanEditMultipleObjects]
public class Emerald_AI_Editor : Editor {

	//ints
	SerializedProperty TabNumberProp;
	SerializedProperty TemperamentTabNumberProp;
	SerializedProperty DetectionTagsTabNumberProp;
	SerializedProperty AnimationTabNumberProp;
	SerializedProperty fieldOfViewAngleProp;
	SerializedProperty DetectionRadiusProp;
	SerializedProperty 	WanderRadiusProp;
	SerializedProperty MinimumWaitTimeProp;
	SerializedProperty MaximumWaitTimeProp;
	SerializedProperty WalkSpeedProp;
	SerializedProperty RunSpeedProp;
	SerializedProperty StartingHealthProp;
	SerializedProperty MinimumDamageAmount1Prop;
	SerializedProperty MaximumDamageAmount1Prop;
	SerializedProperty MinimumDamageAmount2Prop;
	SerializedProperty MaximumDamageAmount2Prop;
	SerializedProperty MinimumDamageAmount3Prop;
	SerializedProperty MaximumDamageAmount3Prop;
	SerializedProperty MinimumDamageAmountRunProp;
	SerializedProperty MaximumDamageAmountRunProp;
	SerializedProperty DamageAmount1Prop;
	SerializedProperty DamageAmount2Prop;
	SerializedProperty DamageAmount3Prop;
	SerializedProperty DamageAmountRunProp;
	SerializedProperty TurnSpeedProp;
	SerializedProperty MinimumFollowWaitTimeProp;
	SerializedProperty MaximumFollowWaitTimeProp;
	SerializedProperty MinimumChaseWaitTimeProp;
	SerializedProperty MaximumChaseWaitTimeProp;
	SerializedProperty MaxChaseDistanceProp;
	SerializedProperty HealthPercentageToFleeProp;
	SerializedProperty DeactivateSecondsProp;
	SerializedProperty AlertWanderRadiusProp;
	SerializedProperty MinimumAlertWaitTimeProp;
	SerializedProperty MaximumAlertWaitTimeProp;
	SerializedProperty HealthRegRateProp;
	SerializedProperty RegenerateAmountProp;
	SerializedProperty DeathDelayMinProp;
	SerializedProperty DeathDelayMaxProp;
	SerializedProperty CurrentFactionProp;
	SerializedProperty ExpandedFieldOfViewAngleProp;
	SerializedProperty ExpandedDetectionRadiusProp;
	SerializedProperty AttacksToRepositionProp;
	SerializedProperty StationaryIdleSecondsMinProp;
	SerializedProperty StationaryIdleSecondsMaxProp;
	SerializedProperty IdleSoundsSecondsMinProp;
	SerializedProperty IdleSoundsSecondsMaxProp;

	SerializedProperty FactionRelation1Prop;
	SerializedProperty FactionRelation2Prop;
	SerializedProperty FactionRelation3Prop;
	SerializedProperty FactionRelation4Prop;
	SerializedProperty FactionRelation5Prop;

	SerializedProperty Attack1ProjectileSpeedProp;
	SerializedProperty Attack2ProjectileSpeedProp;
	SerializedProperty Attack3ProjectileSpeedProp;
	SerializedProperty RunAttackProjectileSpeedProp;

	//floats
	SerializedProperty MinimumAttackSpeedProp;
	SerializedProperty MaximumAttackSpeedProp;
	SerializedProperty StoppingDistanceProp;
	SerializedProperty FollowingStoppingDistanceProp;
	SerializedProperty RunAttackDistanceProp;
	SerializedProperty AgentRadiusProp;
	SerializedProperty AgentBaseOffsetProp;
	SerializedProperty AgentTurnSpeedProp;
	SerializedProperty AgentAccelerationProp;
	SerializedProperty MaxXAngleProp;
	SerializedProperty MaxZAngleProp;
	SerializedProperty RunAnimationSpeedProp;
	SerializedProperty Idle1AnimationSpeedProp;
	SerializedProperty Idle2AnimationSpeedProp;
	SerializedProperty Idle3AnimationSpeedProp;
	SerializedProperty IdleAlertAnimationSpeedProp;
	SerializedProperty IdleWarningAnimationSpeedProp;
	SerializedProperty IdleCombatAnimationSpeedProp;
	SerializedProperty WalkAnimationSpeedProp;
	SerializedProperty Attack1AnimationSpeedProp;
	SerializedProperty Attack2AnimationSpeedProp;
	SerializedProperty Attack3AnimationSpeedProp;
	SerializedProperty RunAttackAnimationSpeedProp;
	SerializedProperty TurnLeftAnimationSpeedProp;
	SerializedProperty TurnRightAnimationSpeedProp;
	SerializedProperty HitAnimationSpeedProp;
	SerializedProperty DeathAnimationSpeedProp;
	SerializedProperty AngleNeededToTurnProp;
	SerializedProperty AngleNeededToTurnRunningProp;
	SerializedProperty Attack1DelayProp;
	SerializedProperty Attack2DelayProp;
	SerializedProperty Attack3DelayProp;
	SerializedProperty RunAttackDelayProp;
	SerializedProperty FootStepSecondsWalkProp;
	SerializedProperty FootStepSecondsRunProp;
	SerializedProperty AttackDistanceProp;
	SerializedProperty ProjectileTimeoutTimeProp;
	SerializedProperty Attack1CollisionSecondsProp;
	SerializedProperty Attack2CollisionSecondsProp;
	SerializedProperty Attack3CollisionSecondsProp;
	SerializedProperty RunAttackCollisionSecondsProp;
	SerializedProperty Effect1TimeoutSecondsProp;
	SerializedProperty Effect2TimeoutSecondsProp;
	SerializedProperty Effect3TimeoutSecondsProp;
	SerializedProperty RunEffectTimeoutSecondsProp;
	SerializedProperty BloodEffectTimeoutSecondsProp;
	SerializedProperty TooCloseDistanceProp;
	SerializedProperty PlayerYOffsetProp;

	SerializedProperty Attack1HeatSeekingSecondsProp;
	SerializedProperty Attack2HeatSeekingSecondsProp;
	SerializedProperty Attack3HeatSeekingSecondsProp;
	SerializedProperty RunAttackHeatSeekingSecondsProp;

	//enums
	SerializedProperty BehaviorProp;
	SerializedProperty ConfidenceProp;
	SerializedProperty RandomizeDamageProp;
	SerializedProperty DetectionTypeProp;
	SerializedProperty MaxChaseDistanceTypeProp;
	SerializedProperty CombatTypeProp;
	SerializedProperty CreateHealthBarsProp;
	SerializedProperty UseCombatTextProp;
	SerializedProperty CustomizeHealthBarProp;
	SerializedProperty UseCustomFontProp;
	SerializedProperty AnimateCombatTextProp;
	SerializedProperty DisplayAINameProp;
	SerializedProperty DisplayAITitleProp;
	SerializedProperty DisplayAILevelProp;
	SerializedProperty OpposingFactionsEnumProp;
	SerializedProperty RefillHealthRTSProp;
	SerializedProperty WanderTypeProp;
	SerializedProperty WaypointTypeProp;
	SerializedProperty AlignAIWithGroundProp;
	SerializedProperty WanderAnimationProp;
	SerializedProperty UseBloodEffectProp;
	SerializedProperty AttackSoundsEnumProp;
	SerializedProperty InjuredSoundsEnumProp;
	SerializedProperty WarningSoundsEnumProp;
	SerializedProperty DeathSoundsEnumProp;
	SerializedProperty FootStepSoundsEnumProp;
	SerializedProperty UseWarningAnimationProp;
	SerializedProperty TotalLODsProp;
	SerializedProperty HasMultipleLODsProp;
	SerializedProperty AlignAIOnStartProp;
	SerializedProperty AlignmentQualityProp;
	SerializedProperty UseSeparateAlertWaitTimeProp;
	SerializedProperty UseSeparateAlertRadiusProp;
	SerializedProperty TotalAttackAnimationsProp;
	SerializedProperty TotalIdleAnimationsProp;
	SerializedProperty FollowSpeedTypeProp;
	SerializedProperty FootstepSoundTypeProp;
	SerializedProperty PickTargetMethodProp;
	SerializedProperty AIAttacksPlayerProp;
	SerializedProperty UseNonAITagProp;
	SerializedProperty WeaponTypeProp;
	SerializedProperty UseRunAttacksProp;
	SerializedProperty ObstructionDetectionQualityProp;
	SerializedProperty RecycleAIProp;
	SerializedProperty AvoidanceQualityProp;
	SerializedProperty DynamicMovementProp;
	SerializedProperty IdleSoundsEnumProp;

	SerializedProperty Attack1HeatSeekingProp;
	SerializedProperty Attack2HeatSeekingProp;
	SerializedProperty Attack3HeatSeekingProp;
	SerializedProperty RunAttackHeatSeekingProp;

	SerializedProperty Attack1EffectOnCollisionProp;
	SerializedProperty Attack2EffectOnCollisionProp;
	SerializedProperty Attack3EffectOnCollisionProp;
	SerializedProperty RunAttackEffectOnCollisionProp;
	SerializedProperty Attack1SoundOnCollisionProp;
	SerializedProperty Attack2SoundOnCollisionProp;
	SerializedProperty Attack3SoundOnCollisionProp;
	SerializedProperty RunAttackSoundOnCollisionProp;


	SerializedProperty UseAlertAnimationProp;
	SerializedProperty UseRandomRotationOnStartProp;
	SerializedProperty UseDeactivateDelayProp;
	SerializedProperty AIRegeneratesHealthProp;
	SerializedProperty DisableAIWhenNotInViewProp;

	//string
	SerializedProperty AINameProp;
	SerializedProperty AITitleProp;
	SerializedProperty AILevelProp;
	SerializedProperty PlayerTagProp;
	SerializedProperty FollowTagProp;
	SerializedProperty UITagProp;
	SerializedProperty NonAITagProp;
	SerializedProperty EmeraldTagProp;

	//objects
	SerializedProperty HealthBarImageProp;
	SerializedProperty HealthBarBackgroundImageProp;
	SerializedProperty CombatTextFontProp;
	SerializedProperty CombatTextPosProp;
	SerializedProperty BloodEffectProp;
	SerializedProperty Renderer1Prop;
	SerializedProperty Renderer2Prop;
	SerializedProperty Renderer3Prop;
	SerializedProperty Renderer4Prop;
	SerializedProperty Attack1ProjectileProp;
	SerializedProperty Attack2ProjectileProp;
	SerializedProperty Attack3ProjectileProp;
	SerializedProperty RunAttackProjectileProp;
	SerializedProperty RangedAttackTransformProp;

	SerializedProperty Attack1CollisionEffectProp;
	SerializedProperty Attack1ImpactSoundProp;
	SerializedProperty Attack2CollisionEffectProp;
	SerializedProperty Attack2ImpactSoundProp;
	SerializedProperty Attack3CollisionEffectProp;
	SerializedProperty Attack3ImpactSoundProp;
	SerializedProperty RunAttackCollisionEffectProp;
	SerializedProperty RunAttackImpactSoundProp;

	//vectors 
	SerializedProperty HealthBarPosProp;
	SerializedProperty NameTextSizeProp;
	SerializedProperty HealthBarScaleProp;
	SerializedProperty BloodPosOffsetProp;
	SerializedProperty AINamePosProp;

	//color
	SerializedProperty HealthBarColorProp;
	SerializedProperty HealthBarBackgroundColorProp;
	SerializedProperty CombatTextColorProp;
	SerializedProperty NameTextColorProp;
	SerializedProperty LevelTextColorProp;

	//Lists
	SerializedProperty OpposingFaction1Prop;
	SerializedProperty OpposingFaction2Prop;
	SerializedProperty OpposingFaction3Prop;
	SerializedProperty OpposingFaction4Prop;
	SerializedProperty OpposingFaction5Prop;

	SerializedProperty AttackSound1Prop;
	SerializedProperty AttackSound2Prop;
	SerializedProperty AttackSound3Prop;
	SerializedProperty AttackSound4Prop;
	SerializedProperty AttackSound5Prop;

	SerializedProperty InjuredSound1Prop;
	SerializedProperty InjuredSound2Prop;
	SerializedProperty InjuredSound3Prop;
	SerializedProperty InjuredSound4Prop;
	SerializedProperty InjuredSound5Prop;

	SerializedProperty WarningSound1Prop;
	SerializedProperty WarningSound2Prop;
	SerializedProperty WarningSound3Prop;
	SerializedProperty WarningSound4Prop;
	SerializedProperty WarningSound5Prop;

	SerializedProperty DeathSound1Prop;
	SerializedProperty DeathSound2Prop;
	SerializedProperty DeathSound3Prop;
	SerializedProperty DeathSound4Prop;
	SerializedProperty DeathSound5Prop;

	SerializedProperty IdleSound1Prop;
	SerializedProperty IdleSound2Prop;
	SerializedProperty IdleSound3Prop;
	SerializedProperty IdleSound4Prop;
	SerializedProperty IdleSound5Prop;

	SerializedProperty FootStepSound1Prop;
	SerializedProperty FootStepSound2Prop;
	SerializedProperty FootStepSound3Prop;
	SerializedProperty FootStepSound4Prop;
	SerializedProperty FootStepSound5Prop;

	//bools
	SerializedProperty HealthBarsFoldoutProp;
	SerializedProperty CombatTextFoldoutProp;
	SerializedProperty NameTextFoldoutProp;
	SerializedProperty AnimationsUpdatedProp;
	SerializedProperty BehaviorFoldout;
	SerializedProperty ConfidenceFoldout;
	SerializedProperty WanderFoldout;
	SerializedProperty CombatStyleFoldout;
	SerializedProperty WaypointsFoldout;
	SerializedProperty Projectile1UpdatedProp;
	SerializedProperty Projectile2UpdatedProp;
	SerializedProperty Projectile3UpdatedProp;
	SerializedProperty RunProjectileUpdatedProp;
	SerializedProperty BloodEffectUpdatedProp;

	//Animations
	SerializedProperty Idle1Prop;
	SerializedProperty Idle2Prop;
	SerializedProperty Idle3Prop;
	SerializedProperty WalkProp;
	SerializedProperty RunProp;
	SerializedProperty IdleAlertProp;
	SerializedProperty IdleWarningProp;
	SerializedProperty IdleCombatProp;
	SerializedProperty Hit1Prop;
	SerializedProperty TurnLeftProp;
	SerializedProperty TurnRightProp;
	SerializedProperty RunAttackProp;
	SerializedProperty Attack1Prop;
	SerializedProperty Attack2Prop;
	SerializedProperty Attack3Prop;
	SerializedProperty DeadProp;

	SerializedProperty DetectionLayerMaskProp;
	SerializedProperty ObstructionDetectionLayerMaskProp;

	public string[] TabName = new string[] {"Temperament", "AI's Settings", "Detection/Tags", "UI", "Sounds", "Waypoint Editor", "Animations", "Documentation"};
	public string[] TemperamentName = new string[] {"Stats", "Movement", "Combat", "NavMesh", "Optimize"};

	Texture TemperamentIcon;
	Texture SettingsIcon;
	Texture DetectTagsIcon;
	Texture UIIcon;
	Texture SoundIcon;
	Texture WaypointEditorIcon;
	Texture AnimationIcon;
	Texture DocumentationIcon;

	void LoadFactionData()
	{
		string path = "Assets/Emerald AI 2.0/Resources/EmeraldAIFactions.txt";

		TextAsset FactionData = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

		if (FactionData != null){
			string[] textLines =  FactionData.text.Split (',');

			foreach (string s in textLines){
				if (!Emerald_AI.StringFactionList.Contains(s) && s != ""){
					Emerald_AI.StringFactionList.Add(s);
				}
			}
		}
	}

	void OnEnable () {

		Emerald_AI self = (Emerald_AI)target;

		LoadFactionData();

		//ints
		TabNumberProp = serializedObject.FindProperty ("TabNumber");
		TemperamentTabNumberProp = serializedObject.FindProperty ("TemperamentTabNumber");
		DetectionTagsTabNumberProp = serializedObject.FindProperty ("DetectionTagsTabNumber");
		AnimationTabNumberProp = serializedObject.FindProperty ("AnimationTabNumber");
		fieldOfViewAngleProp = serializedObject.FindProperty ("fieldOfViewAngle");
		DetectionRadiusProp = serializedObject.FindProperty ("DetectionRadius");
		WanderRadiusProp = serializedObject.FindProperty ("WanderRadius");
		MinimumWaitTimeProp = serializedObject.FindProperty ("MinimumWaitTime");
		MaximumWaitTimeProp = serializedObject.FindProperty ("MaximumWaitTime");
		WalkSpeedProp = serializedObject.FindProperty ("WalkSpeed");
		RunSpeedProp = serializedObject.FindProperty ("RunSpeed");
		StartingHealthProp = serializedObject.FindProperty ("StartingHealth");
		MinimumDamageAmount1Prop = serializedObject.FindProperty ("MinimumDamageAmount1");
		MaximumDamageAmount1Prop = serializedObject.FindProperty ("MaximumDamageAmount1");
		MinimumDamageAmount2Prop = serializedObject.FindProperty ("MinimumDamageAmount2");
		MaximumDamageAmount2Prop = serializedObject.FindProperty ("MaximumDamageAmount2");
		MinimumDamageAmount3Prop = serializedObject.FindProperty ("MinimumDamageAmount3");
		MaximumDamageAmount3Prop = serializedObject.FindProperty ("MaximumDamageAmount3");
		MinimumDamageAmountRunProp = serializedObject.FindProperty ("MinimumDamageAmountRun");
		MaximumDamageAmountRunProp = serializedObject.FindProperty ("MaximumDamageAmountRun");
		DamageAmount1Prop = serializedObject.FindProperty ("DamageAmount1");
		DamageAmount2Prop = serializedObject.FindProperty ("DamageAmount2");
		DamageAmount3Prop = serializedObject.FindProperty ("DamageAmount3");
		DamageAmountRunProp = serializedObject.FindProperty ("DamageAmountRun");
		TurnSpeedProp = serializedObject.FindProperty ("TurnSpeed");
		MinimumFollowWaitTimeProp = serializedObject.FindProperty ("MinimumFollowWaitTime");
		MaximumFollowWaitTimeProp = serializedObject.FindProperty ("MaximumFollowWaitTime");
		MinimumChaseWaitTimeProp = serializedObject.FindProperty ("MinimumChaseWaitTime");
		MaximumChaseWaitTimeProp = serializedObject.FindProperty ("MaximumChaseWaitTime");
		MaxChaseDistanceProp = serializedObject.FindProperty ("MaxChaseDistance");
		HealthPercentageToFleeProp = serializedObject.FindProperty ("HealthPercentageToFlee");
		DeactivateSecondsProp = serializedObject.FindProperty ("DeactivateSeconds");
		AlertWanderRadiusProp = serializedObject.FindProperty ("AlertWanderRadius");
		MinimumAlertWaitTimeProp = serializedObject.FindProperty ("MinimumAlertWaitTime");
		MaximumAlertWaitTimeProp = serializedObject.FindProperty ("MaximumAlertWaitTime");
		RegenerateAmountProp = serializedObject.FindProperty ("RegenerateAmount");
		DeathDelayMinProp = serializedObject.FindProperty ("DeathDelayMin");
		DeathDelayMaxProp = serializedObject.FindProperty ("DeathDelayMax");
		CurrentFactionProp = serializedObject.FindProperty ("CurrentFaction");
		StationaryIdleSecondsMinProp = serializedObject.FindProperty ("StationaryIdleSecondsMin");
		StationaryIdleSecondsMaxProp = serializedObject.FindProperty ("StationaryIdleSecondsMax");
		IdleSoundsSecondsMinProp = serializedObject.FindProperty ("IdleSoundsSecondsMin");
		IdleSoundsSecondsMaxProp = serializedObject.FindProperty ("IdleSoundsSecondsMax");

		FactionRelation1Prop = serializedObject.FindProperty ("FactionRelation1");
		FactionRelation2Prop = serializedObject.FindProperty ("FactionRelation2");
		FactionRelation3Prop = serializedObject.FindProperty ("FactionRelation3");
		FactionRelation4Prop = serializedObject.FindProperty ("FactionRelation4");
		FactionRelation5Prop = serializedObject.FindProperty ("FactionRelation5");

		Attack1ProjectileSpeedProp = serializedObject.FindProperty ("Attack1ProjectileSpeed");
		Attack2ProjectileSpeedProp = serializedObject.FindProperty ("Attack2ProjectileSpeed");
		Attack3ProjectileSpeedProp = serializedObject.FindProperty ("Attack3ProjectileSpeed");
		RunAttackProjectileSpeedProp = serializedObject.FindProperty ("RunAttackProjectileSpeed");

		//floats
		MinimumAttackSpeedProp = serializedObject.FindProperty ("MinimumAttackSpeed");
		MaximumAttackSpeedProp = serializedObject.FindProperty ("MaximumAttackSpeed");
		StoppingDistanceProp = serializedObject.FindProperty ("StoppingDistance");
		FollowingStoppingDistanceProp = serializedObject.FindProperty ("FollowingStoppingDistance");
		RunAttackDistanceProp = serializedObject.FindProperty ("RunAttackDistance");
		AgentRadiusProp = serializedObject.FindProperty ("AgentRadius");
		AgentBaseOffsetProp = serializedObject.FindProperty ("AgentBaseOffset");
		AgentTurnSpeedProp = serializedObject.FindProperty ("AgentTurnSpeed");
		AgentAccelerationProp = serializedObject.FindProperty ("AgentAcceleration");
		MaxXAngleProp = serializedObject.FindProperty ("MaxXAngle");
		MaxZAngleProp = serializedObject.FindProperty ("MaxZAngle");
		Idle1AnimationSpeedProp = serializedObject.FindProperty ("Idle1AnimationSpeed");
		Idle2AnimationSpeedProp = serializedObject.FindProperty ("Idle2AnimationSpeed");
		Idle3AnimationSpeedProp = serializedObject.FindProperty ("Idle3AnimationSpeed");
		IdleAlertAnimationSpeedProp = serializedObject.FindProperty ("IdleAlertAnimationSpeed");
		IdleWarningAnimationSpeedProp = serializedObject.FindProperty ("IdleWarningAnimationSpeed");
		IdleCombatAnimationSpeedProp = serializedObject.FindProperty ("IdleCombatAnimationSpeed");
		RunAnimationSpeedProp = serializedObject.FindProperty ("RunAnimationSpeed");
		WalkAnimationSpeedProp = serializedObject.FindProperty ("WalkAnimationSpeed");
		Attack1AnimationSpeedProp = serializedObject.FindProperty ("Attack1AnimationSpeed");
		Attack2AnimationSpeedProp = serializedObject.FindProperty ("Attack2AnimationSpeed");
		Attack3AnimationSpeedProp = serializedObject.FindProperty ("Attack3AnimationSpeed");
		RunAttackAnimationSpeedProp = serializedObject.FindProperty ("RunAttackAnimationSpeed");
		TurnLeftAnimationSpeedProp = serializedObject.FindProperty ("TurnLeftAnimationSpeed");
		TurnRightAnimationSpeedProp = serializedObject.FindProperty ("TurnRightAnimationSpeed");
		HitAnimationSpeedProp = serializedObject.FindProperty ("HitAnimationSpeed");
		DeathAnimationSpeedProp = serializedObject.FindProperty ("DeathAnimationSpeed");
		AngleNeededToTurnProp = serializedObject.FindProperty ("AngleNeededToTurn");
		AngleNeededToTurnRunningProp = serializedObject.FindProperty ("AngleNeededToTurnRunning");
		Attack1DelayProp = serializedObject.FindProperty ("Attack1Delay");
		Attack2DelayProp = serializedObject.FindProperty ("Attack2Delay");
		Attack3DelayProp = serializedObject.FindProperty ("Attack3Delay");
		RunAttackDelayProp = serializedObject.FindProperty ("RunAttackDelay");
		FootStepSecondsWalkProp = serializedObject.FindProperty ("FootStepSecondsWalk");
		FootStepSecondsRunProp = serializedObject.FindProperty ("FootStepSecondsRun");
		AttackDistanceProp = serializedObject.FindProperty ("AttackDistance");
		ProjectileTimeoutTimeProp = serializedObject.FindProperty ("ProjectileTimeoutTime");
		HealthRegRateProp = serializedObject.FindProperty ("HealthRegRate");
		Attack1CollisionSecondsProp = serializedObject.FindProperty ("Attack1CollisionSeconds");
		Attack2CollisionSecondsProp = serializedObject.FindProperty ("Attack2CollisionSeconds");
		Attack3CollisionSecondsProp = serializedObject.FindProperty ("Attack3CollisionSeconds");
		RunAttackCollisionSecondsProp = serializedObject.FindProperty ("RunAttackCollisionSeconds");
		Effect1TimeoutSecondsProp = serializedObject.FindProperty ("Effect1TimeoutSeconds");
		Effect2TimeoutSecondsProp = serializedObject.FindProperty ("Effect2TimeoutSeconds");
		Effect3TimeoutSecondsProp = serializedObject.FindProperty ("Effect3TimeoutSeconds");
		RunEffectTimeoutSecondsProp = serializedObject.FindProperty ("RunEffectTimeoutSeconds");
		BloodEffectTimeoutSecondsProp = serializedObject.FindProperty ("BloodEffectTimeoutSeconds");
		TooCloseDistanceProp = serializedObject.FindProperty ("TooCloseDistance");
		PlayerYOffsetProp = serializedObject.FindProperty ("PlayerYOffset");
		Attack1HeatSeekingSecondsProp = serializedObject.FindProperty ("Attack1HeatSeekingSeconds");
		Attack2HeatSeekingSecondsProp = serializedObject.FindProperty ("Attack2HeatSeekingSeconds");
		Attack3HeatSeekingSecondsProp = serializedObject.FindProperty ("Attack3HeatSeekingSeconds");
		RunAttackHeatSeekingSecondsProp = serializedObject.FindProperty ("RunAttackHeatSeekingSeconds");
		ExpandedFieldOfViewAngleProp = serializedObject.FindProperty ("ExpandedFieldOfViewAngle");
		ExpandedDetectionRadiusProp = serializedObject.FindProperty ("ExpandedDetectionRadius");

		//enums
		BehaviorProp = serializedObject.FindProperty ("BehaviorRef");
		ConfidenceProp = serializedObject.FindProperty ("ConfidenceRef");
		RandomizeDamageProp = serializedObject.FindProperty ("RandomizeDamageRef");
		DetectionTypeProp = serializedObject.FindProperty ("DetectionTypeRef");
		MaxChaseDistanceTypeProp = serializedObject.FindProperty ("MaxChaseDistanceTypeRef");
		CombatTypeProp = serializedObject.FindProperty ("CombatTypeRef");
		CreateHealthBarsProp = serializedObject.FindProperty ("CreateHealthBarsRef");
		UseCombatTextProp = serializedObject.FindProperty ("UseCombatTextRef");
		CustomizeHealthBarProp = serializedObject.FindProperty ("CustomizeHealthBarRef");
		UseCustomFontProp = serializedObject.FindProperty ("UseCustomFontRef");
		AnimateCombatTextProp = serializedObject.FindProperty ("AnimateCombatTextRef");
		DisplayAINameProp = serializedObject.FindProperty ("DisplayAINameRef");
		DisplayAITitleProp = serializedObject.FindProperty ("DisplayAITitleRef");
		DisplayAILevelProp = serializedObject.FindProperty ("DisplayAILevelRef");
		OpposingFactionsEnumProp = serializedObject.FindProperty ("OpposingFactionsEnumRef");
		RefillHealthRTSProp = serializedObject.FindProperty ("RefillHealthRTSRef");
		WanderTypeProp = serializedObject.FindProperty ("WanderTypeRef");
		WaypointTypeProp = serializedObject.FindProperty ("WaypointTypeRef");
		AlignAIWithGroundProp = serializedObject.FindProperty ("AlignAIWithGroundRef");
		//LMProp = serializedObject.FindProperty ("LM");
		WanderAnimationProp = serializedObject.FindProperty ("WanderAnimationRef");
		UseBloodEffectProp = serializedObject.FindProperty ("UseBloodEffectRef");
		AttackSoundsEnumProp = serializedObject.FindProperty ("AttackSoundsEnumRef");
		InjuredSoundsEnumProp = serializedObject.FindProperty ("InjuredSoundsEnumRef");
		WarningSoundsEnumProp = serializedObject.FindProperty ("WarningSoundsEnumRef");
		DeathSoundsEnumProp = serializedObject.FindProperty ("DeathSoundsEnumRef");
		FootStepSoundsEnumProp = serializedObject.FindProperty ("FootStepSoundsEnumRef");
		UseWarningAnimationProp = serializedObject.FindProperty ("UseWarningAnimationRef");
		TotalLODsProp = serializedObject.FindProperty ("TotalLODsRef");
		HasMultipleLODsProp = serializedObject.FindProperty ("HasMultipleLODsRef");
		AlignAIOnStartProp = serializedObject.FindProperty ("AlignAIOnStartRef");
		AlignmentQualityProp = serializedObject.FindProperty ("AlignmentQualityRef");
		UseSeparateAlertWaitTimeProp = serializedObject.FindProperty ("UseSeparateAlertWaitTimeRef");
		UseSeparateAlertRadiusProp = serializedObject.FindProperty ("UseSeparateAlertRadiusRef");
		TotalAttackAnimationsProp = serializedObject.FindProperty ("TotalAttackAnimationsRef");
		TotalIdleAnimationsProp = serializedObject.FindProperty ("TotalIdleAnimationsRef");
		FollowSpeedTypeProp = serializedObject.FindProperty ("FollowSpeedTypeRef");
		FootstepSoundTypeProp = serializedObject.FindProperty ("FootstepSoundTypeRef");
		PickTargetMethodProp = serializedObject.FindProperty ("PickTargetMethodRef");
		AIAttacksPlayerProp = serializedObject.FindProperty ("AIAttacksPlayerRef");
		UseNonAITagProp = serializedObject.FindProperty ("UseNonAITagRef");
		WeaponTypeProp = serializedObject.FindProperty ("WeaponTypeRef");
		UseRunAttacksProp = serializedObject.FindProperty ("UseRunAttacksRef");
		ObstructionDetectionQualityProp = serializedObject.FindProperty ("ObstructionDetectionQualityRef");
		RecycleAIProp = serializedObject.FindProperty ("RecycleAIRef");
		AvoidanceQualityProp = serializedObject.FindProperty ("AvoidanceQualityRef");
		DynamicMovementProp = serializedObject.FindProperty ("DynamicMovementRef");
		IdleSoundsEnumProp = serializedObject.FindProperty ("IdleSoundsEnumRef");

		Attack1HeatSeekingProp = serializedObject.FindProperty ("Attack1HeatSeekingRef");
		Attack2HeatSeekingProp = serializedObject.FindProperty ("Attack2HeatSeekingRef");
		Attack3HeatSeekingProp = serializedObject.FindProperty ("Attack3HeatSeekingRef");
		RunAttackHeatSeekingProp = serializedObject.FindProperty ("RunAttackHeatSeekingRef");

		Attack1EffectOnCollisionProp = serializedObject.FindProperty ("Attack1EffectOnCollisionRef");
		Attack2EffectOnCollisionProp = serializedObject.FindProperty ("Attack2EffectOnCollisionRef");
		Attack3EffectOnCollisionProp = serializedObject.FindProperty ("Attack3EffectOnCollisionRef");
		RunAttackEffectOnCollisionProp = serializedObject.FindProperty ("RunAttackEffectOnCollisionRef");
		Attack1SoundOnCollisionProp = serializedObject.FindProperty ("Attack1SoundOnCollisionRef");
		Attack2SoundOnCollisionProp = serializedObject.FindProperty ("Attack2SoundOnCollisionRef");
		Attack3SoundOnCollisionProp = serializedObject.FindProperty ("Attack3SoundOnCollisionRef");
		RunAttackSoundOnCollisionProp = serializedObject.FindProperty ("RunAttackSoundOnCollisionRef");

		UseAlertAnimationProp = serializedObject.FindProperty ("UseAlertAnimationRef");
		UseRandomRotationOnStartProp = serializedObject.FindProperty ("UseRandomRotationOnStartRef");
		UseDeactivateDelayProp = serializedObject.FindProperty ("UseDeactivateDelayRef");
		AIRegeneratesHealthProp = serializedObject.FindProperty ("AIRegeneratesHealthRef");
		DisableAIWhenNotInViewProp = serializedObject.FindProperty ("DisableAIWhenNotInViewRef");

		//string
		AINameProp = serializedObject.FindProperty ("AIName");
		AITitleProp = serializedObject.FindProperty ("AITitle");
		AILevelProp = serializedObject.FindProperty ("AILevel");
		PlayerTagProp = serializedObject.FindProperty ("PlayerTag");
		FollowTagProp = serializedObject.FindProperty ("FollowTag");
		UITagProp = serializedObject.FindProperty ("UITag");
		EmeraldTagProp = serializedObject.FindProperty ("EmeraldTag");
		NonAITagProp = serializedObject.FindProperty ("NonAITag");

		//objects
		HealthBarImageProp = serializedObject.FindProperty ("HealthBarImage");
		HealthBarBackgroundImageProp = serializedObject.FindProperty ("HealthBarBackgroundImage");
		CombatTextPosProp = serializedObject.FindProperty ("CombatTextPos");
		CombatTextFontProp = serializedObject.FindProperty ("CombatTextFont");
		BloodEffectProp = serializedObject.FindProperty ("BloodEffect");
		Attack1ProjectileProp = serializedObject.FindProperty ("Attack1Projectile");
		Attack2ProjectileProp = serializedObject.FindProperty ("Attack2Projectile");
		Attack3ProjectileProp = serializedObject.FindProperty ("Attack3Projectile");
		RunAttackProjectileProp = serializedObject.FindProperty ("RunAttackProjectile");
		RangedAttackTransformProp = serializedObject.FindProperty ("RangedAttackTransform");

		Attack1CollisionEffectProp = serializedObject.FindProperty ("Attack1CollisionEffect");
		Attack1ImpactSoundProp = serializedObject.FindProperty ("Attack1ImpactSound");
		Attack2CollisionEffectProp = serializedObject.FindProperty ("Attack2CollisionEffect");
		Attack2ImpactSoundProp = serializedObject.FindProperty ("Attack2ImpactSound");
		Attack3CollisionEffectProp = serializedObject.FindProperty ("Attack3CollisionEffect");
		Attack3ImpactSoundProp = serializedObject.FindProperty ("Attack3ImpactSound");
		RunAttackCollisionEffectProp = serializedObject.FindProperty ("RunAttackCollisionEffect");
		RunAttackImpactSoundProp = serializedObject.FindProperty ("RunAttackImpactSound");

		//vector
		HealthBarPosProp = serializedObject.FindProperty ("HealthBarPos");
		NameTextSizeProp = serializedObject.FindProperty ("NameTextSize");
		HealthBarScaleProp = serializedObject.FindProperty ("HealthBarScale");
		BloodPosOffsetProp = serializedObject.FindProperty ("BloodPosOffset");
		AINamePosProp = serializedObject.FindProperty ("AINamePos");

		//color
		HealthBarColorProp = serializedObject.FindProperty ("HealthBarColor");
		HealthBarBackgroundColorProp = serializedObject.FindProperty ("HealthBarBackgroundColor");
		CombatTextColorProp = serializedObject.FindProperty ("CombatTextColor");
		NameTextColorProp = serializedObject.FindProperty ("NameTextColor");
		LevelTextColorProp = serializedObject.FindProperty ("LevelTextColor");

		//bool
		HealthBarsFoldoutProp = serializedObject.FindProperty ("HealthBarsFoldout");
		CombatTextFoldoutProp = serializedObject.FindProperty ("CombatTextFoldout");
		NameTextFoldoutProp = serializedObject.FindProperty ("NameTextFoldout");
		AnimationsUpdatedProp = serializedObject.FindProperty ("AnimationsUpdated");
		WaypointsFoldout = serializedObject.FindProperty ("WaypointsFoldout");

		Projectile1UpdatedProp = serializedObject.FindProperty ("Projectile1Updated");
		Projectile2UpdatedProp = serializedObject.FindProperty ("Projectile2Updated");
		Projectile3UpdatedProp = serializedObject.FindProperty ("Projectile3Updated");
		RunProjectileUpdatedProp = serializedObject.FindProperty ("RunProjectileUpdated");
		BloodEffectUpdatedProp = serializedObject.FindProperty ("BloodEffectUpdated");

		BehaviorFoldout = serializedObject.FindProperty ("BehaviorFoldout");
		ConfidenceFoldout = serializedObject.FindProperty ("ConfidenceFoldout");
		WanderFoldout = serializedObject.FindProperty ("WanderFoldout");
		CombatStyleFoldout = serializedObject.FindProperty ("CombatStyleFoldout");

		OpposingFaction1Prop = serializedObject.FindProperty("OpposingFaction1");
		OpposingFaction2Prop = serializedObject.FindProperty("OpposingFaction2");
		OpposingFaction3Prop = serializedObject.FindProperty("OpposingFaction3");
		OpposingFaction4Prop = serializedObject.FindProperty("OpposingFaction4");
		OpposingFaction5Prop = serializedObject.FindProperty("OpposingFaction5");

		AttackSound1Prop = serializedObject.FindProperty("AttackSound1");
		AttackSound2Prop = serializedObject.FindProperty("AttackSound2");
		AttackSound3Prop = serializedObject.FindProperty("AttackSound3");
		AttackSound4Prop = serializedObject.FindProperty("AttackSound4");
		AttackSound5Prop = serializedObject.FindProperty("AttackSound5");

		InjuredSound1Prop = serializedObject.FindProperty("InjuredSound1");
		InjuredSound2Prop = serializedObject.FindProperty("InjuredSound2");
		InjuredSound3Prop = serializedObject.FindProperty("InjuredSound3");
		InjuredSound4Prop = serializedObject.FindProperty("InjuredSound4");
		InjuredSound5Prop = serializedObject.FindProperty("InjuredSound5");

		WarningSound1Prop = serializedObject.FindProperty("WarningSound1");
		WarningSound2Prop = serializedObject.FindProperty("WarningSound2");
		WarningSound3Prop = serializedObject.FindProperty("WarningSound3");
		WarningSound4Prop = serializedObject.FindProperty("WarningSound4");
		WarningSound5Prop = serializedObject.FindProperty("WarningSound5");

		DeathSound1Prop = serializedObject.FindProperty("DeathSound1");
		DeathSound2Prop = serializedObject.FindProperty("DeathSound2");
		DeathSound3Prop = serializedObject.FindProperty("DeathSound3");
		DeathSound4Prop = serializedObject.FindProperty("DeathSound4");
		DeathSound5Prop = serializedObject.FindProperty("DeathSound5");

		IdleSound1Prop = serializedObject.FindProperty("IdleSound1");
		IdleSound2Prop = serializedObject.FindProperty("IdleSound2");
		IdleSound3Prop = serializedObject.FindProperty("IdleSound3");
		IdleSound4Prop = serializedObject.FindProperty("IdleSound4");
		IdleSound5Prop = serializedObject.FindProperty("IdleSound5");

		FootStepSound1Prop = serializedObject.FindProperty("FootStepSound1");
		FootStepSound2Prop = serializedObject.FindProperty("FootStepSound2");
		FootStepSound3Prop = serializedObject.FindProperty("FootStepSound3");
		FootStepSound4Prop = serializedObject.FindProperty("FootStepSound4");
		FootStepSound5Prop = serializedObject.FindProperty("FootStepSound5");

		Renderer1Prop = serializedObject.FindProperty("Renderer1");
		Renderer2Prop = serializedObject.FindProperty("Renderer2");
		Renderer3Prop = serializedObject.FindProperty("Renderer3");
		Renderer4Prop = serializedObject.FindProperty("Renderer4");

		//Animations
		Idle1Prop = serializedObject.FindProperty("Idle1");
		Idle2Prop = serializedObject.FindProperty("Idle2");
		Idle3Prop = serializedObject.FindProperty("Idle3");
		WalkProp = serializedObject.FindProperty("Walk");
		RunProp = serializedObject.FindProperty("Run");
		IdleAlertProp = serializedObject.FindProperty("IdleAlert");
		IdleWarningProp = serializedObject.FindProperty("IdleWarning");
		IdleCombatProp = serializedObject.FindProperty("IdleCombat");
		Hit1Prop = serializedObject.FindProperty("Hit1");
		TurnLeftProp = serializedObject.FindProperty("TurnLeft");
		TurnRightProp = serializedObject.FindProperty("TurnRight");
		RunAttackProp = serializedObject.FindProperty("RunAttack");
		Attack1Prop = serializedObject.FindProperty("Attack1");
		Attack2Prop = serializedObject.FindProperty("Attack2");
		Attack3Prop = serializedObject.FindProperty("Attack3");
		DeadProp = serializedObject.FindProperty("DeadAnim");

		DetectionLayerMaskProp = serializedObject.FindProperty("DetectionLayerMask");
		ObstructionDetectionLayerMaskProp = serializedObject.FindProperty("ObstructionDetectionLayerMask");

		if (TemperamentIcon == null) TemperamentIcon = Resources.Load("TemperamentIcon") as Texture;
		if (SettingsIcon == null) SettingsIcon = Resources.Load("SettingsIcon") as Texture;
		if (DetectTagsIcon == null) DetectTagsIcon = Resources.Load("DetectTagsIcon") as Texture;
		if (UIIcon == null) UIIcon = Resources.Load("UIIcon") as Texture;
		if (SoundIcon == null) SoundIcon = Resources.Load("SoundIcon") as Texture;
		if (WaypointEditorIcon == null) WaypointEditorIcon = Resources.Load("WaypointEditorIcon") as Texture;
		if (AnimationIcon == null) AnimationIcon = Resources.Load("AnimationIcon") as Texture;
		if (DocumentationIcon == null) DocumentationIcon = Resources.Load("DocumentationIcon") as Texture;
	}

	public override void OnInspectorGUI () {
		Emerald_AI self = (Emerald_AI)target;

		serializedObject.Update ();

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
		myFoldoutStyle.fontStyle = FontStyle.Bold;
		myFoldoutStyle.fontSize = 12;
		myFoldoutStyle.active.textColor = Color.black;
		myFoldoutStyle.focused.textColor = Color.black;
		myFoldoutStyle.onHover.textColor = Color.black;
		myFoldoutStyle.normal.textColor = Color.black;
		myFoldoutStyle.onNormal.textColor = Color.black;
		myFoldoutStyle.onActive.textColor = Color.black;
		myFoldoutStyle.onFocused.textColor = Color.black;
		Color myStyleColor = Color.black;

		GUIContent[] TabButtons = new GUIContent[8] {new GUIContent("Temperament", TemperamentIcon), new GUIContent("AI's Settings", SettingsIcon), new GUIContent(" Detection \n & Tags", DetectTagsIcon), 
			new GUIContent("UI Settings", UIIcon), new GUIContent("Sounds", SoundIcon), new GUIContent(" Waypoint\nEditor", WaypointEditorIcon), new GUIContent("Animations", AnimationIcon), new GUIContent("Docs", DocumentationIcon)};

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		TabNumberProp.intValue = GUILayout.SelectionGrid (TabNumberProp.intValue, TabButtons, 4, EditorStyles.miniButtonRight, GUILayout.Height(68), GUILayout.Width(90 * Screen.width/100));
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		//Behavior Options
		if (TabNumberProp.intValue == 0){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(TemperamentIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("Temperament Options", style, GUILayout.ExpandWidth(true));
			//GUILayout.Space(2);
			EditorGUILayout.LabelField("The Temperament Options allow you to control an AI's behavior, how it reacts to certain situations, and how it chooses to wander or move to a destination.", EditorStyles.helpBox);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			BehaviorFoldout.boolValue = Foldout(BehaviorFoldout.boolValue, "Behavior", true, myFoldoutStyle);

			if (BehaviorFoldout.boolValue){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("The Behavior Type allows you to define the behavior your AI will have. All AI will only react to targets that have targets' tags set within the AI's Tags List.", EditorStyles.helpBox);

				CustomPopup (new Rect(), new GUIContent(), BehaviorProp, "Behavior Type", typeof(Emerald_AI.CurrentBehavior));

				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Passive){
					EditorGUILayout.LabelField("Passive - Passive AI will not attack. They will simply wander around. If they are attacked, they will react according to their Confidence Level. If you would like your AI to not attack, even if attacked first, simply enable 'Does Not Attack'.", EditorStyles.helpBox);
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious){
					EditorGUILayout.LabelField("Cautious - Cautious AI will either flee or act territorial depending on their Confidence Level. Territorial AI will warn targets before attacking their target. An AI is set as territorial if their Confidence Level is set to Brave or higher.", EditorStyles.helpBox);
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					EditorGUILayout.LabelField("Companion - Companion AI will follow around a target and help them fight. Companion AI will wander until their follow target is set. This is best done with a script and calling the public function SetTarget.", EditorStyles.helpBox);
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Aggressive){
					EditorGUILayout.LabelField("Aggressive - Aggressive AI will attack any target that comes within their trigger radius.", EditorStyles.helpBox);
				}
				GUI.backgroundColor = Color.white;

				EditorGUILayout.EndVertical();
			}
				
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			ConfidenceFoldout.boolValue = Foldout(ConfidenceFoldout.boolValue, "Confidence", true, myFoldoutStyle);

			if (ConfidenceFoldout.boolValue){
				EditorGUILayout.BeginVertical("Box");
				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Companion AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Pet AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				EditorGUILayout.LabelField("The Confidence Level gives you more control over how your AI reacts with their Behavior Type.", EditorStyles.helpBox);

				CustomPopup (new Rect(), new GUIContent(), ConfidenceProp, "Confidence Level", typeof(Emerald_AI.ConfidenceType));

				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious){
					if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward){
						EditorGUILayout.LabelField("Coward - AI with a Coward confidence level will flee when they encounter a target.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Brave){
						EditorGUILayout.LabelField("Brave - AI with a Brave confidence level will become territorial when a target enters their trigger radius. If the target doesn't leave its radius before its territorial seconds have been reached, the AI will attack the target. This AI will flee once its health reaches the amount you've set below.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Foolhardy){
						EditorGUILayout.LabelField("Foolhardy - AI with a Foolhardy confidence level will become territorial when a target enters their trigger radius. If the target doesn't leave its radius before its territorial seconds have been reached, the AI will attack the target. This AI will never flee and continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

					}
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Aggressive){
					if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("Aggressive AI cannot be set to Coward. This AI will automatically be set to Brave on Start.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Brave){
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Brave - Brave Aggressive AI will fight any target on sight or detection, but attempt to flee once its health reaches the percentage you've set.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Foolhardy){
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Foolhardy - Foolhardy Aggressive AI will fight any target on sight or detection and will never flee. They will continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

					}
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Passive){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward){
						EditorGUILayout.LabelField("Coward - Coward Passive AI will wander according to their wander settings, but only flee when attacked.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Brave){
						EditorGUILayout.LabelField("Brave - Brave Passive AI will wander according to their wander settings, but only attack when attacked. They will attempt to flee once its health reaches the percentage you've set.", EditorStyles.helpBox);
					}
					else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Foolhardy){
						EditorGUILayout.LabelField("Foolhardy - Foolhardy Passive AI will wander according to their wander settings, but only attack when attacked. They will never flee and continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

					}
				}
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.ConfidenceRef != Emerald_AI.ConfidenceType.Brave){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Brave Confidence Level Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomIntSlider(new Rect(), new GUIContent(), HealthPercentageToFleeProp, "Health Level to Flee", 1, 99);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the percent of health loss that's needed to trigger a flee attempt.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;

				EditorGUILayout.EndVertical();
			}
				
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			WanderFoldout.boolValue = Foldout(WanderFoldout.boolValue, "Wander Type", true, myFoldoutStyle);

			if (WanderFoldout.boolValue){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Controls the type of wandering technique this AI will use. AI will react to targets according to their Behavior Type.", EditorStyles.helpBox);
				CustomPopup (new Rect(), new GUIContent(), WanderTypeProp, "Wander Type", typeof(Emerald_AI.WanderType));

				if (self.WanderTypeRef == Emerald_AI.WanderType.Dynamic){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Dynamic - Allows an AI to randomly wander by dynamically generate waypoints around their Wander Radius.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}
				else if (self.WanderTypeRef == Emerald_AI.WanderType.Waypoints){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Waypoints - Allows you to define waypoints that the AI will move between.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("You can edit and create Waypoints by going to the Waypoint Editor tab. Would you like to do this now?", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					if (GUILayout.Button("Open the Waypoint Editor")){
						TabNumberProp.intValue = 5;
					}
				}
				else if (self.WanderTypeRef == Emerald_AI.WanderType.Stationary){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Stationary - Allows an AI to stay stationary in the same position and will not move unless a target enters their trigger radius.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntField(new Rect(), new GUIContent(), StationaryIdleSecondsMinProp, "Min Idle Animation Seconds");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("When using more than 1 idle animation, this controls the minimum amount of seconds needed before switching to the next idle animation. This will be randomized with the Max Idle Animation Seconds.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntField(new Rect(), new GUIContent(), StationaryIdleSecondsMaxProp, "Max Idle Animation Seconds");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("When using more than 1 idle animation, this controls the maximum amount of seconds needed before switching to the next idle animation. This will be randomized with the Min Idle Animation Seconds.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}
				else if (self.WanderTypeRef == Emerald_AI.WanderType.Destination){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Destination - Allows an AI to travle to a single destination. Once it reaches it destination, it will stay stationary.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.Space();

				if (self.WanderTypeRef == Emerald_AI.WanderType.Dynamic){
					CustomIntSlider(new Rect(), new GUIContent(), WanderRadiusProp, "Dynamic Wander Radius", 1, 300);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the radius that the AI uses to wander. The AI will randomly pick waypoints within this radius.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomPopup (new Rect(), new GUIContent(), UseSeparateAlertRadiusProp, "Use Alert Radius?", typeof(Emerald_AI.YesOrNo));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not the AI will use a separate Wander Radius for when it's in Alert Mode to simulate an AI being aware of a possible target by expanding its search radius.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					if (self.UseSeparateAlertRadiusRef == Emerald_AI.YesOrNo.Yes){
						CustomIntSlider(new Rect(), new GUIContent(), AlertWanderRadiusProp, "Alert Wander Radius", WanderRadiusProp.intValue, 300);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the radius that the AI uses to wander when in Alert Mode. The AI will randomly pick waypoints within this radius.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}
				}

				EditorGUILayout.EndVertical();
			}
				
			if (self.WanderTypeRef == Emerald_AI.WanderType.Destination){
				if (self.SingleDestination == Vector3.zero){
					self.SingleDestination = new Vector3(self.transform.position.x,self.transform.position.y,self.transform.position.z+5);
				}
			}
				
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			CombatStyleFoldout.boolValue = Foldout(CombatStyleFoldout.boolValue, "Combat Style", true, myFoldoutStyle);

			if (CombatStyleFoldout.boolValue){
				EditorGUILayout.BeginVertical("Box");
				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				EditorGUILayout.LabelField("Controls whether a Companion AI will fight Offensively or Defensively.", EditorStyles.helpBox);
				CustomPopup (new Rect(), new GUIContent(), CombatTypeProp, "Combat Style", typeof(Emerald_AI.CombatType));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);

				if (self.CombatTypeRef == Emerald_AI.CombatType.Defensive){
					EditorGUILayout.LabelField("Defensive - Defensive Companioin AI will only attack a target if it is in active combat mode.", EditorStyles.helpBox);
				}
				else if (self.CombatTypeRef == Emerald_AI.CombatType.Offensive){
					EditorGUILayout.LabelField("Offensive - Offensive Companioin AI will attack any target that is within their trigger radius.", EditorStyles.helpBox);
				}
				GUI.backgroundColor = Color.white;
				EditorGUILayout.EndVertical();
			}
		}

		//AI's Settings
		if (TabNumberProp.intValue == 1){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(SettingsIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("AI's Settings", style, GUILayout.ExpandWidth(true));
			//EditorGUILayout.LabelField("The AI's Settings section gives you control over your AI's settings related to movement, health, NavMesh, optimizations, and damage settings.", EditorStyles.helpBox);
			GUILayout.Space(4);
			GUIContent[] AIStatsButtons = new GUIContent[5] { new GUIContent("Stats"), new GUIContent("Movement"), new GUIContent("Combat"), new GUIContent("NavMesh"), new GUIContent("Optimize")};
			TemperamentTabNumberProp.intValue = GUILayout.Toolbar(TemperamentTabNumberProp.intValue, AIStatsButtons, EditorStyles.miniButton, GUILayout.Height(25),GUILayout.Width(88 * Screen.width/100)); //GUILayout.Width(88 * Screen.width/100)
			GUILayout.Space(1);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if (TemperamentTabNumberProp.intValue == 0){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Controls all of an AI's stats such as health, name, and level.", EditorStyles.helpBox);
				EditorGUILayout.Space();

				CustomStringField(new Rect(), new GUIContent(), AINameProp, "AI's Name");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The name of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomStringField(new Rect(), new GUIContent(), AITitleProp, "AI's Title");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The title of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), AILevelProp, "AI's Level");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The level of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), StartingHealthProp, "Health");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how much health this AI has.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomPopup(new Rect(), new GUIContent(), RefillHealthRTSProp, "Refill Health on Return?", typeof(Emerald_AI.RefillHealthRTS));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not your AI will fully refill their health when they return to start. Return to start happens when a target out runs, or flees from, an AI or it's called programmatically.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), AIRegeneratesHealthProp, "Use Regenerate Health", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI can regenerate health when not in combat.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.AIRegeneratesHealthRef == Emerald_AI.YesOrNo.Yes){
					CustomFloatField(new Rect(), new GUIContent(), HealthRegRateProp, "Regen Rate");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls, in seconds, the rate in which health is regenerated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntField(new Rect(), new GUIContent(), RegenerateAmountProp, "Regen Amount");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how much health is refilled after each Regen Rate.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.EndVertical();
			}

			if (TemperamentTabNumberProp.intValue == 2){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.BeginVertical("Box",GUILayout.Width(92 * Screen.width/100));
				EditorGUILayout.LabelField("Combat Settings", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("The Combat section controls all combat related settings. Some AI, such as Cautious AI with a Coward Confidence Type, will not use the Combat section.", EditorStyles.helpBox);
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("Box",GUILayout.Width(92 * Screen.width/100));

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Damage Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();

				CustomPopup (new Rect(), new GUIContent(), WeaponTypeProp, "Weapon Type", typeof(Emerald_AI.WeaponType));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether your AI will use Ranged or Melee combat.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
					CustomObjectField(new Rect(), new GUIContent(), RangedAttackTransformProp, "Ranged Attack Transform", typeof(Transform), true);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("The transfrom that's used to for spawning ranged attacks. This transform can be anything on your AI such as their hand for magic effects or their bow for bow and arrows.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomFloatField(new Rect(), new GUIContent(), ProjectileTimeoutTimeProp, "Projectile Timeout Time");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls, in seconds, how fast projectiles will timeout and be disabled.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomFloatField(new Rect(), new GUIContent(), PlayerYOffsetProp, "Player Offset Y Position");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the Y position offset when firing projectiles at a player. Depending on the character controller, this may need to be adjusted to ensure the projectiles are not firing too high or too low.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				CustomPopup (new Rect(), new GUIContent(), RandomizeDamageProp, "Use Random Damage", typeof(Emerald_AI.RandomizeDamage));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Use Random Damage controls whether your AI will randomize their damage or use a constant damage amount.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
					EditorGUILayout.LabelField("Projectiles", EditorStyles.boldLabel);
				}
				else if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
					EditorGUILayout.LabelField("Damage Amounts", EditorStyles.boldLabel);
				}
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(15);
				EditorGUILayout.BeginVertical();

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
					CustomPopup (new Rect(), new GUIContent(), TotalAttackAnimationsProp, "Total Projectiles Amount", typeof(Emerald_AI.TotalAttackAnimations));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how many attacks this AI will use when attacking.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
				else if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
					CustomPopup (new Rect(), new GUIContent(), TotalAttackAnimationsProp, "Total Attacks", typeof(Emerald_AI.TotalAttackAnimations));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how many attacks this AI will use when attacking.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){

					GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Attack 1 Projectile", EditorStyles.boldLabel);
					GUI.backgroundColor= Color.white;
					EditorGUILayout.EndVertical();
					GUI.backgroundColor = Color.white;

					CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1ProjectileProp, "Projectile Object", typeof(GameObject), true, 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("The projectile gameobject your AI will use for the Attack 1 animation.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Attack 1", EditorStyles.boldLabel);
					GUI.backgroundColor= Color.white;
					EditorGUILayout.EndVertical();
					GUI.backgroundColor = Color.white;
				}
				if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.Yes){
					CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount1Prop, "Min Damage");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the minimum damage your AI can do with Attack 1. This amount will be randomized with your maximum damage.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount1Prop, "Max Damage");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum damage your AI can do with Attack 1. This amount will be randomized with your minimum damage.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					//EditorGUILayout.Space();
				}
				if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.No){
					CustomIntField(new Rect(), new GUIContent(), DamageAmount1Prop, "Damage");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how much damage your AI can do with Attack 1.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){

					CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack1ProjectileSpeedProp, "Projectile Speed", 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how fast the Attack 1 Projectile will go.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack1CollisionSecondsProp, "Collision Timeout", 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how fast the Attack 1 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile has a trail or effect. " +
						"A value of 0 can be used if you'd like to deactivate instantly.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomPopupProjectiles (new Rect(), new GUIContent(), Attack1HeatSeekingProp, "Heat Seeking?", typeof(Emerald_AI.HeatSeeking), 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to hit their target and makes it more challanging for them to avoid.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					if (self.Attack1HeatSeekingRef == Emerald_AI.HeatSeeking.Yes){

						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(15);
						EditorGUILayout.BeginVertical();

						CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack1HeatSeekingSecondsProp, "Heat Seeking Seconds", 1);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						EditorGUILayout.EndVertical();
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();
					}

					CustomPopupProjectiles (new Rect(), new GUIContent(), Attack1EffectOnCollisionProp, "Effect on Collision?", typeof(Emerald_AI.EffectOnCollision), 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not this projectile will play an effect upon collision.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					if (self.Attack1EffectOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){

						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(15);
						EditorGUILayout.BeginVertical();

						CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 1);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect1TimeoutSecondsProp, "Effect Timeout Seconds", 1);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how long the effect will be visible before being deactivated.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						EditorGUILayout.EndVertical();
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();
					}

					CustomPopupProjectiles (new Rect(), new GUIContent(), Attack1SoundOnCollisionProp, "Sound on Collision?", typeof(Emerald_AI.EffectOnCollision), 1);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not this projectile will play a sound upon collision.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					if (self.Attack1SoundOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(15);
						EditorGUILayout.BeginVertical();
						CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 1);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The sound effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.EndVertical();
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();
					}

					if (Projectile1UpdatedProp.boolValue && Attack1ProjectileProp.objectReferenceValue != null){
						EditorGUILayout.BeginHorizontal("Box");
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("Projectile 1 has changed, please update.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						if (GUILayout.Button("Update Projectile 1")){
							if (self.Attack1Projectile.GetComponent<EmeraldProjectile>() == null){
								self.Attack1Projectile.AddComponent<EmeraldProjectile>();
								self.Attack1Projectile.GetComponent<SphereCollider>().radius = 0.1f;
								EmeraldProjectile ProjectileScript = self.Attack1Projectile.GetComponent<EmeraldProjectile>();

								//Set custom variables
								ProjectileScript.CollisionEffect = (GameObject)Attack1CollisionEffectProp.objectReferenceValue;
								ProjectileScript.ImpactSound = (AudioClip)Attack1ImpactSoundProp.objectReferenceValue;
								ProjectileScript.ProjectileSpeed = Attack1ProjectileSpeedProp.intValue;
								ProjectileScript.CollisionTime = Attack1CollisionSecondsProp.floatValue;
								ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack1EffectOnCollisionProp.intValue;
								ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack1SoundOnCollisionProp.intValue;
								ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack1HeatSeekingProp.intValue;
								ProjectileScript.HeatSeekingSeconds = Attack1HeatSeekingSecondsProp.floatValue;

								if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>() == null){
									self.Attack1CollisionEffect.AddComponent<ProjectileTimeout>();
									self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
									EditorUtility.SetDirty(self.Attack1CollisionEffect);
								}
								else if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>() != null){
									self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
									EditorUtility.SetDirty(self.Attack1CollisionEffect);
								}
							}
							else{
								EmeraldProjectile ProjectileScript = self.Attack1Projectile.GetComponent<EmeraldProjectile>();
								self.Attack1Projectile.GetComponent<SphereCollider>().radius = 0.1f;

								//Set custom variables
								ProjectileScript.CollisionEffect = (GameObject)Attack1CollisionEffectProp.objectReferenceValue;
								ProjectileScript.ImpactSound = (AudioClip)Attack1ImpactSoundProp.objectReferenceValue;
								ProjectileScript.ProjectileSpeed = Attack1ProjectileSpeedProp.intValue;
								ProjectileScript.CollisionTime = Attack1CollisionSecondsProp.floatValue;
								ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack1EffectOnCollisionProp.intValue;
								ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack1SoundOnCollisionProp.intValue;
								ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack1HeatSeekingProp.intValue;
								ProjectileScript.HeatSeekingSeconds = Attack1HeatSeekingSecondsProp.floatValue;

								if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>() == null){
									self.Attack1CollisionEffect.AddComponent<ProjectileTimeout>();
									self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
									EditorUtility.SetDirty(self.Attack1CollisionEffect);
								}
								else if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>() != null){
									self.Attack1CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
									EditorUtility.SetDirty(self.Attack1CollisionEffect);
								}
							}
							EditorUtility.SetDirty(self);
							EditorUtility.SetDirty(self.Attack1Projectile);
							Projectile1UpdatedProp.boolValue = false;
						}
						EditorGUILayout.EndHorizontal();
					}

					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}

				if (self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Two || self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Three){
					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){

						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Attack 2 Projectile", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;

						CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2ProjectileProp, "Projectile Object", typeof(GameObject), true, 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The projectile gameobject your AI will use for the Attack 2 animation.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
						EditorGUILayout.Space();
						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Attack 2", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.Yes){
						CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount2Prop, "Min Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the minimum damage your AI can do with Attack 2. This amount will be randomized with your maximum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount2Prop, "Max Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the maximum damage your AI can do with Attack 2. This amount will be randomized with your minimum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.No){
						CustomIntField(new Rect(), new GUIContent(), DamageAmount2Prop, "Attack 2 Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how much damage your AI can do with Attack 2.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}


					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
						CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack2ProjectileSpeedProp, "Projectile Speed", 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Attack 2 Projectile will go.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack2CollisionSecondsProp, "Collision Timeout", 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Attack 2 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile has a trail or effect. " +
							"A value of 0 can be used if you'd like to deactivate instantly.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack2HeatSeekingProp, "Heat Seeking?", typeof(Emerald_AI.HeatSeeking), 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to hit their target and makes it more challanging for them to avoid.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack2HeatSeekingRef == Emerald_AI.HeatSeeking.Yes){

							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack2HeatSeekingSecondsProp, "Heat Seeking Seconds", 2);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack2EffectOnCollisionProp, "Effect on Collision?", typeof(Emerald_AI.EffectOnCollision), 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play an effect upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack2EffectOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 2);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect2TimeoutSecondsProp, "Effect Timeout Seconds", 2);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how long the effect will be visible before being deactivated.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack2SoundOnCollisionProp, "Sound on Collision?", typeof(Emerald_AI.EffectOnCollision), 2);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play a sound upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack2SoundOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();
							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 2);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The sound effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						if (Projectile2UpdatedProp.boolValue && Attack2ProjectileProp.objectReferenceValue != null){
							EditorGUILayout.BeginHorizontal("Box");
							GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
							EditorGUILayout.LabelField("Projectile 2 has changed, please update.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							if (GUILayout.Button("Update Projectile 2")){
								if (self.Attack2Projectile.GetComponent<EmeraldProjectile>() == null){
									self.Attack2Projectile.AddComponent<EmeraldProjectile>();
									self.Attack2Projectile.GetComponent<SphereCollider>().radius = 0.1f;
									EmeraldProjectile ProjectileScript = self.Attack2Projectile.GetComponent<EmeraldProjectile>();

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)Attack2CollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)Attack2ImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = Attack2ProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = Attack2CollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack2EffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack2SoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack2HeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = Attack2HeatSeekingSecondsProp.floatValue;

									if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.Attack2CollisionEffect.AddComponent<ProjectileTimeout>();
										self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack2CollisionEffect);
									}
									else if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack2CollisionEffect);
									}
								}
								else{
									EmeraldProjectile ProjectileScript = self.Attack2Projectile.GetComponent<EmeraldProjectile>();
									self.Attack2Projectile.GetComponent<SphereCollider>().radius = 0.1f;

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)Attack2CollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)Attack2ImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = Attack2ProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = Attack2CollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack2EffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack2SoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack2HeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = Attack2HeatSeekingSecondsProp.floatValue;

									if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.Attack2CollisionEffect.AddComponent<ProjectileTimeout>();
										self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack2CollisionEffect);
									}
									else if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.Attack2CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack2CollisionEffect);
									}
								}
								EditorUtility.SetDirty(self);
								EditorUtility.SetDirty(self.Attack2Projectile);
								Projectile2UpdatedProp.boolValue = false;
							}
							EditorGUILayout.EndHorizontal();
						}

						EditorGUILayout.Space();
						EditorGUILayout.Space();
					}

				}

				if (self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Three){
					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){

						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Attack 3 Projectile", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;

						CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3ProjectileProp, "Projectile Object", typeof(GameObject), true, 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The projectile gameobject your AI will use for the Attack 3 animation.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
						EditorGUILayout.Space();
						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Attack 3", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.Yes){
						CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount3Prop, "Min Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the minimum damage your AI can do with Attack 3. This amount will be randomized with your maximum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount3Prop, "Max Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the maximum damage your AI can do with Attack 3. This amount will be randomized with your minimum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.No){
						CustomIntField(new Rect(), new GUIContent(), DamageAmount3Prop, "Attack 3 Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how much damage your AI can do with Attack 3.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}

					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
						CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack3ProjectileSpeedProp, "Projectile Speed", 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Attack 3 Projectile will go.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack3CollisionSecondsProp, "Collision Timeout", 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Attack 3 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile has a trail or effect. " +
							"A value of 0 can be used if you'd like to deactivate instantly.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack3HeatSeekingProp, "Heat Seeking?", typeof(Emerald_AI.HeatSeeking), 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to hit their target and makes it more challanging for them to avoid.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack3HeatSeekingRef == Emerald_AI.HeatSeeking.Yes){

							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack3HeatSeekingSecondsProp, "Heat Seeking Seconds", 3);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack3EffectOnCollisionProp, "Effect on Collision?", typeof(Emerald_AI.EffectOnCollision), 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play an effect upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack3EffectOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 3);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect3TimeoutSecondsProp, "Effect Timeout Seconds", 3);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how long the effect will be visible before being deactivated.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), Attack3SoundOnCollisionProp, "Sound on Collision?", typeof(Emerald_AI.EffectOnCollision), 3);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play a sound upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.Attack3SoundOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();
							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 3);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The sound effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						if (Projectile3UpdatedProp.boolValue && Attack3ProjectileProp.objectReferenceValue != null){
							EditorGUILayout.BeginHorizontal("Box");
							GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
							EditorGUILayout.LabelField("Projectile 3 has changed, please update.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							if (GUILayout.Button("Update Projectile 3")){
								if (self.Attack3Projectile.GetComponent<EmeraldProjectile>() == null){
									self.Attack3Projectile.AddComponent<EmeraldProjectile>();
									self.Attack3Projectile.GetComponent<SphereCollider>().radius = 0.1f;
									EmeraldProjectile ProjectileScript = self.Attack3Projectile.GetComponent<EmeraldProjectile>();

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)Attack3CollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)Attack3ImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = Attack3ProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = Attack3CollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack3EffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack3SoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack3HeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = Attack3HeatSeekingSecondsProp.floatValue;

									if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.Attack3CollisionEffect.AddComponent<ProjectileTimeout>();
										self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack3CollisionEffect);
									}
									else if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack3CollisionEffect);
									}
								}
								else{
									EmeraldProjectile ProjectileScript = self.Attack3Projectile.GetComponent<EmeraldProjectile>();
									self.Attack3Projectile.GetComponent<SphereCollider>().radius = 0.1f;

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)Attack3CollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)Attack3ImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = Attack3ProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = Attack3CollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack3EffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)Attack3SoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)Attack3HeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = Attack3HeatSeekingSecondsProp.floatValue;

									if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.Attack3CollisionEffect.AddComponent<ProjectileTimeout>();
										self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds= Effect3TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack3CollisionEffect);
									}
									else if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.Attack3CollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.Attack3CollisionEffect);
									}
								}
								EditorUtility.SetDirty(self);
								EditorUtility.SetDirty(self.Attack3Projectile);
								Projectile3UpdatedProp.boolValue = false;
							}
							EditorGUILayout.EndHorizontal();
						}

						EditorGUILayout.Space();
						EditorGUILayout.Space();
					}
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
					EditorGUILayout.Space();
					CustomFloatSlider(new Rect(), new GUIContent(), AttackDistanceProp, "Attack Distance", 1.5f, 40);
				}
				else if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
					EditorGUILayout.Space();
					EditorGUILayout.Space();
					CustomFloatSlider(new Rect(), new GUIContent(), AttackDistanceProp, "Attack Distance", 1.5f, 40);
				}
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the distance in which an AI will attack.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.Space();
				CustomFloatSlider(new Rect(), new GUIContent(), TooCloseDistanceProp, "Too Close Distance", 0f, 25);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the distance when an AI will backup if they get too close to their target. This is also useful for ranged AI keeping their distance from attackers.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), UseRunAttacksProp, "Use Run Attacks", typeof(Emerald_AI.UseRunAttacks));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will use run attacks.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.UseRunAttacksRef == Emerald_AI.UseRunAttacks.Yes){

					CustomFloatSlider(new Rect(), new GUIContent(), RunAttackDistanceProp, "Run Attack Distance", 1f, 15);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the additional distance in which an AI will attack with its running animation. This value is based off of your AI's Attack Distance.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(15);
					EditorGUILayout.BeginVertical();

					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Run Attack Projectile", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;

						CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackProjectileProp, "Projectile Object", typeof(GameObject), true, 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The projectile gameobject your AI will use for the Run Attack animation.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Melee){
						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Run Attack", EditorStyles.boldLabel);
						GUI.backgroundColor= Color.white;
						EditorGUILayout.EndVertical();
						GUI.backgroundColor = Color.white;
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.Yes){
						CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmountRunProp, "Min Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the minimum damage your AI can do with a Run Attack. This amount will be randomized with your maximum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmountRunProp, "Max Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the maximum damage your AI can do with a Run Attack. This amount will be randomized with your minimum damage.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}
					if (self.RandomizeDamageRef == Emerald_AI.RandomizeDamage.No){
						CustomIntField(new Rect(), new GUIContent(), DamageAmountRunProp, "Run Attack Damage");
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how much damage your AI can do with a Run Attack.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}

					if (self.WeaponTypeRef == Emerald_AI.WeaponType.Ranged){
						CustomIntFieldProjectiles(new Rect(), new GUIContent(), RunAttackProjectileSpeedProp, "Projectile Speed", 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Run Attack Projectile will go.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunAttackCollisionSecondsProp, "Collision Timeout", 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls how fast the Run Attacl Projectile will deactivate after it has collided with an object. A delay is useful if your projectile has a trail or effect. " +
							"A value of 0 can be used if you'd like to deactivate instantly.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();

						CustomPopupProjectiles (new Rect(), new GUIContent(), RunAttackHeatSeekingProp, "Heat Seeking?", typeof(Emerald_AI.HeatSeeking), 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to hit their target and makes it more challanging for them to avoid.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.RunAttackHeatSeekingRef == Emerald_AI.HeatSeeking.Yes){

							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunAttackHeatSeekingSecondsProp, "Heat Seeking Seconds", 4);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), RunAttackEffectOnCollisionProp, "Effect on Collision?", typeof(Emerald_AI.EffectOnCollision), 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play an effect upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.RunAttackEffectOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();

							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackCollisionEffectProp, "Collision Effect", typeof(GameObject), true, 4);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunEffectTimeoutSecondsProp, "Effect Timeout Seconds", 4);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("Controls how long the effect will be visible before being deactivated.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;

							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						CustomPopupProjectiles (new Rect(), new GUIContent(), RunAttackSoundOnCollisionProp, "Sound on Collision?", typeof(Emerald_AI.EffectOnCollision), 4);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls whether or not this projectile will play a sound upon collision.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;

						if (self.RunAttackSoundOnCollisionRef == Emerald_AI.EffectOnCollision.Yes){
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space(15);
							EditorGUILayout.BeginVertical();
							CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 4);
							GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
							EditorGUILayout.LabelField("The sound effect that happens after your projectile has collided with an object.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							EditorGUILayout.EndVertical();
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}

						if (RunProjectileUpdatedProp.boolValue && RunAttackProjectileProp.objectReferenceValue != null){
							EditorGUILayout.BeginHorizontal("Box");
							GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
							EditorGUILayout.LabelField("The Run Projectile has changed, please update.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
							if (GUILayout.Button("Update Run Projectile")){
								if (self.RunAttackProjectile.GetComponent<EmeraldProjectile>() == null){
									self.RunAttackProjectile.AddComponent<EmeraldProjectile>();
									self.RunAttackProjectile.GetComponent<SphereCollider>().radius = 0.1f;
									EmeraldProjectile ProjectileScript = self.RunAttackProjectile.GetComponent<EmeraldProjectile>();

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)RunAttackCollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)RunAttackImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = RunAttackProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = RunAttackCollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)RunAttackEffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)RunAttackSoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)RunAttackHeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = RunAttackHeatSeekingSecondsProp.floatValue;

									if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.RunAttackCollisionEffect.AddComponent<ProjectileTimeout>();
										self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.RunAttackCollisionEffect);
									}
									else if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.RunAttackCollisionEffect);
									}
								}
								else{
									EmeraldProjectile ProjectileScript = self.RunAttackProjectile.GetComponent<EmeraldProjectile>();
									self.RunAttackProjectile.GetComponent<SphereCollider>().radius = 0.1f;

									//Set custom variables
									ProjectileScript.CollisionEffect = (GameObject)RunAttackCollisionEffectProp.objectReferenceValue;
									ProjectileScript.ImpactSound = (AudioClip)RunAttackImpactSoundProp.objectReferenceValue;
									ProjectileScript.ProjectileSpeed = RunAttackProjectileSpeedProp.intValue;
									ProjectileScript.CollisionTime = RunAttackCollisionSecondsProp.floatValue;
									ProjectileScript.EffectOnCollisionRef = (EmeraldProjectile.EffectOnCollision)RunAttackEffectOnCollisionProp.intValue;
									ProjectileScript.SoundOnCollisionRef = (EmeraldProjectile.EffectOnCollision)RunAttackSoundOnCollisionProp.intValue;
									ProjectileScript.HeatSeekingRef = (EmeraldProjectile.HeatSeeking)RunAttackHeatSeekingProp.intValue;
									ProjectileScript.HeatSeekingSeconds = RunAttackHeatSeekingSecondsProp.floatValue;

									if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>() == null){
										self.RunAttackCollisionEffect.AddComponent<ProjectileTimeout>();
										self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.RunAttackCollisionEffect);
									}
									else if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>() != null){
										self.RunAttackCollisionEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
										EditorUtility.SetDirty(self.RunAttackCollisionEffect);
									}
								}
								EditorUtility.SetDirty(self);
								EditorUtility.SetDirty(self.RunAttackProjectile);
								RunProjectileUpdatedProp.boolValue = false;
							}
							EditorGUILayout.EndHorizontal();
						}

						EditorGUILayout.Space();
						EditorGUILayout.Space();
					}

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();

				}
					
				EditorGUILayout.Space();
				CustomFloatField(new Rect(), new GUIContent(), MinimumAttackSpeedProp, "Min Attack Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the minimum amount of time it takes for your AI to attack again after an initial attack. This amount is randomized with Maximum Attack Speed. If the Min Attack Speed is set to 0, your AI has the chance to immediately attack after the current attack animation has finished playing.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomFloatField(new Rect(), new GUIContent(), MaximumAttackSpeedProp, "Max Attack Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the maximum amount of time it takes for your AI to attack again after an initial attack. This amount is randomized with Minimum Attack Speed.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("Box",GUILayout.Width(92 * Screen.width/100));

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Combat Actions & Effects Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();

				CustomPopup (new Rect(), new GUIContent(), DynamicMovementProp, "Use Dynamic Movement", typeof(Emerald_AI.DynamicMovement));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will move dynamically during combat to keep it from staying too stationary.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				/*
				if (self.UseBloodEffectRef == Emerald_AI.UseBloodEffect.Yes){

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomIntField(new Rect(), new GUIContent(), AttacksToRepositionProp, "Attacks to ");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how long the hit effect will be visible before being deactivated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();

				}
				*/

				CustomPopup (new Rect(), new GUIContent(), UseBloodEffectProp, "Use Hit Effect", typeof(Emerald_AI.UseBloodEffect));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will use a hit effect when it receives melee damage.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.UseBloodEffectRef == Emerald_AI.UseBloodEffect.Yes){

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomObjectFieldHitEffect(new Rect(), new GUIContent(), BloodEffectProp, "Hit Effect", typeof(GameObject), true);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("The blood effect that will appear when this AI receives damage.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomFloatFieldHitEffect(new Rect(), new GUIContent(), BloodEffectTimeoutSecondsProp, "Hit Effect Timeout Seconds");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls how long the hit effect will be visible before being deactivated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					if (BloodEffectUpdatedProp.boolValue && BloodEffectProp.objectReferenceValue != null){
						EditorGUILayout.BeginHorizontal("Box");
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("Hit Effect has changed, please update.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						if (GUILayout.Button("Update Hit Effect")){
							if (self.BloodEffect != null && self.BloodEffect.GetComponent<ProjectileTimeout>() == null){
								self.BloodEffect.AddComponent<ProjectileTimeout>();
								self.BloodEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = BloodEffectTimeoutSecondsProp.floatValue;
								EditorUtility.SetDirty(self.BloodEffect);
							}
							else if (self.BloodEffect != null && self.BloodEffect.GetComponent<ProjectileTimeout>() != null){
								self.BloodEffect.GetComponent<ProjectileTimeout>().TimeoutSeconds = BloodEffectTimeoutSecondsProp.floatValue;
								EditorUtility.SetDirty(self.BloodEffect);
							}
							EditorUtility.SetDirty(self);
							EditorUtility.SetDirty(self.BloodEffect);
							BloodEffectUpdatedProp.boolValue = false;
						}
						EditorGUILayout.EndHorizontal();
					}
					EditorGUILayout.Space();

					CustomVector3Field(new Rect(), new GUIContent(), BloodPosOffsetProp, "Hit Effect Position");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the offset position of the hit effect.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}

				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Companion AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Pet AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				CustomIntField(new Rect(), new GUIContent(), MinimumChaseWaitTimeProp, "Min Chase Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the minimum amount of time with how quickly an AI will react to chasing their target. This amount will be randomized with the Maximum Chase Time.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Companion AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Pet AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				CustomIntField(new Rect(), new GUIContent(), MaximumChaseWaitTimeProp, "Max Chase Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the maximum amount of time with how quickly an AI will react to chasing their target. This amount will be randomized with the Minimum Chase Time.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), DeathDelayMinProp, "Min Resume Wandering");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the minimum amount of time with how quickly an AI will resume wandering according to its Wandering Type after it has engaged in combat. This amount will be randomized with the Maximum Resume Wandering Delay.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), DeathDelayMaxProp, "Max Resume Wandering");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the maximum amount of time with how quickly an AI will resume wandering according to its Wandering Type after it has engaged in combat. This amount will be randomized with the Minimum Resume Wandering Delay.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Companion AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Pet AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Cautious){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Cautious AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomPopup (new Rect(), new GUIContent(), UseWarningAnimationProp, "Use Warning", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will play a warning animation and sound if they feel threatened. The Warning animation can be set within the Animations tab under the Idle section.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Cautious && self.BehaviorRef != Emerald_AI.CurrentBehavior.Aggressive){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Cautious and Aggressive AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomPopup (new Rect(), new GUIContent(), UseAlertAnimationProp, "Use Alert Animation", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will play an alert animation when it detects a target is near by. The Alert animation can be set within the Animations tab under the Idle section.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			}

			if (TemperamentTabNumberProp.intValue == 1){
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Movement Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.LabelField("Controls all of an AI's movement related settings such as movement speeds, alignment, turning, wait times, and stopping distances.", EditorStyles.helpBox);
				EditorGUILayout.EndVertical();

				GUI.backgroundColor = Color.white;

				EditorGUILayout.BeginVertical("Box");

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Speed Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				CustomFloatField(new Rect(), new GUIContent(), WalkSpeedProp, "Walk Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how fast your AI walks.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomFloatField(new Rect(), new GUIContent(), RunSpeedProp, "Run Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how fast your AI runs.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomPopup(new Rect(), new GUIContent(), WanderAnimationProp, "Movement Animation", typeof(Emerald_AI.WanderAnimation));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the type of animation your AI will use when using waypoints, moving to its destination, or wandering. Note: If needed, this can be changed programmatically during runtime.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomFloatSlider(new Rect(), new GUIContent(), StoppingDistanceProp, "Stopping Distance", 1, 40);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the distance in which an AI will stop before waypoints and targets.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion && self.BehaviorRef != Emerald_AI.CurrentBehavior.Pet){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				CustomFloatSlider(new Rect(), new GUIContent(), FollowingStoppingDistanceProp, "Following Stopping Distance", 1, 20);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the distance in which an AI will stop in front of their following targets.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion && self.BehaviorRef != Emerald_AI.CurrentBehavior.Pet){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				CustomPopup(new Rect(), new GUIContent(), FollowSpeedTypeProp, "Follow Speed Type", typeof(Emerald_AI.FollowSpeedType));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the type of speed your Companion and Pet AI will use. Dynamic allows an AI's speed to automatically adjust to how fast your follow target is going. Manual uses the speeds set within the Editor.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("Box");

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Turn Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				CustomIntField(new Rect(), new GUIContent(), AngleNeededToTurnProp, "Turning Angle Stationary");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the angle needed to play a turn animation while an AI is stationary. Emerald can automatically detect whether an AI is turing left or right. Note: You can use a walking animation in place of a turning animation if your AI doesn't one.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), AngleNeededToTurnRunningProp, "Turning Angle Moving");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the angle needed to play a turn animation while an AI is moving. Emerald can automatically detect whether an AI is turing left or right. Note: You can use a walking animation in place of a turning animation if your AI doesn't one.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), UseRandomRotationOnStartProp, "Random Roation on Start", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not AI will be randomly rotated on Start.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward && self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Not Useable with Cautious Coward AI)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomFloatField(new Rect(), new GUIContent(), TurnSpeedProp, "Combat Turn Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how fast your AI turns while in combat.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("Box");

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Wait Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				CustomIntField(new Rect(), new GUIContent(), MinimumWaitTimeProp, "Min Wait Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the minimum amount of seconds before generating a new waypoint, when using the Dynamic Wander Type. This amount is randomized with the Maximim Wait Time.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), MaximumWaitTimeProp, "Max Wait Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the maximum amount of seconds before generating a new waypoint, when using the Dynamic Wander Type. This amount is randomized with the Minimum Wait Time.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), UseSeparateAlertWaitTimeProp, "Use Alert Wait Time?", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not the AI will use a separate Wait Time for when it's in Alert Mode to simulate an AI being aware of a possible target by taking less time between generating waypoints.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.UseSeparateAlertWaitTimeRef == Emerald_AI.YesOrNo.Yes){
					CustomIntField(new Rect(), new GUIContent(), MinimumAlertWaitTimeProp, "Min Alert Wait Time");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the minimum amount of seconds before generating a new waypoint, when this AI is alert (When a target is within the AI's trigger radius). This amount is randomized with the Maximim Alert Wait Time.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntField(new Rect(), new GUIContent(), MaximumAlertWaitTimeProp, "Max Alert Wait Time");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum amount of seconds before generating a new waypoint, when this AI is alert (When a target is within the AI's trigger radius). This amount is randomized with the Minimum Alert Wait Time.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}
					
				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion && self.BehaviorRef != Emerald_AI.CurrentBehavior.Pet){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomIntField(new Rect(), new GUIContent(), MinimumFollowWaitTimeProp, "Min Follow Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how quickly a Companion AI will react to following their target.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion && self.BehaviorRef != Emerald_AI.CurrentBehavior.Pet){
					EditorGUILayout.Space();
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomIntField(new Rect(), new GUIContent(), MaximumFollowWaitTimeProp, "Max Follow Time");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how quickly a Companion AI will react to following their target.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("Box");

				GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Alignment Settings", EditorStyles.boldLabel);
				GUI.backgroundColor= Color.white;
				EditorGUILayout.EndVertical();
				GUI.backgroundColor = Color.white;

				CustomPopup (new Rect(), new GUIContent(), AlignAIWithGroundProp, "Align AI?", typeof(Emerald_AI.AlignAIWithGround));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Aligns AI to the angle of the terrain and other objects for added realism.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.AlignAIWithGroundRef == Emerald_AI.AlignAIWithGround.Yes){
					CustomPopup (new Rect(), new GUIContent(), AlignmentQualityProp, "Align Quality", typeof(Emerald_AI.AlignmentQuality));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the quality of the Align AI feature by controlling how often it's updated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomPopup (new Rect(), new GUIContent(), AlignAIOnStartProp, "Align on Start?", typeof(Emerald_AI.YesOrNo));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Calculates the Align AI feature on Start.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomFloatField(new Rect(), new GUIContent(), MaxXAngleProp, "Max X Angle");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum X (forward and backwards) angle for an AI to rotate to when aligning with the ground.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomFloatField(new Rect(), new GUIContent(), MaxZAngleProp, "Max Z Angle");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum Z (left and right) angle for an AI to rotate to when aligning with the ground.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.EndVertical();
			}

			if (TemperamentTabNumberProp.intValue == 3){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("NavMesh Settings", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Controls all of an AI's NavMesh related settings.", EditorStyles.helpBox);
				EditorGUILayout.Space();

				CustomFloatField(new Rect(), new GUIContent(), AgentBaseOffsetProp, "Agent Base Offset");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the NavMesh Agent's base offset. The base offset gives you control over how high your AI will be above the ground. If you AI is a flying AI type, you will want to adjust this value higher so your AI will be above the ground.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomFloatSlider(new Rect(), new GUIContent(), AgentRadiusProp, "Agent Radius", 0.1f, 8);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the NavMesh Agent's avoidence radius.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomFloatField(new Rect(), new GUIContent(), AgentTurnSpeedProp, "Agent Turn Speed");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the NavMesh Agent's turn speed when not in combat.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomFloatField(new Rect(), new GUIContent(), AgentAccelerationProp, "Agent Acceleration");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the NavMesh Agent's acceleration speed. The acceleration speed gives you control over how fast your AI will get to its max speed.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomPopup(new Rect(), new GUIContent(), AvoidanceQualityProp, "Agent Avoidance Quality", typeof(Emerald_AI.AvoidanceQuality));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the NavMesh Agent's avoidance quality which controls how accurately your AI will navigate through avoidable objects at the cost of perfomance. These objects include other AI and Nav Mesh Obstacles. If you would like to not use this feature, set the Avoidance Quality to None.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				EditorGUILayout.EndVertical();
			}

			if (TemperamentTabNumberProp.intValue == 4){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Optimization Settings", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Controls all of the settings that can help optimize an AI such as diabling an AI when their model is culled and allowing an AI object to be recycled using a custom object pooling system.", EditorStyles.helpBox);
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), RecycleAIProp, "Recycle AI?", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI can be recycled when using systems such as object pooling. When an AI has been killled, and they are disabled, they will reset to their default state when re-enabled.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				
				CustomPopup (new Rect(), new GUIContent(), DisableAIWhenNotInViewProp, "Disable when Off-Screen", typeof(Emerald_AI.YesOrNo));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI is disabled when off screen or is culled.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.DisableAIWhenNotInViewRef == Emerald_AI.YesOrNo.Yes){
					CustomPopup (new Rect(), new GUIContent(), UseDeactivateDelayProp, "Use Deactivate Delay", typeof(Emerald_AI.YesOrNo));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not there is a delay when using the Disable when Off-Screen feature. If set to No, the AI will be disabled instantly.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				if (self.UseDeactivateDelayRef == Emerald_AI.YesOrNo.Yes && self.DisableAIWhenNotInViewRef == Emerald_AI.YesOrNo.Yes){
					CustomIntSlider(new Rect(), new GUIContent(), DeactivateSecondsProp, "Deactivate Seconds", 1, 60);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the amount of seconds until the AI will be disabled when either culled or off-screen.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				if (self.DisableAIWhenNotInViewRef == Emerald_AI.YesOrNo.Yes){
					CustomPopup (new Rect(), new GUIContent(), HasMultipleLODsProp, "Has Multiple LODs", typeof(Emerald_AI.YesOrNo));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not the Disable when Off-Screen feature will use multiple LODs. An AI using this feature must have at have an LOD Group with at least 2 levels. Note: If your AI has multiple LODs, this feature needs to be enabled in order for the Disable when Off-Screen feature to work.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					if (self.HasMultipleLODsRef == Emerald_AI.YesOrNo.Yes){

						EditorGUILayout.Space();
						EditorGUILayout.Space();
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Auto Grab LODs will attempt to automatically grab your AI's LODs. Your AI must have a LOD Group component with at least 2 levels to use this feature.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						if (GUILayout.Button("Auto Grab LODs")){
							LODGroup _LODGroup = self.GetComponentInChildren<LODGroup>();

							if (_LODGroup == null){
								EditorUtility.DisplayDialog("Error","No LOD Group could be found. Please ensure that your AI has an LOD group that has at least 2 levels. The Multiple LOD Feature has been disabled.", "Okay");
								self.HasMultipleLODsRef = Emerald_AI.YesOrNo.No;
							}
							else if (_LODGroup != null){
								LOD[] AllLODs = _LODGroup.GetLODs();

								if (_LODGroup.lodCount <= 4){
									TotalLODsProp.intValue = (_LODGroup.lodCount-2);
								}

								if (_LODGroup.lodCount > 1){
									for (int i = 0; i < _LODGroup.lodCount; i++){
										if (i == 0){
											Renderer1Prop.objectReferenceValue = AllLODs[i].renderers[0];
										}
										if (i == 1){
											Renderer2Prop.objectReferenceValue = AllLODs[i].renderers[0];
										}
										if (i == 2){
											Renderer3Prop.objectReferenceValue = AllLODs[i].renderers[0];
										}
										if (i == 3){
											Renderer4Prop.objectReferenceValue = AllLODs[i].renderers[0];
										}
									}


								}
								else if (_LODGroup.lodCount == 1){
									EditorUtility.DisplayDialog("Warning","Your AI's LOD Group only has 1 level and it needs to have at least 2. The Multiple LOD feature has been disabled.", "Okay");
									self.HasMultipleLODsRef = Emerald_AI.YesOrNo.No;
								}
							}

						}
						EditorGUILayout.Space();
						EditorGUILayout.Space();
						EditorGUILayout.Space();

						CustomPopup (new Rect(), new GUIContent(), TotalLODsProp, "Total LODs", typeof(Emerald_AI.TotalLODsEnum));
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Controls the amount of LODs the Disable when Off-Screen feature will use. You will need to apply each level from your AI's LOD Group below. If any renderers are missing, this feature will be disabled.", EditorStyles.helpBox);
						EditorGUILayout.LabelField("This feature only supports 1 model per level. Your AI's main model should be used.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();

						if (self.TotalLODsRef == Emerald_AI.TotalLODsEnum.Two){
							CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
						}
						else if (self.TotalLODsRef == Emerald_AI.TotalLODsEnum.Three){
							CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer3Prop, "Renderer 3", typeof(Renderer), true);
						}
						else if (self.TotalLODsRef == Emerald_AI.TotalLODsEnum.Four){
							CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer3Prop, "Renderer 3", typeof(Renderer), true);
							CustomObjectField(new Rect(), new GUIContent(), Renderer4Prop, "Renderer 4", typeof(Renderer), true);
						}
					}
				}

				EditorGUILayout.EndVertical();
			}
		}

		//Detection
		if (TabNumberProp.intValue == 2){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(DetectTagsIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("Detection & Tags", style, GUILayout.ExpandWidth(true));
			GUILayout.Space(4);
			GUIContent[] DetectionTagsButtons = new GUIContent[2] { new GUIContent("Detection Options"), new GUIContent("Tag Options")};
			DetectionTagsTabNumberProp.intValue = GUILayout.Toolbar(DetectionTagsTabNumberProp.intValue, DetectionTagsButtons, EditorStyles.miniButton, GUILayout.Height(25), GUILayout.Width(90 * Screen.width/100));
			GUILayout.Space(1);
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();

			if (DetectionTagsTabNumberProp.intValue == 0){
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.BeginVertical("Box",GUILayout.Width(91 * Screen.width/100));
				EditorGUILayout.LabelField("Detection Options", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("Controls various detection related options and settings such as radius distances, target detection, and field of view.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight && self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("Companion AI cannot use the Line of Sight Detection Type. It will automatically be switched to Trigger on Start.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				else if (self.DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight && self.BehaviorRef == Emerald_AI.CurrentBehavior.Passive){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("Passive AI cannot use the Line of Sight Detection Type. It will automatically be switched to Trigger on Start.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomPopup (new Rect(), new GUIContent(), DetectionTypeProp, "Detection Type", typeof(Emerald_AI.DetectionType));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the type of detection that is used for detecting targets.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Companion){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("Companion AI will automatically use the Closest Pick Target Method.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomPopup (new Rect(), new GUIContent(), PickTargetMethodProp, "Pick Target Method", typeof(Emerald_AI.PickTargetMethod));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the type of method used to pick an AI's first target.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;

				if (self.PickTargetMethodRef == Emerald_AI.PickTargetMethod.Closest){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Closest - When a target is first detected or seen, the AI will search for the nearest target within range sometimes resulting in the AI picking a different target that may not currently be seen. " +
						"However, this usually ends up with the best results and keeps AI evenly distibuted during larger battles. AI will not initialize an attack unless one is seen first.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}
				else if (self.PickTargetMethodRef == Emerald_AI.PickTargetMethod.FirstDetected){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("First Detected - Picks the target that is first seen or detected. This is the most realistic, but can sometimes result in multiple AI picking the same target.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				//Added
				EditorGUI.BeginChangeCheck();
				var layersSelection = EditorGUILayout.MaskField("Obstruction Ignore Layers", LayerMaskToField(ObstructionDetectionLayerMaskProp.intValue), InternalEditorUtility.layers);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(self, "Layers changed");
					ObstructionDetectionLayerMaskProp.intValue = FieldToLayerMask(layersSelection);
				}
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The layers that should be ignored when an AI is using its obstruction detection for attacking with melee and ranged attacks. These are objects that may prevent an AI from seeing the player target object. If your player has nothing that will block the AI's sight, you can set the layermask to Nothing.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				//Added

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), ObstructionDetectionQualityProp, "Detection Quality", typeof(Emerald_AI.ObstructionDetectionQuality));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the quality of the Obstruction Detection feature by controlling how often it's updated", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;

				EditorGUILayout.Space();

				if (self.DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight){
					EditorGUILayout.Space();
					CustomIntSlider(new Rect(), new GUIContent(), fieldOfViewAngleProp, "Field of View", 1, 360);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the field of view your AI uses to detect targets.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntSlider(new Rect(), new GUIContent(), ExpandedFieldOfViewAngleProp, "Expanded Field of View", 1, 360);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the field of view after your AI has been damaged and no target has been found to allow the AI a better opportunity to find its attacker.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomIntSlider(new Rect(), new GUIContent(), DetectionRadiusProp, "Detection Distance", 1, 100);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the distance of the field of view as well as the AI's detection radius. When enabled, AI can go into 'Alert Mode' when an target is near, but not visible.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					CustomIntSlider(new Rect(), new GUIContent(), ExpandedDetectionRadiusProp, "Expanded Detection Distance", 1, 100);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the distance of the field of view as well as the AI's detection radius after your AI has been damaged but no target has found to allow the AI a better opportunity to find its attacker.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				if (self.DetectionTypeRef == Emerald_AI.DetectionType.Trigger){
					CustomIntSlider(new Rect(), new GUIContent(), DetectionRadiusProp, "Detection Distance", 1, 100);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the distance of the AI's trigger radius. When a valid target is within this radius, the AI will react according to its Behavior Type.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward && self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious){
					CustomPopup (new Rect(), new GUIContent(), MaxChaseDistanceTypeProp, "Distance Type", typeof(Emerald_AI.MaxChaseDistanceType));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the AI's target for detecting the distance to stop fleeing.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}
				else {
					CustomPopup (new Rect(), new GUIContent(), MaxChaseDistanceTypeProp, "Distance Type", typeof(Emerald_AI.MaxChaseDistanceType));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the AI's target for detecting the distance to stop attacking.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
				}

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				if (MaxChaseDistanceProp.intValue <= DetectionRadiusProp.intValue){
					MaxChaseDistanceProp.intValue = DetectionRadiusProp.intValue+10;
				}

				if (self.ConfidenceRef != Emerald_AI.ConfidenceType.Coward && self.BehaviorRef != Emerald_AI.CurrentBehavior.Cautious){
					CustomIntSlider(new Rect(), new GUIContent(), MaxChaseDistanceProp, "Chase Distance", DetectionRadiusProp.intValue+10, 200);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum amount of distance the AI will travel before giving up on their target. This distance can be based on either distance away from its target or its starting position.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
				else if (self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward && self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious){
					CustomIntSlider(new Rect(), new GUIContent(), MaxChaseDistanceProp, "Flee Range", DetectionRadiusProp.intValue+10, 200);
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the maximum amount of distance the AI will travel before they will stop fleeing. This distance can be based on either distance away from its target or its starting position.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
				EditorGUILayout.EndVertical();
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (DetectionTagsTabNumberProp.intValue == 1){
				EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);
				EditorGUILayout.HelpBox("Here you can setup your AI's tags and layers. The Target Tags are tags that the AI will see as targets. It will act according to its behavior type. For more infromation regarding the Tag Options, please see the docs section within the Emerald Editor.", MessageType.None, true);
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				CustomTagField(new Rect(), new GUIContent(), EmeraldTagProp, "Emerald Unity Tag");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The Unity Tag used to define other Emerald AI objects. This is the tag that was created using Unity's Tag pulldown at the top of the gameobject.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				//Added
				EditorGUI.BeginChangeCheck();
				var layersSelection = EditorGUILayout.MaskField("Detection Layers", LayerMaskToField(DetectionLayerMaskProp.intValue), InternalEditorUtility.layers);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The Detection Layers controls what layers this AI can detect as possible targets, if the AI has the appropriate Emerald Unity Tag.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(self, "Layers changed");
					DetectionLayerMaskProp.intValue = FieldToLayerMask(layersSelection);
				}
				//Added

				EditorGUILayout.Space();
				EditorGUILayout.Space();
			
				CustomEnum(new Rect(), new GUIContent(), CurrentFactionProp, "Faction");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("An AI's Faction is the name used to control combat reaction with other AI. This is the name other AI will use when looking for opposing targets.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Factions can be created and removed using the Faction Manager. ", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				if (GUILayout.Button("Open Faction Manager")){
					EditorWindow APS = EditorWindow.GetWindow(typeof(EmeraldAIFactionManager));
					APS.minSize = new Vector2(600f, 725f);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Companion && self.BehaviorRef != Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("(Companion AI Only)", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				EditorGUILayout.HelpBox("The Follower Tag controls the Tag that this AI will follow. This happens when this AI's trigger radius hits the said tag. This feature does not have to be used. " +
					"If you'd like to manually set the AI's follower, you can do so programmatically.", MessageType.None, true);
				CustomTagField(new Rect(), new GUIContent(), FollowTagProp, "Follower Tag");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("If you would like to not use this feature, you can set the Follower Tag to Untagged.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				EditorGUILayout.HelpBox("You must define your Player's Unity Tag separately. This allows the AI to determine if the target is another AI or not.", MessageType.None, true);
				CustomTagField(new Rect(), new GUIContent(), PlayerTagProp, "Player Tag");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The Player Tag is the Unity Tag used to detect player targets.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("Not Useable with Pet AI.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				if (self.BehaviorRef != Emerald_AI.CurrentBehavior.Cautious && self.ConfidenceRef != Emerald_AI.ConfidenceType.Coward || self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious && self.ConfidenceRef != Emerald_AI.ConfidenceType.Coward){
					CustomPopup (new Rect(), new GUIContent(), AIAttacksPlayerProp, "AI Attacks Player?", typeof(Emerald_AI.AIAttacksPlayer));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not this AI will attack a player with the given tag.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space ();
				}
				else if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Cautious && self.ConfidenceRef == Emerald_AI.ConfidenceType.Coward){
					CustomPopup (new Rect(), new GUIContent(), AIAttacksPlayerProp, "AI Flees From Player?", typeof(Emerald_AI.AIAttacksPlayer));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether or not this AI will flee from a player with the given tag.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space ();
				}

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				CustomTagField(new Rect(), new GUIContent(), UITagProp, "UI Tag");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("The UI Tag is the Unity Tag that will trigger the AI's UI, when enabled.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), UseNonAITagProp, "Use Non-AI Tag?", typeof(Emerald_AI.UseNonAITag));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls whether or not this AI will attack a player with the given tag.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space ();

				if (self.UseNonAITagRef == Emerald_AI.UseNonAITag.Yes){
					EditorGUILayout.HelpBox("The Non-AI Unity Tag is another type of tag AI can use for the behavior types such as avoid cars, areas of water, or other avoidable objects that are not AI objects.", MessageType.None, true);
					CustomTagField(new Rect(), new GUIContent(), NonAITagProp, "Non-AI Tag");
					GUI.backgroundColor = new Color(1f,1,0.25f,0.25f);
					EditorGUILayout.LabelField("Note: The layer of a Non-AI object must be included in an AI's 'Number of Layers' list.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				CustomPopup (new Rect(), new GUIContent(), OpposingFactionsEnumProp, "Total Faction Relations", typeof(Emerald_AI.OpposingFactionsEnum));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls which factions this AI sees as enemies and allies.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				GUI.backgroundColor = new Color(1f,1,0.25f,0.25f);
				EditorGUILayout.LabelField("Note: The Faction Relations use an AI's Faction not Unity tags.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();

				if (self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.One || 
					self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Two || 
					self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Three || 
					self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Four ||
					self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Five){

					if (FactionRelation1Prop.intValue == 0){
						GUI.backgroundColor = new Color(0.8f,0f,0f,1f);
					}
					else if (FactionRelation1Prop.intValue == 1){
						GUI.backgroundColor = new Color(0.0f,0.0f,1f,0.4f);
					}
					else if (FactionRelation1Prop.intValue == 2){
						GUI.backgroundColor = new Color(0.0f,0.75f,0f,1f);
					}
					EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					var RelationStyle = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
					RelationStyle.normal.textColor = Color.white;
					EditorGUILayout.LabelField("Faction Relation 1", RelationStyle, GUILayout.ExpandWidth(true));
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();

					GUILayout.Space(8);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction1Prop, "Faction", Color.white);
					GUILayout.FlexibleSpace();
					CustomPopupColor (new Rect(), new GUIContent(), FactionRelation1Prop, "Relation", typeof(Emerald_AI.RelationType), Color.white);
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();

					GUILayout.Space(8);
					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
				}
				if (self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Two || self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Three 
					|| self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Four || self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Five){

					if (FactionRelation2Prop.intValue == 0){
						GUI.backgroundColor = new Color(0.8f,0f,0f,1f);
					}
					else if (FactionRelation2Prop.intValue == 1){
						GUI.backgroundColor = new Color(0.0f,0.0f,1f,0.4f);
					}
					else if (FactionRelation2Prop.intValue == 2){
						GUI.backgroundColor = new Color(0.0f,0.75f,0f,1f);
					}
					EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					var RelationStyle = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
					RelationStyle.normal.textColor = Color.white;
					EditorGUILayout.LabelField("Faction Relation 2", RelationStyle, GUILayout.ExpandWidth(true));
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();

					GUILayout.Space(8);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction2Prop, "Faction", Color.white);
					GUILayout.FlexibleSpace();
					CustomPopupColor (new Rect(), new GUIContent(), FactionRelation2Prop, "Relation", typeof(Emerald_AI.RelationType), Color.white);
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();

					GUILayout.Space(8);
					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
				}
				if (self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Three 
					|| self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Four
					|| self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Five){

					if (FactionRelation3Prop.intValue == 0){
						GUI.backgroundColor = new Color(0.8f,0f,0f,1f);
					}
					else if (FactionRelation3Prop.intValue == 1){
						GUI.backgroundColor = new Color(0.0f,0.0f,1f,0.4f);
					}
					else if (FactionRelation3Prop.intValue == 2){
						GUI.backgroundColor = new Color(0.0f,0.75f,0f,1f);
					}
					EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					var RelationStyle = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
					RelationStyle.normal.textColor = Color.white;
					EditorGUILayout.LabelField("Faction Relation 3", RelationStyle, GUILayout.ExpandWidth(true));
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();

					GUILayout.Space(8);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction3Prop, "Faction", Color.white);
					GUILayout.FlexibleSpace();
					CustomPopupColor (new Rect(), new GUIContent(), FactionRelation3Prop, "Relation", typeof(Emerald_AI.RelationType), Color.white);
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();

					GUILayout.Space(8);
					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
				}
				if (self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Four || self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Five){

					if (FactionRelation4Prop.intValue == 0){
						GUI.backgroundColor = new Color(0.8f,0f,0f,1f);
					}
					else if (FactionRelation4Prop.intValue == 1){
						GUI.backgroundColor = new Color(0.0f,0.0f,1f,0.4f);
					}
					else if (FactionRelation4Prop.intValue == 2){
						GUI.backgroundColor = new Color(0.0f,0.75f,0f,1f);
					}
					EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					var RelationStyle = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
					RelationStyle.normal.textColor = Color.white;
					EditorGUILayout.LabelField("Faction Relation 4", RelationStyle, GUILayout.ExpandWidth(true));
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();

					GUILayout.Space(8);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction4Prop, "Faction", Color.white);
					GUILayout.FlexibleSpace();
					CustomPopupColor (new Rect(), new GUIContent(), FactionRelation4Prop, "Relation", typeof(Emerald_AI.RelationType), Color.white);
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();

					GUILayout.Space(8);
					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
				}
				if (self.OpposingFactionsEnumRef == Emerald_AI.OpposingFactionsEnum.Five){

					if (FactionRelation5Prop.intValue == 0){
						GUI.backgroundColor = new Color(0.8f,0f,0f,1f);
					}
					else if (FactionRelation5Prop.intValue == 1){
						GUI.backgroundColor = new Color(0.0f,0.0f,1f,0.4f);
					}
					else if (FactionRelation5Prop.intValue == 2){
						GUI.backgroundColor = new Color(0.0f,0.75f,0f,1f);
					}
					EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					var RelationStyle = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
					RelationStyle.normal.textColor = Color.white;
					EditorGUILayout.LabelField("Faction Relation 5", RelationStyle, GUILayout.ExpandWidth(true));
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();

					GUILayout.Space(8);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction5Prop, "Faction", Color.white);
					GUILayout.FlexibleSpace();
					CustomPopupColor (new Rect(), new GUIContent(), FactionRelation5Prop, "Relation", typeof(Emerald_AI.RelationType), Color.white);
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();

					GUILayout.Space(8);
					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();
			}

		}

		if (TabNumberProp.intValue == 3){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(UIIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("UI Settings", style, GUILayout.ExpandWidth(true));
			GUILayout.Space(2);
			EditorGUILayout.LabelField("Controls the use and settings of Emerald's built-in Health Bars and Combat Text. In order for UI to be visible, a player of the appropriate tag must enter an AI's trigger radius. " +
				"You can set an AI's UI Tag under the Detection and Tag tab.", EditorStyles.helpBox);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			NameTextFoldoutProp.boolValue = Foldout(NameTextFoldoutProp.boolValue, "Name Text Settings", true, myFoldoutStyle);

			if (NameTextFoldoutProp.boolValue){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), DisplayAINameProp, "Display AI Name", typeof(Emerald_AI.DisplayAIName));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Enables or disables the display of the AI's name. When enabled, the AI's name will be visible above its health bar.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.DisplayAINameRef == Emerald_AI.DisplayAIName.Yes){

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomVector3Field(new Rect(), new GUIContent(), AINamePosProp, "AI Name Position");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the position of the AI's name text.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomVector2Field(new Rect(), new GUIContent(), NameTextSizeProp, "AI Name Text Size");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the size of the AI's name text.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomColorField(new Rect(), new GUIContent(), NameTextColorProp, "AI Name Color");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls color of the AI's name text.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomPopup (new Rect(), new GUIContent(), DisplayAITitleProp, "Display AI Title", typeof(Emerald_AI.DisplayAITitle));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Enables or disables the display of the AI's title. When enabled, the AI's title will be visible above its health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}

				if (self.CreateHealthBarsRef == Emerald_AI.CreateHealthBars.No){
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("You must have Auto Create Health Bars enabled to use this feature.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}
				CustomPopup (new Rect(), new GUIContent(), DisplayAILevelProp, "Display AI Level", typeof(Emerald_AI.DisplayAILevel));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Enables or disables the display of the AI's level. When enabled, the AI's level will be visible to the left of its health bar.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				if (self.DisplayAILevelRef == Emerald_AI.DisplayAILevel.Yes){

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomColorField(new Rect(), new GUIContent(), LevelTextColorProp, "Level Color");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls color of the AI's Level Text.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical();
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			HealthBarsFoldoutProp.boolValue = Foldout(HealthBarsFoldoutProp.boolValue, "Health Bar Settings", true, myFoldoutStyle);

			if (HealthBarsFoldoutProp.boolValue){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.Space();
				CustomPopup (new Rect(), new GUIContent(), CreateHealthBarsProp, "Auto Create Health Bars", typeof(Emerald_AI.CreateHealthBars));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Enables or disables the use of Emerald automatically creating health bars for your AI. Enabling this will open up additional settings.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.CreateHealthBarsRef== Emerald_AI.CreateHealthBars.Yes){
					
					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomVector3Field(new Rect(), new GUIContent(), HealthBarPosProp, "Health Bar Position");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the starting position of the AI's created health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomVector3Field(new Rect(), new GUIContent(), HealthBarScaleProp, "Health Bar Scale");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the scale of the AI's created health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					if (self.BehaviorRef == Emerald_AI.CurrentBehavior.Pet){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("Not Useable with Pet AI. Health bars will be disabled.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}

					CustomColorField(new Rect(), new GUIContent(), HealthBarColorProp, "Health Bar Color");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls color of the AI's health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomColorField(new Rect(), new GUIContent(), HealthBarBackgroundColorProp, "Background Color");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the background color of the AI's health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomPopup (new Rect(), new GUIContent(), CustomizeHealthBarProp, "Customize Health Bar?", typeof(Emerald_AI.CustomizeHealthBar));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Gives you controls over using custom sprites for the AI's health bar.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					EditorGUILayout.Space();
					EditorGUILayout.Space();

					if (self.CustomizeHealthBarRef == Emerald_AI.CustomizeHealthBar.Yes){
						EditorGUILayout.LabelField("Health Bar Sprites", EditorStyles.boldLabel);
						EditorGUILayout.Space();

						CustomObjectField(new Rect(), new GUIContent(), HealthBarImageProp, "Bar", typeof(Sprite), true);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Customizes the health bar sprite for the AI's health bar.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();

						CustomObjectField(new Rect(), new GUIContent(), HealthBarBackgroundImageProp, "Bar Background", typeof(Sprite), true);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Customizes the health bar's background sprite for the AI's health bar.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}


					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.EndVertical();
			}


			EditorGUILayout.Space();
			EditorGUILayout.Space();

			CombatTextFoldoutProp.boolValue = Foldout(CombatTextFoldoutProp.boolValue, "Combat Text Settings", true, myFoldoutStyle);

			if (CombatTextFoldoutProp.boolValue){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.Space();

				CustomPopup (new Rect(), new GUIContent(), UseCombatTextProp, "Auto Create Combat Text", typeof(Emerald_AI.UseCombatText));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Enables or disables the use of Emerald automatically creating combat text for your AI. Enabling this will open up additional settings.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.UseCombatTextRef== Emerald_AI.UseCombatText.Yes){

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical();

					CustomPopup (new Rect(), new GUIContent(), AnimateCombatTextProp, "Animate Type", typeof(Emerald_AI.AnimateCombatText));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls whether not the AI's combat text is animated. If enabled, the AI's combat text will animate upwards each time a damage number appears. If disabled, the combat text will stay in the same place each time a damage number appears.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomVector3Field(new Rect(), new GUIContent(), CombatTextPosProp, "Combat Text Position");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the starting position of the AI's Combat Text.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomColorField(new Rect(), new GUIContent(), CombatTextColorProp, "Combat Text Color");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls color of the AI's Combat Text color.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();

					CustomPopup (new Rect(), new GUIContent(), UseCustomFontProp, "Use Custom Font?", typeof(Emerald_AI.UseCustomFont));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Allows you to customize the Combat Text's font.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();

					if (self.UseCustomFontRef == Emerald_AI.UseCustomFont.Yes){
						CustomObjectField(new Rect(), new GUIContent(), CombatTextFontProp, "Custom Font", typeof(Font), true);
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("The custom font the Combat Text will use.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
						EditorGUILayout.Space();
					}

					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.EndVertical();
			}
		}

		if (TabNumberProp.intValue == 4){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(SoundIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("Sounds", style, GUILayout.ExpandWidth(true));
			GUILayout.Space(2);
			EditorGUILayout.LabelField("Controls various sound related options and settings such as footsteps and combat sounds.", EditorStyles.helpBox);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Attack Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), AttackSoundsEnumProp, "Total Attack Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many attack sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.AttackSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();
				if (self.AttackSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), AttackSound1Prop, "Attack Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.AttackSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), AttackSound1Prop, "Attack Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound2Prop, "Attack Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.AttackSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), AttackSound1Prop, "Attack Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound2Prop, "Attack Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound3Prop, "Attack Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.AttackSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), AttackSound1Prop, "Attack Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound2Prop, "Attack Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound3Prop, "Attack Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound4Prop, "Attack Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.AttackSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), AttackSound1Prop, "Attack Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound2Prop, "Attack Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound3Prop, "Attack Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound4Prop, "Attack Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), AttackSound5Prop, "Attack Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();
				
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Injured Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), InjuredSoundsEnumProp, "Total Injured Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many injured sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.InjuredSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();
				if (self.InjuredSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound1Prop, "Injured Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.InjuredSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound1Prop, "Injured Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound2Prop, "Injured Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.InjuredSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound1Prop, "Injured Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound2Prop, "Injured Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound3Prop, "Injured Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.InjuredSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound1Prop, "Injured Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound2Prop, "Injured Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound3Prop, "Injured Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound4Prop, "Injured Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.InjuredSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound1Prop, "Injured Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound2Prop, "Injured Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound3Prop, "Injured Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound4Prop, "Injured Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), InjuredSound5Prop, "Injured Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
				
			EditorGUILayout.EndVertical();
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Warning Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), WarningSoundsEnumProp, "Total Warning Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many warning sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.WarningSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();
				if (self.WarningSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), WarningSound1Prop, "Warning Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.WarningSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), WarningSound1Prop, "Warning Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound2Prop, "Warning Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.WarningSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), WarningSound1Prop, "Warning Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound2Prop, "Warning Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound3Prop, "Warning Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.WarningSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), WarningSound1Prop, "Warning Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound2Prop, "Warning Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound3Prop, "Warning Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound4Prop, "Warning Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.WarningSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), WarningSound1Prop, "Warning Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound2Prop, "Warning Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound3Prop, "Warning Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound4Prop, "Warning Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), WarningSound5Prop, "Warning Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
				
			EditorGUILayout.EndVertical();
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Death Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), DeathSoundsEnumProp, "Total Death Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many death sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.DeathSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();
				if (self.DeathSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), DeathSound1Prop, "Death Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.DeathSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), DeathSound1Prop, "Death Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound2Prop, "Death Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.DeathSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), DeathSound1Prop, "Death Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound2Prop, "Death Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound3Prop, "Death Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.DeathSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), DeathSound1Prop, "Death Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound2Prop, "Death Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound3Prop, "Death Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound4Prop, "Death Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.DeathSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), DeathSound1Prop, "Death Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound2Prop, "Death Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound3Prop, "Death Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound4Prop, "Death Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), DeathSound5Prop, "Death Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
				
			EditorGUILayout.EndVertical();


			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Idle Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), IdleSoundsEnumProp, "Total Idle Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many idle sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.IdleSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();

				EditorGUILayout.Space();
				CustomIntField(new Rect(), new GUIContent(), IdleSoundsSecondsMinProp, "Min Idle Sound Seconds");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the minimum amount of seconds needed before playing a random idle sound from the list below. This amuont will be randomized with the Max Idle Sound Seconds.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomIntField(new Rect(), new GUIContent(), IdleSoundsSecondsMaxProp, "Max Idle Sound Seconds");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the maximum amount of seconds needed before playing a random idle sound from the list below. This amuont will be randomized with the Min Idle Sound Seconds.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.IdleSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), IdleSound1Prop, "Idle Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.IdleSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), IdleSound1Prop, "Idle Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound2Prop, "Idle Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.IdleSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), IdleSound1Prop, "Idle Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound2Prop, "Idle Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound3Prop, "Idle Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.IdleSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), IdleSound1Prop, "Idle Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound2Prop, "Idle Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound3Prop, "Idle Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound4Prop, "Idle Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.IdleSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), IdleSound1Prop, "Idle Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound2Prop, "Idle Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound3Prop, "Idle Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound4Prop, "Idle Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), IdleSound5Prop, "Idle Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();


			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical("Box");

			GUI.backgroundColor = new Color(0.2f,0.2f,0.2f,0.25f);
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Footstep Sounds", EditorStyles.boldLabel);
			GUI.backgroundColor= Color.white;
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Color.white;

			CustomPopup (new Rect(), new GUIContent(), FootStepSoundsEnumProp, "Total Footstep Sounds", typeof(Emerald_AI.TotalSoundsEnum));
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Controls how many footstep sounds this AI will use.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.FootStepSoundsEnumRef != Emerald_AI.TotalSoundsEnum.None){

				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical();
				if (self.FootStepSoundsEnumRef == Emerald_AI.TotalSoundsEnum.One){
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound1Prop, "Footstep Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.FootStepSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Two){
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound1Prop, "Footstep Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound2Prop, "Footstep Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.FootStepSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Three){
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound1Prop, "Footstep Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound2Prop, "Footstep Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound3Prop, "Footstep Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.FootStepSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Four){
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound1Prop, "Footstep Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound2Prop, "Footstep Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound3Prop, "Footstep Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound4Prop, "Footstep Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				else if (self.FootStepSoundsEnumRef == Emerald_AI.TotalSoundsEnum.Five){
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound1Prop, "Footstep Sound 1", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound2Prop, "Footstep Sound 2", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound3Prop, "Footstep Sound 3", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound4Prop, "Footstep Sound 4", typeof(AudioClip), false);
					EditorGUILayout.Space ();
					CustomObjectField(new Rect(), new GUIContent(), FootStepSound5Prop, "Footstep Sound 5", typeof(AudioClip), false);
					EditorGUILayout.Space ();
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Space ();

				CustomPopup (new Rect(), new GUIContent(), FootstepSoundTypeProp, "Footstep Sound Type", typeof(Emerald_AI.FootstepSoundType));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how footsteps are calculated.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;

				if (self.FootstepSoundTypeRef == Emerald_AI.FootstepSoundType.AnimationEvent){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Animation Event - You will need to manually create an Animator Event to use this feature. A tutorial can be found in the Emerald documentation.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
				}

				if (self.FootstepSoundTypeRef == Emerald_AI.FootstepSoundType.Seconds){
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Seconds - Controls the speed in seconds that each footstep sound will play while walking and running.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;

					CustomFloatField(new Rect(), new GUIContent(), FootStepSecondsWalkProp, "Walk Footstep Seconds");
					CustomFloatField(new Rect(), new GUIContent(), FootStepSecondsRunProp, "Run Footstep Seconds");
				}
				EditorGUILayout.Space();
			}

			EditorGUILayout.EndVertical();
		}

		if (TabNumberProp.intValue ==5){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space ();

			if (self.WanderTypeRef != Emerald_AI.WanderType.Waypoints){
				GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
				EditorGUILayout.LabelField("You can only use the Waypoint Editor if your AI's Wander Type is set to Waypoints, would you like to enable this AI to use Waypoints?", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				if (GUILayout.Button("Enable Waypoints")){
					WanderTypeProp.intValue = 1;
				}
				EditorGUILayout.Space();
				EditorGUILayout.Space();
			}


			if (self.WanderTypeRef == Emerald_AI.WanderType.Waypoints){
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
				var style4 = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
				EditorGUILayout.LabelField(new GUIContent(WaypointEditorIcon), style4, GUILayout.ExpandWidth(true), GUILayout.Height(32));
				EditorGUILayout.LabelField("Waypoint Editor", style4, GUILayout.ExpandWidth(true));
				GUILayout.Space(2);
				EditorGUILayout.HelpBox("Here you can define waypoints for your AI to follow. Simply press the 'Add Waypoint' button to create a waypoint. The AI will follow each created waypoint in the order they are created. A line will be drawn to visually represent this.", MessageType.None, true);
				EditorGUILayout.EndVertical();
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				if(self.WaypointsList != null && Selection.objects.Length == 1){
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Controls what an AI will do when it reaches its last waypoint.", EditorStyles.helpBox);
					CustomPopup (new Rect(), new GUIContent(), WaypointTypeProp, "Waypoint Type", typeof(Emerald_AI.WaypointType));
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					GUI.backgroundColor = Color.white;

					if (self.WaypointTypeRef == (Emerald_AI.WaypointType.Loop)){
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Loop - When an AI reaches its last waypoint, it will set the first waypoint as its next waypoint thus creating a loop.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
					else if (self.WaypointTypeRef == (Emerald_AI.WaypointType.Reverse)){
						GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
						EditorGUILayout.LabelField("Reverse - When an AI reaches its last waypoint, it will reverse the AI's waypoints making the last waypoint its first. This allows AI to patorl back and forth through narrow or one way areas.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
						
					EditorGUILayout.Space ();

					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
					if (GUILayout.Button("Add Waypoint"))
					{
						Vector3 newPoint = new Vector3(0,0,0);

						if (self.WaypointsList.Count == 0){
							newPoint = new Vector3(self.transform.position.x,self.transform.position.y,self.transform.position.z+5);
						}
						else if (self.WaypointsList.Count > 0){
							newPoint = new Vector3(self.WaypointsList[self.WaypointsList.Count-1].x,self.WaypointsList[self.WaypointsList.Count-1].y,self.WaypointsList[self.WaypointsList.Count-1].z+5);
						}

						self.WaypointsList.Add(newPoint);
						EditorUtility.SetDirty(self);
					}

					var style = new GUIStyle(GUI.skin.button);
					style.normal.textColor = Color.red;

					if (GUILayout.Button("Clear All Waypoints", style) && EditorUtility.DisplayDialog("Clear Waypoints?", "Are you sure you want to clear all of this AI's waypoints? This process cannot be undone.", "Yes", "Cancel")){
						self.WaypointsList.Clear();
						EditorUtility.SetDirty(self);
					}
					GUI.contentColor = Color.white;
					GUI.backgroundColor = Color.white;

					EditorGUILayout.EndVertical();
					EditorGUILayout.Space ();
					EditorGUILayout.Space ();

					WaypointsFoldout.boolValue = Foldout(WaypointsFoldout.boolValue, "Waypoints", true, myFoldoutStyle);

					if (WaypointsFoldout.boolValue){
						EditorGUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);
						EditorGUILayout.LabelField("All of this AI's current waypoints. Waypoints can be individually removed by pressing the ''Remove Point'' button.", EditorStyles.helpBox);
						EditorGUILayout.Space ();

						if(self.WaypointsList.Count > 0){
							for (int j = 0; j < self.WaypointsList.Count; ++j){
								GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
								EditorGUILayout.LabelField("Waypoint "+(j+1), EditorStyles.toolbarButton);
								GUI.backgroundColor = Color.white;

								if (GUILayout.Button("Remove Point", EditorStyles.miniButton, GUILayout.Height(18))){
									self.WaypointsList.RemoveAt(j);
									--j;
									EditorUtility.SetDirty(self);
								}
								GUILayout.Space(10);
							}
						}


						EditorGUILayout.EndVertical();
					}

				}
				else if (self.WaypointsList != null && Selection.objects.Length > 1){
					EditorGUILayout.BeginVertical("Box");
					GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
					EditorGUILayout.LabelField("Waypoints do not support multi-object editing. If you'd like to edit an AI's waypoints, please only have 1 AI selected at a time.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.EndVertical();
				}
			}
		}

		if (TabNumberProp.intValue == 6){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style3 = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(AnimationIcon), style3, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("Animation Settings", style3, GUILayout.ExpandWidth(true));

			if (self.overrideController == null){
				EditorGUILayout.LabelField("To create an Animator Controller, press the 'Create Animator Controller' button below. This will create an Animator Controller and assign the animations you've entered below.", EditorStyles.helpBox);
			}
			else if (self.overrideController != null){
				EditorGUILayout.LabelField("You will need to press the 'Update Animator Controller' button in order for your changes to be updated.", EditorStyles.helpBox);
				EditorGUILayout.LabelField(" Note: If you have multiple AI using the same Animator Controller, such as duplicated or copied AI, you will need to have all said objects selected when updating the Animator Controller.", EditorStyles.helpBox);
			}

			GUILayout.Space(6);
			GUI.backgroundColor = Color.white;
			GUILayout.Space(2);
			GUIContent[] AnimationButtons = new GUIContent[3] { new GUIContent("Idle"), new GUIContent("Movement"), new GUIContent("Combat")};
			AnimationTabNumberProp.intValue = GUILayout.Toolbar(AnimationTabNumberProp.intValue, AnimationButtons, EditorStyles.miniButton, GUILayout.Height(20),GUILayout.Width(88 * Screen.width/100));
			GUILayout.Space(1);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (AnimationTabNumberProp.intValue == 0){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.LabelField("Idle Animations", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("The Idle Animations section allows you to set all idle animations that this AI will use when wandering and the idle animations that will be used in combat. ", EditorStyles.helpBox);
				EditorGUILayout.Space();
				CustomPopup (new Rect(), new GUIContent(), TotalIdleAnimationsProp, "Total Idle Animations", typeof(Emerald_AI.TotalIdleAnimations));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how many idle animations this AI will use when grazing or wandering.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), Idle1Prop, "Idle 1 Animation", typeof(AnimationClip), false);
				if (self.Idle1 != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.Idle1);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Idle 1' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), Idle1AnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				if (self.TotalIdleAnimationsRef == Emerald_AI.TotalIdleAnimations.Two || self.TotalIdleAnimationsRef == Emerald_AI.TotalIdleAnimations.Three){
					CustomAnimationField(new Rect(), new GUIContent(), Idle2Prop, "Idle 2 Animation", typeof(AnimationClip), false);
					if (self.Idle2 != null){
						var settings = AnimationUtility.GetAnimationClipSettings(self.Idle2);
						if (!settings.loopTime || !settings.loopBlend){
							GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
							EditorGUILayout.LabelField("The 'Idle 2' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
						}
					}
					CustomFloatField(new Rect(), new GUIContent(), Idle2AnimationSpeedProp, "Animation Speed");
					EditorGUILayout.Space();
				}

				if (self.TotalIdleAnimationsRef == Emerald_AI.TotalIdleAnimations.Three){
					CustomAnimationField(new Rect(), new GUIContent(), Idle3Prop, "Idle 3 Animation", typeof(AnimationClip), false);
					if (self.Idle3 != null){
						var settings = AnimationUtility.GetAnimationClipSettings(self.Idle3);
						if (!settings.loopTime || !settings.loopBlend){
							GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
							EditorGUILayout.LabelField("The 'Idle 3' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
							GUI.backgroundColor = Color.white;
						}
					}
					CustomFloatField(new Rect(), new GUIContent(), Idle3AnimationSpeedProp, "Animation Speed");
					EditorGUILayout.Space();
				}

				CustomAnimationField(new Rect(), new GUIContent(), IdleAlertProp, "Idle Alert", typeof(AnimationClip), false);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the animation that will play when an AI detects that a target is near, but cannot be seen.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				if (self.IdleAlert != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.IdleAlert);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Idle Alert' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), IdleAlertAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), IdleWarningProp, "Idle Warning", typeof(AnimationClip), false);
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the animation that the AI will play to warn a target that they will attack, if the target doesn't leave their attack radius soon.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				CustomFloatField(new Rect(), new GUIContent(), IdleWarningAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), IdleCombatProp, "Idle Combat", typeof(AnimationClip), false);
				if (self.IdleCombat != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.IdleCombat);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Idle Combat' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), IdleCombatAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical();
			}

			if (AnimationTabNumberProp.intValue == 1){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), WalkProp, "Walk Animation", typeof(AnimationClip), false);
				if (self.Walk != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.Walk);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Walk' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), WalkAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), RunProp, "Run Animation", typeof(AnimationClip), false);
				if (self.Run != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.Run);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Run' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), RunAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), TurnLeftProp, "Turn Left", typeof(AnimationClip), false);
				if (self.TurnLeft != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.TurnLeft);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Turn Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), TurnLeftAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), TurnRightProp, "Turn Right", typeof(AnimationClip), false);
				if (self.TurnRight != null){
					var settings = AnimationUtility.GetAnimationClipSettings(self.TurnRight);
					if (!settings.loopTime || !settings.loopBlend){
						GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
						EditorGUILayout.LabelField("The 'Turn Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Pose' and 'Loop Time' to true.", EditorStyles.helpBox);
						GUI.backgroundColor = Color.white;
					}
				}
				CustomFloatField(new Rect(), new GUIContent(), TurnRightAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical();
			}

			if (AnimationTabNumberProp.intValue == 2){
				EditorGUILayout.BeginVertical("Box");
				EditorGUILayout.Space();
				CustomPopup (new Rect(), new GUIContent(), TotalAttackAnimationsProp, "Total Attack Animations", typeof(Emerald_AI.TotalAttackAnimations));
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls how many attack animations this AI will use when attacking.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), Attack1Prop, "Attack 1", typeof(AnimationClip), false);
				CustomFloatField(new Rect(), new GUIContent(), Attack1AnimationSpeedProp, "Animation Speed");
				CustomFloatField(new Rect(), new GUIContent(), Attack1DelayProp, "Attack 1 Delay");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the delay of an AI's attack before the damage is sent to be received. This is helpful to ensure your animations sync with when an attack is calculated.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Two || self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Three){
					CustomAnimationField(new Rect(), new GUIContent(), Attack2Prop, "Attack 2", typeof(AnimationClip), false);
					CustomFloatField(new Rect(), new GUIContent(), Attack2AnimationSpeedProp, "Animation Speed");
					CustomFloatField(new Rect(), new GUIContent(), Attack2DelayProp, "Attack 2 Delay");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the delay of an AI's attack before the damage is sent to be received. This is helpful to ensure your animations sync with when an attack is calculated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}

				if (self.TotalAttackAnimationsRef == Emerald_AI.TotalAttackAnimations.Three){
					CustomAnimationField(new Rect(), new GUIContent(), Attack3Prop, "Attack 3", typeof(AnimationClip), false);
					CustomFloatField(new Rect(), new GUIContent(), Attack3AnimationSpeedProp, "Animation Speed");
					CustomFloatField(new Rect(), new GUIContent(), Attack3DelayProp, "Attack 3 Delay");
					GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
					EditorGUILayout.LabelField("Controls the delay of an AI's attack before the damage is sent to be received. This is helpful to ensure your animations sync with when an attack is calculated.", EditorStyles.helpBox);
					GUI.backgroundColor = Color.white;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}

				CustomAnimationField(new Rect(), new GUIContent(), RunAttackProp, "Run Attack", typeof(AnimationClip), false);
				CustomFloatField(new Rect(), new GUIContent(), RunAttackAnimationSpeedProp, "Animation Speed");
				CustomFloatField(new Rect(), new GUIContent(), RunAttackDelayProp, "Run Attack Delay");
				GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
				EditorGUILayout.LabelField("Controls the delay of an AI's attack before the damage is sent to be received. This is helpful to ensure your animations sync with when an attack is calculated.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), Hit1Prop, "Hit", typeof(AnimationClip), false);
				CustomFloatField(new Rect(), new GUIContent(), HitAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();

				CustomAnimationField(new Rect(), new GUIContent(), DeadProp, "Dead", typeof(AnimationClip), false);
				CustomFloatField(new Rect(), new GUIContent(), DeathAnimationSpeedProp, "Animation Speed");
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical();
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (self.overrideController == null && Selection.gameObjects.Length == 1)
			{
				GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
				EditorGUILayout.LabelField("In order for this AI to have animations, you must create an Animator Controller for it. To do so, press the 'Create Animator Controller' button below.'", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
			}

			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.Space();
			if (AnimationsUpdatedProp.boolValue && self.overrideController != null)
			{
				GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
				EditorGUILayout.LabelField("The animations have been changed.", EditorStyles.helpBox);
				EditorGUILayout.LabelField("Please press the 'Update Animator Controller' button below to update the animations.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
			}
			else if (!AnimationsUpdatedProp.boolValue && self.overrideController != null)
			{
				GUI.backgroundColor = new Color(0.1f,1f,0.1f,0.5f);
				EditorGUILayout.LabelField("The Animator Controller is up to date.", EditorStyles.helpBox);
				GUI.backgroundColor = Color.white;
			}

			if (self.overrideController == null){
				if (GUILayout.Button("Create Animator Controller")){
					self.FilePath = EditorUtility.SaveFilePanelInProject("Save as OverrideController", "New OverrideController", "overrideController", "Please enter a file name to save the file to");
					if (self.FilePath != string.Empty){
						AssetDatabase.CopyAsset("Assets/Emerald AI 2.0/Animator/Emerald Override.overrideController", self.FilePath);
						self.overrideController = AssetDatabase.LoadAssetAtPath(self.FilePath, typeof(AnimatorOverrideController)) as AnimatorOverrideController;

						string Temp = self.FilePath;
						Temp = Temp.Replace(".overrideController", ".controller"); 
						AssetDatabase.CopyAsset("Assets/Emerald AI 2.0/Animator/Temp Emerald Animator (Do Not Edit).controller", Temp);
						self.overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath(Temp, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

						if (self.AIAnimator == null){
							self.AIAnimator = self.gameObject.GetComponent<Animator>();
						}

						if (self.overrideController != null){
							self.SetupAnimations(self.AIAnimator);
						}	

						if (Selection.gameObjects.Length > 1){ 
							foreach (GameObject G in Selection.gameObjects){
								if (G.GetComponent<Emerald_AI>() != null){
									Emerald_AI EmeraldComponent = G.GetComponent<Emerald_AI>();
									EmeraldComponent.overrideController = AssetDatabase.LoadAssetAtPath(self.FilePath, typeof(AnimatorOverrideController)) as AnimatorOverrideController;
									EmeraldComponent.overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath(Temp, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
									EmeraldComponent.AIAnimator = G.GetComponent<Animator>();
									EmeraldComponent.SetupAnimations(EmeraldComponent.AIAnimator);
								}
							}
						}

						AnimationsUpdatedProp.boolValue = false;
					}
				}
			}

			if (self.overrideController != null){
				if (GUILayout.Button("Update Animator Controller")){
					
					if (self.AIAnimator == null){
						self.AIAnimator = self.gameObject.GetComponent<Animator>();
					}
						
					self.SetupAnimations(self.AIAnimator);
					AnimationsUpdatedProp.boolValue = false;
				}

				var style = new GUIStyle(GUI.skin.button);
				style.normal.textColor = Color.red;
				if (GUILayout.Button("Clear Animator Controller", style) && EditorUtility.DisplayDialog("Clear Animator Controller?", "Are you sure you want to clear this AI's Animator Controller? This process cannot be undone.", "Yes", "Cancel")){
					foreach (GameObject G in Selection.gameObjects){
						if (G.GetComponent<Emerald_AI>() != null){
							G.GetComponent<Emerald_AI>().overrideController = null;
							G.GetComponent<Emerald_AI>().AIAnimator = null;
						}
					}
				}
				GUI.contentColor = Color.white;
				GUI.backgroundColor = Color.white;
			}

			EditorGUILayout.Space();
			EditorGUILayout.EndVertical();

		}

		if (TabNumberProp.intValue == 7){
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginVertical("Box",GUILayout.Width(90 * Screen.width/100));
			var style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
			EditorGUILayout.LabelField(new GUIContent(DocumentationIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
			EditorGUILayout.LabelField("Documentation", style, GUILayout.ExpandWidth(true));
			GUILayout.Space(2);
			EditorGUILayout.LabelField("Emerald's Docs can all be found below. This is to give users easy access to tutorials, script references, and documentation all from within the Emerald Editor. Each " +
				"section is online so users always get the most up to date material.", EditorStyles.helpBox);
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Documentation", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Contains detailed guides and tutorials to help get you started and familiar with Emerald.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Documentation", GUILayout.Height(28))){
				Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit?usp=sharing");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Written Tutorials", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Various written tutorials covering a wide range of features and implementations.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Written Tutorials", GUILayout.Height(28))){
				Application.OpenURL("https://docs.google.com/document/d/1QGSEdc2-6bks22KIelYlBw_601uHce2BVLM3QTq5axg/edit?usp=sharing");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();


			EditorGUILayout.LabelField("Video Tutorials", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Various video tutorials covering a wide range of features and implementations.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Video Tutorials", GUILayout.Height(28))){
				Application.OpenURL("https://www.youtube.com/playlist?list=PLlyiPBj7FznY7q4bdDQgGYgUByYpeCe07");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Scripting Reference", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("A site that has all of Emerald's API documented with script examples.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Scripting Reference", GUILayout.Height(28))){
				Application.OpenURL("http://www.blackhorizonstudios.com/docs/emerald-script-reference/");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Solutions to Possible Issues", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("Get solutions to possible issues you may have. If you've encountered an issue, there is most likely a solution for it here.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Solutions to Possible Issues", GUILayout.Height(28))){
				Application.OpenURL("https://docs.google.com/document/d/1_NjuySY0x7OjRv0lTZzEEP4-AOozmItoJkxgbDgtRQ8/edit?usp=sharing");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Emerald AI Forums", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("The forums are a great way to get quick support as well being kept update to date on upcoming updates.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Emerald AI Forums", GUILayout.Height(28))){
				Application.OpenURL("https://forum.unity.com/threads/update-coming-soon-emerald-ai-dynamic-wildlife-breeding-predators-prey-herds-npcs-more.336521/");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Customer Support", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("All the contact information you will need to get quick support.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Customer Support", GUILayout.Height(28))){
				Application.OpenURL("http://www.blackhorizonstudios.com/contact/");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Report a Bug", EditorStyles.boldLabel);
			GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
			EditorGUILayout.LabelField("If you've encountered a bug, you can fill out a bug report here. This allows bugs to be well documented so they can be fixed as soon as possible.", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
			if (GUILayout.Button("Report a Bug", GUILayout.Height(28))){
				Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdzHNlLSCyv2k3LPT5STMSRuWPLFIanci0rTuC7BjQQgAoDgA/viewform?usp=sf_link");
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties();
	}

	//Draw all of our Editor related controls, settings, and effects
	void OnSceneGUI () {
		Emerald_AI self = (Emerald_AI)target;
		Handles.color = new Color(255, 0, 0, 0.07f);

		//Draw two arcs each with 50% of the field of view in opposite directions
		if (self.DetectionTypeRef == Emerald_AI.DetectionType.LineOfSight && TabNumberProp.intValue == 2 && DetectionTagsTabNumberProp.intValue == 0){
			Handles.DrawSolidArc(self.transform.position, self.transform.up, self.transform.forward,self.fieldOfViewAngle/2, (float)self.DetectionRadius);
			Handles.DrawSolidArc(self.transform.position, self.transform.up, self.transform.forward,-self.fieldOfViewAngle/2, (float)self.DetectionRadius);
			Handles.color = Color.white;
		}
		else if (self.DetectionTypeRef == Emerald_AI.DetectionType.Trigger && TabNumberProp.intValue == 2 && DetectionTagsTabNumberProp.intValue == 0){
			Handles.DrawSolidDisc(self.transform.position, self.transform.up, (float)self.DetectionRadius);
			Handles.color = Color.white;
		}
			
		if (self.WanderTypeRef == Emerald_AI.WanderType.Dynamic && TabNumberProp.intValue == 0){
			Handles.color = new Color(0, 255, 0, 1.0f);
			Handles.DrawWireDisc(self.transform.position, self.transform.up, (float)self.WanderRadius);
			Handles.color = Color.white;

			if (self.UseSeparateAlertRadiusRef == Emerald_AI.YesOrNo.Yes && TabNumberProp.intValue == 0){
				Handles.color = new Color(255, 255, 0, 1.0f);
				Handles.DrawWireDisc(self.transform.position, self.transform.up, (float)self.AlertWanderRadius);
				Handles.color = Color.white;
			}
		}

		if (TabNumberProp.intValue == 3){
			if (self.DisplayAINameRef == Emerald_AI.DisplayAIName.Yes){
				Handles.color = self.NameTextColor;
				Handles.DrawLine(new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, self.transform.localPosition.z), new Vector3(self.AINamePos.x, self.AINamePos.y+0.6f, self.AINamePos.z)+self.transform.localPosition);
				Handles.color = Color.white;
			}

			if (self.CreateHealthBarsRef == Emerald_AI.CreateHealthBars.Yes){
				Handles.color = self.HealthBarColor;
				Handles.DrawLine(new Vector3(self.transform.localPosition.x+0.5f, self.transform.localPosition.y, self.transform.localPosition.z), new Vector3(self.HealthBarPos.x+0.5f, self.HealthBarPos.y-0.2f, self.HealthBarPos.z)+self.transform.localPosition);
				Handles.color = Color.white;
			}

			if (self.UseCombatTextRef== Emerald_AI.UseCombatText.Yes){
				Handles.color = self.CombatTextColor;
				Handles.DrawLine(new Vector3(self.transform.localPosition.x-0.5f, self.transform.localPosition.y, self.transform.localPosition.z), new Vector3((self.CombatTextPos.x-0.5f)+self.transform.localPosition.x, self.CombatTextPos.y+self.transform.localPosition.y, self.CombatTextPos.z+self.transform.localPosition.z)); //self.transform.localScale.y+
				Handles.color = Color.white;
			}
		}
			
		if (TabNumberProp.intValue == 5 && self.WanderTypeRef == Emerald_AI.WanderType.Waypoints){
			if (self.WaypointsList.Count > 0 && self.WaypointsList != null){
				Handles.color = Color.blue;
				Handles.DrawLine(self.transform.position, self.WaypointsList[0]);
				Handles.color = Color.white;

				Handles.color = Color.green;
				for (int i = 0; i < self.WaypointsList.Count - 1; i++){
					Handles.DrawLine(self.WaypointsList[i], self.WaypointsList[i + 1]);
				}
				Handles.color = Color.white;

				Handles.color = Color.green;
				if (self.WaypointTypeRef == (Emerald_AI.WaypointType.Loop)){
					Handles.DrawLine(self.WaypointsList[0], self.WaypointsList[self.WaypointsList.Count-1]);
				}
				Handles.color = Color.white;

				Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
				for (int i = 0; i < self.WaypointsList.Count; i++){
					Handles.SphereHandleCap(0,self.WaypointsList[i],Quaternion.identity,0.5f,EventType.Repaint);
					drawString("Waypoint "+(i+1),self.WaypointsList[i]+Vector3.up, Color.white);
				}

				Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
				for (int i = 0; i < self.WaypointsList.Count; i++){
					self.WaypointsList[i] = Handles.PositionHandle(self.WaypointsList[i],Quaternion.identity);
				}

				#if UNITY_EDITOR
					EditorUtility.SetDirty(self);
				#endif
			}
		}
			
		if (TabNumberProp.intValue == 0 && self.WanderTypeRef == Emerald_AI.WanderType.Destination && self.SingleDestination != Vector3.zero){
			Handles.color = Color.green;
			Handles.DrawLine(self.transform.position, self.SingleDestination);
			Handles.color = Color.white;

			Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
			Handles.SphereHandleCap(0,self.SingleDestination,Quaternion.identity,0.5f,EventType.Repaint);
			drawString("Destination Point",self.SingleDestination+Vector3.up, Color.white);

			Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
			self.SingleDestination = Handles.PositionHandle(self.SingleDestination,Quaternion.identity);

			#if UNITY_EDITOR
			EditorUtility.SetDirty(self);
			#endif
		}
	}

	static public void drawString(string text, Vector3 worldPos, Color? colour = null) {

		GUIStyle style = new GUIStyle(); 
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;

		UnityEditor.Handles.BeginGUI();

		var restoreColor = GUI.color;

		if (colour.HasValue) GUI.color = colour.Value;
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

		if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0){
			GUI.color = restoreColor;
			UnityEditor.Handles.EndGUI();
			return;
		}

		Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
		GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text, style);
		GUI.color = restoreColor;
		UnityEditor.Handles.EndGUI();
	}
		
	//Custom functions for handling serialized properties. This allows the support of 
	//multi-object editing as well as undo and redo.
	void CustomIntSlider (Rect position, GUIContent label, SerializedProperty property, string Name, int MinValue, int MaxValue) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.IntSlider (Name, property.intValue, MinValue, MaxValue);
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomFloatSlider (Rect position, GUIContent label, SerializedProperty property, string Name, float MinValue, float MaxValue) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.Slider (Name, property.floatValue, MinValue, MaxValue);
		if (EditorGUI.EndChangeCheck ())
			property.floatValue = newValue;

		EditorGUI.EndProperty ();
	}
		
	void CustomPopup (Rect position, GUIContent label, SerializedProperty property, string nameOfLabel, Type typeOfEnum){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		string[] enumNamesList = System.Enum.GetNames (typeOfEnum);

		var newValue = EditorGUILayout.Popup (nameOfLabel, property.intValue, enumNamesList);
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	//Test
	void CustomPopupColor (Rect position, GUIContent label, SerializedProperty property, string nameOfLabel, Type typeOfEnum, Color TextColor){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		string[] enumNamesList = System.Enum.GetNames (typeOfEnum);

		var Style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
		Style.normal.textColor = TextColor;

		EditorGUILayout.LabelField(new GUIContent(nameOfLabel), Style, GUILayout.Width(75));
		var newValue = EditorGUILayout.Popup ("", property.intValue, enumNamesList, GUILayout.Width(65));

		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomIntField (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.IntField (Name, property.intValue);
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomIntFieldProjectiles (Rect position, GUIContent label, SerializedProperty property, string Name, int ProjectileNumber) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.IntField (Name, property.intValue);

		if (newValue != property.intValue){
			if (ProjectileNumber == 1){
				Projectile1UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 2){
				Projectile2UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 3){
				Projectile3UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 4){
				RunProjectileUpdatedProp.boolValue = true;
			}
		}

		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomObjectFieldProjectiles (Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential, int ProjectileNumber) {
		if (IsEssential && property.objectReferenceValue == null){
			GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
			EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
		}

		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.ObjectField (Name, property.objectReferenceValue, typeOfObject, true);

		if (newValue != property.objectReferenceValue){
			if (ProjectileNumber == 1){
				Projectile1UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 2){
				Projectile2UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 3){
				Projectile3UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 4){
				RunProjectileUpdatedProp.boolValue = true;
			}
		}

		if (EditorGUI.EndChangeCheck ())
			property.objectReferenceValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomObjectFieldHitEffect (Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential) {
		if (IsEssential && property.objectReferenceValue == null){
			GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
			EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
		}

		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.ObjectField (Name, property.objectReferenceValue, typeOfObject, true);

		if (newValue != property.objectReferenceValue){
			BloodEffectUpdatedProp.boolValue = true;
		}

		if (EditorGUI.EndChangeCheck ())
			property.objectReferenceValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomFloatFieldProjectiles (Rect position, GUIContent label, SerializedProperty property, string Name, int ProjectileNumber) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.FloatField (Name, property.floatValue);

		if (newValue != property.floatValue){
			if (ProjectileNumber == 1){
				Projectile1UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 2){
				Projectile2UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 3){
				Projectile3UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 4){
				RunProjectileUpdatedProp.boolValue = true;
			}
		}

		if (EditorGUI.EndChangeCheck ())
			property.floatValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomFloatFieldHitEffect (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.FloatField (Name, property.floatValue);

		if (newValue != property.floatValue){
			BloodEffectUpdatedProp.boolValue = true;
		}

		if (EditorGUI.EndChangeCheck ())
			property.floatValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomPopupProjectiles (Rect position, GUIContent label, SerializedProperty property, string nameOfLabel, Type typeOfEnum, int ProjectileNumber){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		string[] enumNamesList = System.Enum.GetNames (typeOfEnum);

		var newValue = EditorGUILayout.Popup (nameOfLabel, property.intValue, enumNamesList);

		if (newValue != property.intValue){
			if (ProjectileNumber == 1){
				Projectile1UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 2){
				Projectile2UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 3){
				Projectile3UpdatedProp.boolValue = true;
			}
			else if (ProjectileNumber == 4){
				RunProjectileUpdatedProp.boolValue = true;
			}
		}

		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomFloatField (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.FloatField (Name, property.floatValue);
		if (EditorGUI.EndChangeCheck ())
			property.floatValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomObjectField (Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential) {
		if (IsEssential && property.objectReferenceValue == null){
			GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
			EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
		}

		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.ObjectField (Name, property.objectReferenceValue, typeOfObject, true);

		if (EditorGUI.EndChangeCheck ())
			property.objectReferenceValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomAnimationField (Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential) {
		if (IsEssential && property.objectReferenceValue == null){
			GUI.backgroundColor = new Color(10f,0.0f,0.0f,0.25f);
			EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;
		}

		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.ObjectField (Name, property.objectReferenceValue, typeOfObject, true);

		if (newValue != property.objectReferenceValue){
			AnimationsUpdatedProp.boolValue = true;
		}

		if (EditorGUI.EndChangeCheck ())
			property.objectReferenceValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomVector2Field (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.Vector2Field(Name, property.vector2Value);
		if (EditorGUI.EndChangeCheck ())
			property.vector2Value = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomVector3Field (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.Vector3Field(Name, property.vector3Value);
		if (EditorGUI.EndChangeCheck ())
			property.vector3Value = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomColorField (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.ColorField(Name, property.colorValue);
		if (EditorGUI.EndChangeCheck ())
			property.colorValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomStringField (Rect position, GUIContent label, SerializedProperty property, string Name) {
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.TextField(Name, property.stringValue);
		if (EditorGUI.EndChangeCheck ())
			property.stringValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomLayerMask (Rect position, GUIContent label, SerializedProperty property, string Name){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.MaskField(Name, property.intValue, UnityEditorInternal.InternalEditorUtility.layers);
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomEnum (Rect position, GUIContent label, SerializedProperty property, string Name){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.Popup(Name, property.intValue, Emerald_AI.StringFactionList.ToArray());
		if (property.intValue == Emerald_AI.StringFactionList.Count){
			property.intValue -= 1;
		}
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	//Test
	void CustomEnumColor (Rect position, GUIContent label, SerializedProperty property, string Name, Color TextColor){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();

		var Style = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
		Style.normal.textColor = TextColor;

		EditorGUILayout.LabelField(new GUIContent(Name), Style, GUILayout.Width(50));
		var newValue = EditorGUILayout.Popup("", property.intValue, Emerald_AI.StringFactionList.ToArray(), GUILayout.MaxWidth(100), GUILayout.ExpandWidth(true));

		if (property.intValue == Emerald_AI.StringFactionList.Count){
			property.intValue -= 1;
		}
		if (EditorGUI.EndChangeCheck ())
			property.intValue = newValue;

		EditorGUI.EndProperty ();
	}

	void CustomTagField (Rect position, GUIContent label, SerializedProperty property, string Name){
		label = EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.TagField(Name, property.stringValue);
		if (EditorGUI.EndChangeCheck ())
			property.stringValue = newValue;

		EditorGUI.EndProperty ();
	}

	public static bool Foldout(bool foldout, GUIContent content, bool toggleOnLabelClick, GUIStyle style){
		Rect position = GUILayoutUtility.GetRect(40f, 40f, 16f, 16f, style);
		return EditorGUI.Foldout(position, foldout, content, toggleOnLabelClick, style);
	}
	public static bool Foldout(bool foldout, string content, bool toggleOnLabelClick, GUIStyle style){
		return Foldout(foldout, new GUIContent(content), toggleOnLabelClick, style);
	}
		
	// Converts the field value to a LayerMask
	private LayerMask FieldToLayerMask(int field)
	{
		LayerMask mask = 0;
		var layers = InternalEditorUtility.layers;
		for (int c = 0; c < layers.Length; c++)
		{
			if ((field & (1 << c)) != 0)
			{
				mask |= 1 << LayerMask.NameToLayer(layers[c]);
			}
		}
		return mask;
	}
	// Converts a LayerMask to a field value
	private int LayerMaskToField(LayerMask mask)
	{
		int field = 0;
		var layers = InternalEditorUtility.layers;
		for (int c = 0; c < layers.Length; c++)
		{
			if ((mask & (1 << LayerMask.NameToLayer(layers[c]))) != 0)
			{
				field |= 1 << c;
			}
		}
		return field;
	}
}
