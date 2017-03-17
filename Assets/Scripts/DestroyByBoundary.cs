using UnityEngine;

/// <summary>
/// Script to destroy all objects which exit the boundary.
/// </summary>
public class DestroyByBoundary : MonoBehaviour {

    /// <summary>
    /// Destroys the enemy who exits the trigger.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
