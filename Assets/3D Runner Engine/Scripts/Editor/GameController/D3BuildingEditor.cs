using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(D3Building))]

public class D3BuildingEditor : Editor
{
	D3WorldCurver GameW;
	int CurvatureID;
	int DistanceID;
	public static void SetSceneViewGizmos(bool gizmosOn)
	{
#if UNITY_EDITOR
		UnityEditor.SceneView sv = UnityEditor.EditorWindow.GetWindow<UnityEditor.SceneView>();
		sv.drawGizmos = gizmosOn;
#endif
	}

	private void OnEnable()
	{
		if (FindObjectOfType<D3WorldCurver>())
		{
            GameW = FindObjectOfType<D3WorldCurver>();

            SetSceneViewGizmos(true);

            CurvatureID = Shader.PropertyToID("_Curvature");
            DistanceID = Shader.PropertyToID("_Distance");

        }
		
	}

    public override void OnInspectorGUI()
    {
		GUILayout.BeginVertical("GroupBox");

		EditorGUILayout.TextArea("Attention Editing the Asset with the curve is difficult, disable it and enable it when necessary", GUI.skin.GetStyle("HelpBox"));
		GUILayout.Space(10f);

		if (GUILayout.Button("Disbled Curve"))
		{

			Vector3 curvature2 = GameW.CurvatureScaleUnit == 0 ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 0f, 0f) / GameW.CurvatureScaleUnit;

			Shader.SetGlobalVector(CurvatureID, curvature2);
			Shader.SetGlobalFloat(DistanceID, 0);

		}

		GUILayout.EndVertical();

		GUILayout.BeginVertical("GroupBox", GUILayout.ExpandWidth(true));

		GUILayout.Label("Building ", EditorStyles.boldLabel);
		GUILayout.Label("Gray Zone: Do not decorate, zone reserved for the road.", EditorStyles.boldLabel);
		GUILayout.Label("Green Zone: Buildings or Decoration, place your design here.", EditorStyles.boldLabel);

		GUILayout.EndVertical();

	}

}