using System.Collections;
using UnityEngine;


 public class HoursPanelState : CurrentNumberPanelState
 {
    private const int maxFirstVelue = 2;
    private const int maxSecondVelue = 3;

    public override void ChangeNumbers(NumerPanel panel)
    {
        SetValues(panel);
        if (panel.hoursArray[0] > maxFirstVelue)
            ResetNumbers(panel);
        if (panel.hoursArray[0] ==maxFirstVelue && panel.hoursArray[1] > maxSecondVelue)
            ResetNumbers(panel);
    }

    protected override void ResetNumbers(NumerPanel panel)
    {
        Debug.Log("large Number");

        panel.hoursArray[0] = 0;
        panel.hoursArray[0] = 0;
        SetValues(panel);
    }
    protected override void SetValues(NumerPanel panel)
    {

        panel.currentArray = panel.hoursArray;
        panel.hoursStirng = null;
        for (int i = 0; i < panel.hoursArray.Length; i++)
            panel.hoursStirng += panel.hoursArray[i];
        int.TryParse(panel.hoursStirng, out panel._alram.hours);
        panel._hoursText.text = panel.hoursStirng;
    }
}
