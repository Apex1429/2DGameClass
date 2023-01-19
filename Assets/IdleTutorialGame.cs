using UnityEngine.UI;
using UnityEngine;

public class IdleTutorialGame : MonoBehaviour
{
    //Episdoe 1
    public Text coinsText;
    public Text clickValueText;
    public double coins;
    public double coinsClickValue;

    //Episde 2
    public Text coinsPerSecText;
    public Text clickUpgrade1Text;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public double coinsPerSecond;
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;

    public double productionUpgrade1Cost;
    public int productionUpgrade1Level;

    //Episode 3

    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;    

    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;


    // Default prices
    public void Start() 
    {
        
        clickUpgrade1Cost = 10;
        clickUpgrade2Cost = 100;
        productionUpgrade1Cost = 25;
        coinsClickValue = 1;
        productionUpgrade2Cost = 250;
        productionUpgrade2Power = 5;
    }

    public void Update() 
    {
        coinsPerSecond = productionUpgrade1Level + (productionUpgrade2Power * productionUpgrade2Level);

        clickValueText.text = "Click\n+" + coinsClickValue + " Coins";
        coinsText.text = "Coins: " + coins.ToString("F0");
        coinsPerSecText.text = coinsPerSecond.ToString("F0") + " coins/s";

        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1Cost.ToString("F0") + "coins\nPower: +1 Click\nLevel: " + clickUpgrade1Level;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0") + "coins\nPower: +5 Click\nLevel: " + clickUpgrade2Level;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1Cost.ToString("F0") + "coins\nPower: +1 coins/s\nLevel: " + productionUpgrade1Level;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2Cost.ToString("F0") + "coins\nPower: +"  + productionUpgrade2Power + " coins/s\nLevel: " + productionUpgrade2Level;

        coins += coinsPerSecond * Time.deltaTime;

    }

    public void Click()
    {
        coins += coinsClickValue;
    }

    public void BuyClickUpgrade1()
    {
        if (coins >= clickUpgrade1Cost)
        {
            clickUpgrade1Level++;
            coins -= clickUpgrade1Cost;
            clickUpgrade1Cost *= 1.07;
            coinsClickValue++;
        }


    }

    public void BuyClickUpgrade2Cost()
    {
        if (coins >= clickUpgrade2Cost)
        {
            clickUpgrade2Level++;
            coins -= clickUpgrade2Cost;
            clickUpgrade2Cost *= 1.09;
            coinsClickValue += 5;
        }
    }

    public void BuyProductionUpgrade1()
        {
            if (coins >= productionUpgrade1Cost)
            {
                productionUpgrade1Level++;
                coins -= productionUpgrade1Cost;
                productionUpgrade1Cost *= 1.07;
            }
        }

    public void BuyProductionUpgrade2()
        {
            if (coins >= productionUpgrade2Cost)
            {
                productionUpgrade2Level++;
                coins -= productionUpgrade2Cost;
                productionUpgrade2Cost *= 1.09;
            }
        }

}
