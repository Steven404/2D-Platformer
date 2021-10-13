using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingBOX : MonoBehaviour
{
    public GameObject DissapearingBox;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dissapear());
    }

    private IEnumerator Dissapear() {
        yield return new WaitForSeconds(5);
        DissapearingBox.SetActive(false);
    }

}
