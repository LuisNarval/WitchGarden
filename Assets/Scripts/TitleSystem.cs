using UnityEngine;

public class TitleSystem : MonoBehaviour
{
    [Header("REFERENCE")]
    public SceneSystem sceneSystem;

    public void goToGame()
    {
        sceneSystem.nextScene();
    }

}