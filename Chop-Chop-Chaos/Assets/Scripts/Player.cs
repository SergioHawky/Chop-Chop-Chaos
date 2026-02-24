using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    // Torna o campo privado editável no Inspector
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Game_Input gameInput;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float rotationSpeed = 10f;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);
            if (canMove)
                moveDir = moveDirx;
            else
            {
                Vector3 moveDirz = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);
                if (canMove)
                    moveDir = moveDirz;
            }
        }
        if (canMove)
            transform.position += moveDir * Time.deltaTime * moveSpeed;                                     // Multiplica por Time.deltaTime para tornar o movimento independente da taxa de frames.
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);      // Faz o jogador olhar na direção do movimento, slerp suaviza a rotação.

    }

    public bool IsWalking() {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    }
}
