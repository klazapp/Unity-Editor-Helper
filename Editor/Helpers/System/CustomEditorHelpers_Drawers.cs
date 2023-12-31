using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace com.Klazapp.IndoorMap.Editor
{
    //TODO: Complete draw property
    public static partial class CustomEditorHelpers
    {
        #region Public Access
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawHorizontalLine(int space = 0)
        {
            var defaultBgColor = GUI.backgroundColor;
            DrawSpace(space);
            GUI.backgroundColor = Color.white;
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            DrawSpace(space);
            GUI.backgroundColor = defaultBgColor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawSpace(int space)
        {
            EditorGUILayout.Space(space);
        }
     
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawBox(int boxWidth = 0, int boxHeight = 0, Color boxColor = new(), string titleText = "", GUIStyle titleStyle = null, Texture2D iconTexture = null)
        {
            //Store original color
            var originalBgColor = GUI.backgroundColor;

            //Retrieve current skin
            var currentSkin = GUI.skin;
            var currentSkinBoxNormalBg = currentSkin.box.normal.background;
            currentSkin.box.normal.background = Texture2D.whiteTexture;
           
            //Set new color
            GUI.backgroundColor = boxColor;

            var (boxWidthGUILayoutOptions, boxHeightGUILayoutOptions) = GetGUILayoutOptions(boxWidth, boxHeight);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.BeginVertical(currentSkin.box, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            EditorGUILayout.BeginHorizontal();

            titleStyle ??= new GUIStyle();
            
            if (iconTexture != null)
            {
                var titleWithIconGUI = new GUIContent
                {       
                    image = iconTexture,
                
                    //Add spacing betwixt texture and text
                    text = "     " + titleText,
                };
                
                GUILayout.Label(titleWithIconGUI, titleStyle, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            }
            else
            {
                GUILayout.Label(titleText, titleStyle, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            }
       
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            //Restore original color and skin
            GUI.backgroundColor = originalBgColor;
            currentSkin.box.normal.background = currentSkinBoxNormalBg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawProperty(SerializedProperty prop, int boxWidth = 0, int boxHeight = 0, bool readOnly = false)
        {
            if (readOnly)
            {
                GUI.enabled = false;
            }
            
            var (boxWidthGUILayoutOptions, boxHeightGUILayoutOptions) = GetGUILayoutOptions(boxWidth, boxHeight);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical(GUI.skin.box, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);

            DrawProperty(prop, readOnly);
          
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            if (readOnly)
            {
                GUI.enabled = true;
            }
        }
        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawProperty(SerializedProperty prop, bool readOnly = false)
        {
            if (readOnly)
            {
                GUI.enabled = false;
            }
            
            var propType = prop.propertyType;
            switch (propType)
            {
                case SerializedPropertyType.Generic:
                    var isArray = prop.isArray;

                    if (isArray)
                    {
                        EditorGUILayout.PropertyField(prop, true);
                    }
                    else
                    {
                        var val = prop.boxedValue;
                        if (val is float3)
                        {
                            EditorGUILayout.Vector3Field("", prop.GetFloat3AsVector());
                        }
                        else if (val is quaternion)
                        {
                            EditorGUILayout.Vector4Field("", prop.GetQuaternionAsVector());
                        }
                        else if (val is Quaternion)
                        {
                            EditorGUILayout.Vector4Field("", prop.GetQuaternionAsVector());
                        }
                        else
                        {
                            EditorGUILayout.PropertyField(prop, true);
                        }
                    }
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Boolean:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Float:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.String:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Color:
                    EditorGUILayout.ColorField(prop.colorValue);
                    break;
                case SerializedPropertyType.ObjectReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.LayerMask:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Enum:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector2:
                    EditorGUILayout.Vector2Field("", prop.vector2Value);
                    break;
                case SerializedPropertyType.Vector3:
                    EditorGUILayout.Vector3Field("", prop.vector3Value);
                    break;
                case SerializedPropertyType.Vector4:
                    EditorGUILayout.Vector4Field("", prop.vector4Value);
                    break;
                case SerializedPropertyType.Rect:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ArraySize:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Character:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.AnimationCurve:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Bounds:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Gradient:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Quaternion:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ExposedReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector2Int:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector3Int:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.RectInt:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.BoundsInt:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ManagedReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Hash128:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (readOnly)
            {
                GUI.enabled = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (GUILayoutOption widthOption, GUILayoutOption heightOption) GetGUILayoutOptions(int width, int height)
        {
            var widthGUILayoutOptions = width == 0 ? GUILayout.ExpandWidth(true) : GUILayout.Width(width);
            var heightGUILayoutOptions = height == 0 ? GUILayout.ExpandHeight(true) : GUILayout.Height(height);

            return (widthGUILayoutOptions, heightGUILayoutOptions);
        }
    }
}