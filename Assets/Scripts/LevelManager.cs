using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Here loading screen can be added
//Level Manager
// Co robi:
//Zarządza przejściami między poziomami
// Na czym powinien być:
//Wymaga image jako child, który jest loading screenem oraz dodanej referencji do jego image
//Player znajduje ten kompontent by zmienić poziom
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    static private bool exists = false;
    [SerializeField] Image loadingScreen;
    [SerializeField] AudioManager aManager;

    void Awake()
    {
        if (exists)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
        if (loadingScreen != null)
        {
            loadingScreen.enabled = false;
        }
        aManager = GetComponentInChildren<AudioManager>();
    }

    void PlayTrack()
    {
        if(aManager!=null)
        {
            aManager.Shuffle();
        }
    }

    void SetPlayerPosition(Vector2 pos)
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
            player.ClearState();
            player.transform.position = pos;
        }
    }
    Vector2 GetEntrancePos()
    {
        int count = Resources.FindObjectsOfTypeAll<Entrance>().Length;
        if(count>0)
        {
            return Resources.FindObjectsOfTypeAll<Entrance>()[0].transform.position;
        }
        return new Vector2(0,0);
    }
    Vector2 GetExitPos()
    {
        int count = Resources.FindObjectsOfTypeAll<Exit>().Length;
        if(count>0)
        {
            return Resources.FindObjectsOfTypeAll<Exit>()[0].transform.position;
        }
        return new Vector2(0,0);
    }
    IEnumerator screenLoading(int i)
    {
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = true;
        }
        if(aManager!=null)
        {
            aManager.setSong(0);
        }
        int index = SceneManager.GetActiveScene().buildIndex+i;
        SceneManager.LoadScene(index);
        yield return new WaitForSeconds(0.5f);
        float t1 = Time.time;
        while(SceneManager.GetActiveScene().buildIndex!=index)
        {
            yield return new WaitForSeconds(0.05f);
            if(Time.time-t1>5f)
            {
                break;
            }
        }
        if(loadingScreen!=null)
        {
            loadingScreen.enabled = false;
        }
        if(i<0)
        {
            Debug.Log(GetExitPos());
            SetPlayerPosition(GetExitPos());
        }
        else
        {

            Debug.Log(GetEntrancePos());
            SetPlayerPosition(GetEntrancePos());
        }
        PlayTrack();
        yield return null;
    }

    public void SwitchForth()
    {
        IEnumerator coroutine = screenLoading(1);
        StartCoroutine(coroutine);

    }
    public void SwitchBack()
    {
        IEnumerator coroutine = screenLoading(-1);
        StartCoroutine(coroutine);

    }
}


