using NBitcoin;
using System;

namespace WalletWasabi.Exceptions
{
	public class InsufficientBalanceException : Exception
	{
		public Money Minimum { get; }
		public Money Actual { get; }

		public InsufficientBalanceException(Money minimum, Money actual) : base($"Needed: {minimum.ToString(false, true)} GRS, got only: {actual.ToString(false, true)} GRS.")
		{
			Minimum = minimum ?? Money.Zero;
			Actual = actual ?? Money.Zero;
		}
	}
}
