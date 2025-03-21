using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "New Scene SO", menuName = "Scene SO")]
public class GameSceneSO : ScriptableObject
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;

    public enum GameSceneType
    {
        Tutorial,
        Location, 
        Menu, 

        //Special scenes
        Initialisation,
        PersistentManagers
        
    }
}
