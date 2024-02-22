using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameaAspectRatioScaler : MonoBehaviour
{
   public float targetAspectRatio = 16f / 9f; // Set your target aspect ratio here

    private void Start()
    {
        ScaleCamera();
    }

    private void ScaleCamera()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;

        float scaleHeight = currentAspectRatio / targetAspectRatio;

        Camera cameraComponent = GetComponent<Camera>();

        // If the current aspect ratio is wider than the target aspect ratio, scale by height
        if (currentAspectRatio > targetAspectRatio)
        {
            cameraComponent.orthographicSize *= scaleHeight;
        }
        // If the current aspect ratio is narrower than the target aspect ratio, scale by width
        else
        {
            cameraComponent.orthographicSize /= scaleHeight;
        }
    }
}
