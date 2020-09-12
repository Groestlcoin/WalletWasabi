using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalletWasabi.Exceptions;
using WalletWasabi.Helpers;

namespace WalletWasabi.Blockchain.Blocks
{
	public class SmartHeader
	{
		public SmartHeader(uint256 blockHash, uint256 prevHash, uint height, DateTimeOffset blockTime)
		{
			BlockHash = Guard.NotNull(nameof(blockHash), blockHash);
			PrevHash = Guard.NotNull(nameof(prevHash), prevHash);
			if (blockHash == prevHash)
			{
				throw new InvalidOperationException($"{nameof(blockHash)} cannot be equal to {nameof(prevHash)}. Value: {blockHash}.");
			}

			Height = height;
			BlockTime = blockTime;
		}

		public uint256 BlockHash { get; }
		public uint256 PrevHash { get; }
		public uint Height { get; }
		public DateTimeOffset BlockTime { get; }

		#region SpecialHeaders

		private static SmartHeader StartingHeaderMain { get; } = new SmartHeader(
			new uint256("0000000019972e461d5c8d1c60108bcb299d274ade2be8194e0f0da6b16c7ead"),
			new uint256("000000001c1cb3a90a4e57cc9b1f8bbfa7b3501588af054b2e545fcd6d2e8b51"),
			481824,
			DateTimeOffset.FromUnixTimeSeconds(1424703945));

		private static SmartHeader StartingHeaderTestNet { get; } = new SmartHeader(
			new uint256("0000008df6e4f82cc6f1c71d461bf739e0e20a44e19e5535feabcfb79acc6136"),
			new uint256("000000a3ac8ba4b3a313ed37d951ce51d5720d1152b89635d3038f74f4ec4cc4"),
			828575,
			DateTimeOffset.FromUnixTimeSeconds(1541082333));

		private static SmartHeader StartingHeaderRegTest { get; } = new SmartHeader(
			NBitcoin.Altcoins.Groestlcoin.Instance.Regtest.GenesisHash,
			NBitcoin.Altcoins.Groestlcoin.Instance.Regtest.GetGenesis().Header.HashPrevBlock,
			0,
			NBitcoin.Altcoins.Groestlcoin.Instance.Regtest.GetGenesis().Header.BlockTime);

		/// <summary>
		/// Where the first possible bech32 transaction ever can be found.
		/// </summary>
		public static SmartHeader GetStartingHeader(Network network)
			=> network.NetworkType switch
			{
				NetworkType.Mainnet => StartingHeaderMain,
				NetworkType.Testnet => StartingHeaderTestNet,
				NetworkType.Regtest => StartingHeaderRegTest,
				_ => throw new NotSupportedNetworkException(network)
			};

		#endregion SpecialHeaders
	}
}
