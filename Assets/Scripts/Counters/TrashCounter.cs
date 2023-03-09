using System;

public class TrashCounter : BaseCounter
{
    public static event Action<TrashCounter> OnAnyObjectTrashed;
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) 
            return;
        
        player.GetKitchenObject().DestroySelf();
        
        OnAnyObjectTrashed?.Invoke(this);
    }
    
    public new static void ResetStaticData() => OnAnyObjectTrashed = null;
}
