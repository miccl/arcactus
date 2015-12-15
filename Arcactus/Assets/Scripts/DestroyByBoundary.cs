using UnityEngine;
using System.Collections;

/// <summary>
/// Script to destroy all objects which exit the boundary.
/// </summary>
public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
