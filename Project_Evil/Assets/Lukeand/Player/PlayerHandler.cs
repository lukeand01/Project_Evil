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

    public PlayerMagic playerMagic { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public BlockClass block {  get; private set; }

    public Camera cam {  get; private set; }    

    private void Awake()
    {
        Instance = this;

        playerMove = GetComponent<PlayerMove>();
        playerController = GetComponent<PlayerController>(); 
        playerCombat = GetComponent<PlayerCombat>();
        playerInventory = GetComponent<PlayerInventory>();
        playerDamageable = GetComponent<PlayerDamageable>();
        playerCamera = GetComponent<PlayerCamera>();
        playerMagic = GetComponent<PlayerMagic>();  

        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        block = new BlockClass();
    }

    private void FixedUpdate()
    {
        CheckForInteractable();
    }

    #region CHECK FOR WORLD ITEMS
    //use last dir for that.
    //get the first in raycast

    //i want the mouse to have some influence
    //from the list we get the one that teh mouse is the closest.

    IInteractable currentInteract;

    void CheckForInteractable()
    {

        RaycastHit2D[] check = Physics2D.CircleCastAll(transform.position + playerMove.lastDir, 2, Vector2.one, 5, LayerMask.GetMask("Interactable"));

        if (check.Length <= 0)
        {
            DealWithInteractable(null);

            return;
        }

        IInteractable target = null;

        if (check.Length == 1)
        {
            target = check[0].collider.gameObject.GetComponent<IInteractable>();
           
        }
        else
        {
            target = GetClosestItemToMouse(check);

        }

        

        DealWithInteractable(target);

        //we check the closests one.

    }

    IInteractable GetClosestItemToMouse(RaycastHit2D[] cast)
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        GameObject currentInteract = null;
        float currentClosest = 0;

        currentClosest = Vector3.Distance(mousePos, cast[0].collider.transform.position);
        currentInteract = cast[0].collider.gameObject; 


        for (int i = 1; i < cast.Length; i++)
        {
            float distance = Vector3.Distance(mousePos, cast[i].collider.transform.position);

            if(distance < currentClosest)
            {
                currentClosest = distance;
                currentInteract = cast[i].collider.gameObject;

            }

        }

        IInteractable interactable = currentInteract.GetComponent<IInteractable>();

        if(interactable == null)
        {
            Debug.Log("no interact");
        }

        return interactable;
    }

    void DealWithInteractable(IInteractable newInteractable)
    {
        if (currentInteract != null)
        {
            currentInteract.CallUI(false);
            currentInteract = null;

        }


        if (newInteractable == null) return;

        

        currentInteract = newInteractable;

        if (currentInteract == null)
        {
            return;
        }
        currentInteract.CallUI(true);

    }


    public void InteractWithInteractable()
    {

        if(currentInteract == null) return;

        currentInteract.Interact();
    }

    #endregion

}
