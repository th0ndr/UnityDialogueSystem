namespace DialogueManager.Editors
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using DialogueManager.GameComponents;

    [CustomEditor( typeof( DialogueManagerComponent ) )]
    public class DialogueManagerEditor : Editor
    {
        private SerializedProperty gameConversationsProperty;
        private SerializedProperty canvasObjectsProperty;
        private SerializedProperty waitTimeProperty;
        private SerializedProperty voiceVolumeProperty;
        private SerializedProperty doubleTapProperty;
        private SerializedProperty nextKeyProperty;
        void OnEnable()
        {
            gameConversationsProperty = serializedObject.FindProperty("Model.GameConversationsPrefab");
            canvasObjectsProperty = serializedObject.FindProperty( "Model.CanvasObjectsPrefab" );
            waitTimeProperty = serializedObject.FindProperty( "Model.WaitTime" );
            voiceVolumeProperty = serializedObject.FindProperty( "Model.VoiceVolume" );
            doubleTapProperty = serializedObject.FindProperty( "Model.DoubleTap" );
            nextKeyProperty = serializedObject.FindProperty( "Model.NextKey" );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(gameConversationsProperty, false);
            EditorGUILayout.PropertyField( canvasObjectsProperty, false );
            EditorGUILayout.PropertyField( waitTimeProperty, true );
            EditorGUILayout.PropertyField( voiceVolumeProperty, true );
            EditorGUILayout.PropertyField( doubleTapProperty, true );
            EditorGUILayout.PropertyField( nextKeyProperty, true );
            serializedObject.ApplyModifiedProperties();
        }
    }
}