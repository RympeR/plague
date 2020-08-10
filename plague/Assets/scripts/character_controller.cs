using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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
    Light light_;
    Rigidbody2D rb;
    float run;
    float hp_run = 5f;
    bool jump = true;
    bool whenlook;
    int current_torch_amount;
    float plague_percentage = 0.0f;
    float hp;
    bool near_fire;
    bool torch_lighted = false;
    public GameObject torch_no_light;
    public GameObject torch_with_light;

    private void Awake()
    {
        get_torch_off();
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = 100f * (1 - plague_percentage);

        InvokeRepeating("changeHealth", 0f, 0.2f);

        light_ = GetComponent<Light>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        run = joystick.Horizontal * hp_run;

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
    
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "torch")
        {
            current_torch_amount = torch.IncreaseTorchAmount();
            run = torch.CalculateSpeedSlow(run);
            Destroy(obj.gameObject);
        }
        if (obj.tag == "fire")
        {
            near_fire = true;
        }
        if (obj.tag == "Finish")
        {
            LevelController.instance.YouWin();
        }
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.tag == "fire")
        {
            light_on_torch();
            torch_lighted = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        //obj.tag == "torch" ||
        if (obj.tag == "fire")
        {
            near_fire = false;
            //current_torch_amount = torch.IncreaseTorchAmount();
            run = torch.CalculateSpeedSlow(run);
            Invoke("torch_controller", 5f);
        }
    }
    void torch_controller()
    {
        if (torch_lighted)
        {
            get_torch_off();
        }
        else
        {
            light_on_torch();
        }
    }
    void light_on_torch()
    {
        torch_with_light.GetComponent<SpriteRenderer>().enabled = true;
        torch_no_light.GetComponent<SpriteRenderer>().enabled = false;
    }

    void get_torch_off()
    {
        torch_with_light.GetComponent<SpriteRenderer>().enabled = false;
        torch_no_light.GetComponent<SpriteRenderer>().enabled = true;

    }
    
    void changeHealth()
    {
        if (torch_with_light.GetComponent<SpriteRenderer>().enabled)
        {
            near_fire = true;
        }
        else
        {
            near_fire = false;
        }
        if (!near_fire)
            decreaseHealth();
        else
            increaseHealth();
    }
    void increaseHealth()
    {

        hp_run = 5 * hp / 100;
        Debug.Log(run);
        light_.range = 49 - (100 / hp) * 9;
        if (hp <= 20) light_.range = 6;
        if (hp<100) hp++;
        Debug.Log(hp);
    }
    void decreaseHealth()
    {
        hp_run = 5 * hp / 100;
        Debug.Log(run);
        light_.range = 46 - (100 / hp) * 6;
        if (hp <= 20)
        {
            light_.range = 6;
        }
        if (hp == 0)
        {
            Invoke("die", 2f);
        }
        hp--;
        Debug.Log(hp);
    }
    void die()
    {
        LevelController.instance.youLose();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Flip()
    {
        if (joystick.Horizontal > .2f) whenlook = true;
        if (joystick.Horizontal < -.2f) whenlook = false;

        if (!whenlook)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
