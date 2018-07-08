namespace DialogueManager.Editors
{
    using UnityEngine;
    using System.Collections;
    using System.Linq;
    using UnityEditor;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using System.Collections.Generic;

    [CustomEditor(typeof(ConversationComponent))]
    public class ConversationEditor : Editor
    {
        private static GUILayoutOption miniButtonWidth = GUILayout.Width(24f);
        private static GUILayoutOption normalButtonWidth = GUILayout.Width(85f);

        private static GUIContent addStatusButton = new GUIContent("Add Status", "Add Status");
        private static GUIContent removeStatusButton = new GUIContent("-", "Remove Status");
        private static GUIContent addDialogueButton = new GUIContent("Add Dialogue", "Add Dialogue");
        private static GUIContent removeDialogueButton = new GUIContent("-", "Remove Dialogue");
        private static GUIContent addConversationButton = new GUIContent("Add Conversation", "Add Conversation");
        private static GUIContent removeConversationButton = new GUIContent("-", "Remove Conversation");
        private static GUIContent addPendingStatusButton = new GUIContent("Add", "Add PendingStatus");
        private static GUIContent removePendingStatusButton = new GUIContent("-", "Remove PendingStatus");

        private SerializedProperty nameProperty;
        private SerializedProperty statusProperty;
        private int foldoutDisplay = -1;
        private int dialogueFoldoutDisplay = -1;
        private int newConversationsFoldoutDisplay = -1;

        private bool dialogueFoldout;
        void OnEnable()
        {
            nameProperty = serializedObject.FindProperty("Model.Name");
            statusProperty = serializedObject.FindProperty("Model.Status");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ConversationComponent conversationComponent = (ConversationComponent)target;

            EditorGUILayout.PropertyField(nameProperty, true);
            this.DisplayStatusFields(conversationComponent.Model);
            //EditorGUILayout.PropertyField(statusProperty, true);

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayStatusFields(Conversation conversation)
        {
            if(conversation.Status == null)
            {
                conversation.Status = new List<ConversationStatus>();
            }

            if (conversation.Status.Count > 0)
            {
                string[] statusListNames = conversation.Status.Select(s => s.ToString()).ToArray();
                conversation.ActiveStatusIndex = EditorGUILayout.Popup(
                    "Active Status",
                    conversation.ActiveStatusIndex,
                    statusListNames,
                    EditorStyles.popup);
                conversation.ActiveStatus = conversation.Status[conversation.ActiveStatusIndex];

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Status List", EditorStyles.boldLabel);
                this.DisplayStatusElement(conversation, statusListNames);
            }
            if (GUILayout.Button(addStatusButton, EditorStyles.miniButton, normalButtonWidth))
            {
                ConversationStatus newStatus = new ConversationStatus();
                newStatus.Name = "Status " + (conversation.Status.Count + 1);
                conversation.Status.Add(newStatus);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        private void DisplayStatusElement(Conversation conversation, string[] statusListNames)
        {
            List<ConversationStatus> status = conversation.Status;
            for (int i = 0; i < status.Count; i++)
            {

                bool display = i == foldoutDisplay;
                display = EditorGUILayout.Foldout(display, status[i].Name);
                if (!display && i == foldoutDisplay)
                {
                    foldoutDisplay = -1;
                }
                if (display)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    // NAME AND REMOVE BUTTON
                    foldoutDisplay = i;
                    GUILayout.BeginHorizontal();
                    status[i].Name = EditorGUILayout.TextField("Status Name", status[i].Name);
                    if (GUILayout.Button(removeStatusButton, EditorStyles.miniButton, miniButtonWidth))
                    {
                        conversation.Status.RemoveAt(i);
                        foldoutDisplay = -1;
                        break;
                    }
                    GUILayout.EndHorizontal();

                    // NEXT STATUS
                    if (status[i].NextStatusIndex >= status.Count)
                    {
                        status[i].NextStatusIndex = 0;
                    }
                    status[i].NextStatusIndex = EditorGUILayout.Popup(
                        "Next Status",
                        status[i].NextStatusIndex,
                        statusListNames,
                        EditorStyles.popup);
                    status[i].NextStatus = status[status[i].NextStatusIndex];

                    // DIALOGUE
                    this.DisplayDialogues(status[i].Dialogue);

                    // NEW CONVERSATIONS
                    this.DisplayNewConversations(status[i].NewConversations);

                    EditorGUILayout.EndVertical();
                }

            }
        }

        private void DisplayDialogues(List<Sentence> dialogue)
        {
            if(dialogue == null)
            {
                dialogue = new List<Sentence>();
            }

            EditorGUILayout.LabelField("Dialogue List", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            for (int i = 0; i < dialogue.Count; i++)
            {

                GUILayout.BeginHorizontal();
                bool display = i == dialogueFoldoutDisplay;
                display = EditorGUILayout.Foldout(display, "Dialogue" + (i+1));
                if (GUILayout.Button(removeStatusButton, EditorStyles.miniButton, miniButtonWidth))
                {
                    dialogue.RemoveAt(i);
                    dialogueFoldoutDisplay = -1;
                    break;
                }
                GUILayout.EndHorizontal();
                if (!display && i == dialogueFoldoutDisplay)
                {
                    dialogueFoldoutDisplay = -1;
                }
                if (display)
                {
                    dialogueFoldoutDisplay = i;
                    EditorGUI.indentLevel++;
                    dialogue[i].Character = EditorGUILayout.ObjectField("Character", dialogue[i].Character, typeof(Character), true) as Character;

                    if ( dialogue[i].Character != null )
                    {
                        string[] expressionListNames = dialogue[i].Character.expressions.Select( e => e.Name ).ToArray();
                        dialogue[i].ExpressionIndex = EditorGUILayout.Popup(
                            "Expression",
                            dialogue[i].ExpressionIndex,
                            expressionListNames,
                            EditorStyles.popup );
                        EditorGUILayout.LabelField( "Paragraph:" );
                        EditorGUI.indentLevel++;
                        dialogue[i].Paragraph = EditorGUILayout.TextArea( dialogue[i].Paragraph, GUILayout.MaxHeight( 75 ) );
                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUI.indentLevel--;
                }
            }

            if (GUILayout.Button(addDialogueButton, EditorStyles.miniButton, normalButtonWidth))
            {
                Sentence newSentence = new Sentence();
                dialogue.Add(newSentence);
            }
            EditorGUI.indentLevel--;
        }

        private void DisplayNewConversations(List<PendingStatus> conversations)
        {
            if(conversations == null)
            {
                conversations = new List<PendingStatus>();
            }
            EditorGUILayout.LabelField("New Conversations", EditorStyles.boldLabel);

            for (int i = 0; i < conversations.Count; i++)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginHorizontal();
                bool display = i == newConversationsFoldoutDisplay;
                display = EditorGUILayout.Foldout(display, conversations[i].ConversationName);
                if (GUILayout.Button(removeStatusButton, EditorStyles.miniButton, miniButtonWidth))
                {
                    conversations.RemoveAt(i);
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
                    conversations[i].ConversationName = EditorGUILayout.TextField("Conversation", conversations[i].ConversationName);
                    conversations[i].StatusName = EditorGUILayout.TextField("Status", conversations[i].StatusName);
                    conversations[i].Importance = EditorGUILayout.IntField("Importance", conversations[i].Importance);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }

            if (GUILayout.Button(addPendingStatusButton, EditorStyles.miniButton, normalButtonWidth))
            {
                PendingStatus pendingStatus = new PendingStatus();
                conversations.Add(pendingStatus);
            }
        }
    }
}