// Copyright (c) ATHellboy (Alireza Tarahomi) Limited. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjectDropdown.Extension;

namespace ScriptableObjectDropdown.Editor
{
    // TODO: Mixed value (-) for selecting multi objects
    // TODO: Don't clear the ScriptableObjects list when it is unselected
    [CustomPropertyDrawer(typeof(ScriptableObjectDropdownAttribute))]
    [CustomPropertyDrawer(typeof(ScriptableObjectReference))]
    public class ScriptableObjectDropdownDrawer : PropertyDrawer
    {
        private readonly GenericMenu.MenuFunction2 _onSelectedScriptableObject;
        private readonly int _controlHint = typeof(ScriptableObjectDropdownAttribute).GetHashCode();

        private ScriptableObject _selectedScriptableObject;
        private List<ScriptableObject> _scriptableObjects = new List<ScriptableObject>();
        private GUIContent _popupContent = new GUIContent();
        private int _selectedControlID;
        private bool _isChanged;

        public ScriptableObjectDropdownDrawer()
        {
            _onSelectedScriptableObject = OnSelectedScriptableObject;

            EditorApplication.projectChanged += ClearCache;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ScriptableObjectDropdownAttribute castedAttribute = attribute as ScriptableObjectDropdownAttribute;
            if (_scriptableObjects.Count == 0)
            {
                GetScriptableObjects(castedAttribute);
            }

            Draw(position, label, property, castedAttribute);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorStyles.popup.CalcHeight(GUIContent.none, 0);
        }

        /// <summary>
        /// How you can get type of field which it uses PropertyAttribute
        /// </summary>
        private Type GetPropertyType(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = parentType.GetFieldViaPath(property.propertyPath);
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }
            return null;
        }

        private bool ValidateAttribute(ScriptableObjectDropdownAttribute attribute)
        {
            if (attribute.BaseType.IsInterface)
            {
                return true;
            }

            if (attribute.BaseType.IsSubclassOf(typeof(ScriptableObject)))
            {
                return true;
            }
            return false;
        }

        private bool ValidateProperty(SerializedProperty property)
        {
            Type propertyType = GetPropertyType(property);
            if (propertyType == null || propertyType != typeof(ScriptableObjectReference))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// When new ScriptableObject added to the project
        /// </summary>
        private void ClearCache()
        {
            _scriptableObjects.Clear();
        }

        /// <summary>
        /// Gets ScriptableObjects just when it is selected or new ScriptableObject added to the project
        /// </summary>
        private void GetScriptableObjects(ScriptableObjectDropdownAttribute attribute)
        {
            if (attribute.BaseType.IsClass)
            {
                string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", attribute.BaseType));
                for (int i = 0; i < guids.Length; i++)
                {
                    _scriptableObjects.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), attribute.BaseType) as ScriptableObject);
                }
            }

            if (attribute.BaseType.IsInterface)
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => attribute.BaseType.IsAssignableFrom(p));

                foreach (Type type in types)
                {
                    string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", type));
                    for (int i = 0; i < guids.Length; i++)
                    {
                        _scriptableObjects.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), attribute.BaseType) as ScriptableObject);
                    }
                }
            }
        }

        private void Draw(Rect position, GUIContent label,
            SerializedProperty property, ScriptableObjectDropdownAttribute attribute)
        {
            if (label != null && label != GUIContent.none)
                position = EditorGUI.PrefixLabel(position, label);

            if (!ValidateAttribute(attribute))
            {
                EditorGUI.LabelField(position, "PropertyAttribute baseType does not inherit ScriptableObject");
                return;
            }

            if (!ValidateProperty(property))
            {
                EditorGUI.LabelField(position, "Use it with ScriptableObjectReference");
                return;
            }

            if (_scriptableObjects.Count == 0)
            {
                EditorGUI.LabelField(position, "This type asset does not exist in the project");
                return;
            }

            UpdateScriptableObjectSelectionControl(position, label, property, attribute);
        }

        private void UpdateScriptableObjectSelectionControl(Rect position, GUIContent label,
            SerializedProperty property, ScriptableObjectDropdownAttribute attribute)
        {
            SerializedProperty value = property.FindPropertyRelative("value");
            ScriptableObject output = DrawScriptableObjectSelectionControl(position, label,
                value.objectReferenceValue as ScriptableObject, attribute);

            if (_isChanged)
            {
                _isChanged = false;
                value.objectReferenceValue = output;
            }
        }

        private ScriptableObject DrawScriptableObjectSelectionControl(Rect position, GUIContent label,
            ScriptableObject scriptableObject, ScriptableObjectDropdownAttribute attribute)
        {
            bool triggerDropDown = false;
            int controlID = GUIUtility.GetControlID(_controlHint, FocusType.Keyboard, position);

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.ExecuteCommand:
                    if (Event.current.commandName == "ScriptableObjectReferenceUpdated")
                    {
                        if (_selectedControlID == controlID)
                        {
                            if (scriptableObject != _selectedScriptableObject)
                            {
                                scriptableObject = _selectedScriptableObject;
                                _isChanged = true;
                            }

                            _selectedControlID = 0;
                            _selectedScriptableObject = null;
                        }
                    }
                    break;

                case EventType.MouseDown:
                    if (GUI.enabled && position.Contains(Event.current.mousePosition))
                    {
                        GUIUtility.keyboardControl = controlID;
                        triggerDropDown = true;
                        Event.current.Use();
                    }
                    break;

                case EventType.KeyDown:
                    if (GUI.enabled && GUIUtility.keyboardControl == controlID)
                    {
                        if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Space)
                        {
                            triggerDropDown = true;
                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.Repaint:
                    if (scriptableObject == null)
                    {
                        _popupContent.text = "Nothing";
                    }
                    else
                    {
                        _popupContent.text = scriptableObject.name;
                    }
                    EditorStyles.popup.Draw(position, _popupContent, controlID);
                    break;
            }

            if (_scriptableObjects.Count != 0 && triggerDropDown)
            {
                _selectedControlID = controlID;
                _selectedScriptableObject = scriptableObject;

                DisplayDropDown(position, scriptableObject, attribute.grouping);
            }

            return scriptableObject;
        }

        private void DisplayDropDown(Rect position, ScriptableObject selectedScriptableObject, ScriptableObjectGrouping grouping)
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("Nothing"), selectedScriptableObject == null, _onSelectedScriptableObject, null);
            menu.AddSeparator("");

            for (int i = 0; i < _scriptableObjects.Count; ++i)
            {
                var scriptableObject = _scriptableObjects[i];

                string menuLabel = MakeDropDownGroup(scriptableObject, grouping);
                if (string.IsNullOrEmpty(menuLabel))
                    continue;

                var content = new GUIContent(menuLabel);
                menu.AddItem(content, scriptableObject == selectedScriptableObject, _onSelectedScriptableObject, scriptableObject);
            }

            menu.DropDown(position);
        }

        private void OnSelectedScriptableObject(object userData)
        {
            _selectedScriptableObject = userData as ScriptableObject;
            var scriptableObjectReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("ScriptableObjectReferenceUpdated");
            EditorWindow.focusedWindow.SendEvent(scriptableObjectReferenceUpdatedEvent);
        }

        private string FindScriptableObjectFolderPath(ScriptableObject scriptableObject)
        {
            string path = AssetDatabase.GetAssetPath(scriptableObject);
            path = path.Replace("Assets/", "");
            path = path.Replace(".asset", "");

            return path;
        }

        private string MakeDropDownGroup(ScriptableObject scriptableObject, ScriptableObjectGrouping grouping)
        {
            string path = FindScriptableObjectFolderPath(scriptableObject);

            switch (grouping)
            {
                default:
                case ScriptableObjectGrouping.None:
                    path = path.Replace("/", " > ");
                    return path;

                case ScriptableObjectGrouping.ByFolder:
                    return path;

                case ScriptableObjectGrouping.ByFolderFlat:
                    int last = path.LastIndexOf('/');
                    string part1 = path.Substring(0, last);
                    string part2 = path.Substring(last);
                    path = part1.Replace("/", " > ") + part2;
                    return path;
            }
        }
    }
}