namespace DialogueManager.Editors
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using System.Collections.Generic;

    [CustomEditor( typeof( ConversationComponent ) )]
    public class ConversationEditor : Editor
    {
        private SerializedProperty nameProperty;
        private SerializedProperty activeStatusProperty;
        private SerializedProperty statusProperty;

        int selected = 1;

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

            // DropDown Demo
            ConversationComponent cc = ( ConversationComponent )target;
            Conversation c = cc.Model;
            
            List<string> statusList = new List<string>();
            foreach (ConversationStatus status in c.Status)
            {
                statusList.Add( status.Name );
            }
            string[] ahhh = statusList.ToArray();
            selected = EditorGUILayout.Popup( "Status List", selected, ahhh, EditorStyles.popup );
            EditorGUILayout.LabelField( selected.ToString() );
            serializedObject.ApplyModifiedProperties();
        }

    }
}