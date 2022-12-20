using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public class SetMeshLightMapScale : MonoBehaviour
{
    public bool run;

    private void Start()
    {

        Debug.Log("gameobject = " + gameObject.name);
    }

    private void OnDrawGizmos()
    {


        if (run)
        {
            run = false;

            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer m in meshes)
            {
                float scale = 1f;
                SerializedObject so = new SerializedObject(m);
                so.FindProperty("m_ScaleInLightmap").floatValue = scale;
                so.ApplyModifiedProperties();
            }
        }
    }
}

#endif
