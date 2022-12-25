using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerHealth))] 
public class PlayerHealthEditor : Editor 
{
	public override void OnInspectorGUI () 
	{
		PlayerHealth self = (PlayerHealth)target;

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("Health Options", EditorStyles.boldLabel);

		self.startingHealth = EditorGUILayout.FloatField ("Starting Health", self.startingHealth);

		EditorGUILayout.HelpBox("Your Starting health is the health your player will start with. If you plan on using the health bar UI, this value should be at 100.", MessageType.None, true);

		EditorGUILayout.Space();

		self.currentHealth = EditorGUILayout.FloatField ("Current Health", self.currentHealth);

		EditorGUILayout.HelpBox("The current health your player currently has.", MessageType.None, true);

		EditorGUILayout.Space();

		self.healthRegen = EditorGUILayout.FloatField ("Health Regen", self.healthRegen);

		EditorGUILayout.HelpBox("Health Regen is the multipler in which your player will regenerate health.", MessageType.None, true);

		EditorGUILayout.Space();

		self.healthBar = (Slider)EditorGUILayout.ObjectField ("Health Bar", self.healthBar, typeof(Slider), true);
		
		EditorGUILayout.HelpBox("The Health Bar Slider that your player will use for its health bar.", MessageType.None, true);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.LabelField("Sound Options", EditorStyles.boldLabel);

		self.useHitSounds = EditorGUILayout.Toggle ("Use Hit Sounds", self.useHitSounds);
		
		EditorGUILayout.HelpBox("Use Hit Sounds enable or disable Hit Sounds.", MessageType.None, true);
		
		EditorGUILayout.Space();
		
		if (self.useHitSounds)
		{
			self.hitSoundSize = EditorGUILayout.IntSlider("Hit Sound Size", self.hitSoundSize, 1, 20);
			
			EditorGUILayout.HelpBox("Hit Sounds allow you to set an array of sounds that will play dynamically feach time the player is dealt damage. This will pick from a selection of up to 20 sounds. You can choose to enable or disable sounds using the Use Hit Sounds check box.", MessageType.None, true);
			
			EditorGUILayout.Space();
			
			if(self.hitSoundSize > self.foldOutListHit.Count)              
			{
				var temp = (self.hitSoundSize - self.foldOutListHit.Count);
				for(int j = 0; j < temp ; j++)
					self.foldOutListHit.Add(true);                      
			}
			
			if(self.hitSoundSize > self.hitSounds.Count)                 
			{
				var temp1 = self.hitSoundSize - self.hitSounds.Count;
				for(int j = 0; j < temp1 ; j++)
				{
					//self.hitSounds.Add(new AudioClip() );    
				}
			}
			
			if(self.hitSounds.Count > self.hitSoundSize)
			{
				self.hitSounds.RemoveRange( (self.hitSoundSize), self.hitSounds.Count - (self.hitSoundSize));       
				self.foldOutListHit.RemoveRange( (self.hitSoundSize), self.foldOutListHit.Count-(self.hitSoundSize));
			}
			
			for(int i = 0; i < self.hitSounds.Count; i++)
			{                   
				int tempCount = i + 1;
				self.hitSounds[i] = (AudioClip)EditorGUILayout.ObjectField("Hit Sound " + tempCount + ":" , self.hitSounds[i], typeof(AudioClip), true );
				GUILayout.Space(10);
			}
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();


		/*
		Rect r = EditorGUILayout.BeginVertical();
		EditorGUI.ProgressBar(r, self.currentHealth*0.01f, "Halfway there!");
		GUILayout.Space(16);
		EditorGUILayout.EndVertical();
		*/
	}
}
