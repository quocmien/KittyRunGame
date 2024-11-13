using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(D3SoundManager))]
public class D3SoundManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Texture2D m_Logo = (Texture2D)Resources.Load("Img/D3Icon", typeof(Texture2D));
        GUILayout.Label(m_Logo);
        GUILayout.BeginVertical("GroupBox");
        GUILayout.Space(10f);

        GUILayout.Label("This Script is Controlled by Infinity Runner Engine.\nTo Edit Go to Unity Menu:\nDenvzla Estudio/3D Infinity Runner Engine/Welcome Window", EditorStyles.boldLabel);

        GUILayout.Space(10f);

        GUILayout.EndVertical();


    }
}
