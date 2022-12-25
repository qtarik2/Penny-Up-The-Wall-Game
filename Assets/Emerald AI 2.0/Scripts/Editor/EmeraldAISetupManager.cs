using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class EmeraldAISetupManager : EditorWindow
{
    public GameObject ObjectToSetup;
    public AnimationClip Idle1, Idle2, Idle3, Walk, Run, IdleAlert, IdleWarning, IdleCombat, Hit1, TurnLeft, TurnRight, RunAttack, Attack1, Attack2, Attack3, DeadAnim;
    public List<GameObject> ObjectsToSetup = new List<GameObject>();
    public enum AIBehavior { Passive = 0, Cautious = 1, Companion = 2, Aggressive = 3, Pet = 4 };
    public AIBehavior AIBehaviorRef = AIBehavior.Passive;
    public enum ConfidenceType { Coward = 0, Brave = 1, Foolhardy = 2 };
    public ConfidenceType ConfidenceRef = ConfidenceType.Brave;
    public enum WanderType { Dynamic = 0, Waypoints = 1, Stationary = 2, Destination = 3 };
    public WanderType WanderTypeRef = WanderType.Dynamic;
    public enum SetupAnimations { Now = 0, Later = 1 };
    public SetupAnimations SetupAnimationsRef = SetupAnimations.Now;
    public enum SetupOptimizationSettings { Yes = 0, No = 1 };
    public SetupOptimizationSettings SetupOptimizationSettingsRef = SetupOptimizationSettings.Yes;
    Animator AIAnimator;
    AnimatorOverrideController overrideController;
    BoxBoundsHandle m_BoundsHandle;
    Vector3 TotalBondsSize;
    Vector3 TotalBondsExtends;
    Vector2 scrollPos;
    string FilePath;
    Bounds RendererBounds;
    Texture SettingsIcon;
    static float secs = 0;
    static double startVal = 0;
    static double progress = 0;
    Vector3 _T;
    void OnInspectorUpdate()
    {
        Repaint();
    }
    [MenuItem("Window/Emerald AI/Setup Manager #%e", false, 200)]
    public static void ShowWindow()
    {
        EditorWindow APS = EditorWindow.GetWindow(typeof(EmeraldAISetupManager));
        APS.minSize = new Vector2(600f, 650f);
    }
    void OnEnable()
    {
        if (SettingsIcon == null) SettingsIcon = Resources.Load("SettingsIcon") as Texture;
    }
    void OnGUI()
    {
        GUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(90 * Screen.width / 100));
        var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField(new GUIContent(SettingsIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
        EditorGUILayout.LabelField("Emerald AI Setup Manager - v1.0", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.HelpBox("With the Emerald AI Setup Manager, you can apply an Emerald AI component to an object. Be aware that closing the Emerald Setup Manager will lose all references you've entered below. Make sure you select 'Setup AI' before closing, if you'd like your changes to be applied.", MessageType.None, true);
        GUILayout.Space(4);
        EditorGUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Space(25);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginVertical("Box");

        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Setup Settings", EditorStyles.boldLabel);
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndVertical();
        GUI.backgroundColor = Color.white;

        GUILayout.Space(15);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("The object that the Emerald AI system will be added to.", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        if (ObjectToSetup == null)
        {
            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
            EditorGUILayout.LabelField("This field cannot be left blank.", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }
        ObjectToSetup = (GameObject)EditorGUILayout.ObjectField("AI Object", ObjectToSetup, typeof(GameObject), true);
        GUILayout.Space(10);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("Please select your AI's Behavior.", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        AIBehaviorRef = (AIBehavior)EditorGUILayout.EnumPopup("AI's Behavior", AIBehaviorRef);
        GUILayout.Space(10);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("Please select your AI's Confidence.", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        ConfidenceRef = (ConfidenceType)EditorGUILayout.EnumPopup("AI's Confidence", ConfidenceRef);
        GUILayout.Space(10);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("Please select your AI's Wander Type.", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        WanderTypeRef = (WanderType)EditorGUILayout.EnumPopup("AI's Wander Type", WanderTypeRef);
        GUILayout.Space(10);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("Would you like the Setup Manager to automatically setup Emerald's optimization settings? This allows Emerald to be deactivated when an AI is culled or not visible which will help improve performance.", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        SetupOptimizationSettingsRef = (SetupOptimizationSettings)EditorGUILayout.EnumPopup("Auto Optimize", SetupOptimizationSettingsRef);
        GUILayout.Space(10);

        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
        EditorGUILayout.HelpBox("Would you like to setup your animations now or later?", MessageType.None, true);
        GUI.backgroundColor = Color.white;
        SetupAnimationsRef = (SetupAnimations)EditorGUILayout.EnumPopup("Setup Animations", SetupAnimationsRef);
        if (SetupAnimationsRef == SetupAnimations.Later)
        {
            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
            EditorGUILayout.HelpBox("You can setup your animations later via the Emerald Editor.", MessageType.None, true);
            GUI.backgroundColor = Color.white;
        }
        GUILayout.Space(10);

        if (SetupAnimationsRef == SetupAnimations.Now && ObjectToSetup == null)
        {
            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
            EditorGUILayout.LabelField("You must have an object applied to the AI Object slot before you can create an Animator Controller for it.", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }

        if (SetupAnimationsRef == SetupAnimations.Now && ObjectToSetup != null)
        {
            if (ObjectToSetup.GetComponent<Animator>() != null)
            {
                AIAnimator = ObjectToSetup.GetComponent<Animator>();
            }
            else if (ObjectToSetup.GetComponent<Animator>() == null)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.LabelField("You must have an Animator Component on your AI Object in order to create an Animator Controller.", EditorStyles.helpBox);
                GUI.backgroundColor = Color.white;
            }
        }
        EditorGUI.BeginDisabledGroup(SetupAnimationsRef == SetupAnimations.Now && ObjectToSetup == null || SetupAnimationsRef == SetupAnimations.Now && ObjectToSetup != null && ObjectToSetup.GetComponent<Animator>() == null);
        if (SetupAnimationsRef == SetupAnimations.Now && overrideController == null)
        {
            if (GUILayout.Button("Create Animator Controller"))
            {
                FilePath = EditorUtility.SaveFilePanelInProject("Save as OverrideController", "New OverrideController", "overrideController", "Please enter a file name to save the file to");
                if (FilePath != string.Empty)
                {
                    _T = ObjectToSetup.transform.position;

                    AssetDatabase.CopyAsset("Assets/Emerald AI 2.0/Animator/Emerald Override.overrideController", FilePath);
                    overrideController = AssetDatabase.LoadAssetAtPath(FilePath, typeof(AnimatorOverrideController)) as AnimatorOverrideController;

                    string Temp = FilePath;
                    Temp = Temp.Replace(".overrideController", ".controller");
                    AssetDatabase.CopyAsset("Assets/Emerald AI 2.0/Animator/Temp Emerald Animator (Do Not Edit).controller", Temp);
                    overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath(Temp, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

                    AIAnimator = ObjectToSetup.GetComponent<Animator>();
                    AIAnimator.runtimeAnimatorController = overrideController;

                    ObjectToSetup.transform.position = _T;
                }
            }
        }
        EditorGUI.EndDisabledGroup();
        if (SetupAnimationsRef == SetupAnimations.Now && overrideController != null)
        {
            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
            EditorGUILayout.LabelField("Note: Closing the Emerald Setup Manager will lose all references you've entered to the animations clips below. Make sure you select 'Setup AI' before closing, if you'd like your changes to be applied.", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;

            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
            EditorGUILayout.LabelField("Idle Animations", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;

            Idle1 = (AnimationClip)EditorGUILayout.ObjectField("Idle 1 Animation", Idle1, typeof(AnimationClip), true);
            Idle2 = (AnimationClip)EditorGUILayout.ObjectField("Idle 2 Animation", Idle2, typeof(AnimationClip), true);
            Idle3 = (AnimationClip)EditorGUILayout.ObjectField("Idle 3 Animation", Idle3, typeof(AnimationClip), true);
            IdleAlert = (AnimationClip)EditorGUILayout.ObjectField("Idle Alert Animation", IdleAlert, typeof(AnimationClip), true);
            IdleWarning = (AnimationClip)EditorGUILayout.ObjectField("Idle Warning Animation", IdleWarning, typeof(AnimationClip), true);
            IdleCombat = (AnimationClip)EditorGUILayout.ObjectField("Idle Combat Animation", IdleCombat, typeof(AnimationClip), true);

            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
            EditorGUILayout.LabelField("Movement Animations", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
            Walk = (AnimationClip)EditorGUILayout.ObjectField("Walk Animation", Walk, typeof(AnimationClip), true);
            Run = (AnimationClip)EditorGUILayout.ObjectField("Run Animation", Run, typeof(AnimationClip), true);
            TurnLeft = (AnimationClip)EditorGUILayout.ObjectField("Turn Left Animation", TurnLeft, typeof(AnimationClip), true);
            TurnRight = (AnimationClip)EditorGUILayout.ObjectField("Turn Right Animation", TurnRight, typeof(AnimationClip), true);

            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
            EditorGUILayout.LabelField("Combat Animations", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
            Attack1 = (AnimationClip)EditorGUILayout.ObjectField("Attack 1 Animation", Attack1, typeof(AnimationClip), true);
            Attack2 = (AnimationClip)EditorGUILayout.ObjectField("Attack 2 Animation", Attack2, typeof(AnimationClip), true);
            Attack3 = (AnimationClip)EditorGUILayout.ObjectField("Attack 3 Animation", Attack3, typeof(AnimationClip), true);
            RunAttack = (AnimationClip)EditorGUILayout.ObjectField("Run Attack Animation", RunAttack, typeof(AnimationClip), true);
            Hit1 = (AnimationClip)EditorGUILayout.ObjectField("Hit Animation", Hit1, typeof(AnimationClip), true);
            DeadAnim = (AnimationClip)EditorGUILayout.ObjectField("Death Animation", DeadAnim, typeof(AnimationClip), true);
        }
        GUILayout.Space(35);
        if (SetupAnimationsRef == SetupAnimations.Now && overrideController == null)
        {
            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
            EditorGUILayout.LabelField("You must create an Animator Controller for this AI, when using the Setup Animations Now feature.", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }

        if (ObjectToSetup == null)
        {
            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
            EditorGUILayout.LabelField("You must have an object applied to the AI Object slot before you can complete the setup process.", EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }

        EditorGUI.BeginDisabledGroup(SetupAnimationsRef == SetupAnimations.Now && overrideController == null || ObjectToSetup == null);
        if (GUILayout.Button("Setup AI"))
        {
            if (EditorUtility.DisplayDialog("Emerald AI Setup Manager", "Are you sure you'd like to setup an AI on this object?", "Setup", "Cancel"))
            {
                //u	PrefabUtility.DisconnectPrefabInstance(ObjectToSetup);
                AssignEmeraldAIComponents();
                startVal = EditorApplication.timeSinceStartup;
            }
        }
        GUILayout.Space(25);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        GUILayout.Space(25);
        GUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        GUILayout.Space(30);

        if (secs > 0)
        {
            progress = EditorApplication.timeSinceStartup - startVal;

            if (progress < secs)
            {
                EditorUtility.DisplayProgressBar("Emerald AI Setup Manager", "Setting up AI...", (float)(progress / secs));
            }
            else
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }
    void AssignEmeraldAIComponents()
    {
        List<SkinnedMeshRenderer> ObjRenderer = new List<SkinnedMeshRenderer>();

        if (ObjectToSetup != null && ObjectToSetup.GetComponent<Emerald_AI>() == null && ObjectToSetup.GetComponent<Animator>() != null)
        {
            secs = 1.5f;
            ObjectToSetup.AddComponent<Emerald_AI>();
            ObjectToSetup.GetComponent<AudioSource>().spatialBlend = 1;
            ObjectToSetup.GetComponent<AudioSource>().dopplerLevel = 0;
            ObjectToSetup.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
            ObjectToSetup.GetComponent<AudioSource>().minDistance = 1;
            ObjectToSetup.GetComponent<AudioSource>().maxDistance = 50;

            Emerald_AI Emerald = ObjectToSetup.GetComponent<Emerald_AI>();

            if (SetupAnimationsRef == SetupAnimations.Later)
            {
                //Get the AI's Animator and set the default Runtime Controller
                Emerald.AIAnimator = ObjectToSetup.GetComponent<Animator>();
                Emerald.AIAnimator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath("Assets/Emerald AI 2.0/Animator/Temp Emerald Animator (Do Not Edit).controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
            }

            Emerald.BehaviorRef = (Emerald_AI.CurrentBehavior)AIBehaviorRef;
            Emerald.ConfidenceRef = (Emerald_AI.ConfidenceType)ConfidenceRef;
            Emerald.WanderTypeRef = (Emerald_AI.WanderType)WanderTypeRef;

            if (SetupAnimationsRef == SetupAnimations.Now)
            {
                Emerald.AIAnimator = ObjectToSetup.GetComponent<Animator>();
                Emerald.overrideController = overrideController;

                Emerald.Idle1 = Idle1;
                Emerald.Idle2 = Idle2;
                Emerald.Idle3 = Idle3;
                Emerald.IdleAlert = IdleAlert;
                Emerald.IdleWarning = IdleWarning;
                Emerald.IdleCombat = IdleCombat;
                Emerald.Walk = Walk;
                Emerald.Run = Run;
                Emerald.TurnLeft = TurnLeft;
                Emerald.TurnRight = TurnRight;
                Emerald.RunAttack = RunAttack;
                Emerald.Attack1 = Attack1;
                Emerald.Attack2 = Attack2;
                Emerald.Attack3 = Attack3;
                Emerald.Hit1 = Hit1;
                Emerald.DeadAnim = DeadAnim;

                if (Idle3 != null && Idle2 != null)
                {
                    Emerald.TotalIdleAnimationsRef = (Emerald_AI.TotalIdleAnimations)2;
                }
                if (Idle3 == null && Idle2 != null)
                {
                    Emerald.TotalIdleAnimationsRef = (Emerald_AI.TotalIdleAnimations)1;
                }
                if (Idle2 == null && Idle3 == null)
                {
                    Emerald.TotalIdleAnimationsRef = (Emerald_AI.TotalIdleAnimations)0;
                }
                if (Attack3 != null && Attack2 != null)
                {
                    Emerald.TotalAttackAnimationsRef = (Emerald_AI.TotalAttackAnimations)2;
                }
                if (Attack3 == null && Attack2 != null)
                {
                    Emerald.TotalAttackAnimationsRef = (Emerald_AI.TotalAttackAnimations)1;
                }
                if (Attack2 == null && Attack3 == null)
                {
                    Emerald.TotalAttackAnimationsRef = (Emerald_AI.TotalAttackAnimations)0;
                }

                Emerald.SetupAnimations(AIAnimator);
            }

            if (SetupOptimizationSettingsRef == SetupOptimizationSettings.Yes)
            {
                Emerald.DisableAIWhenNotInViewRef = Emerald_AI.YesOrNo.Yes;
                if (ObjectToSetup.GetComponent<LODGroup>() != null)
                {
                    LODGroup _LODGroup = ObjectToSetup.GetComponentInChildren<LODGroup>();

                    if (_LODGroup != null)
                    {
                        LOD[] AllLODs = _LODGroup.GetLODs();

                        if (_LODGroup.lodCount <= 4)
                        {
                            Emerald.TotalLODsRef = (Emerald_AI.TotalLODsEnum)(_LODGroup.lodCount - 2);
                            Emerald.HasMultipleLODsRef = Emerald_AI.YesOrNo.Yes;
                        }

                        if (_LODGroup.lodCount > 1)
                        {
                            for (int i = 0; i < _LODGroup.lodCount; i++)
                            {
                                if (i == 0)
                                {
                                    Emerald.Renderer1 = AllLODs[i].renderers[0];
                                }
                                if (i == 1)
                                {
                                    Emerald.Renderer2 = AllLODs[i].renderers[0];
                                }
                                if (i == 2)
                                {
                                    Emerald.Renderer3 = AllLODs[i].renderers[0];
                                }
                                if (i == 3)
                                {
                                    Emerald.Renderer4 = AllLODs[i].renderers[0];
                                }
                            }

                        }
                        else if (_LODGroup.lodCount == 1)
                        {
                            Emerald.HasMultipleLODsRef = Emerald_AI.YesOrNo.No;
                        }
                    }
                    else if (_LODGroup == null)
                    {
                        Emerald.HasMultipleLODsRef = Emerald_AI.YesOrNo.No;
                    }
                }
            }
            Component[] AllComponents = ObjectToSetup.GetComponents<Component>();

            for (int i = 0; i < AllComponents.Length; i++)
            {
                UnityEditorInternal.ComponentUtility.MoveComponentUp(Emerald);
            }
            foreach (SkinnedMeshRenderer R in ObjectToSetup.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (R != ObjectToSetup.GetComponent<SkinnedMeshRenderer>())
                {
                    ObjRenderer.Add(R);
                }
            }

            if (ObjRenderer.Count > 0)
            {
                Bounds RendererBounds = ObjRenderer[0].bounds;
                ObjectToSetup.GetComponent<BoxCollider>().size = new Vector3(RendererBounds.extents.x, RendererBounds.size.y, RendererBounds.extents.z);
                ObjectToSetup.GetComponent<BoxCollider>().center = new Vector3(ObjectToSetup.GetComponent<BoxCollider>().center.x, RendererBounds.extents.y, ObjectToSetup.GetComponent<BoxCollider>().center.z);
            }
            overrideController = null;
            ObjectToSetup = null;
        }
        else if (ObjectToSetup == null)
        {
            EditorUtility.DisplayDialog("Emerald AI Setup Manager - Oops!", "There is no object assigned to the AI Object slot. Please assign one and try again.", "Okay");
        }
        else if (ObjectToSetup.GetComponent<Emerald_AI>() != null)
        {
            EditorUtility.DisplayDialog("Emerald AI Setup Manager - Oops!", "There is already an Emerald AI component on this object. Please choose another object to apply an Emerald AI component to.", "Okay");
        }
        else if (ObjectToSetup.GetComponent<Animator>() == null)
        {
            EditorUtility.DisplayDialog("Emerald AI Setup Manager - Oops!", "There is no Animator component attached to your AI. Please assign one and try again.", "Okay");
        }
    }
}
