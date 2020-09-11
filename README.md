[GroestlMix Wallet](https://groestlcoin.org) is an open-source, non-custodial, privacy-focused Groestlcoin wallet for desktop, that implements [Chaumian CoinJoin](https://github.com/nopara73/ZeroLink/#ii-chaumian-coinjoin).

The main privacy features on the network level:
- Tor-only by default.
- BIP 158 block filters for private light client.
- Opt-in connection to user full node.

and on the blockchain level:
- Intuitive ZeroLink CoinJoin integration.
- Superb coin selection and labeling.
- Dust attack protections.

For more information, please check out the [GroestlMix Documentation](https://groestlcoin.org/forum), an archive of knowledge about the nuances of Groestlcoin privacy and how to properly use GroestlMix.


# [Download GroestlMix](https://github.com/Groestlcoin/WalletWasabi/releases)

# Build From Source Code

## Get The Requirements

1. Get Git: https://git-scm.com/downloads
2. Get .NET Core 3.1 SDK: https://www.microsoft.com/net/download
3. Optionally disable .NET's telemetry by typing `export DOTNET_CLI_TELEMETRY_OPTOUT=1` on Linux and macOS or `setx DOTNET_CLI_TELEMETRY_OPTOUT 1` on Windows.

## Get GroestlMix

Clone & Restore & Build

```sh
git clone https://github.com/Groestlcoin/WalletWasabi.git
cd WalletWasabi/WalletWasabi.Gui
dotnet build
```

## Run GroestlMix

Run GroestlMix with `dotnet run` from the `WalletWasabi.Gui` folder.

## Update GroestlMix

```sh
git pull
```
