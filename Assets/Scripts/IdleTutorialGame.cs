using UnityEngine; 
using UnityEngine.UI; 
using System;
using BreakInfinity;
using static BreakInfinity.BigDouble;


public class IdleTutorialGame : MonoBehaviour 
{ 

    public Text coinsText; 
    public Text clickValueText; 
    public Text coinsPerSecText; 
    public Text clickUpgrade1Text; 
    public Text clickUpgrade2Text; 
    public Text productionUpgrade1Text; 
    public Text productionUpgrade2Text; 
    public Text gemsText;
    public Text gemBoostText;
    public Text gemsToGetText;
    public Text clickUpgrade1MaxText;
    public Text clickUpgrade2MaxText;
    public Text productionUpgrade1MaxText;
    public Text productionUpgrade2MaxText;

    public BigDouble coins; 
    public BigDouble coinsPerSecond; 
    public BigDouble coinsClickValue; 
    public BigDouble productionUpgrade2Power;
    public BigDouble gems;
    public BigDouble gemBoost;
    public BigDouble gemsToGet;

    public BigDouble clickUpgrade1Level;
    public BigDouble clickUpgrade2Level; 
    public BigDouble productionUpgrade1Level;     
    public BigDouble productionUpgrade2Level; 
    public BigDouble tabSwitcher;

    public Image clickUpgrade1Bar;
    public Image clickUpgrade2Bar;
    public Image productionUpgrade1Bar;
    public Image productionUpgrade2Bar;

    public CanvasGroup mainMenuGroup;
    public CanvasGroup upgradesGroup;
 
 
    public void Start()  
    { 
        Application.targetFrameRate = 60;

        CanvasGroupChanger(true, mainMenuGroup);
        CanvasGroupChanger(false, upgradesGroup);

        tabSwitcher = 0;

        Load();

    } 
    //todo study this. Creating methods should help clean the code drastically.
    public void CanvasGroupChanger(bool x, CanvasGroup y)
    {
        if (x)
        {
            y.alpha = 1;
            y.interactable = true;
            y.blocksRaycasts = true;
            return;
        }
        y.alpha = 0;
        y.interactable = false;
        y.blocksRaycasts = false;
    }


    public void Load()
    {
        coins = Parse(PlayerPrefs.GetString("coins", "0"));
        coinsClickValue = Parse(PlayerPrefs.GetString("coinsClickValue", "1")); 
        productionUpgrade2Power = Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5")); 

        gems = Parse(PlayerPrefs.GetString("gems", "0"));

        clickUpgrade1Level = Parse(PlayerPrefs.GetString("clickUpgrade1Level", "0")); 
        clickUpgrade2Level = Parse(PlayerPrefs.GetString("clickUpgrade2Level", "0"));  
        productionUpgrade1Level = Parse(PlayerPrefs.GetString("productionUpgrade1Level", "0")); 
        productionUpgrade2Level = Parse(PlayerPrefs.GetString("productionUpgrade2Level", "0")); 
    }
 
    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("coinsClickValue", coinsClickValue.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());

        PlayerPrefs.SetString("gems", gems.ToString());

        PlayerPrefs.SetString("clickUpgrade1Level", clickUpgrade1Level.ToString());
        PlayerPrefs.SetString("clickUpgrade2Level", clickUpgrade2Level.ToString());
        PlayerPrefs.SetString("productionUpgrade1Level", productionUpgrade1Level.ToString());
        PlayerPrefs.SetString("productionUpgrade2Level", productionUpgrade2Level.ToString());
        

    }

    public void Reset()
    {
        coins = 0;
        coinsClickValue = 1; 
        productionUpgrade2Power = 5; 
        clickUpgrade1Level = 0; 
        clickUpgrade2Level = 0; 
        productionUpgrade1Level = 0; 
        productionUpgrade2Level = 0;
        gems = 0;
        gemBoost = 0;
    }

    
        public BigDouble BuyClickUpgrade1MaxCount()
    {
        var b = 10;
        var c = coins;
        var r = 1.07;
        var k = clickUpgrade1Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));
        return n;
    }

        public BigDouble BuyClickUpgrade2MaxCount()
    {
        var b = 100;
        var c = coins;
        var r = 1.09;
        var k = clickUpgrade2Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));
        return n;
    }

        public BigDouble BuyProductionUpgrade1MaxCount()
    {
        var b = 25;
        var c = coins;
        var r = 1.07;
        var k = productionUpgrade1Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));
        return n;
    }
        public BigDouble BuyProductionUpgrade2MaxCount()
    {
        var b = 250;
        var c = coins;
        var r = 1.09;
        var k = productionUpgrade2Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));
        return n;
    }



    public void Update()  
    { 
        //*Gem Boost System
        gemsToGet = (150 * Sqrt(coins / 1e7));
        gemBoost = (gems * 0.05) + 1;
        //todo Prestige starts to show +1 at around 400 coins. Fix
        gemsToGetText.text = "Prestige:\n+" + Floor(gemsToGet).ToString("F0") + " Gems";
        gemsText.text = "Gems: " + Floor(gems).ToString("F0");
        gemBoostText.text = gemBoost.ToString("F2") + "x boost";

        coinsPerSecond = productionUpgrade1Level + (productionUpgrade2Power * productionUpgrade2Level); 
 
        clickValueText.text = "Click\n+" + NotationMethod(coinsClickValue, y: "F0") + " Coins";
        coinsText.text = "Coins: " + NotationMethod(coins, y: "F0");
        coinsPerSecText.text = coinsPerSecond.ToString("F0") + " coins/s"; 
 
        //* Click Updgrade 1 Cost and Level exponent system 
        string clickUpgrade1CostString; 
        var clickUpgrade1Cost = 10 * Pow(1.07, clickUpgrade1Level);
        if (clickUpgrade1Cost > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(clickUpgrade1Cost)))); 
            var mantissa = (clickUpgrade1Cost / Pow(10, exponent)); 
            clickUpgrade1CostString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            clickUpgrade1CostString = clickUpgrade1Cost.ToString("F0"); 
 
        string clickUpgrade1LevelString; 
        if (clickUpgrade1Level > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(clickUpgrade1Level)))); 
            var mantissa = (clickUpgrade1Level / Pow(10, exponent)); 
            clickUpgrade1LevelString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            clickUpgrade1LevelString = clickUpgrade1Level.ToString("F0"); 
 
        //* Click Updgrade 2 Cost and Level exponent system 
        string clickUpgrade2CostString; 
        var clickUpgrade2Cost = 100 * Pow(1.09, clickUpgrade2Level);
        if (clickUpgrade2Cost > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(clickUpgrade2Cost)))); 
            var mantissa = (clickUpgrade2Cost / Pow(10, exponent)); 
            clickUpgrade2CostString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            clickUpgrade2CostString = clickUpgrade2Cost.ToString("F0"); 
 
        string clickUpgrade2LevelString; 
        if (clickUpgrade2Level > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(clickUpgrade2Level)))); 
            var mantissa = (clickUpgrade2Level / Pow(10, exponent)); 
            clickUpgrade2LevelString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            clickUpgrade2LevelString = clickUpgrade2Level.ToString("F0");     
 
 
 
        //* Production Upgrade 1 Cost and Level exponent system 
 
        string productionUpgrade1CostString; 
        var productionUpgrade1Cost = 25 * Pow(1.07, productionUpgrade1Level);
        if (productionUpgrade1Cost > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(productionUpgrade1Cost)))); 
            var mantissa = (productionUpgrade1Cost / Pow(10, exponent)); 
            productionUpgrade1CostString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            productionUpgrade1CostString = productionUpgrade1Cost.ToString("F0"); 
 
        string productionUpgrade1LevelString; 
        if (productionUpgrade1Level > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(productionUpgrade1Level)))); 
            var mantissa = (productionUpgrade1Level / Pow(10, exponent)); 
            productionUpgrade1LevelString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            productionUpgrade1LevelString = productionUpgrade1Level.ToString("F0");   
 
 
        //* Production Upgrade 2 Cost and Level exponent system 
 
        string productionUpgrade2CostString; 
        var productionUpgrade2Cost = 250 * Pow(1.09, productionUpgrade2Level);
        if (productionUpgrade2Cost > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(productionUpgrade2Cost)))); 
            var mantissa = (productionUpgrade2Cost / Pow(10, exponent)); 
            productionUpgrade2CostString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            productionUpgrade2CostString = productionUpgrade2Cost.ToString("F0"); 
 
        string productionUpgrade2LevelString; 
        if (productionUpgrade2Level > 1000) 
        { 
            var exponent = (Floor(Log10(Abs(productionUpgrade2Level)))); 
            var mantissa = (productionUpgrade2Level / Pow(10, exponent)); 
            productionUpgrade2LevelString = mantissa.ToString("F2") + "e" + exponent; 
        }     
        else 
            productionUpgrade2LevelString = productionUpgrade2Level.ToString("F0");   
 
        //*Value Exponents 
        
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + " coins\nPower: +" + gemBoost.ToString("F2") + "Click\nLevel: " + clickUpgrade1LevelString; 
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2CostString + " coins\nPower: +" + (productionUpgrade2Power * gemBoost).ToString("F2") + "Click\nLevel: " + clickUpgrade2LevelString; 
 
        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1CostString + "coins\nPower: +" + gemBoost.ToString("F2") + "coins/s\nLevel: " + productionUpgrade1LevelString; 
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2CostString + "coins\nPower: +" + (productionUpgrade2Power * gemBoost).ToString("F2") + "coins/s\nLevel: " + productionUpgrade2LevelString; 
            



        coins += coinsPerSecond * Time.deltaTime; 

        //*progress bars

        // Click Upgrade 1 Bar
        if (coins / clickUpgrade1Cost < 0.01)
            clickUpgrade1Bar.fillAmount = 0;
        else if (coins / clickUpgrade1Cost < 0)    
            clickUpgrade1Bar.fillAmount = 1;
        else    
            clickUpgrade1Bar.fillAmount = (float) (coins / clickUpgrade1Cost).ToDouble();

        // Click Upgrade 2 Bar
        if (coins / clickUpgrade2Cost < 0.01)
            clickUpgrade2Bar.fillAmount = 0;
        else if (coins / clickUpgrade2Cost < 0)    
            clickUpgrade2Bar.fillAmount = 1;
        else    
            clickUpgrade2Bar.fillAmount = (float) (coins / clickUpgrade2Cost).ToDouble();

        // Production Upgrade 1 Bar
        if (coins / productionUpgrade1Cost < 0.01)
            productionUpgrade1Bar.fillAmount = 0;
        else if (coins / productionUpgrade1Cost < 0)    
            productionUpgrade1Bar.fillAmount = 1;
        else    
            productionUpgrade1Bar.fillAmount = (float) (coins / productionUpgrade1Cost).ToDouble();

        // Production Upgrade 2 Bar
        if (coins / productionUpgrade2Cost < 0.01)
            productionUpgrade2Bar.fillAmount = 0;
        else if (coins / productionUpgrade2Cost < 0)    
            productionUpgrade2Bar.fillAmount = 1;
        else    
            productionUpgrade2Bar.fillAmount = (float) (coins / productionUpgrade2Cost).ToDouble();





        clickUpgrade1MaxText.text = "Buy Max (" + BuyClickUpgrade1MaxCount() + ")";

        clickUpgrade2MaxText.text = "Buy Max (" + BuyClickUpgrade2MaxCount() + ")";

        productionUpgrade1MaxText.text = "Buy Max (" + BuyProductionUpgrade1MaxCount() + ")";

        productionUpgrade2MaxText.text = "Buy Max (" + BuyProductionUpgrade2MaxCount() + ")";

        Save();
    } 


        public string NotationMethod(BigDouble x, string y)
        {
        if (x > 1000)
        {
            var exponent = Floor(Log10(Abs(x))); 
            var mantissa = x / Pow(10, exponent); 
            return mantissa.ToString( format: "F2") + "e" + exponent;
        } 
            return x.ToString(y);

        }

        public string NotationMethod(float x, string y)
        {
        if (x > 1000)
        {
            var exponent = Mathf.Floor(Mathf.Log10(Mathf.Abs(x))); 
            var mantissa = x / Mathf.Pow(10, exponent); 
            return mantissa.ToString( format: "F2") + "e" + exponent;
        } 
            return x.ToString(y);

        }


    //*Button funcitons
    
    public void Prestige()
    {
        if (coins > 1000)
        {
        Reset();
        gems += gemsToGet;
        
        }
    }
 
    public void Click() 
    { 
        coins += coinsClickValue; 
    } 

    //* For Testing Only
        public void Rich() 
    { 
        coins = 1000;
    } 
 
    public void BuyClickUpgrade1() 
    { 
        var cost = 10 * Pow(1.07, clickUpgrade1Level);
        if (coins >= cost) 
        { 
            
            clickUpgrade1Level++; 
            coins -= cost; 
            
            coinsClickValue++; 
        } 
    }

    public void BuyClickUpgrade1Max()
    {
        var b = 10;
        var c = coins;
        var r = 1.07;
        var k = clickUpgrade1Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (coins >= cost)
        {
            clickUpgrade1Level += n;
            coins -= cost;
            coinsClickValue += n;
        }
    } 
 
    public void BuyClickUpgrade2() 
    { 
        var cost = 100 * Pow(1.09, clickUpgrade2Level);
        if (coins >= cost) 
        { 
            clickUpgrade2Level++; 
            coins -= cost; 
             
            coinsClickValue += 5; 
        } 
    } 
    
    public void BuyClickUpgrade2Max()
    {
        var b = 100;
        var c = coins;
        var r = 1.09;
        var k = clickUpgrade2Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (coins >= cost)
        {
            clickUpgrade2Level += n;
            coins -= cost;
            coinsClickValue += n * 5;
        }
    }
 
    public void BuyProductionUpgrade1() 
        { 
            var cost = 25 * Pow(1.07, productionUpgrade1Level);
            if (coins >= cost) 
            { 
                productionUpgrade1Level++; 
                coins -= cost; 
                
            } 
        } 

        public void BuyProductionUpgrade1Max()
    {
        var b = 25;
        var c = coins;
        var r = 1.07;
        var k = productionUpgrade1Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (coins >= cost)
        {
            productionUpgrade1Level += n;
            coins -= cost;
            
        }
    }    
 
    public void BuyProductionUpgrade2() 
        { 
            var cost = 250 * Pow(1.09, productionUpgrade2Level);
            if (coins >= cost) 
            { 
                productionUpgrade2Level++; 
                coins -= cost; 
                
            } 
        } 

        public void BuyProductionUpgrade2Max()
    {
        var b = 250;
        var c = coins;
        var r = 1.09;
        var k = productionUpgrade2Level;
        var n = Floor(Log((c * (r - 1)) / (b * Pow(r, k)) +1, r));

        var cost = b * (Pow(r, k) * (Pow(r, n) - 1) / (r - 1));

        if (coins >= cost)
        {
            productionUpgrade2Level += n;
            coins -= cost;
            
        }
    } 
    //todo important for switching windows
    public void ChagneTabs(string id)
    {
        switch (id)
        {
            case "Upgrades":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(true, upgradesGroup);
               
                break;
            case "Main":
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
   
                break;
        }
    }
 
} 



