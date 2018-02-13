namespace pointcache.SerializedType {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class SpecifyBaseTypeAttribute : PropertyAttribute {

        private Type type;

        public SpecifyBaseTypeAttribute(Type type) {
            this.type = type;
        }

        public Type Type
        {
            get {
                return type;
            }
        }
    }

}