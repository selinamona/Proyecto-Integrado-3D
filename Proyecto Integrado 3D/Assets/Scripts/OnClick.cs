using System.Collections;
using UnityEngine;

public class OnClick : MonoBehaviour
{

    public bool ClickedSpace;
    public bool Touching;
    public KeyCode Arrow;
    public GameObject Player;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Arrow) && Touching )
        {
            StartCoroutine(ClickedSpaceAtTheMoment());
        }
        if (Input.GetKeyDown(Arrow) && Touching)
        {
            Player.GetComponent<Health>().CurrentHealth -= 1;
        }
    }
    IEnumerator ClickedSpaceAtTheMoment()
    {
        ClickedSpace = true;
        yield return new WaitForSecondsRealtime(0.05f);
        ClickedSpace = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Touching = true;
        if (ClickedSpace)
        {
           Destroy(collision.gameObject); 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Touching = false;
    }
}
