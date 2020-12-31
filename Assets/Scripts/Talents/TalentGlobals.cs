
public static class TalentGlobals 
{
    
    public static int AntibioDiscount { get; private set; } = 0;
    public static int AntifungalDiscount { get; private set; } = 0;
    public static int VaccineDiscount { get; private set; } = 0;
    public static int VitaminDiscount { get; private set; } = 0;

    public static int ExtraMoney { get; private set; } = 0;
    public static float ExtraVaccineEffect { get; private set; } = 0;
    public static float ExtraVitaminEffect { get; private set; } = 0;
    public static float ExtraImmuneDefense { get; private set; } = 0;

    public static float VirusSpreadReduction { get; private set; } = 0;
    public static float BacteriaSpreadReduction { get; private set; } = 0;
    public static float FungiSpreadReduction { get; private set; } = 0;

    public static bool EmergencyActive { get; private set; } = false;
    public static bool MegaAntibioActive { get; private set; } = false;
    public static bool MegaVitaminActive { get; private set; } = false;


    public static void LoadTalentGlobals()
    {
        if(ES3.FileExists("Save/TalentGlobals"))
        {

            AntibioDiscount = ES3.Load<int>("AntibioDiscount", "Save/TalentGlobals");
            AntifungalDiscount = ES3.Load<int>("AntifungalDiscount", "Save/TalentGlobals");
            VaccineDiscount = ES3.Load<int>("VaccineDiscount", "Save/TalentGlobals");
            VitaminDiscount = ES3.Load<int>("VitaminDiscount", "Save/TalentGlobals");

            ExtraMoney = ES3.Load<int>("ExtraMoney", "Save/TalentGlobals");
            ExtraVaccineEffect = ES3.Load<float>("ExtraVaccineEffect", "Save/TalentGlobals");
            ExtraVitaminEffect = ES3.Load<float>("ExtraVitaminEffect", "Save/TalentGlobals");
            ExtraImmuneDefense = ES3.Load<float>("ExtraImmuneDefense", "Save/TalentGlobals");

            VirusSpreadReduction = ES3.Load<float>("VirusSpreadReduction", "Save/TalentGlobals");
            BacteriaSpreadReduction = ES3.Load<float>("BacteriaSpreadReduction", "Save/TalentGlobals");
            FungiSpreadReduction = ES3.Load<float>("FungiSpreadReduction", "Save/TalentGlobals");

            EmergencyActive = ES3.Load<bool>("EmergencyActive", "Save/TalentGlobals");
            MegaAntibioActive = ES3.Load<bool>("MegaAntibioActive", "Save/TalentGlobals");
            MegaVitaminActive = ES3.Load<bool>("MegaVitaminActive", "Save/TalentGlobals");
        }
        else
        {
            
            ES3.Save("AntibioDiscount", AntibioDiscount, "Save/TalentGlobals");
            ES3.Save("AntifungalDiscount", AntifungalDiscount, "Save/TalentGlobals");
            ES3.Save("VaccineDiscount", VaccineDiscount, "Save/TalentGlobals");
            ES3.Save("VitaminDiscount", VitaminDiscount, "Save/TalentGlobals");

            ES3.Save("ExtraMoney", ExtraMoney, "Save/TalentGlobals");
            ES3.Save("ExtraVaccineEffect", ExtraVaccineEffect, "Save/TalentGlobals");
            ES3.Save("ExtraVitaminEffect", ExtraVitaminEffect, "Save/TalentGlobals");
            ES3.Save("ExtraImmuneDefense", ExtraImmuneDefense, "Save/TalentGlobals");

            ES3.Save("VirusSpreadReduction", VirusSpreadReduction, "Save/TalentGlobals");
            ES3.Save("BacteriaSpreadReduction", BacteriaSpreadReduction, "Save/TalentGlobals");
            ES3.Save("FungiSpreadReduction", FungiSpreadReduction, "Save/TalentGlobals");

            ES3.Save("EmergencyActive", EmergencyActive, "Save/TalentGlobals");
            ES3.Save("MegaAntibioActive", MegaAntibioActive, "Save/TalentGlobals");
            ES3.Save("MegaVitaminActive", MegaVitaminActive, "Save/TalentGlobals");
        }
        
    }

   

    public static void ChangeAntibioDiscount(int discount)
    {
        AntibioDiscount += discount;
        ES3.Save("AntibioDiscount", AntibioDiscount, "Save/TalentGlobals");
    }

    public static void ChangeAntifungalDiscount(int discount)
    {
        AntifungalDiscount += discount;
        ES3.Save("AntifungalDiscount", AntifungalDiscount, "Save/TalentGlobals");
    }

    public static void ChangeVaccineDiscount(int discount)
    {
        VaccineDiscount += discount;
        ES3.Save("VaccineDiscount", VaccineDiscount, "Save/TalentGlobals");
    }

    public static void ChangeVitaminDiscount(int discount)
    {
        VitaminDiscount += discount;
        ES3.Save("VitaminDiscount", VitaminDiscount, "Save/TalentGlobals");
    }




    public static void ChangeExtraMoney(int money)
    {
        ExtraMoney += money;
        ES3.Save("Money", ExtraMoney, "Save/TalentGlobals");
    }

    public static void ChangeExtraVaccineEffect(float effect)
    {
        ExtraVaccineEffect += effect;
        ES3.Save("ExtraVaccineEffect", ExtraVaccineEffect, "Save/TalentGlobals");
    }

    public static void ChangeExtraVitaminEffect(float effect)
    {
        ExtraVitaminEffect += effect;
        ES3.Save("ExtraVitaminEffect", ExtraVitaminEffect, "Save/TalentGlobals");
    }

    public static void ChangeExtraImmuneDefense(float defense)
    {
        ExtraImmuneDefense += defense;
        ES3.Save("ExtraImmuneDefense", ExtraImmuneDefense, "Save/TalentGlobals");
    }





    public static void ChangeVirusSpreadReduction(float spreadReduction)
    {
        VirusSpreadReduction += spreadReduction;
        ES3.Save("VirusSpreadReduction", VirusSpreadReduction, "Save/TalentGlobals");
    }

    public static void ChangeBacteriaSpreadReduction(float spreadReduction)
    {
        BacteriaSpreadReduction += spreadReduction;
        ES3.Save("BacteriaSpreadReduction", BacteriaSpreadReduction, "Save/TalentGlobals");
    }

    public static void ChangeFungiSpreadReduction(float spreadReduction)
    {
        FungiSpreadReduction += spreadReduction;
        ES3.Save("FungiSpreadReduction", FungiSpreadReduction, "Save/TalentGlobals");
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
