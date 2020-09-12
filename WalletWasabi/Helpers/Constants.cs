using NBitcoin;
using NBitcoin.Protocol;
using System;
using WalletWasabi.Backend.Models.Responses;
using WalletWasabi.Exceptions;

namespace WalletWasabi.Helpers
{
	public static class Constants
	{
		public const string ClientSupportBackendVersionMin = "3";
		public const string ClientSupportBackendVersionMax = "4";
		public const string BackendMajorVersion = "4";

		/// <summary>
		/// By changing this, we can force to start over the transactions file, so old incorrect transactions would be cleared.
		/// It is also important to force the KeyManagers to be reindexed when this is changed by renaming the BlockState Height related property.
		/// </summary>
		public const string ConfirmedTransactionsVersion = "2";

		public const uint ProtocolVersionWitnessVersion = 70012;

		public const int P2wpkhInputSizeInBytes = 41;
		public const int P2wpkhInputVirtualSize = 68;
		public const int P2pkhInputSizeInBytes = 145;
		public const int OutputSizeInBytes = 33;

		// https://en.bitcoin.it/wiki/Bitcoin
		// There are a maximum of 10,500,000,000,000,000 Groestlcoin elements (called gros), which are currently most commonly measured in units of 100,000,000 known as GRS. Stated another way, no more than 105 million GRS can ever be created.
		public const long MaximumNumberOfSatoshis = 10500000000000000;

		public const int TwentyMinutesConfirmationTarget = 2;
		public const int OneDayConfirmationTarget = 144;
		public const int SevenDaysConfirmationTarget = 1008;

		public const int BigFileReadWriteBufferSize = 1 * 1024 * 1024;

		public const int DefaultTorSocksPort = 9050;

		public const int DefaultMainNetBitcoinP2pPort = 1331;
		public const int DefaultTestNetBitcoinP2pPort = 17777;
		public const int DefaultRegTestBitcoinP2pPort = 18888;

		public const int DefaultMainNetBitcoinCoreRpcPort = 1441;
		public const int DefaultTestNetBitcoinCoreRpcPort = 17766;
		public const int DefaultRegTestBitcoinCoreRpcPort = 18443;

		public const double TransactionRBFSignalRate = 0.02; // 2% RBF transactions

		public const decimal DefaultDustThreshold = 0.00005m;

		public const string AlphaNumericCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		public const string CapitalAlphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public const string BuiltinBitcoinNodeName = "Groestlcoin Core";

		public static readonly Version ClientVersion = new Version(1, 1, 12, 0);
		public static readonly Version HwiVersion = new Version("1.1.2");
		public static readonly Version BitcoinCoreVersion = new Version("2.20.1");
		public static readonly Version LegalDocumentsVersion = new Version(2, 0);

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

		public static readonly ExtPubKey FallBackCoordinatorExtPubKey = NBitcoinHelpers.BetterParseExtPubKey("xpub6D2PqhWBAbF3xgfaAUW73KnaCXUroArcgMTzNkNzfVX7ykkSzQGbqaXZeaNyxKbZojAAqDwsne6B7NcVhiTrXbGYrQNq1yF76NkgdonGrEa");

		public static string[] UserAgents = new[]
		{
			"/Groestlcoin:2.20.1/",
			"/Groestlcoin:2.19.1/",
			"/Groestlcoin:2.18.2/",
			"/Groestlcoin:2.17.2/",
			"/Groestlcoin:2.16.3/",
			"/Groestlcoin:2.16.0/",
		};

		public static string ClientSupportBackendVersionText => ClientSupportBackendVersionMin == ClientSupportBackendVersionMax
				? ClientSupportBackendVersionMin
				: $"{ClientSupportBackendVersionMin} - {ClientSupportBackendVersionMax}";
	}
}
