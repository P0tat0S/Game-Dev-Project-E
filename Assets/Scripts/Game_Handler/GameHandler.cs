using UnityEngine;

public class GameHandler : MonoBehaviour{
    public Transform pfHealthBar;

    private void Start(){
        HealthSystem healthSystem = new HealthSystem(100);//creation of healthbar

        //General healthbar with no reference
        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(-19,11), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: "+healthSystem.GetHealth());

        healthSystem.Damage(10);
        Debug.Log("Damage: "+healthSystem.GetHealth());

        //healthSystem.Heal(10);
        //Debug.Log("Healed: "+healthSystem.GetHealth());

    }

}
