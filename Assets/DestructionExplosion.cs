using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruction", 0.6f);
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
