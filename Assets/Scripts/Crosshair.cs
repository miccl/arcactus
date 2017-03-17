using UnityEngine;
using System.Collections;

/// <summary>
/// Script to create a crosshair in vr.
/// </summary>
public class Crosshair : MonoBehaviour {


    public Camera CameraFacing;
    private Vector3 originalScale;

    // Use this for initialization
    void Start()
    {
        originalScale = transform.localScale;
    }

    /// <summary>
    /// Updates the position and scale of the crosshair.
    /// Updates the crosshair position based on the camera position <paramref name="CameraFacing"/>.
   ///  Scales the crosshair based on the distance to the nearest aimed object.
    /// </summary>
    void Update()
    {
        RaycastHit hit;
        float distance;
        if (Physics.Raycast(new Ray(CameraFacing.transform.position,
                                     CameraFacing.transform.rotation * Vector3.forward),
                             out hit))
        {
            distance = hit.distance;
        }
        else
        {
            distance = CameraFacing.farClipPlane * 0.95f;
        }

        transform.position = CameraFacing.transform.position +
            CameraFacing.transform.rotation * Vector3.forward * distance;
        transform.LookAt(CameraFacing.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        if (distance < 10.0f)
        {
            distance *= 1 + 5 * Mathf.Exp(-distance);
        }
        transform.localScale = originalScale * distance;
    }
}
