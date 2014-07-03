Unity-MZ
===========

Unity developing by Mizuki.

**Read me is not up to data**

# Includes

## FastRuntimeSpritesheet

## GameElements

## Utilities

### Common

- MapReduce: Fake map reduce :D, it's just do iterate operation in collection struct.
- DefaultValue: Default value access.
- Enum: Enum support functions.
- MapReduce: Fake map reduce :D, just apply action in collection.
- ProjectTemplate: Template that support make an project have `Save`, `Load` ... etc.
- Verifier: Verifier functions.
 
### UnityExtension

- Debug: Some debug functions, include Log(display function name and message, support c# string formater). use Unity Debug clas.
- CollectionsHelp: 
- DatabaseTypeConvert: Convert value from `object` type (from database) to `system type`.
- EditorGUI: Unity GUI support.
- EditorPart: Help to make Editor on Unity Inspector.
- MapTransform: Fake map action for transform.
- Math: Some useful math functions.
- VectorConvert: Convert Vector3 <-> Vector2. use for 2D game developing.

# Compile `dll` with mono `mcs`

Debug logs compile to dll is very useful to trace on error line or code block, because mono allow to debug project code, not step into framework code.

Before Mono 2.X, it is easy to import `UnityEngine.dll` and `UnityEditor.dll` reference from `/Applications/Unity/Unity.app/Contents/Frameworks/Managed/` to build dll, but fail in 4.01.

Depened on [Using Mono DLLs in a Unity Project](http://docs.unity3d.com/Documentation/Manual/UsingDLL.html) can use command line to do it.

Go to the `src/MZDebug/`, here is the source of `libMZDebug.dll` name `libMZDebug`.

Open command line interface to here, and type  

`mcs -r:/Applications/Unity/Unity.app/Contents/Frameworks/Managed/UnityEngine.dll -target:library libMZDebug`

will generate `libMZDebug.dll`, then put it into `src/MZUtilities/`.

** ATTENTION: ** This method not very well for larger/complex developing ... :(