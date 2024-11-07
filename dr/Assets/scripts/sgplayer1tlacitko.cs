using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sgplayer1tlacitko : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void prechod1()
    {
        SceneManager.LoadScene(1);
    }

    public void prechod2()
    {
        SceneManager.LoadScene(2);
    }
}
