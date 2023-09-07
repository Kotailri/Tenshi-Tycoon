using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static HoverBox hoverBox;
    public static IncomeController incomeController;

    public static string LongToString(long num)
    {
        int roundDecimals = 3;
        int numDigits = (int)Math.Floor(Mathf.Log10(num) + 1);

        if (numDigits > 9 && numDigits <= (9 + 3))
            return (Math.Round(num / Math.Pow(10, 9), roundDecimals)).ToString() + " billion";

        if (numDigits > 12 && numDigits <= (12 + 3))
            return (Math.Round(num / Math.Pow(10, 12), roundDecimals)).ToString() + " trillion";

        if (numDigits > 15 && numDigits <= (15 + 3))
            return (Math.Round(num / Math.Pow(10, 15), roundDecimals)).ToString() + " quadrillion";

        if (numDigits > 18 && numDigits <= (18 + 3))
            return (Math.Round(num / Math.Pow(10, 18), roundDecimals)).ToString() + " quintillion";

        if (numDigits > 21 && numDigits <= (21 + 3))
            return (Math.Round(num / Math.Pow(10, 21), roundDecimals)).ToString() + " sextillion";

        if (numDigits > 24 && numDigits <= (24 + 3))
            return (Math.Round(num / Math.Pow(10, 24), roundDecimals)).ToString() + " septillion";

        if (numDigits > 27 && numDigits <= (27 + 3))
            return (Math.Round(num / Math.Pow(10, 27), roundDecimals)).ToString() + " octillion";

        if (numDigits > 30 && numDigits <= (30 + 3))
            return (Math.Round(num / Math.Pow(10, 30), 1)).ToString() + " nonillion";

        if (numDigits > 33 && numDigits <= (33 + 3))
            return (Math.Round(num / Math.Pow(10, 33), roundDecimals)).ToString() + " decillion";

        if (numDigits > 36 && numDigits <= (36 + 3))
            return (Math.Round(num / Math.Pow(10, 36), roundDecimals)).ToString() + " undecillion";

        if (numDigits > 39 && numDigits <= (39 + 3))
            return (Math.Round(num / Math.Pow(10, 39), roundDecimals)).ToString() + " duodecillion";

        if (numDigits > 42 && numDigits <= (42 + 3))
            return (Math.Round(num / Math.Pow(10, 42), roundDecimals)).ToString() + " tredecillion";

        if (numDigits > 45 && numDigits <= (45 + 3))
            return (Math.Round(num/Math.Pow(1, 45), roundDecimals)).ToString() + " quattuordecillion";

        return $"{num:n0}";
    }
}
