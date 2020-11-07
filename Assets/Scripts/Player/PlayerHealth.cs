using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int healthVal = 100;

    private Slider healthSlider;
    private GameObject uiScore;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthSlider.value = healthVal;

        uiScore = GameObject.Find("UIHolder");
    }

    public void Damage(int damage)
    {
        healthVal -= damage;
        if(healthVal < 0)
        {
            healthVal = 0;
        }

        healthSlider.value = healthVal;

        if(healthVal == 0)
        {
            uiScore.SetActive(false);
            GameplayManager.instance.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
