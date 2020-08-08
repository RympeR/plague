using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class character_controller : MonoBehaviour
{
    //OnCollisionEnter
    //OnCollisionExit
    //OnTriggerEnter
    //OnTriggerExit
    //OnCollisionStay
    //OnTriggerStay
    public Joystick joystick;
    Rigidbody2D rb;
    float run;
    bool jump = true;
    bool whenlook;
    int current_torch_amount;
    float plague_percentage;
    float hp;
    bool near_fire;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(torch.IncreaseTorchAmount());
    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        run = joystick.Horizontal * 5f;

        if (joystick.Vertical >= .5f & jump)
        {
            jump = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * 7.5f, ForceMode2D.Impulse);
        }
        
        if (joystick.Vertical <= .5f & current_torch_amount > 0)
        {
            current_torch_amount = torch.DecreaseTorchAmount();
            Debug.Log(current_torch_amount);
        }
        rb.velocity = new Vector2(run, rb.velocity.y);
        Flip();
    }
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "ground" & jump == false)
        {
            jump = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "torch" || obj.tag == "fire")
        {
            InvokeRepeating("decreaseHealth", 0f, 0.5f);

            current_torch_amount = torch.IncreaseTorchAmount();
            run = torch.CalculateSpeedSlow(run);
        }
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == "torch")
        {
            current_torch_amount = torch.IncreaseTorchAmount();
            run = torch.CalculateSpeedSlow(run);
            Destroy(obj.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.tag == "torch" || obj.tag == "fire")
        {
            InvokeRepeating("increaseHealth", 0f, 0.5f);
            
            current_torch_amount = torch.IncreaseTorchAmount();
            run = torch.CalculateSpeedSlow(run);
        }
    }
    void increaseHealth()
    {
        hp++;
        Debug.Log(hp);
    }
    void decreaseHealth()
    {
        if (hp == 0)
        {
            Invoke("die", 2f);
        }
        hp--;
        Debug.Log(hp);
    }
    void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Flip()
    {
        if (joystick.Horizontal > .2f) whenlook = true;
        if (joystick.Horizontal < -.2f) whenlook = false;

        if (!whenlook)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
