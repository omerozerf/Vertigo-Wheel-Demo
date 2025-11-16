#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace _Scripts.Editor
{
    public static class AutoSpriteAtlasCreator
    {
        // Menu: Tools/Vertigo UI/Create Sprite Atlas From Folder
        [MenuItem("Tools/Vertigo UI/Create Sprite Atlas From Folder")]
        private static void CreateSpriteAtlasFromFolder()
        {
            var selection = Selection.activeObject;
            if (selection == null)
            {
                Debug.LogError("First select a folder in the Project view.");
                return;
            }

            var selectedPath = AssetDatabase.GetAssetPath(selection);
            if (string.IsNullOrEmpty(selectedPath) || !AssetDatabase.IsValidFolder(selectedPath))
            {
                Debug.LogError("Selected object is not a folder.");
                return;
            }

            var folderName = Path.GetFileName(selectedPath);
            var atlasName = $"atlas_{folderName}";
            var atlasPath = Path.Combine(selectedPath, atlasName + ".spriteatlas").Replace("\\", "/");

            var existingAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(atlasPath);
            if (existingAtlas != null)
            {
                if (!EditorUtility.DisplayDialog("Sprite Atlas",
                        $"Already exists a Sprite Atlas named '{atlasName}'. Do you want to overwrite it?",
                        "Yes, override.", "No, cancel."))
                {
                    return;
                }
            }
            
            var atlas = new SpriteAtlas();

            // Packing Settings
            var packingSettings = new SpriteAtlasPackingSettings
            {
                enableTightPacking = false,  
                enableRotation = true, 
                padding = 4
            };
            atlas.SetPackingSettings(packingSettings);

            // Texture Settings
            var textureSettings = new SpriteAtlasTextureSettings
            {
                readable = false,
                generateMipMaps = false,
                sRGB = true,
                filterMode = FilterMode.Bilinear
            };
            atlas.SetTextureSettings(textureSettings);

            // General default platform settings
            var platformSettings = new TextureImporterPlatformSettings
            {
                name = "DefaultTexturePlatform",
                maxTextureSize = 2048,
                format = TextureImporterFormat.RGBA32,
                compressionQuality = 50,
                overridden = true
            };
            atlas.SetPlatformSettings(platformSettings);

            var guids = AssetDatabase.FindAssets("t:Sprite", new[] { selectedPath });
            var sprites = new Object[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                sprites[i] = sprite;
            }

            if (sprites.Length == 0)
            {
                Debug.LogWarning("The selected folder doesn't contain any Sprites.");
            }
            else
            {
                atlas.Add(sprites);
            }

            if (existingAtlas == null)
                AssetDatabase.CreateAsset(atlas, atlasPath);
            else
                EditorUtility.SetDirty(existingAtlas);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Created Sprite Atlas '{atlasName}' at '{atlasPath}'");
            EditorGUIUtility.PingObject(atlas);
        }
    }
}
#endif