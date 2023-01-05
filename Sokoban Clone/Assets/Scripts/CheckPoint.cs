using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Goal goalScript;
    // Start is called before the first frame update
    void Start()
    {
        goalScript = gameObject.transform.parent.GetComponent<Goal>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            goalScript.goals += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            goalScript.goals -= 1;
        }
    }
}
