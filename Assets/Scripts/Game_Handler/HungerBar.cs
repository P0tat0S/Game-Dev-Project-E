using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour {
    //Hunger system setup
    private HungerSystem hungerSystem;
    public void Setup(HungerSystem hungerSystem) {
        this.hungerSystem = hungerSystem;

        hungerSystem.OnHungerChanged += HungerSystem_OnHungerChanged;
    }

    //Event handler that will scale the hungerbar in relation to the percentage
    private void HungerSystem_OnHungerChanged(object sender, System.EventArgs e) {
        transform.Find("Bar").localScale =new Vector3(hungerSystem.GetHungerPercentage(), 1);
    }
}
