namespace DialogueManager.InspectorEditors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Static public values of the buttons for the Editors
    /// </summary>
    public class EditorButtons
    {
        /// <summary> Width of a normal Button </summary>
        public static GUILayoutOption NormalButtonWidth = GUILayout.Width( 85f );

        /// <summary> Width of a Mini Button </summary>
        public static GUILayoutOption MiniButtonWidth = GUILayout.Width( 24f );

        /// <summary> Add Status Editor GUI Button </summary>
        public static GUIContent AddStatusButton = new GUIContent( "Add Status", "Add Status" );

        /// <summary> Remove Status Editor GUI Button </summary>
        public static GUIContent RemoveStatusButton = new GUIContent( "-", "Remove Status" );

        /// <summary> Add Dialgue Editor GUI Button </summary>
        public static GUIContent AddDialogueButton = new GUIContent( "Add Dialogue", "Add Dialogue" );

        /// <summary> Remove Dialogue Editor GUI Button </summary>
        public static GUIContent RemoveDialogueButton = new GUIContent( "-", "Remove Dialogue" );

        /// <summary> Add Conversation Button </summary>
        public static GUIContent AddConversationButton = new GUIContent( "Add Conversation", "Add Conversation" );

        /// <summary> Add Status Button </summary>
        public static GUIContent RemoveConversationButton = new GUIContent( "-", "Remove Conversation" );

        /// <summary> Add Status Button </summary>
        public static GUIContent AddPendingStatusButton = new GUIContent( "Add", "Add PendingStatus" );

        /// <summary> Add Status Button </summary>
        public static GUIContent RemovePendingStatusButton = new GUIContent( "-", "Remove PendingStatus" );
    }
}
