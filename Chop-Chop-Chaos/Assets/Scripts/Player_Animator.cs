using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]private Player player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("IsWalking", player.IsWalking());
    }
}
