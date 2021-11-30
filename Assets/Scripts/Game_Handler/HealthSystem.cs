using System;

public class HealthSystem{
    public event EventHandler OnHealthChanged;
    private float health;
    private float healthMax;


    /*********************
        Helper Functions
    **********************/
    public HealthSystem(float healthMax) {//Constructor
        this.healthMax = healthMax;
        health = healthMax;
    }

    public float GetHealth(){
        return health;
    }

    public float GetHealthPercentage(){
        return (float)health / healthMax;
    }

    public void Damage(float damageAmount){
        health -= damageAmount;
        if (health < 0) health = 0;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(float healAmount){
        health += healAmount;
        if (health > healthMax) health = healthMax;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

}
