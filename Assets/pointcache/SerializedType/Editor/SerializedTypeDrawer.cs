namespace pointcache.SerializedType {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;


    [CustomPropertyDrawer(typeof(SerializedType))]
    [CustomPropertyDrawer(typeof(SpecifyBaseTypeAttribute), true)]
    public class SerializedTypeDrawer : PropertyDrawer {

        private const int kLineHeight = 16;
        private const int LeftOffset = 20;
        private const int LabelSize = 120;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {


            EditorGUI.BeginProperty(pos, label, prop);

            if (label != null && label != GUIContent.none) {
                pos = EditorGUI.PrefixLabel(pos, label);
            }

            string[] Typenames = TypeExposeHandler.TypeNamesStringArr;
            if (attribute != null) {
                SpecifyBaseTypeAttribute att = attribute as SpecifyBaseTypeAttribute;
                Typenames = TypeExposeHandler.type_id_dict.Keys
                    .Where(x => att.Type.IsAssignableFrom(x))
                    .Select(x => x.Name)
                    .ToArray();

            }

            string[] withNull = new string[Typenames.Length + 1];
            withNull[0] = "null";
            for (int i = 1; i < withNull.Length; i++) {
                withNull[i] = Typenames[i - 1];
            }

            SerializedProperty idProp = prop.FindPropertyRelative("_targetID");

            string id = idProp.stringValue;

            int currentIndex = Array.FindIndex(TypeExposeHandler.TypeAttributesIDStringArr, w => w == id);

            if (currentIndex == -1)
                currentIndex = 0;

            int newIndex = EditorGUI.Popup(pos, currentIndex, withNull);
            if (newIndex != currentIndex) {
                if (newIndex == 0) {
                    idProp.stringValue = string.Empty;
                }
                else {
                    idProp.stringValue = TypeExposeHandler.TypeAttributesIDStringArr[newIndex];
                }
            }

            EditorGUI.EndProperty();

        }
    }
}