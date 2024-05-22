using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CheckStart : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Reset();
        if (File.Exists(Path.Combine("Q:\\nvram\\BIOS_ENABLE")))
        {

            Debug.Log("BIOS Enabled");
            videoPlayerObject.SetActive(true);
            StartCoroutine(checkState());
        }
        else
        {
            Debug.Log("BIOS Skipped");
            transition();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Reset()
    {
        string path = Path.Combine("Q:\\proc\\", "current_config.txt");
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine("haruna=false");
            sw.WriteLine("sp_en=found_cvt");
            sw.WriteLine("keychip=not_ready");
            sw.Close();
        }
        string path1 = Path.Combine("Q:\\proc\\", "state.txt");
        using (StreamWriter sw = File.CreateText(path1))
        {
            sw.WriteLine("STEP 1=Boot=false");
            sw.WriteLine("error=false");
            sw.Close();
        }
        string path2 = Path.Combine("Q:\\proc\\", "install.txt");
        using (StreamWriter sw = File.CreateText(path2))
        {
            sw.WriteLine("");
            sw.Close();
        }
        string path3 = Path.Combine("Q:\\proc\\", "lifecycle_state.txt");
        using (StreamWriter sw = File.CreateText(path3))
        {
            sw.WriteLine("STEP 1=Initilizing=false");
            sw.WriteLine("error=false");
            sw.Close();
        }
        string path4 = Path.Combine("Q:\\proc\\", "config_errors.txt");
        using (StreamWriter sw = File.CreateText(path4))
        {
            sw.WriteLine("");
            sw.Close();
        }
    }

    private IEnumerator checkState()
    {
        while (true)
        {
            if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
            {
                Debug.Log("BIOS End");
                transition();
                break;
            }
            yield return new WaitForSeconds(0.125f);
        }
    }

    private void transition()
    {
        if (File.Exists(Path.Combine("Q:\\nvram\\LIFECYCLE_CONTROLLER")))
        {
            SceneManager.LoadScene("LevelSME");
        }
        else
        {

            if (File.Exists(Path.Combine("C:\\Windows\\ARS_NOVA\\update\\system_update.ps1")))
            {
                Debug.Log("System Update Found");
                SceneManager.LoadScene("LevelUpdate");
            }
            else if (File.Exists(Path.Combine("Q:\\nvram\\PLATFORM_INSTALLED")))
            {
                Debug.Log("Ready to Boot");
                SceneManager.LoadScene("LevelNormal");
            }
            else
            {
                Debug.Log("Platform Installer Triggred");
                SceneManager.LoadScene("LevelPre");
            }
        }
    }
}
