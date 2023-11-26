using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.InputSystem;

[System.Serializable]
public class SaveData
{
    public float CheckpointX, CheckpointY;
    public List<Item> InventorySave = new List<Item>();
    public int maxCharge, currentCharge;
}

public class CheckpointManager : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();
    public PlayerBaseInputs playerControls;
    private InputAction Interact;
    private Transform Player;

    public Transform CurrentCheckpoint;
    private SaveData saveData = new SaveData();
    private static CheckpointManager instance;
    private Vector2 lastCheckpointPos;

    private Flashlight flashlight;
    private Inventory inventory;

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
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        flashlight = Player.GetChild(1).GetComponent<Flashlight>();
        inventory = Player.GetComponent<Inventory>();
        inventory.itemData = GetComponent<ItemDataBase>();
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
        SaveData data = DataService.LoadData<SaveData>("/SaveData.json",false);
        saveData.CheckpointX = data.CheckpointX;
        saveData.CheckpointY = data.CheckpointY;
        lastCheckpointPos.x = saveData.CheckpointX;
        lastCheckpointPos.y = saveData.CheckpointY;
        inventory.PlayerItems = data.InventorySave;
        flashlight.Charge = data.currentCharge;
        flashlight.MaxCharge = data.maxCharge;
    }

    private void SaveCheckpoint(InputAction.CallbackContext context)
    {
        if(CurrentCheckpoint != null)
        {
            flashlight.Charge = flashlight.MaxCharge;
            lastCheckpointPos = CurrentCheckpoint.position;
            saveData.CheckpointX = CurrentCheckpoint.position.x;
            saveData.CheckpointY = CurrentCheckpoint.position.y;
            saveData.InventorySave = inventory.PlayerItems;
            saveData.maxCharge = flashlight.MaxCharge;
            saveData.currentCharge = flashlight.Charge;
            if(DataService.SaveData("/SaveData.json",saveData,false))
            {
                Debug.Log("Saved");
            }
        }
    }
}
