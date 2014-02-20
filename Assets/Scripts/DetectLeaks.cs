using UnityEngine;
using System.Collections;

public class DetectLeaks : MonoBehaviour {

    void OnGUI() {
        GUILayout.Label("All " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
        GUILayout.Label("Textures " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Texture)).Length);
        GUILayout.Label("Audio Clips " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.AudioClip)).Length);
        GUILayout.Label("Meshes " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Mesh)).Length);
        GUILayout.Label("Materials " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Material)).Length);
        GUILayout.Label("GameObjects " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.GameObject)).Length);
        GUILayout.Label("Components " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Component)).Length);
    }
}
