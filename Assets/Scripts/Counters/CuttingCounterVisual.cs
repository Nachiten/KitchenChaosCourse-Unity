using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;
    private readonly int CutTrigger = Animator.StringToHash("Cut");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged(float progress)
    {
        if (progress == 0f)
            return;
        
        animator.SetTrigger(CutTrigger);
    }
}
