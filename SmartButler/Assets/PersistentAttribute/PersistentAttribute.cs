// Persistent Attribute
// Version 1.0.0
// Samuel Schultze
// samuelschultze@gmail.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Force the field or property value to persist when the program closes, the member needs to be static for this to work
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class PersistentAttribute : Attribute {

    /// <summary>
    /// Force the field or property value to persist when the program closes, the member needs to be static for this to work
    /// </summary>
    public PersistentAttribute() {
    }

    /// <summary>
    /// Force the field or property value to persist when the program closes, the member needs to be static for this to work
    /// </summary>
    /// <param name="key">Custom key to save the member on PlayerPrefs, default is class name + member name</param>
    public PersistentAttribute(string key) {
        this.key = key;
    }

    /// <summary>
    /// Custom key to save the member on PlayerPrefs, default is class name + member name
    /// </summary>
    public string key { get; set; }

    /// <summary>
    /// The value that will be used when none is set yet
    /// </summary>
    public object defaultValue { get; set; }

    private const BindingFlags BINDING = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    private static Dictionary<MemberInfo, PersistentAttribute> members;

    [RuntimeInitializeOnLoadMethod]
    private static void Init() {
        LoadMembers();

        foreach(var member in members)
            try {
                var field = member.Key as FieldInfo;
                var property = member.Key as PropertyInfo;
                var type = member.Key is FieldInfo ? field.FieldType : property.PropertyType;
                var value = null as object;

                if(string.IsNullOrEmpty(member.Value.key)) {
                    member.Value.key = string.Format("{0}.{1}", member.Key.DeclaringType, member.Key.Name);
                }

                if((member.Value.defaultValue == null || member.Value.defaultValue.GetType() != type) && member.Key is FieldInfo) {
                    member.Value.defaultValue = field.GetValue(null);
                }

                if(member.Value.defaultValue != null && member.Value.defaultValue.GetType() != type) {
                    value = Get(member.Value.key, null, type);
                }
                else {
                    value = Get(member.Value.key, member.Value.defaultValue, type);
                }

                if(member.Key is FieldInfo) {
                    field.SetValue(null, value);
                }
                else {
                    property.GetSetMethod(true).Invoke(null, new object[] { value });
                }
            }
            catch(Exception e) {
                Debug.LogException(e);
            }

        StartSaving();
    }

    private static void LoadMembers() {
        members = new Dictionary<MemberInfo, PersistentAttribute>();

        var reflectedMembers = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                from type in assembly.GetTypes()
                                where type.IsSubclassOf(typeof(MonoBehaviour))
                                from member in type.GetMembers(BINDING)
                                where member.IsDefined(typeof(PersistentAttribute), false)
                                where ValidateMember(member)
                                select member);

        foreach(var member in reflectedMembers) {
            members.Add(member, (PersistentAttribute)member.GetCustomAttributes(typeof(PersistentAttribute), false)[0]);
        }
    }

    private static bool ValidateMember(MemberInfo member) {
        var field = member as FieldInfo;
        var property = member as PropertyInfo;
        var isField = member is FieldInfo;
        var isProperty = member is PropertyInfo;
        var name = string.Format("{0}.{1}", member.DeclaringType, member.Name);
        var type = isField ? field.FieldType : property.PropertyType;

        if(!isField && !isProperty) {
            return false;
        }

        else if(isField && field.IsInitOnly) {
            Debug.LogWarningFormat("The field '{0}' is readonly, that's not allowed for persistence", name);
            return false;
        }
        else if(isProperty && (!property.CanRead || !property.CanWrite)) {
            Debug.LogWarningFormat("The property '{0}' don't have an {1} accessor, that's not allowed for persistence", name, !property.CanRead ? "getter" : "setter");
            return false;
        }
        else if((isField && !field.IsStatic) || (isProperty && !property.GetGetMethod(true).IsStatic)) {
            Debug.LogWarningFormat("The field or property '{0}' is not static, that's not allowed for persistence", name);
            return false;
        }
        else if(!IsSupportedForSave(type)) {
            Debug.LogWarningFormat("'{0}' is not supported for persistence, on {1}", type, name);
            return false;
        }
        else {
            return true;
        }
    }

    private static bool IsSupportedForSave(Type type) {
        return type.IsDefined(typeof(SerializableAttribute), true);
    }

    private static void StartSaving() {
        var obj = GameObject.Find("PersistenceCoroutineStarter") ?? new GameObject("PersistenceCoroutineStarter");
        var comp = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();

        obj.hideFlags = HideFlags.HideAndDontSave;

        comp.StopAllCoroutines();
        comp.StartCoroutine(SaveAsync());
    }

    private static IEnumerator SaveAsync() {
#if UNITY_EDITOR
        while(UnityEditor.EditorApplication.isPlaying) {
#else
        while(true) {
#endif
            yield return new WaitForEndOfFrame();
            foreach(var member in members) {
                try {
                    if(member.Key is FieldInfo) {
                        var field = member.Key as FieldInfo;
                        var value = field.GetValue(null);

                        Set(member.Value.key, value, field.FieldType);
                    }
                    else {
                        var property = member.Key as PropertyInfo;
                        var value = property.GetGetMethod(true).Invoke(null, null);

                        Set(member.Value.key, value, property.PropertyType);
                    }
                }
                catch(Exception e) {
                    Debug.LogWarning(e);
                };
            }
        }
    }

    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void Set(string key, object value, Type type) {
        if(type == typeof(string)) {
            PlayerPrefs.SetString(key, (string)value);
        }
        else if(type == typeof(int)) {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if(type == typeof(float)) {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if(type == typeof(bool)) {
            PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
        }
#if UNITY_5_3_OR_NEWER
        else {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
        }
#endif
    }

    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    /// <param name="defaultValue">The value that will be returned if the key is not set</param>
    public static object Get(string key, object defaultValue, Type type) {
        if(!PlayerPrefs.HasKey(key)) {
            return defaultValue;
        }

        if(type == typeof(string)) {
            return PlayerPrefs.GetString(key);
        }
        else if(type == typeof(int)) {
            return PlayerPrefs.GetInt(key);
        }
        else if(type == typeof(float)) {
            return PlayerPrefs.GetFloat(key);
        }
        else if(type == typeof(bool)) {
            return PlayerPrefs.GetInt(key) == 1;
        }
        else {
#if UNITY_5_3_OR_NEWER
            return JsonUtility.FromJson(PlayerPrefs.GetString(key), type);
#else
            Debug.LogWarning("JsonUtility isn't available in unity versions older than 5.3, consider using a newer version if you want PersistentAttribute to work properly");

            if(defaultValue != null) {
                return defaultValue;
            }
            if(type.IsValueType) {
                return Activator.CreateInstance(type);
            }
            else {
                return null;
            }
#endif
        }
    }

}