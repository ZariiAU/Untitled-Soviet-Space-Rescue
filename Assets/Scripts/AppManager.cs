using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public void LockCursor(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseTime(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }
}
