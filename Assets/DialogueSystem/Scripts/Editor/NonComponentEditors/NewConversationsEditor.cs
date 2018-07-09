namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Inspector Editor for the New Conversations unlocked
    /// </summary>
    public class NewConversationsEditor
    {
        /// <summary> Index of the displayed element in the new conversations Foldout </summary>
        private static int newConversationsFoldoutDisplay = -1;

        /// <summary>
        /// Displays on the Inspector GUI a List of PendingStatus
        /// </summary>
        /// <param name="conversations">List of Pending Status (new conversations unlocked)</param>
        public static void Display( List<PendingStatus> conversations )
        {
            if (conversations == null)
            {
                conversations = new List<PendingStatus>();
            }

            EditorGUILayout.LabelField( "New Conversations", EditorStyles.boldLabel );

            for (int i = 0; i < conversations.Count; i++)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginHorizontal();
                bool display = i == newConversationsFoldoutDisplay;
                display = EditorGUILayout.Foldout( display, conversations[i].ConversationName );
                if (GUILayout.Button( EditorButtons.RemovePendingStatusButton, EditorStyles.miniButton, EditorButtons.MiniButtonWidth ))
                {
                    conversations.RemoveAt( i );
                    newConversationsFoldoutDisplay = -1;
                    break;
                }

                GUILayout.EndHorizontal();
                if (!display && i == newConversationsFoldoutDisplay)
                {
                    newConversationsFoldoutDisplay = -1;
                }

                if (display)
                {
                    newConversationsFoldoutDisplay = i;
                    EditorGUI.indentLevel++;
                    conversations[i].ConversationName = EditorGUILayout.TextField( "Conversation", conversations[i].ConversationName );
                    conversations[i].StatusName = EditorGUILayout.TextField( "Status", conversations[i].StatusName );
                    conversations[i].Importance = EditorGUILayout.IntField( "Importance", conversations[i].Importance );
                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }

            if (GUILayout.Button( EditorButtons.AddPendingStatusButton, EditorStyles.miniButton, EditorButtons.NormalButtonWidth ))
            {
                PendingStatus pendingStatus = new PendingStatus();
                conversations.Add( pendingStatus );
            }
        }
    }
}