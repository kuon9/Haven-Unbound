using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.InputSystem;

[System.Serializable]
public class CheckpointData
{
    public float x;
    public float y;
}

public class CheckpointManager : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();
    public PlayerBaseInputs playerControls;
    private InputAction Interact;
    public Transform Player;

    public Transform CurrentCheckpoint;
    private CheckpointData checkpointData = new CheckpointData();
    private static CheckpointManager instance;
    public Vector2 lastCheckpointPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);

        playerControls = new PlayerBaseInputs();
    }
    private void Start()
    {
        LoadCheckpoint();
        Player.position = lastCheckpointPos;
    }

    private void OnEnable()
    {
        Interact = playerControls.Player.Interact;
        Interact.Enable();
        Interact.performed += SaveCheckpoint;
    }
    private void OnDisable()
    {
        //Interact.Disable();
    }
    public void LoadCheckpoint()
    {
        CheckpointData data= DataService.LoadData<CheckpointData>("/PlayerCheckpoint.json",false);
        checkpointData.x = data.x;
        checkpointData.y = data.y;
        lastCheckpointPos.x = checkpointData.x;
        lastCheckpointPos.y = checkpointData.y;
    }

    private void SaveCheckpoint(InputAction.CallbackContext context)
    {
        if(CurrentCheckpoint != null)
        {
            lastCheckpointPos = CurrentCheckpoint.position;
            checkpointData.x = CurrentCheckpoint.position.x;
            checkpointData.y = CurrentCheckpoint.position.y;
            if(DataService.SaveData("/PlayerCheckpoint.json",checkpointData,false))
            {
                Debug.Log("Saved");
            }
        }
    }
}
