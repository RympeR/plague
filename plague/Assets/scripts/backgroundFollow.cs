using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerCharacter;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerCharacter.position.x, playerCharacter.position.y, transform.position.z);
    }

  
}
