using UnityEngine;
using System.Collections;

public class ManagerController : MonoBehaviour {

	/// <summary>
	/// Starts a scene with the given name.
	/// </summary>
	/// <param name="scene">The name of the scene.</param>
	public void StartScene(string scene)
    {
        Application.LoadLevel(scene);
        Debug.Log("Click");
    }

}
