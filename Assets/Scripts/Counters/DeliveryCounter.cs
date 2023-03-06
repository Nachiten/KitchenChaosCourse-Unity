public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() || !player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            return;
        
        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
        
        // Only accepts plates
        player.GetKitchenObject().DestroySelf();
    }
}
