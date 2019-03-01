using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{
    public GameObject barrier;
    public Text wait_go;
    public Material[] colors;
    public Renderer center;
    public Text progress;
    public Text Round;
    private int curr;
    private int total;
    private int round;


    void Start()
    {
        center.sharedMaterial = colors[0];
        curr = 0;
        total = 10;
        round = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //curr++;
        progress_text();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Go")
        {
            HideBarrier();
            go_text();
            center.sharedMaterial = colors[2];
        

        }
        else if (other.gameObject.tag == "Wait")
        {
            ShowBarrier();
            wait_text();
            center.sharedMaterial = colors[1];
            
        }
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

    void go_text()
    {
        wait_go.text = "Go";
    }

    void progress_text()
    {
        progress.text = "Progress: " + curr.ToString() + "/" + total.ToString();
    }

    void round_text()
    {
        Round.text = "Round: " + round.ToString();
    }

    
}
