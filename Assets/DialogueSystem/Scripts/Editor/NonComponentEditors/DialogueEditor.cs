namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Inspector Editor for the Dialogue
    /// </summary>
    public class DialogueEditor
    {
        /// <summary> Index of the displayed element in the dialogue Foldout </summary>
        private static int dialogueFoldoutDisplay = -1;

        /// <summary>
        /// Displays on the Inspector GUI a Dialogue
        /// </summary>
        /// <param name="dialogue">Dialogue to be displayed</param>
        public static void Display( Dialogue dialogue )
        {
            if (dialogue.Sentences == null)
            {
                dialogue.Sentences = new List<Sentence>();
            }

            List<Sentence> sentences = dialogue.Sentences;
            EditorGUILayout.LabelField( "Dialogue List", EditorStyles.boldLabel );
            EditorGUI.indentLevel++;
            for (int i = 0; i < sentences.Count; i++)
            {
                GUILayout.BeginHorizontal();
                bool display = i == dialogueFoldoutDisplay;
                display = EditorGUILayout.Foldout( display, "Dialogue" + ( i + 1 ) );
                if (GUILayout.Button( EditorButtons.RemoveDialogueButton, EditorStyles.miniButton, EditorButtons.MiniButtonWidth ))
                {
                    sentences.RemoveAt( i );
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
                    SentenceEditor.Display( sentences[i] );
                    EditorGUI.indentLevel--;
                }
            }

            if (GUILayout.Button( EditorButtons.AddDialogueButton, EditorStyles.miniButton, EditorButtons.NormalButtonWidth ))
            {
                Sentence newSentence = new Sentence();
                sentences.Add( newSentence );
            }

            EditorGUI.indentLevel--;
        }
    }
}