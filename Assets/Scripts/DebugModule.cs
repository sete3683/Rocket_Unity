using UnityEngine;

public class DebugModule : MonoBehaviour
{
    [SerializeField] private Collider collider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneHandler.NextScene();
        else if (Input.GetKeyDown(KeyCode.C))
            collider.enabled ^= true;
    }
}
