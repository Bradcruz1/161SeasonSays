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

    public GameObject Puddle;
    public GameObject IcePatch;
    public GameObject Wind;
    
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

        if (this.CompareTag("Spring"))
        {
            Instantiate(Puddle, new Vector3(Random.Range(-28f, 28f), 2f, Random.Range(-28f, 28f)), Quaternion.identity);
        }

        if (this.CompareTag("Winter"))
        {
            Instantiate(IcePatch, new Vector3(Random.Range(-28f, 28f), 2.2f, Random.Range(-28f, 28f)), Quaternion.identity);
        }

        if (this.CompareTag("Fall"))
        {
            Instantiate(Wind, new Vector3(Random.Range(-28f, 28f), 2.2f, Random.Range(-28f, 28f)), Quaternion.identity);
        }

        if(this.CompareTag("Summer"))
        {
            
        }

    }

    public IEnumerator lightUp()
    {
        material.color = Color.white;

        yield return new WaitForSeconds(1);

        material.color = color;
    }
}
