namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Inspector Editor for the Conversation Status List
    /// </summary>
    public class ConversationStatusEditor
    {
        /// <summary> Index of the displayed element in the status Foldout </summary>
        private static int statusFoldoutDisplay = -1;

        /// <summary> List of ConversationStatus being displayed </summary>
        private static List<ConversationStatus> status;

        /// <summary> Array with the names of all the Status </summary>
        private static string[] statusNames;

        /// <summary>
        /// Displays on the Inspector GUI a List of Conversation Status
        /// </summary>
        /// <param name="status">List of Conversation Status</param>
        /// <param name="statusListNames">Array containing the Names of each Conversation Status</param>
        public static void Display( List<ConversationStatus> status, string[] statusListNames )
        {
            ConversationStatusEditor.status = status;
            ConversationStatusEditor.statusNames = statusListNames;
            for (int i = 0; i < status.Count; i++)
            {
                bool display = i == statusFoldoutDisplay;
                display = EditorGUILayout.Foldout( display, status[i].Name );
                if (!display && i == statusFoldoutDisplay)
                {
                    statusFoldoutDisplay = -1;
                }

                if (display)
                {
                    EditorGUILayout.BeginVertical( GUI.skin.box );
                    DisplayNameAndRemoveButton( i );
                    DisplayNextStatus( i );
                    DialogueEditor.Display( status[i].Dialogue );
                    NewConversationsEditor.Display( status[i].NewConversations );
                    EditorGUILayout.EndVertical();
                }
            }
        }

        /// <summary>
        /// Displays the Name and the Remove Button of the Conversation Status on the Inspector
        /// </summary>
        /// <param name="i">Index in the List of the Status</param>
        private static void DisplayNameAndRemoveButton( int i )
        {
            statusFoldoutDisplay = i;
            GUILayout.BeginHorizontal();
            status[i].Name = EditorGUILayout.TextField( "Status Name", status[i].Name );
            if (GUILayout.Button( EditorButtons.RemoveStatusButton, EditorStyles.miniButton, EditorButtons.MiniButtonWidth ))
            {
                status.RemoveAt( i );
                statusFoldoutDisplay = -1;
                return;
            }

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Displays a Dropdown menu for the user to select the Status to be the next one after the current
        /// </summary>
        /// <param name="i">Index in the List of the Status</param>
        private static void DisplayNextStatus( int i )
        {
            if (status[i].NextStatusIndex >= status.Count)
            {
                status[i].NextStatusIndex = 0;
            }

            status[i].NextStatusIndex = EditorGUILayout.Popup(
                "Next Status",
                status[i].NextStatusIndex,
                statusNames,
                EditorStyles.popup );
            status[i].NextStatus = status[status[i].NextStatusIndex];
        }
    }
}