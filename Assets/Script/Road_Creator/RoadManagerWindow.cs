using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor;
using UnityEngine;
using static Road_Script;

public class RoadManagerWindow : EditorWindow
{
    [MenuItem("Tools/Road Editor")]

    public static void Open()
    {
        GetWindow<RoadManagerWindow>();
    }

    public Transform roadRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("roadRoot"));

        if (roadRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");

            DrawButtons();

            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }


    private Road_Type selectedRoadType = Road_Type.straight;

    void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Select Road Type:");
        selectedRoadType = (Road_Type)EditorGUILayout.EnumPopup(selectedRoadType);
        EditorGUILayout.EndHorizontal();
        
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Road_Script>())
        {
            var roadScript = Selection.activeGameObject.GetComponent<Road_Script>();
            var roadType = roadScript.roadType;

            if(roadType == Road_Script.Road_Type.straight || roadType == Road_Script.Road_Type.turn)
            {
                if (GUILayout.Button("Add Next Road"))
                {
                    AddNewRoad();
                }
            }
            else if(roadType == Road_Script.Road_Type.Tsplit)
            {
                if (GUILayout.Button("Rotate Road"))
                {
                    RotateRoad();
                }

                if (GUILayout.Button("Add Next Road straight"))
                {
                    AddNewRoad();
                }

                if (GUILayout.Button("Add Next Road side"))
                {
                    AddNewRoad(1);
                }
            }
            else if (roadType == Road_Script.Road_Type.cross)
            {
                if (GUILayout.Button("Add Next Road left"))
                {
                    AddNewRoad(-1);
                }
                if (GUILayout.Button("Add Next Road straight"))
                {
                    AddNewRoad();
                }
                if (GUILayout.Button("Add Next Road right"))
                {
                    AddNewRoad(1);
                }
            }
        }
    }

    void AddNewRoad(int rotation = 0)
    {
        //Rotation -1 += -90
        //Rotation 0  += 0
        //Rotation 1  += 90
    }

    void RotateRoad()
    {

    }

}
