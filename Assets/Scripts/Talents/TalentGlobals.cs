
public static class TalentGlobals 
{
    public static int Money { get; private set; } = 0;
    public static int AntibioCost { get; private set; } = 0;
    public static int FungiCost { get; private set; } = 0;
    public static int VaccineCost { get; private set; } = 0;
    public static int VitaminCost { get; private set; } = 0;

    public static float VaccineEffect { get; private set; } = 0;
    public static float VitaminEffect { get; private set; } = 0;
    public static float StartImmuneDefense { get; private set; } = 0;

    public static float VirusSpreadChance { get; private set; } = 0;
    public static float BacteriaSpreadChance { get; private set; } = 0;
    public static float FungiSpreadChance { get; private set; } = 0;

    public static bool EmergencyActive { get; private set; } = false;
    public static bool MegaAntibioActive { get; private set; } = false;
    public static bool MegaVitaminActive { get; private set; } = false;


    public static void LoadTalentGlobals()
    {
        if(ES3.FileExists("Save/TalentGlobals"))
        {
            Money = ES3.Load<int>("Money", "Save/TalentGlobals");
            AntibioCost = ES3.Load<int>("AntibioCost", "Save/TalentGlobals");
            FungiCost = ES3.Load<int>("FungiCost", "Save/TalentGlobals");
            VaccineCost = ES3.Load<int>("VaccineCost", "Save/TalentGlobals");
            VitaminCost = ES3.Load<int>("VitaminCost", "Save/TalentGlobals");
            VaccineEffect = ES3.Load<float>("VaccineEffect", "Save/TalentGlobals");
            VitaminEffect = ES3.Load<float>("VitaminEffect", "Save/TalentGlobals");
            StartImmuneDefense = ES3.Load<float>("StartImmuneDefense", "Save/TalentGlobals");
            VirusSpreadChance = ES3.Load<float>("VirusSpreadChance", "Save/TalentGlobals");
            BacteriaSpreadChance = ES3.Load<float>("BacteriaSpreadChance", "Save/TalentGlobals");
            FungiSpreadChance = ES3.Load<float>("FungiSpreadChance", "Save/TalentGlobals");
            EmergencyActive = ES3.Load<bool>("EmergencyActive", "Save/TalentGlobals");
            MegaAntibioActive = ES3.Load<bool>("MegaAntibioActive", "Save/TalentGlobals");
            MegaVitaminActive = ES3.Load<bool>("MegaVitaminActive", "Save/TalentGlobals");
        }
        else
        {
            ES3.Save("Money", Money, "Save/TalentGlobals");
            ES3.Save("AntibioCost", AntibioCost, "Save/TalentGlobals");
            ES3.Save("FungiCost", FungiCost, "Save/TalentGlobals");
            ES3.Save("VaccineCost", VaccineCost, "Save/TalentGlobals");
            ES3.Save("VitaminCost", VitaminCost, "Save/TalentGlobals");
            ES3.Save("VaccineEffect", VaccineEffect, "Save/TalentGlobals");
            ES3.Save("VitaminEffect", VitaminEffect, "Save/TalentGlobals");
            ES3.Save("StartImmuneDefense", StartImmuneDefense, "Save/TalentGlobals");
            ES3.Save("VirusSpreadChance", VirusSpreadChance, "Save/TalentGlobals");
            ES3.Save("BacteriaSpreadChance", BacteriaSpreadChance, "Save/TalentGlobals");
            ES3.Save("FungiSpreadChance", FungiSpreadChance, "Save/TalentGlobals");
            ES3.Save("EmergencyActive", EmergencyActive, "Save/TalentGlobals");
            ES3.Save("MegaAntibioActive", MegaAntibioActive, "Save/TalentGlobals");
            ES3.Save("MegaVitaminActive", MegaVitaminActive, "Save/TalentGlobals");
        }
        
    }

    public static void  ChangeMoney(int money)
    {
        Money += money;
        ES3.Save("Money", Money, "Save/TalentGlobals");
    }

    public static void ChangeAntibioCost(int cost)
    {
        AntibioCost += cost;
        ES3.Save("AntibioCost", AntibioCost, "Save/TalentGlobals");
    }

    public static void ChangeFungiCost(int cost)
    {
        FungiCost += cost;
        ES3.Save("FungiCost", FungiCost, "Save/TalentGlobals");
    }

    public static void ChangeVaccineCost(int cost)
    {
        VaccineCost += cost;
        ES3.Save("VaccineCost", VaccineCost, "Save/TalentGlobals");
    }

    public static void ChangeVitaminCost(int cost)
    {
        VitaminCost += cost;
        ES3.Save("VitaminCost", VitaminCost, "Save/TalentGlobals");
    }

    public static void ChangeVaccineEffect(float effect)
    {
        VaccineEffect += effect;
        ES3.Save("VaccineEffect", VaccineEffect, "Save/TalentGlobals");
    }

    public static void ChangeVitaminEffect(float effect)
    {
        VitaminEffect += effect;
        ES3.Save("VitaminEffect", VitaminEffect, "Save/TalentGlobals");
    }

    public static void ChangeStartImmuneDefense(float defense)
    {
        StartImmuneDefense += defense;
        ES3.Save("StartImmuneDefense", StartImmuneDefense, "Save/TalentGlobals");
    }

    public static void ChangeVirusSpreadChance(float spreadChance)
    {
        VirusSpreadChance += spreadChance;
        ES3.Save("VirusSpreadChance", VirusSpreadChance, "Save/TalentGlobals");
    }

    public static void ChangeBacteriaSpreadChance(float spreadChance)
    {
        BacteriaSpreadChance += spreadChance;
        ES3.Save("BacteriaSpreadChance", BacteriaSpreadChance, "Save/TalentGlobals");
    }

    public static void ChangeFungiSpreadChance(float spreadChance)
    {
        FungiSpreadChance += spreadChance;
        ES3.Save("FungiSpreadChance", FungiSpreadChance, "Save/TalentGlobals");
    }

    public static void ActivateEmergency()
    {
        EmergencyActive = true;
        ES3.Save("EmergencyActive", true, "Save/TalentGlobals");
    }

    public static void ActivateMegaAntibio()
    {
        MegaAntibioActive = true;
        ES3.Save("MegaAntibioActive", true, "Save/TalentGlobals");
    }

    public static void ActivateMegaVitamin()
    {
        MegaVitaminActive = true;
        ES3.Save("MegaVitaminActive", true, "Save/TalentGlobals");
    }

    

}
