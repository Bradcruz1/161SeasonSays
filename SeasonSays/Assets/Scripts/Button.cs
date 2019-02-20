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
        if (other.collider.CompareTag("Player")) 
        {
            buttonHit.Invoke(this);
        }
    }

    public IEnumerator lightUp()
    {
        material.color = Color.white;

        yield return new WaitForSeconds(1);

        material.color = color;
    }
}
