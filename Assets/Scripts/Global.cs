using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Global
{
    public static IncomeManager incomeManager;
    public static AnnouncementHandler announcer;
    
    public static ItemManager itemManager;
    public static UpgradeManager upgradeManager;
    public static PerkManager perkManager;

    public static HoverBox hoverBox;
    public static RingClicked ringClicker;

    public static List<IncomeUpdateListener> incomeListeners = new();

    public static void InvokeLambda(Action action, float time)
    {
        MonoBehaviour script = new GameObject().AddComponent<Invoker>();
        script.StartCoroutine(script.GetComponent<Invoker>().InvokeCoroutine(action, time));
    }

    private class Invoker : MonoBehaviour
    {
        public IEnumerator InvokeCoroutine(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            if (isActiveAndEnabled)
            {
                action.Invoke();
                Destroy(gameObject);
            }

        }
    }

    private static string NumToDecimalString(BigInteger num, int pow)
    {
        int roundDecimals = 3;
        BigInteger output = BigInteger.Divide(num, BigInteger.Pow(10, pow));
        int outputLen = output.ToString().Length;
        string str = output.ToString() + "." + num.ToString().Substring(outputLen, roundDecimals);
        return float.Parse(str).ToString();
    }

    public static string LongToString(BigInteger num, bool excludesMillion=false)
    {
        int numDigits = num.ToString().Length;

        if (numDigits > 6 && numDigits <= (6 + 3) && !excludesMillion)
            return NumToDecimalString(num, 6) + " million";

        if (numDigits > 9 && numDigits <= (9 + 3))
            return NumToDecimalString(num, 9) + " billion";

        if (numDigits > 12 && numDigits <= (12 + 3))
            return NumToDecimalString(num, 12) + " trillion";

        if (numDigits > 15 && numDigits <= (15 + 3))
            return NumToDecimalString(num, 15) + " quadrillion";

        if (numDigits > 18 && numDigits <= (18 + 3))
            return NumToDecimalString(num, 18) + " quintillion";

        if (numDigits > 21 && numDigits <= (21 + 3))
            return NumToDecimalString(num, 21) + " sextillion";

        if (numDigits > 24 && numDigits <= (24 + 3))
            return NumToDecimalString(num, 24) + " septillion";

        if (numDigits > 27 && numDigits <= (27 + 3))
            return NumToDecimalString(num, 27) + " octillion";

        if (numDigits > 30 && numDigits <= (30 + 3))
            return NumToDecimalString(num, 30) + " nonillion";

        if (numDigits > 33 && numDigits <= (33 + 3))
            return NumToDecimalString(num, 33) + " decillion";

        if (numDigits > 36 && numDigits <= (36 + 3))
            return NumToDecimalString(num, 36) + " undecillion";

        if (numDigits > 39 && numDigits <= (39 + 3))
            return NumToDecimalString(num, 39) + " duodecillion";

        if (numDigits > 42 && numDigits <= (42 + 3))
            return NumToDecimalString(num, 42) + " tredecillion";

        if (numDigits > 45 && numDigits <= (45 + 3))
            return NumToDecimalString(num, 45) + " quattuordecillion";

        return $"{num:n0}";
    }
}
