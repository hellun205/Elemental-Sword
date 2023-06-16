using Dialogue;
using UnityEditor;
using UnityEngine;

namespace Editor
{
  [CustomPropertyDrawer(typeof(TimingText))]
  public class TimingTextDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      var delay = property.FindPropertyRelative("delay");
      var speed = property.FindPropertyRelative("speed");
      var text = property.FindPropertyRelative("text");
      
      var contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(""));
      
      var tmp = contentPosition.width;

      GUI.skin.label.padding = new RectOffset(3, 3, 6, 6);
      EditorGUIUtility.labelWidth = 10f;
      contentPosition.width = 42f;
      EditorGUI.indentLevel = 0;
      
      EditorGUI.BeginProperty(contentPosition, label, delay);
      {
        EditorGUI.BeginChangeCheck();
        var newVal = EditorGUI.FloatField(contentPosition, new GUIContent("D"), delay.floatValue);
        if (EditorGUI.EndChangeCheck())
          delay.floatValue = newVal;
      }
      
      contentPosition.x += 45f;
      
      EditorGUI.BeginProperty(contentPosition, label, speed);
      {
        EditorGUI.BeginChangeCheck();
        var newVal = EditorGUI.FloatField(contentPosition, new GUIContent("S"), speed.floatValue);
        if (EditorGUI.EndChangeCheck())
          speed.floatValue = newVal;
      }

      contentPosition.x += 45f;
      EditorGUIUtility.labelWidth = 32f;
      contentPosition.width = tmp - 90f;
      
      EditorGUI.BeginProperty(contentPosition, label, text);
      {
        EditorGUI.BeginChangeCheck();
        var newVal = EditorGUI.TextField(contentPosition, new GUIContent(""), text.stringValue);
        if (EditorGUI.EndChangeCheck())
          text.stringValue = newVal;
      }
    }
  }
}