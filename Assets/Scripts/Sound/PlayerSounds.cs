using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSounds : MonoBehaviour
{
    private Player player;

    private float footstepTimer;
    private float footstepInterval = 0.5f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!player.IsWalking()) 
            return;
        
        footstepTimer += Time.deltaTime;

        if (footstepTimer < footstepInterval) 
            return;
        
        footstepTimer = 0;
        SoundManager.Instance.PlayFootstepsSound(player.transform.position, 5f);
    }
}
