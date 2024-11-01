using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Arrow"))
        {
            AutumnGame.Instance.GetPoint();
        }
    }
}
