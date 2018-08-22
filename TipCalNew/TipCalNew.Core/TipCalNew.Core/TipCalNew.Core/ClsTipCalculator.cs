using System;
using System.Collections.Generic;
using System.Text;

namespace TipCalNew.Core
{
    public class ClsTipCalculator : IntTipCalc
    {
        public double BillTotal(double subTotal, int generosity)
        {
            return subTotal + TipAmount(subTotal, generosity);
        }

        public double TipAmount(double subTotal, int generosity)
        {
            return subTotal * ((double)generosity) / 100.0;
        }
    }
}
