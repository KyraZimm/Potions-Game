using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//modified from this page:
//https://coderwall.com/p/wfy-fa/show-a-popup-field-to-serialize-string-or-integer-values-from-a-list-of-choices-in-unity3d

public class WritableDropdownAttribute : PropertyAttribute {
    public delegate string[] GetOptionsList();

    public WritableDropdownAttribute(string[] list) {
        List = list;
    }

    public WritableDropdownAttribute(Type type, string methodName) {
        var method = type.GetMethod(methodName);
        if (method != null)
            List = method.Invoke(null, null) as string[];
        else
            Debug.LogError($"Could not find method {methodName} for {type}.");
    }

    public string[] List { get; private set; }
}