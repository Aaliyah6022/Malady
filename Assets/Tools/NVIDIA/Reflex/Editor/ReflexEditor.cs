/*
 * Copyright (c) 2020, NVIDIA CORPORATION.  All rights reserved.
 *
 * NVIDIA CORPORATION and its licensors retain all intellectual property
 * and proprietary rights in and to this software, related documentation
 * and any modifications thereto.  Any use, reproduction, disclosure or
 * distribution of this software and related documentation without an express
 * license agreement from NVIDIA CORPORATION is strictly prohibited.
 */
using UnityEngine;
using UnityEditor;

using NVIDIA;

namespace NVIDIA
{
    [CustomEditor(typeof(Reflex))]
    public class ReflexEditor : Editor
    {
        private SerializedProperty isFirstCameraProp;
        private SerializedProperty isLastCameraProp;
        private SerializedProperty intervalUsProp;
        private SerializedProperty isLowLatencyModeProp;
        private SerializedProperty isLowLatencyBoostProp;

        void OnEnable()
        {
            isFirstCameraProp = serializedObject.FindProperty("isFirstCamera");
            isLastCameraProp = serializedObject.FindProperty("isLastCamera");
            intervalUsProp = serializedObject.FindProperty("intervalUs");
            isLowLatencyModeProp = serializedObject.FindProperty("isLowLatencyMode");
            isLowLatencyBoostProp = serializedObject.FindProperty("isLowLatencyBoost");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isFirstCameraProp);
            EditorGUILayout.PropertyField(isLastCameraProp);

            if(isFirstCameraProp.boolValue)
            {
                EditorGUILayout.PropertyField(intervalUsProp);
                EditorGUILayout.PropertyField(isLowLatencyModeProp);
                EditorGUILayout.PropertyField(isLowLatencyBoostProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
