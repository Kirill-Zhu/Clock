using System.Collections;
using UnityEngine;



    public class MinutesPanelState : CurrentNumberPanelState
    {
    private const int maxFirstVelue= 5;
    private const int maxSecondVelue = 9;
        public override void ChangeNumbers(NumerPanel panel)
        {

        SetValues(panel);

      
        if (panel.minutesArray[0] > maxFirstVelue)
            ResetNumbers(panel);
        if (panel.minutesArray[1] > maxSecondVelue)
            ResetNumbers(panel);
        }
    protected override void ResetNumbers(NumerPanel panel)
    {
        Debug.Log("large Number");

        panel.minutesArray[0] = 0;
        panel.minutesArray[0] = 0;
        SetValues(panel);
       
    }
    protected override void SetValues(NumerPanel panel)
    {

        panel.currentArray = panel.minutesArray;
        panel.minutesString = null;
        for (int i = 0; i < panel.minutesArray.Length; i++)
            panel.minutesString += panel.minutesArray[i];

        int.TryParse(panel.minutesString, out panel._alram.minutes);
        panel._minutesText.text = panel.minutesString;
    }
}
