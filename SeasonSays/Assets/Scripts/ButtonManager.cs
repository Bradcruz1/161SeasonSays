using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour
{

    public List<GameObject> Seasons;

    private int patternLength = 1;

    private int currentButton = 0;

    private List<int> pattern = new List<int>();

    private List<string> seasons = new List<string>();

    private List<GameObject> weatherEffectsList = new List<GameObject>();

    public bool patternStart = true;
    public bool messedUpPattern = false;

    public GameObject Puddle;
    public GameObject IcePatch;
    public GameObject Wind;
    public GameObject Fire;

    public GameObject Player;

    public GameObject barrier;
    public Text wait_go;
    public Material[] colors;
    public Renderer center;
    public Text progress;
    public Text Round;


    void Awake()
    {
        pattern.Add(UnityEngine.Random.Range(0, 4));
        
        seasons.Add("Spring");
        seasons.Add("Summer");
        seasons.Add("Fall");
        seasons.Add("Winter");

        ShowBarrier();
        wait_text();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject season in Seasons)
        {
            Button b;

            b = season.GetComponent<Button>();

            b.buttonHit.AddListener(completeListener);
        }

        //center.sharedMaterial = colors[0];
        progress_text();
        progress.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (patternStart)
        {
            round_text();
            playPattern();
            if (!messedUpPattern)
            {
                //ClearWeatherEffects();
                StartCoroutine(clearWeatherEffectsList());
            }
            else
                messedUpPattern = false;
        }
        
    }

    void playPattern() 
    {
        patternStart = false;
        StartCoroutine(play());
    }

    IEnumerator play()
    {
        for (int p = 0; p < patternLength; ++p)
        {
            // Debug.Log(p);
            // Debug.Log(seasons[pattern[p]]);

            yield return new WaitForSeconds(1);

            ShowBarrier();
            wait_go.gameObject.SetActive(true);
            progress.gameObject.SetActive(false);

            //center.sharedMaterial = colors[1];
            
            GameObject currentButton = GameObject.FindGameObjectWithTag(seasons[pattern[p]]);

            StartCoroutine(currentButton.GetComponent<Button>().lightUp());
        }

        StartCoroutine(putDownBarrier());

    }

    IEnumerator putDownBarrier()
    {
        yield return new WaitForSeconds(1f);
        HideBarrier();
        //center.sharedMaterial = colors[2];
        wait_go.gameObject.SetActive(false);
        progress.gameObject.SetActive(true);
    }



    void completeListener(Button b)
    {

        StartCoroutine(b.lightUp());
        //choice is right and not last button in pattern
        if (b.tag == seasons[pattern[currentButton]] && currentButton + 1 != patternLength)
        {
            currentButton += 1;

            addWeatherEffect(b);

            progress_text();
            //Debug.Log("Hit1");
        }

        //choice was not right;
        else if (b.tag != seasons[pattern[currentButton]])
        {
            messedUpPattern = true;
            this.GetComponent<AudioSource>().Play();
            addWeatherEffect(b);
            //SceneManager.LoadScene("Over");
            //Debug.Log("Loser");
            //trigger some kind of game over screen
        }

        //choice is right and last button in pattern
        else if (b.tag == seasons[pattern[currentButton]] && currentButton + 1 == patternLength)
        {
            currentButton += 1;
            progress_text();
            round_text();

            addPattern();
            addWeatherEffect(b);

            StartCoroutine(teleportPlayer());

            currentButton = 0;
            //Debug.Log("Hit2");
        }

    }

    IEnumerator teleportPlayer()
    {
        round_text();
        yield return new WaitForSeconds(1);
        progress_text();
        Player.GetComponent<Transform>().position = new Vector3(0,6,0);

        wait_text();
    }

    void addWeatherEffect(Button b)
    {
        float randomX = UnityEngine.Random.Range(-26f, 26f);
        float randomZ = UnityEngine.Random.Range(-26f, 26f);
        //try to add function to avoid spawning under player and fix "Box" problem
        bool underPlayer = checkWithinCircle(Player.GetComponent<Transform>().position.x, Player.GetComponent<Transform>().position.z, 10f);
        bool inCircle = checkWithinCircle(randomX, randomZ, 26f);
        bool inCenter = checkWithinCircle(randomX, randomZ, 8f);

        if (!underPlayer && inCircle && !inCenter) 
        {
            if (b.CompareTag("Spring"))
            {
                GameObject thisPuddle = (GameObject)Instantiate(Puddle, new Vector3(randomX, 1.02f, randomZ), Quaternion.identity);
                weatherEffectsList.Add(thisPuddle);
            }

            if (b.CompareTag("Winter"))
            {
                GameObject thisIcePatch = (GameObject)Instantiate(IcePatch, new Vector3(randomX, 1.02f, randomZ), Quaternion.identity);
                weatherEffectsList.Add(thisIcePatch);
            }

            if (b.CompareTag("Fall"))
            {
                GameObject thisWind = (GameObject)Instantiate(Wind, new Vector3(randomX, 2.2f, randomZ), Quaternion.identity);
                weatherEffectsList.Add(thisWind);
            }

            if(b.CompareTag("Summer"))
            {
                GameObject thisFire = (GameObject)Instantiate(Fire, new Vector3(randomX, 2.2f, randomZ), Quaternion.identity);
                weatherEffectsList.Add(thisFire);
            }
        }
        else 
        {
            addWeatherEffect(b);
        }
    }

    bool checkWithinCircle(float x, float z, float radius)
    {
        float distance = distanceFromCenter(x, z);

        if (distance < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float distanceFromCenter(float x, float z)
    {
        double a = Math.Pow((0 - x), 2);
        double b = Math.Pow((0 - z), 2);

        double d = Math.Sqrt(a+b);
        float dist = (float)d;

        return dist;
    }

    void addPattern()
    {
        pattern.Add(UnityEngine.Random.Range(0,4));
        patternLength += 1;
        patternStart = true;
    }

    void HideBarrier()
    {
        barrier.SetActive(false);
    }

    void ShowBarrier()
    {
        barrier.SetActive(true);
    }
    void wait_text()
    {
        wait_go.text = "Wait";
    }

    void progress_text()
    {
        progress.text = "Progress : " + currentButton.ToString() + "/" + patternLength.ToString();
    }

    void round_text()
    {
        Round.text = "Round : " + patternLength.ToString();
    }

    IEnumerator clearWeatherEffectsList()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject effect in weatherEffectsList)
        {
            Destroy(effect);
        }
        weatherEffectsList.Clear();
    }
    void ClearWeatherEffects()
    {
        //yield return new WaitForSeconds(0.1f);
        foreach (GameObject effect in weatherEffectsList)
        {
            Destroy(effect);
        }
        weatherEffectsList.Clear();
    }
}
