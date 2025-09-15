// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection.Emit;
// using HarmonyLib;
//
// namespace MonkeStatistics.Patches;
//
// [HarmonyPatch(typeof(KIDManager), "OnSessionUpdated")]
// internal static class KIDManagerPatch
// {
//     public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//     {
//         var codes = instructions.AsList();
//
//         int endIndex = codes.IndexOf(OpCodes.Ret);
//
//         return codes;
//     }
// }
