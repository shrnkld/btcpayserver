using NBitcoin;

namespace BTCPayServer
{
    public partial class BTCPayNetworkProvider
    {
        public void InitPIVX()
        {
            //not needed: NBitcoin.Altcoins.Dash.Instance.EnsureRegistered();
            var nbxplorerNetwork = NBXplorerNetworkProvider.GetFromCryptoCode("PIVX");
            Add(new BTCPayNetwork()
            {
                CryptoCode = nbxplorerNetwork.CryptoCode,
                DisplayName = "PIVX",
                BlockExplorerLink = NetworkType == ChainName.Mainnet
                    ? "https://zkbitcoin.com/blocks"
                    : "https://testnet.rockdev.org",
                NBXplorerNetwork = nbxplorerNetwork,
                DefaultRateRules = new[]
                    {
                        "PIVX_X = PIVX_BTC * BTC_X",
                        "PIVX_BTC = coingecko(PIVX_BTC)"
                    },
                CryptoImagePath = "imlegacy/pivx.png",
                DefaultSettings = BTCPayDefaultSettings.GetDefaultSettings(NetworkType),
                //https://github.com/satoshilabs/slips/blob/master/slip-0044.md
                CoinType = NetworkType == ChainName.Mainnet ? new KeyPath("5'")
                    : new KeyPath("1'")
            });
        }
    }
}
