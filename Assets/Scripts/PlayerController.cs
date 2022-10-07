using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
 

    public Transform groundPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask Ground;

    private bool facing = true;
    private int extraJumps;
    public int extraJumpValues;


    public float speed;
    public float jumpForce;
    
    private Rigidbody2D rb;
    private Animator anim;

    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpValues;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (!facing && moveInput > 0)
        {
            Flip();
            //transform.rotation = Quaternion.Euler(Vector3.zero);

        }
        else if (facing && moveInput < 0)
        {
            Flip();
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, Ground);

        //Jump
        if (isGrounded == true)
        {
            extraJumps = extraJumpValues;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            anim.SetTrigger("takeOff");
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            gm.points += 1;
        }
        if (other.CompareTag("Death"))
        {
            SceneManager.LoadScene(0);
        }
    }


    void SwitchToIdle()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
    }

    void SwitchToRunning()
    {
        anim.SetBool("isRunning", true);
    }



    private void Flip()
    {
        facing = !facing;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

}
