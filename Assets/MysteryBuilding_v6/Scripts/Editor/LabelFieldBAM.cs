using UnityEditor;
using UnityEngine;

using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
public class LabelFieldBAM : EditorWindow
// Shows a label in the editor with the seconds since the editor started



{

    AnimBool m_ShowExtraFields;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;


   // [MenuItem("Examples/Editor GUILayout Label Usage")]
    static void Init()
    {
        LabelFieldBAM window = (LabelFieldBAM)EditorWindow.GetWindow(typeof(LabelFieldBAM), true, "My Empty Window");
        window.Show();
    }

    void OnEnable()
    {
        m_ShowExtraFields = new AnimBool(true);
        m_ShowExtraFields.valueChanged.AddListener(new UnityAction(base.Repaint));
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Time since start: ",
            EditorApplication.timeSinceStartup.ToString());
        this.Repaint();


        m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_ShowExtraFields.target);

        //Extra block that can be toggled on and off.
        using (var group = new EditorGUILayout.FadeGroupScope(m_ShowExtraFields.faded))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Color");
                m_Color = EditorGUILayout.ColorField(m_Color);
                EditorGUILayout.PrefixLabel("Text");
                m_String = EditorGUILayout.TextField(m_String);
                EditorGUILayout.PrefixLabel("Number");
                m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
                EditorGUI.indentLevel--;
            }
        }
    }



}





