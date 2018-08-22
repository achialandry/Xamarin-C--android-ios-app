using System;
using System.Collections.Generic;
using System.Text;

namespace TipCalNew.Core
{
    public interface IntTipCalc
    {
        //this returns tip amount user wants to chip in
        double TipAmount(double subTotal, int generosity);

        //this returns total bill plus tip user is to pay in
        double BillTotal(double subTotal, int generosity);
    }
}
