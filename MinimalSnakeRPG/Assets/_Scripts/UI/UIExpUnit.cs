public class UIExpUnit : UIValueBarBase
{
    public override void SetValue(int value, int maxValue)
    {
        if(maxValue==0)
        {
            progressBar.fillAmount = 0;
        }
        else
        {
            progressBar.fillAmount = value / (float)maxValue;
        }
      
    }

    public void SetLevel(int level)
    {
        valueText.text = $"{level}";
    }
}