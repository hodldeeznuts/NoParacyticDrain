using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace NoParacyticDrain
{
    public class NoDrain : Mod
    {
        public NoDrain(ModContentPack content) : base (content)
        {

        }
    }

    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        private static readonly Type patchType = typeof(HarmonyPatches);

        static HarmonyPatches()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("com.github.harmony.rimworld.mod.noparacyticdrain");

            harmony.Patch(
                    original: AccessTools.Method(
                            type: typeof(ListerThings),
                            name: nameof(ListerThings.ThingsOfDef)
                        ),
                    postfix: new HarmonyMethod(type: patchType, name: nameof(FixParacyticCount))
                );
        }

        static List<Thing> FixParacyticCount(List<Thing> __result)
        {
            if (__result.Count > 0 && __result[0].def.defName == "TM_Plant_Paracyte")
            {
                return new List<Thing>();
            }

            return __result;
        }
    }
}
