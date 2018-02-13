namespace pointcache.SerializedType {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class SerializedType {

        [SerializeField, HideInInspector]
        private string _targetID;

        private Type type { get; set; }
        public Type Value
        {
            get {
                if (type == null) {
                    type = TypeExposeHandler.GetTypeByID(_targetID);
                }
                return type;
            }
        }

        public static implicit operator Type(SerializedType var) {
            return var.Value;
        }

    }
}