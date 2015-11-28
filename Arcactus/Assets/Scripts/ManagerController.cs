using UnityEngine;
using System.Collections;

public class ManagerController : MonoBehaviour {

	public void StartScene(string scene)
    {
        Application.LoadLevel(scene);
        Debug.Log("Click");
    }

}
