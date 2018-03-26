using System;
using Xamarin.Forms;

namespace XamarinFormsVisualStates
{
    public partial class LoginPage : ContentPage, IStateNotified
    {
        public LoginPage()
        {
            InitializeComponent();

            Behaviors.Add(new StateNotificationBehavior<LoginPage>());
        }

        public void UpdateVisualState(string newState)
        {
            throw new NotImplementedException();
        }
    }
}
