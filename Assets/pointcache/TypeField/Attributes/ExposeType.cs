namespace pointcache.TypeField {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ExposeTypeAttribute : System.Attribute {

        private string uid;

        public ExposeTypeAttribute(string uid) {
            this.uid = uid;
        }

        public override object TypeId
        {
            get {
                return uid;
            }
        }
    }

}