using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveLad());
    }


    public IEnumerator MoveLad()
    {
        transform.position -= 1f * Vector3.forward;

        yield return new WaitForSeconds(0.1f);

        transform.position += 1.01f * Vector3.forward;

        yield return null;
    }
}
