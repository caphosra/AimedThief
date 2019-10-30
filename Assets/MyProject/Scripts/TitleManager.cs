using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private string firstStageName;

    private void Update()
    {
        if(Input.GetAxisRaw("Fire") > 0)
        {
            SceneManager.LoadScene(firstStageName);
        }
    }    
}