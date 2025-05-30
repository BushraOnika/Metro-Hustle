using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

namespace UnityFolderPreset.Template
{
    internal static class FolderPresetSample
    {
        [MenuItem("Assets/Folder Preset/New Folder", priority = 22)]
        private static void Create()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<DoCreateFolder>(), "New Folder",
                EditorGUIUtility.IconContent(EditorResources.emptyFolderIconName).image as Texture2D,
                null
            );
        }
    }
}