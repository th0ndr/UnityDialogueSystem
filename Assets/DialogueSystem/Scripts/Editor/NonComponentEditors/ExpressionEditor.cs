namespace DialogueManager.InspectorEditors
{
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Inspector Editor for the Conversation Status List
    /// </summary>
    public class ExpressionEditor
    {
        /// <summary> Index of the displayed element in the expression List </summary>
        private static int expressionFoldoutDisplay = -1;

        /// <summary>
        /// Displays on the Inspector GUI a List of Expressions
        /// </summary>
        /// <param name="expressions">Expression List</param>
        public static void Display(List<Expression> expressions)
        {
            for (int i = 0; i < expressions.Count; i++)
            {
                bool display = i == expressionFoldoutDisplay;
                display = EditorGUILayout.Foldout( display, expressions[i].Name );
                if (!display && i == expressionFoldoutDisplay)
                {
                    expressionFoldoutDisplay = -1;
                }

                if (display)
                {
                    expressionFoldoutDisplay = i;
                    EditorGUILayout.BeginVertical( GUI.skin.box );
                    GUILayout.BeginHorizontal();
                    expressions[i].Name = EditorGUILayout.TextField( "Expression Name", expressions[i].Name );
                    if (GUILayout.Button( EditorButtons.RemoveExpressionButton, EditorStyles.miniButton, EditorButtons.MiniButtonWidth ))
                    {
                        expressions.RemoveAt( i );
                        expressionFoldoutDisplay = -1;
                        return;
                    }

                    GUILayout.EndHorizontal();
                    expressions[i].Image = EditorGUILayout.ObjectField( "Image", expressions[i].Image, typeof( Sprite ), true ) as Sprite;

                    EditorGUILayout.EndVertical();
                }
            }
        }
    }
}