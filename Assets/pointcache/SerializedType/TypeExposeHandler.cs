namespace pointcache.TypeField {

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public class TypeExposeHandler {

        private const string TYPEFIELD = "TypeField |";

        public static Dictionary<string, Type> id_type_dict = new Dictionary<string, Type>();
        public static Dictionary<Type, string> type_id_dict = new Dictionary<Type, string>();

        public static string[] TypeAttributesIDStringArr;
        public static string[] TypeNamesStringArr;

        public static Type GetTypeByID(string id) {
            Type type;
            if (!id_type_dict.TryGetValue(id, out type)) {
                Debug.LogError(TYPEFIELD + " Upon deserialization, a serialized type with id: " + id + " , was not located in the codebase. Did you delete or change the TypeID attribute?");
                return null;
            }
            return type;
        }

        static TypeExposeHandler() {
            Assembly asmb = null;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies) {

                if (assembly.FullName.Contains("Assembly-CSharp")) {
                    if (!assembly.FullName.Contains("Assembly-CSharp-firstpass") &&
                        !assembly.FullName.Contains("Assembly-CSharp-Editor")) {
                        asmb = assembly;
                        break;
                    }
                }
            }

            if (asmb == null) {
                Debug.LogError(TYPEFIELD + "Could not locate main assembly, what happened? This results in a broken TypeIDIndex object.");
                return;
            }

            Type[] allTypes = asmb.GetTypes();

            foreach (var type in allTypes) {
                if (type.IsDefined(typeof(ExposeTypeAttribute), false)) {
                    var att = type.GetCustomAttributes(typeof(ExposeTypeAttribute), false)[0] as ExposeTypeAttribute;
                    id_type_dict.Add((string)att.TypeId, type);
                    type_id_dict.Add(type, (string)att.TypeId);
                }
            }

            TypeAttributesIDStringArr = new string[id_type_dict.Count];
            TypeNamesStringArr = new string[id_type_dict.Count];

            int count = 0;

            foreach (var pair in id_type_dict) {
                TypeAttributesIDStringArr[count] = pair.Key;
                TypeNamesStringArr[count] = pair.Value.FullName;
                count++;
            }
        }
    }
}
