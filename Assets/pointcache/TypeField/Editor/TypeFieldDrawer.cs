namespace pointcache.TypeField {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;


    [CustomPropertyDrawer(typeof(TypeField))]
    public class TypeFieldDrawer : PropertyDrawer {

        private const int kLineHeight = 16;
        private const int LeftOffset = 20;
        private const int LabelSize = 120;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {

            EditorGUI.BeginProperty(pos, label, prop);

            if (label != null && label != GUIContent.none) {
                pos = EditorGUI.PrefixLabel(pos, label);
            }

            SerializedProperty idProp = prop.FindPropertyRelative("_targetID");
            string id = idProp.stringValue;

            int currentIndex = Array.FindIndex(TypeExposeHandler.TypeAttributesIDStringArr, w => w == id);

            int newIndex = EditorGUI.Popup(pos, currentIndex, TypeExposeHandler.TypeNamesStringArr);
            if (newIndex != currentIndex) {
                idProp.stringValue = TypeExposeHandler.TypeAttributesIDStringArr[newIndex];
            }

            EditorGUI.EndProperty();

        }
    }
}