using NBitcoin;
using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.Text;
using WalletWasabi.Blockchain;
using WalletWasabi.Blockchain.Blocks;
using Xunit;

namespace WalletWasabi.Tests.UnitTests
{
	public class SmartHeaderTests
	{
		[Fact]
		public void ConstructorTests()
		{
			var blockTime = DateTimeOffset.UtcNow;
			new SmartHeader(uint256.Zero, uint256.One, 0, blockTime);
			new SmartHeader(uint256.Zero, uint256.One, 1, blockTime);
			new SmartHeader(uint256.Zero, uint256.One, 1, blockTime);

			Assert.Throws<ArgumentNullException>(() => new SmartHeader(null, uint256.One, 1, blockTime));
			Assert.Throws<ArgumentNullException>(() => new SmartHeader(uint256.Zero, null, 1, blockTime));
			Assert.Throws<InvalidOperationException>(() => new SmartHeader(uint256.Zero, uint256.Zero, 1, blockTime));
		}

		[Fact]
		public void StartingHeaderTests()
		{
			var startingMain = SmartHeader.GetStartingHeader(NBitcoin.Altcoins.Groestlcoin.Instance.Mainnet);
			var startingTest = SmartHeader.GetStartingHeader(NBitcoin.Altcoins.Groestlcoin.Instance.Testnet);
			var startingReg = SmartHeader.GetStartingHeader(NBitcoin.Altcoins.Groestlcoin.Instance.Regtest);

			var expectedHashMain = new uint256("0000000019972e461d5c8d1c60108bcb299d274ade2be8194e0f0da6b16c7ead");
			var expectedPrevHashMain = new uint256("00000000001937123368f42f9843ecf1d6835f449674e00d3c40d158d91f3dbe");
			uint expectedHeightMain = 481824;
			var expectedTimeMain = DateTimeOffset.FromUnixTimeSeconds(1424704328);

			var expectedHashTest = new uint256("0000008df6e4f82cc6f1c71d461bf739e0e20a44e19e5535feabcfb79acc6136");
			var expectedPrevHashTest = new uint256("0000000f8846debf66f5c5df0d8985800ad4ea21b557a5d01dc34b94eaa3ac70");
			uint expectedHeightTest = 828575;
			var expectedTimeTest = DateTimeOffset.FromUnixTimeSeconds(1541082333);

			var expectedHashReg = Network.RegTest.GenesisHash;
			var expectedPrevHashReg = uint256.Zero;
			uint expectedHeightReg = 0;
			var expectedTimeReg = Network.RegTest.GetGenesis().Header.BlockTime;

			Assert.Equal(expectedHashMain, startingMain.BlockHash);
			Assert.Equal(expectedPrevHashMain, startingMain.PrevHash);
			Assert.Equal(expectedHeightMain, startingMain.Height);
			Assert.Equal(expectedTimeMain, startingMain.BlockTime);

			Assert.Equal(expectedHashTest, startingTest.BlockHash);
			Assert.Equal(expectedPrevHashTest, startingTest.PrevHash);
			Assert.Equal(expectedHeightTest, startingTest.Height);
			Assert.Equal(expectedTimeTest, startingTest.BlockTime);

			Assert.Equal(expectedHashReg, startingReg.BlockHash);
			Assert.Equal(expectedPrevHashReg, startingReg.PrevHash);
			Assert.Equal(expectedHeightReg, startingReg.Height);
			Assert.Equal(expectedTimeReg, startingReg.BlockTime);
		}
	}
}
