using System;
using System.Threading.Tasks;

namespace XamarinFormsVisualStates
{
    public class NFCViewModel : BaseViewModel
    {
        private static readonly TimeSpan SecondsToHoldBetweenStates = TimeSpan.FromSeconds(1);
		private static readonly TimeSpan SecondsToSimulate = TimeSpan.FromSeconds(0.5f);

        public enum States
        {
            NFCUnavailable,
            ReadyToRead,
            ReadSuccessful
        }

        public async Task StartStateMachineAsync()
        {
            while (true)
            {
                State = nameof(States.NFCUnavailable);
                await Task.Delay(SecondsToHoldBetweenStates);

                // We simulate NFC support check
                await Task.Delay(SecondsToSimulate);
                var isNFCSupported = true;

                if (isNFCSupported)
                {
                    // We simulate NFC readiness check
                    await Task.Delay(SecondsToSimulate);
                }
                else
                {
                    continue;
                }

                State = nameof(States.ReadyToRead);
                await Task.Delay(SecondsToHoldBetweenStates);

                // We simulate NFC tag detection
                await Task.Delay(SecondsToSimulate);
                var nfcTagId = string.Empty;

                State = nameof(States.ReadSuccessful);
                await Task.Delay(SecondsToHoldBetweenStates);

                // We simulate navigation to another page
                await Task.Delay(SecondsToSimulate);
            }
        }
    }
}