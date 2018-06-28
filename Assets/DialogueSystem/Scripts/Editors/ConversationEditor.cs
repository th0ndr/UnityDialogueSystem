namespace DialogueManager.Editors
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using DialogueManager.GameComponents;

    [CustomEditor( typeof( ConversationComponent ) )]
    public class LevelScriptEditor : Editor
    {
        private SerializedProperty nameProperty;
        private SerializedProperty activeStatusProperty;
        private SerializedProperty statusProperty;

        void OnEnable()
        {
            nameProperty = serializedObject.FindProperty( "Model.Name" );
            activeStatusProperty = serializedObject.FindProperty( "Model.ActiveStatus" );
            statusProperty = serializedObject.FindProperty( "Model.Status" );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField( nameProperty, true );
            EditorGUILayout.PropertyField( activeStatusProperty, true );
            EditorGUILayout.PropertyField( statusProperty, true );
            serializedObject.ApplyModifiedProperties();
        }

    }
}