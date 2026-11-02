using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject PanelSeetting;
    public GameObject MenuPanel;

    void Start()
    {
        if(PanelSeetting != null)
            PanelSeetting.SetActive(false);
        if (MenuPanel != null) 
            MenuPanel.SetActive(true);
    }
    public void Play()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainPanel");
    }
    public void Setting()
    {
        if (PanelSeetting.activeSelf == false)
        {
            PanelSeetting.SetActive(true);  
            MenuPanel.SetActive(false);     
        }
        else
        {
            PanelSeetting.SetActive(false); 
            MenuPanel.SetActive(true);    
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
