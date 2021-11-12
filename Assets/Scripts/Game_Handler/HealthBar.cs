using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    //Health system setup
    private HealthSystem healthSystem;
    public void Setup(HealthSystem healthSystem) {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    //Event handler that will scale the healthbar in relation to the percentage
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
        transform.Find("Bar").localScale =new Vector3(healthSystem.GetHealthPercentage(), 1);
    }
}
