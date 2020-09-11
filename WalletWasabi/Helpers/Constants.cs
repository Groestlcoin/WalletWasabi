using NBitcoin;
using NBitcoin.Protocol;
using System;
using WalletWasabi.Backend.Models.Responses;
using WalletWasabi.Exceptions;

namespace WalletWasabi.Helpers
{
	public static class Constants
	{
		public static readonly Version ClientVersion = new Version(1, 1, 10, 2);
		public const string BackendMajorVersion = "3";
		public static readonly Version HwiVersion = new Version("1.0.3");
		public static readonly Version BitcoinCoreVersion = new Version("2.20.1");

		/// <summary>
		/// By changing this, we can force to start over the transactions file, so old incorrect transactions would be cleared.
		/// It is also important to force the KeyManagers to be reindexed when this is changed by renaming the BlockState Height related property.
		/// </summary>
		public const string ConfirmedTransactionsVersion = "2";

		public const uint ProtocolVersionWitnessVersion = 70012;

		public static readonly NodeRequirement NodeRequirements = new NodeRequirement
		{
			RequiredServices = NodeServices.NODE_WITNESS,
			MinVersion = ProtocolVersionWitnessVersion,
			MinProtocolCapabilities = new ProtocolCapabilities { SupportGetBlock = true, SupportWitness = true, SupportMempoolQuery = true }
		};

		public static readonly NodeRequirement LocalNodeRequirements = new NodeRequirement
		{
			RequiredServices = NodeServices.NODE_WITNESS,
			MinVersion = ProtocolVersionWitnessVersion,
			MinProtocolCapabilities = new ProtocolCapabilities { SupportGetBlock = true, SupportWitness = true }
		};

		public static readonly NodeRequirement LocalBackendNodeRequirements = new NodeRequirement
		{
			RequiredServices = NodeServices.NODE_WITNESS,
			MinVersion = ProtocolVersionWitnessVersion,
			MinProtocolCapabilities = new ProtocolCapabilities
			{
				SupportGetBlock = true,
				SupportWitness = true,
				SupportMempoolQuery = true,
				SupportSendHeaders = true,
				SupportPingPong = true,
				PeerTooOld = true
			}
		};

		public const int P2wpkhInputSizeInBytes = 41;
		public const int P2pkhInputSizeInBytes = 145;
		public const int OutputSizeInBytes = 33;

		// https://en.bitcoin.it/wiki/Bitcoin
		// There are a maximum of 10,500,000,000,000,000 Groestlcoin elements (called gros), which are currently most commonly measured in units of 100,000,000 known as GRS. Stated another way, no more than 105 million GRS can ever be created.
		public const long MaximumNumberOfSatoshis = 10500000000000000;

		private static readonly BitcoinWitPubKeyAddress MainNetCoordinatorAddress = new BitcoinWitPubKeyAddress("grs1q509twc95s820qrufx2wm8gyqfgjreasdu354gf", NBitcoin.Altcoins.Groestlcoin.Instance.Mainnet);
		private static readonly BitcoinWitPubKeyAddress TestNetCoordinatorAddress = new BitcoinWitPubKeyAddress("tgrs1q2zth7mf447slngwjfyyau5xpcrn9ekk8kxd9ff", NBitcoin.Altcoins.Groestlcoin.Instance.Testnet);
		private static readonly BitcoinWitPubKeyAddress RegTestCoordinatorAddress = new BitcoinWitPubKeyAddress("grsrt1qugk8qlzpwmlydmes48ljpk56nz03vugw93u7zd", NBitcoin.Altcoins.Groestlcoin.Instance.Regtest);

		public static BitcoinWitPubKeyAddress GetCoordinatorAddress(Network network)
		{
			Guard.NotNull(nameof(network), network);

			if (network == NBitcoin.Altcoins.Groestlcoin.Instance.Mainnet)
			{
				return MainNetCoordinatorAddress;
			}
			else if (network == NBitcoin.Altcoins.Groestlcoin.Instance.Testnet)
			{
				return TestNetCoordinatorAddress;
			}
			else if (network == NBitcoin.Altcoins.Groestlcoin.Instance.Regtest)
			{
				return RegTestCoordinatorAddress;
			}
			else
			{
				throw new NotSupportedNetworkException(network);
			}
		}

		public const int TwentyMinutesConfirmationTarget = 2 * 10;
		public const int OneDayConfirmationTarget = 144 * 10;
		public const int SevenDaysConfirmationTarget = 1008;

		public const int BigFileReadWriteBufferSize = 1 * 1024 * 1024;

		public const int DefaultTorSocksPort = 9050;
		public const int DefaultTorBrowserSocksPort = 9150;
		public const int DefaultTorControlPort = 9051;
		public const int DefaultTorBrowserControlPort = 9151;

		public const int DefaultMainNetBitcoinP2pPort = 1331;
		public const int DefaultTestNetBitcoinP2pPort = 17777;
		public const int DefaultRegTestBitcoinP2pPort = 18888;

		public const int DefaultMainNetBitcoinCoreRpcPort = 1441;
		public const int DefaultTestNetBitcoinCoreRpcPort = 17766;
		public const int DefaultRegTestBitcoinCoreRpcPort = 18443;

		public const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public const double TransactionRBFSignalRate = 0.02; // 2% RBF transactions

		public const decimal DefaultDustThreshold = 0.00005m;

		public const long MaxSatoshisSupply = 10_500_000_000_000_000L;
	}
}
