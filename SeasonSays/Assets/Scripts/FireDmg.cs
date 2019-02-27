using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireDmg : MonoBehaviour
{
    public int health = 30;
    public int damage = 5;
    public Text healthbar;
    // Start is called before the first frame update
    void Start()
    {
        HealthText();
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthText();
        if (health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Over");
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            health -= damage;
        }
    }
    
    void HealthText()
    {
        healthbar.text = "Health: " + health.ToString();

    }
}
