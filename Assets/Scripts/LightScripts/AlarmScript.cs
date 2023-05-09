using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    private Light lightVar;
    //public bool fadeOut;

    // Start is called before the first frame update
    void Awake()
    {

        lightVar = GetComponent<Light>();
        //StartCoroutine(FadeInOut());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * 180f, 0f, Space.World);      
        
    }

    /*public IEnumerator FadeInOut()
    {
        while (true)
        {
            lightVar.intensity = Mathf.PingPong(8, 4);

            yield return new WaitForSeconds(2.0f);

            lightVar.intensity = Mathf.PingPong(4, 8);

            yield return new WaitForSeconds(2.0f);
        }
    }*/
}
