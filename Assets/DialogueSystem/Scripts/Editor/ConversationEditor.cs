namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using System.Linq;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    /// <summary>
    /// Inspector custom editor of the Conversation Component
    /// </summary>
    [CustomEditor( typeof( ConversationComponent ) )]
    public class ConversationEditor : Editor
    {
        /// <summary>
        /// When the GUI is displayed
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();
            ConversationComponent conversationComponent = ( ConversationComponent )target;
            Conversation model = conversationComponent.Model;

            model.Name = EditorGUILayout.TextField( "Name", model.Name );
            if (model.Status == null)
            {
                model.Status = new List<ConversationStatus>();
            }

            if (model.Status.Count > 0)
            {
                string[] statusListNames = model.Status.Select( s => s.Name ).ToArray();
                model.ActiveStatusIndex = EditorGUILayout.Popup(
                    "Active Status",
                    model.ActiveStatusIndex,
                    statusListNames,
                    EditorStyles.popup );
                model.ActiveStatus = model.Status[model.ActiveStatusIndex];
                EditorGUILayout.Space();
                EditorGUILayout.LabelField( "Status List", EditorStyles.boldLabel );
                ConversationStatusEditor.Display( model.Status, statusListNames );
            }

            if (GUILayout.Button( EditorButtons.AddStatusButton, EditorStyles.miniButton, EditorButtons.NormalButtonWidth ))
            {
                ConversationStatus newStatus = new ConversationStatus();
                newStatus.Name = "Status " + ( model.Status.Count + 1 );
                model.Status.Add( newStatus );
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();    

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( this.target );
                if (!Application.isPlaying)
                {
                    EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene() );
                }
            }
        }
    }
}