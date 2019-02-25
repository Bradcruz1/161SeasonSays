using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public List<GameObject> Seasons;

    private int patternLength = 1;

    private int currentButton = 0;

    private List<int> pattern = new List<int>();

    private List<string> seasons = new List<string>();

    private bool patternStart = true;

    void Awake()
    {
        pattern.Add(Random.Range(0, 4));
        
        seasons.Add("Spring");
        seasons.Add("Summer");
        seasons.Add("Fall");
        seasons.Add("Winter");
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
    }

    // Update is called once per frame
    void Update()
    {
        if (patternStart)
        {
            playPattern();
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
            Debug.Log(p);
            Debug.Log(seasons[pattern[p]]);

            yield return new WaitForSeconds(2);
            
            GameObject currentButton = GameObject.FindGameObjectWithTag(seasons[pattern[p]]);

            StartCoroutine(currentButton.GetComponent<Button>().lightUp());

        }

    }



    void completeListener(Button b)
    {
        if (b.tag == seasons[pattern[currentButton]] && currentButton + 1 != patternLength)
        {
            currentButton += 1;
        }

        else if (b.tag != seasons[pattern[currentButton]])
        {
            Debug.Log("Loser");
            //trigger some kind of game over screen
        }

        else if (b.tag == seasons[pattern[currentButton]] && currentButton + 1 == patternLength)
        {
            addPattern();
            currentButton = 0;
        }

        
    }

    void addPattern()
    {
        pattern.Add(Random.Range(0,4));
        patternLength += 1;
        patternStart = true;
    }
}
