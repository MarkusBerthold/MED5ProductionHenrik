PersistentAttribute is a C# attribute which allows you to force fields and properties values to persist when the game closes, the values are loaded before start function and are saved during runtime using both PlayerPrefs and Json.

Main features:
	-No need for saving and loading values manually
	-Support for all primitive types(float, int, bool etc.)
	-Support for most of unity built-in types(Vector2, Vector3, Color, Rect etc.)
	-Support your own classes and structs if they are marked as Serializable
	-Easy to use, just add [Persistent] before your field or property declaration and they will be automaticaly saved
	-Uses PlayerPrefs and Json when storing the values, so it should work fine on all platforms supported by unity

Unfortunately, the current version doesn't support array and lists, this will probably be fixed in future versions

Since it uses JsonUtility class and it was first introduced in Unity 5.3, any version older than 5.3 will only store primitive types supported by PlayerPrefs (float, int, string and bool)