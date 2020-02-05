using UnityEngine;
using System.Collections;
public abstract class X_Interaction : MonoBehaviour
{
    protected bool unlocked = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)&&unlocked==true&& GameFile.lockdown ==false)
        {
            DoAction();
        }
        update();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tagger")
        {
            unlocked = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tagger")
        {
            unlocked = false;
        }
    }
    public virtual void DoAction()
    {
    }
    public virtual void update()
    {
    }
}