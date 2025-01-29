using UnityEngine;

public class DevCode : MonoBehaviour
{
    private bool devActive;
    private SC_ResourceManager ScriptResourceManager;
    void Start()
    {
        devActive = false;
        ScriptResourceManager = GameObject.FindWithTag("GameUI").GetComponent<SC_ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !devActive)
        {
            devActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && devActive)
        {
            devActive = false;
        }


        if (Input.GetKeyDown(KeyCode.V) && devActive)
        {
            ScriptResourceManager.happiness += 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.B) && devActive)
        {
            ScriptResourceManager.cleanliness += 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.N) && devActive)
        {
            ScriptResourceManager.power += 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.M) && devActive)
        {
            ScriptResourceManager.money += 2;
            ScriptResourceManager.UpdateSliders();
        }

        
        //----------------------
        
        
        
        if (Input.GetKeyDown(KeyCode.F) && devActive)
        {
            ScriptResourceManager.happiness -= 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.G) && devActive)
        {
            ScriptResourceManager.cleanliness -= 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.H) && devActive)
        {
            ScriptResourceManager.power -= 2;
            ScriptResourceManager.UpdateSliders();
        }
        if (Input.GetKeyDown(KeyCode.J) && devActive)
        {
            ScriptResourceManager.money -= 2;
            ScriptResourceManager.UpdateSliders();
        }

        if (Input.GetKeyDown(KeyCode.L) && devActive)
        {
            ScriptResourceManager.dayTextUpdate();

        }
        
    }
    
    
    
    
}
