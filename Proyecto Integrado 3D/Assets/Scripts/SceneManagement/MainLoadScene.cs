using UnityEngine;

using UnityEngine.SceneManagement;


public class MainLoadScene : MonoBehaviour

{

    private void OnCollisionEnter(Collision collision)

    {

        if (collision.gameObject.name == "Player")

        {

            SceneManager.LoadScene("MainScene");

        }

    }

}