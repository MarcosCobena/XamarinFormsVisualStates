using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsVisualStates
{
    public partial class NFCPage : ContentPage, IStateNotified
	{
		const int HalfOfDefaultAnimationsLength = 125;
        const float DefaultOpacityWhenNFCUnavailable = 0.12f;

        public NFCPage()
		{
			InitializeComponent ();

            Behaviors.Add(new StateNotificationBehavior<NFCPage>());

            ResetImagesToTheirInitialState();

            BindingContext = new NFCViewModel();
        }

        NFCViewModel ViewModel => BindingContext as NFCViewModel;

        public async void UpdateVisualState(string newState)
        {
            switch (newState)
            {
                case nameof(NFCViewModel.States.NFCUnavailable):
                    var easing = Easing.CubicOut;
                    nfcJustNFCImage.Scale = 1;
                    await Task.WhenAll(nfcUnavailableImage.FadeTo(DefaultOpacityWhenNFCUnavailable, easing: easing),
                                       nfcReadyImage.FadeTo(0, easing: easing),
                                       nfcJustNFCImage.FadeTo(0, easing: easing),
                                       nfcJustOKImage.FadeTo(0, easing: easing))
                              .ConfigureAwait(false);
                    break;
                case nameof(NFCViewModel.States.ReadyToRead):
                    await nfcUnavailableImage.FadeTo(1, easing: Easing.CubicIn)
                                             .ConfigureAwait(false);
                    break;
                case nameof(NFCViewModel.States.ReadSuccessful):
                    nfcJustOKImage.Opacity = 1;
                    nfcJustOKImage.Scale = 0;
                    await Task.WhenAll(nfcUnavailableImage.FadeTo(0, easing: Easing.CubicOut),
                                       nfcReadyImage.FadeTo(1, easing: Easing.CubicIn),
                                       nfcJustNFCImage.FadeTo(1, easing: Easing.CubicIn))
                              .ConfigureAwait(false);
                    await Task.WhenAll(nfcJustNFCImage.ScaleTo(0, HalfOfDefaultAnimationsLength, Easing.CubicIn),
                                       nfcJustOKImage.ScaleTo(1.5f, HalfOfDefaultAnimationsLength, Easing.CubicOut))
                              .ConfigureAwait(false);
                    await nfcJustOKImage.ScaleTo(1, HalfOfDefaultAnimationsLength, Easing.CubicIn)
                                        .ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();

            ViewModel.StartStateMachineAsync();
		}

		void ResetImagesToTheirInitialState()
        {
            nfcUnavailableImage.Opacity = DefaultOpacityWhenNFCUnavailable;
            nfcReadyImage.Opacity = 0;
            nfcJustNFCImage.Opacity = 0;
            nfcJustNFCImage.Scale = 1;
            nfcJustOKImage.Opacity = 0;
        }
    }
}