using UnityEngine;

public class ModelSwapper : MonoBehaviour
{
    public GameObject[] playerModelPrefabs; // Array of player model prefabs to swap to
    public GameObject[] petModelPrefabs; // Array of pet model prefabs to swap to

    public void SwapPlayerModel(int modelIndex)
    {
        if (playerModelPrefabs != null && modelIndex < playerModelPrefabs.Length)
        {
            // Save the new player model data
            SavePlayerModel(modelIndex);
        }
        else
        {
            Debug.LogError("Player model prefabs are not assigned, or model index is out of range.");
        }
    }

    public void SwapPetModel(int modelIndex)
    {
        if (petModelPrefabs != null && modelIndex < petModelPrefabs.Length)
        {
            // Save the new pet model data
            SavePetModel(modelIndex);
        }
        else
        {
            Debug.LogError("Pet model prefabs are not assigned, or model index is out of range.");
        }
    }

    private void SavePlayerModel(int modelIndex)
    {
        ModelData modelData = new ModelData { modelName = playerModelPrefabs[modelIndex].name };
        ModelDataHandler.SavePlayerModelData(modelData);
    }

    private void SavePetModel(int modelIndex)
    {
        ModelData modelData = new ModelData { modelName = petModelPrefabs[modelIndex].name };
        ModelDataHandler.SavePetModelData(modelData);
    }
}