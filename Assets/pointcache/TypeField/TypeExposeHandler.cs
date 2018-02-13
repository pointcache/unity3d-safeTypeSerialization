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
        public static Dictionary<string, Type> id_enum_dict = new Dictionary<string, Type>();
        public static Dictionary<Type, string> enum_id_dict = new Dictionary<Type, string>();
        public static Dictionary<Type, EnumValuePair> enum_values_dict = new Dictionary<Type, EnumValuePair>();

        public static string[] TypeAttributesIDStringArr;
        public static string[] TypeNamesStringArr;
        public static string[] EnumAttributesIDStringArr;
        public static string[] EnumNamesStringArr;

        public struct EnumValuePair {
            public EnumValuePair(int size) {
                values = new int[size];
                names = new string[size];
            }
            public int[] values;
            public string[] names;
        }



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
                if (type.IsEnum) {
                    if (type.IsDefined(typeof(ExposeTypeAttribute), false)) {
                        var att = type.GetCustomAttributes(typeof(ExposeTypeAttribute), false)[0] as ExposeTypeAttribute;
                        id_enum_dict.Add((string)att.TypeId, type);
                        enum_id_dict.Add(type, (string)att.TypeId);

                        var vals = Enum.GetValues(type) as int[];
                        var names = Enum.GetNames(type);

                        EnumValuePair pair = new EnumValuePair(vals.Length);

                        for (int i = 0; i < vals.Length; i++) {
                            pair.names[i] = names[i];
                            pair.values[i] = vals[i];
                        }

                        enum_values_dict.Add(type, pair);
                    }
                }
                else
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

            EnumAttributesIDStringArr = new string[id_enum_dict.Count];
            EnumNamesStringArr = new string[id_enum_dict.Count];

            count = 0;

            foreach (var pair in id_enum_dict) {
                EnumAttributesIDStringArr[count] = pair.Key;
                EnumNamesStringArr[count] = pair.Value.FullName;
                count++;
            }
        }
    }
}
