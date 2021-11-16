using System;

public class HungerSystem{
    public event EventHandler OnHungerChanged;
    private float hunger;
    private float hungerMax;


    /*********************
        Helper Functions
    **********************/
    public HungerSystem(float hungerMax) {//Constructor
        this.hungerMax = hungerMax;
        hunger = hungerMax;
    }

    public float GetHunger(){
        return hunger;
    }

    public float GetHungerPercentage(){
        return (float)hunger / hungerMax;
    }

    public void Starve(float starveAmount){
        hunger -= starveAmount;
        if (hunger < 0) hunger = 0;

        if (OnHungerChanged != null) OnHungerChanged(this, EventArgs.Empty);
    }

    public void Eat(float eatAmount){
        hunger += eatAmount;
        if (hunger > hungerMax) hunger = hungerMax;

        if (OnHungerChanged != null) OnHungerChanged(this, EventArgs.Empty);
    }

}
