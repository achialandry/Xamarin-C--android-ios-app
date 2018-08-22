using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;

namespace TipCalNew.Core.Droid
{
    [Activity(Label = "Tip Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        int count = 1;
        int _generosity = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var calculate = new ClsTipCalculator();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"{count++} clicks!"; };

            


            //Get subTotal from layout resource and attach to event 
            EditText _subTotal = FindViewById<EditText>(Resource.Id.subTotal);
           
            TextView _total = FindViewById<TextView>(Resource.Id.total);


            //seekbar for user generosity
            SeekBar _seek = FindViewById<SeekBar>(Resource.Id.seekBar);



            TextView _tipValue = FindViewById<TextView>(Resource.Id.tip);


            _subTotal.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {


                if (string.IsNullOrEmpty(_subTotal.Text))
                {
                    _seek.Enabled = false;
                }
                else
                {
                    _seek.Enabled = true;
                    double _dSubTotal = double.Parse(_subTotal.Text.ToString());

                    var sub_tot = calculate.BillTotal(_dSubTotal, _generosity);

                    _total.Text = string.Format(sub_tot.ToString());
                }

            };

            _seek.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    _generosity = int.Parse(e.Progress.ToString());
                    _tipValue.Text = string.Format("{0}%", e.Progress);

                    double _dSubTotal = double.Parse(_subTotal.Text.ToString());

                    var sub_tot = calculate.BillTotal(_dSubTotal, _generosity);

                    _total.Text = string.Format(sub_tot.ToString());

                }
            };





            //make return key to send or calculate tip
            _subTotal.EditorAction += (sender, e) => {
                if (e.ActionId == Android.Views.InputMethods.ImeAction.Done)
                {
                    //enable seekbar if user enters value
                    if (string.IsNullOrEmpty(_subTotal.Text))
                    {
                        _seek.Enabled = false;
                    }
                    else
                    {
                        _seek.Enabled = true;
                        double _dSubTotal = double.Parse(_subTotal.Text.ToString());

                        var sub_tot = calculate.BillTotal(_dSubTotal, _generosity);

                        _total.Text = string.Format(sub_tot.ToString());
                      
                    }
                }
                else
                {
                    e.Handled = false;
                }
            };




            _subTotal.FocusChange += (object sender, Android.Views.View.FocusChangeEventArgs e) => {
                Android.Views.InputMethods.InputMethodManager imm = (Android.Views.InputMethods.InputMethodManager)Application.Context.GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(_subTotal.WindowToken, 0);
            };




        




    }

        private void DismissKeyboard()
        {
            var view = CurrentFocus;
            if (view != null)
            {
 
                var imm = (Android.Views.InputMethods.InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }


    }
}

