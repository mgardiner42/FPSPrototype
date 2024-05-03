using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthParticles : MonoBehaviour
{

    public GameObject healthParticles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyParticles(1, healthParticles));
    }

    private IEnumerator DestroyParticles(float remaining, GameObject particles)
    {
        while (remaining > 0)
        {
            remaining -= 1f;
            yield return new WaitForSeconds(1f);
        }

        if (particles != null)
        {
            PhotonNetwork.Destroy(particles);
        }
    }

}
