using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   public static PlayerHandler Instance;

     public PlayerMove playerMove { get; private set; }
     public PlayerController playerController { get; private set; }
     public PlayerCombat playerCombat { get; private set; }

    public PlayerInventory playerInventory { get; private set; }

    public PlayerDamageable playerDamageable { get; private set; }

    public PlayerCamera playerCamera { get; private set; }


    public Rigidbody2D rb { get; private set; }

    public BlockClass block {  get; private set; }

    private void Awake()
    {
        Instance = this;

        playerMove = GetComponent<PlayerMove>();
        playerController = GetComponent<PlayerController>(); 
        playerCombat = GetComponent<PlayerCombat>();
        playerInventory = GetComponent<PlayerInventory>();
        playerDamageable = GetComponent<PlayerDamageable>();
        playerCamera = GetComponent<PlayerCamera>();

        rb = GetComponent<Rigidbody2D>();

        block = new BlockClass();
    }

    



}
