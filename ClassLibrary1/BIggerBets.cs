using Il2CppScheduleOne.Casino;
using Il2CppScheduleOne.Casino.UI;
using MelonLoader;
using HarmonyLib;

namespace MultiplayerTrackerFix
{

    [HarmonyPatch(typeof(SlotMachine), ("Awake"))]
    public class Patch_SlothMachine_Awake : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("BiggerBets loaded!");
        }

        private static bool hasPatched = false; //checker bool
        public static void Prefix(SlotMachine __instance)
        {

            __instance.BetAmountLabel.enableAutoSizing = true; // left outside to update with every single slot machine
            int[] expandedBetRange = { 500, 1000, 5000, 10000 }; // expanded slot bet range
            if (hasPatched != true) // checks if patched already as BetAmounts only needs to be updated once and is shared between each instance of Slot Machine
            {


                foreach (int i in expandedBetRange) //appened BetAmounts array with values above
                {
                    SlotMachine.BetAmounts = SlotMachine.BetAmounts.Append<int>(i).ToArray();
                    MelonLogger.Msg($"Added {i} to the slot's bet amount ");
                }
            }

            if (hasPatched) return; //once this has been called flips the bool to true so the patch isn't applied 5 times as Awake is called 5 times, once for each Slot Machine
            {
                hasPatched = true;
                MelonLogger.Msg("Expanded slot bets has been patched already");
            }
        }


    }

   
    /* Fuckin constants
     * [HarmonyPatch(typeof(RTBGameController), "set_LocalPlayerBet")]
    public class Patch_RTBGameController_set_LocalPlayerBet
    {
        public static bool Prefix(ref float __result)
        {
            __result = 10f + (10000f - 10f) * sliderVal;
            MelonLogger.Msg("RTB bet maximum increased");
            return false;
        }

    }
    */
    
}
