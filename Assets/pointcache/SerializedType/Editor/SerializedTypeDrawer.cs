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

            string[] Typenames = null;
            string[] TypeIDs = null;

            if (attribute != null) {

                SpecifyBaseTypeAttribute att = attribute as SpecifyBaseTypeAttribute;

                List<KeyValuePair<Type, string>> filtered = new List<KeyValuePair<Type, string>>();

                foreach (var item in TypeExposeHandler.type_id_dict) {
                    if (att.Type.IsAssignableFrom(item.Key)) {
                        filtered.Add(item);
                    }
                }
                
                int count = filtered.Count;

                Typenames = new string[count + 1];
                Typenames[0] = "null";

                TypeIDs = new string[count + 1];
                TypeIDs[0] = string.Empty;

                for (int i = 1; i < count + 1; i++) {
                    Typenames[i] = filtered[i - 1].Key.FullName;
                    TypeIDs[i] = filtered[i - 1].Value;
                }

            }
            else {

                Typenames = new string[TypeExposeHandler.TypeNamesStringArr.Length + 1];
                Typenames[0] = "null";

                TypeIDs = new string[TypeExposeHandler.TypeAttributesIDStringArr.Length + 1];
                TypeIDs[0] = string.Empty;

                for (int i = 1; i < Typenames.Length; i++) {

                    Typenames[i] = TypeExposeHandler.TypeNamesStringArr[i - 1];
                    TypeIDs[i] = TypeExposeHandler.TypeAttributesIDStringArr[i - 1];

                }
            }

            SerializedProperty idProp = prop.FindPropertyRelative("_targetID");

            string id = idProp.stringValue;

            int currentIndex = Array.FindIndex(TypeIDs, w => w == id);

            if (currentIndex == -1)
                currentIndex = 0;

            int newIndex = EditorGUI.Popup(pos, currentIndex, Typenames);
            if (newIndex != currentIndex) {
                if (newIndex == 0) {
                    idProp.stringValue = string.Empty;
                }
                else {
                    idProp.stringValue = TypeIDs[newIndex];
                }
            }

            EditorGUI.EndProperty();

        }
    }
}