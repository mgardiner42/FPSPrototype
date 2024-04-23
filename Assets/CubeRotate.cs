/*using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public float degreesPerSecond;
    
    // NOTE:
    // THERE IS A NEW TAG FOR GreenCube IN THE EDITOR
    // I DON'T THINK THIS EFFECTS ANYTHING
    
    /*
    //public string name = "GreenCube"; // TODO can I do this in the editor???

    //[PunRPC]
    public void TakeDamage(int _damage)
    {
        Destroy(gameObject);
    }
    */



    // Start is called before the first frame update
    void Start()
    {
        //gameObject.tag = "GreenCube";
        // I tried to make this take damage. Since revereted code
        // pertaining to that
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(
            new Vector3(degreesPerSecond / 2,
                        degreesPerSecond,
                        degreesPerSecond / 3) * Time.deltaTime);
    }
}
