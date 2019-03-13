using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonUnityEvent: UnityEvent<Button> { };

public class Button : MonoBehaviour
{
    private Material material;
    private Color color;
    public ButtonUnityEvent buttonHit = new ButtonUnityEvent();
    private bool checkedMessedUp = false;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        color = material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        var normal = other.contacts[0].normal;
        Debug.Log(normal.y);
        if (normal.y == -1)
        {
            Debug.Log("bump");
            if (other.collider.CompareTag("Player")) 
            {
                buttonHit.Invoke(this);
            }
        }
    }



    public IEnumerator lightUp()
    {
        material.color *= 2f;
        PlayButtonSounds();
        yield return new WaitForSeconds(.5f);

        material.color = color;
    }

    void PlayButtonSounds()
    {
        ButtonManager bm = this.transform.GetComponentInParent<ButtonManager>();
        if (bm.patternStart)
            checkedMessedUp = false;
        if (!bm.messedUpPattern && !checkedMessedUp)
        {
            checkedMessedUp = true;
            this.GetComponent<AudioSource>().Play();
        }
        else if (checkedMessedUp)
        {
            this.GetComponent<AudioSource>().Play();
        }

    }
}
