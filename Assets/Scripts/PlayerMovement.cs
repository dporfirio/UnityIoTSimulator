using UnityEngine;

//control the movement of the player with keyboard
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator animator;
    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            change = Vector3.zero;
            change.x = Input.GetAxis("Horizontal");
            change.y = Input.GetAxis("Vertical");
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("playerMoveX", change.x);
            animator.SetFloat("playerMoveY", change.y);
            animator.SetBool("playerMoving", true);
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().ChangeInc(0);
        }
        else
        {
            animator.SetBool("playerMoving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();

        playerRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
