namespace DialogueManager.Editors
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using DialogueManager.GameComponents;

    [CustomEditor( typeof( DialogueManagerComponent ) )]
    public class DialogueManagerEditor : Editor
    {
        private SerializedProperty dialogueTextProperty;
        private SerializedProperty imageTextProperty;
        private SerializedProperty animatorProperty;
        private SerializedProperty waitTimeProperty;
        private SerializedProperty voiceVolumeProperty;
        private SerializedProperty doubleTapProperty;
        private SerializedProperty nextKeyProperty;
        void OnEnable()
        {
            dialogueTextProperty = serializedObject.FindProperty( "Model.DialogueText" );
            imageTextProperty = serializedObject.FindProperty( "Model.ImageText" );
            animatorProperty = serializedObject.FindProperty( "Model.Animator" );
            waitTimeProperty = serializedObject.FindProperty( "Model.WaitTime" );
            voiceVolumeProperty = serializedObject.FindProperty( "Model.VoiceVolume" );
            doubleTapProperty = serializedObject.FindProperty( "Model.DoubleTap" );
            nextKeyProperty = serializedObject.FindProperty( "Model.NextKey" );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField( dialogueTextProperty, false );
            EditorGUILayout.PropertyField( imageTextProperty, false );
            EditorGUILayout.PropertyField( animatorProperty, false );
            EditorGUILayout.PropertyField( waitTimeProperty, true );
            EditorGUILayout.PropertyField( voiceVolumeProperty, true );
            EditorGUILayout.PropertyField( doubleTapProperty, true );
            EditorGUILayout.PropertyField( nextKeyProperty, true );
            serializedObject.ApplyModifiedProperties();
        }

    }
}