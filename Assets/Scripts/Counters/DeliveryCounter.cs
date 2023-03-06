using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() || !player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
            return;
        
        // Only accepts plates
        player.GetKitchenObject().DestroySelf();
    }
}
