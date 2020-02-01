using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transport : X_Interaction
{
    public float x;
    public float y;
    public string destination;
    // Start is called before the first frame update

    // Update is called once per frame
    public override void DoAction()
    {
        ApplicationData.x = x;
        ApplicationData.y = y;
        SceneManager.LoadScene(destination);
    }
}
