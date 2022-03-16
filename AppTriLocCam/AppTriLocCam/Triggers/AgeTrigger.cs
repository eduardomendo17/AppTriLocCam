using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTriLocCam.Triggers
{
    public class AgeTrigger : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            int n;
            bool isNumeric = int.TryParse(sender.Text, out n);
            if (!isNumeric)
            {
                // No es numérico
                sender.Text = "";
            }
            else
            {
                // Es numérico
                if (n < 0)
                {
                    // Es menor que cero regresamos cero
                    sender.Text = "0";
                }
                else if (n > 99)
                {
                    // Si es mayor que 99 regresamos 99
                    sender.Text = "99";
                }
            }
        }
    }
}
