using System;

using UIKit;

namespace TipCalNew.Core.iOS
{
    public partial class TipCalViewController : UIViewController
    {
        private double StepValue = 1.0;

        public TipCalViewController() : base("TipCalViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Get parent view layout
            var margins = View.LayoutMarginsGuide;


            var _tipCalModel = new ClsTipCalculator();



            //assigning background color to view
            View.BackgroundColor = UIColor.DarkGray;

            //Text field to hold total amount user will pay
            UITextField totalAmount = new UITextField();

            totalAmount.Frame = new CoreGraphics.CGRect(10, 78, View.Bounds.Width - 140, 35);

            //keyboard type for amount to be entered by user
            totalAmount.KeyboardType = UIKeyboardType.DecimalPad;

            totalAmount.BorderStyle = UITextBorderStyle.RoundedRect;

            totalAmount.Placeholder = "Enter Sub Total";


            //Text field to hold total amount user will pay
            UITextField tipAmount = new UITextField();

            tipAmount.Frame = new CoreGraphics.CGRect(10, 120, View.Bounds.Width - 140, 35);

            //keyboard type for amount to be entered by user
            tipAmount.KeyboardType = UIKeyboardType.DecimalPad;

            tipAmount.BorderStyle = UITextBorderStyle.RoundedRect;

            tipAmount.Placeholder = "Tip Percent";

            //Button to calculate tip amount 
            UIButton calculateTipButton = new UIButton(UIButtonType.Custom);

            calculateTipButton.Frame = new CoreGraphics.CGRect(10, 270, View.Bounds.Width/2, 45);

            calculateTipButton.BackgroundColor = UIColor.FromRGB(0, 0.5f, 0);

            calculateTipButton.SetTitle("Calculate", UIControlState.Normal);


            //Label to hold result of calculated Tip
            UILabel incrementTipWithSlider = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(10, 185, View.Bounds.Width / 2 + 10, 45),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = "Tip Value Slider",
            };

            //slider to get tip value 
            UISlider tipSlider = new UISlider();
            tipSlider.Frame = new CoreGraphics.CGRect(10, 220, View.Bounds.Width / 2, 45);
            tipSlider.MinValue = 0;
            tipSlider.MaxValue = 100;
            tipSlider.Value = 15;
            






            //Label to hold result of calculated Tip
            UILabel resultLabel = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(10, 330, View.Bounds.Width/2 +10 , 45),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = "Tip is $0.00",
            };

            //Label to hold total value plus tip amount

            UILabel totalLabel = new UILabel()
            {
                Frame = new CoreGraphics.CGRect(10, 350, View.Bounds.Width/2 +10, 60),
                TextColor = UIColor.Green,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = "Total amount Due:",
            };


            View.AddSubviews(totalAmount, tipAmount, calculateTipButton, incrementTipWithSlider, tipSlider, resultLabel, totalLabel);


            //performing calculations
            calculateTipButton.TouchUpInside += (s, e) =>
           {
                //hide keyboard when button is pressed
                totalAmount.ResignFirstResponder();
               tipAmount.ResignFirstResponder();

               double valueSubTotal = 0;
                double valueTipPercent = 0;
               int tipGiven = 0;

                //try parsing value from total amount text field to value var
                double.TryParse(totalAmount.Text, out valueSubTotal);

                //try parsing value from tip text field to valueTipPercent
                double.TryParse(tipAmount.Text, out valueTipPercent);
                tipGiven = int.Parse(valueTipPercent.ToString());


               resultLabel.Text = $"Tip is {_tipCalModel.TipAmount(valueSubTotal, tipGiven).ToString("C")}";

               totalLabel.Text = $"You Bill: {_tipCalModel.BillTotal(valueSubTotal, tipGiven).ToString("C")}";
           };


            //tracking changes in tip slider and performing an action 
            tipSlider.ValueChanged += (object sender, EventArgs e) =>
            {
                //hide keyboard when button is pressed
                totalAmount.ResignFirstResponder();


                //setting value from slider to tip amount textField
                tipAmount.Text = (Math.Round(tipSlider.Value).ToString());

                double valueSubTotal = 0;
                int valueTipPercent = 0;
               

                //try parsing value from total amount text field to value var
                double.TryParse(totalAmount.Text, out valueSubTotal);

                //try parsing value from tip text field to valueTipPercent
                int.TryParse(tipAmount.Text, out valueTipPercent);



                resultLabel.Text = $"Tip is {_tipCalModel.TipAmount(valueSubTotal, valueTipPercent).ToString("C")}";

                totalLabel.Text = $"You Bill: {_tipCalModel.BillTotal(valueSubTotal, valueTipPercent).ToString("C")}";
            };



        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

