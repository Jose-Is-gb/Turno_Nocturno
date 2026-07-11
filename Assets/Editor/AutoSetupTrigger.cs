using UnityEngine;
using UnityEditor;

public class AutoSetupTrigger
{
    [MenuItem("Tools/Auto Configurar Choque y Escena")]
    public static void Setup()
    {
        // 1. Configurar el bloque que el usuario Tenga SELECCIONADO
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
            if (col == null)
                col = obj.AddComponent<BoxCollider2D>();
            
            col.isTrigger = true; 
            
            ChangeSceneOnTrigger script = obj.GetComponent<ChangeSceneOnTrigger>();
            if (script == null)
                script = obj.AddComponent<ChangeSceneOnTrigger>();
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(obj.scene);
            Debug.Log("El objeto '" + obj.name + "' ya está configurado como zona de choque.");
        }
        else
        {
            Debug.LogWarning("Por favor, selecciona un objeto (como 'puerta') antes de usar esta herramienta.");
        }

        // 2. Configurar el Player 
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            if (player.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0; 
                rb.isKinematic = true; 
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(player.scene);
            }
        }

        // 3. ¡Magia extra! Añadir TODAS las escenas del proyecto a Build Settings para que nunca más den error
        string[] allSceneGuids = AssetDatabase.FindAssets("t:Scene");
        var originalScenes = EditorBuildSettings.scenes;
        var newScenes = new System.Collections.Generic.List<EditorBuildSettingsScene>(originalScenes);

        foreach (string guid in allSceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            bool exists = false;
            foreach (var s in originalScenes)
            {
                if (s.path == scenePath)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                newScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }
        }
        
        EditorBuildSettings.scenes = newScenes.ToArray();

        Debug.Log("¡Todo listo! Asegúrate de escribir el nombre de tu escena en el Inspector.");
    }
}
