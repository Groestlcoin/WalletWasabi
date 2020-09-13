# GroestlMix Setup on RegTest

## Why do this?

RegTest is a local testing environment in which developers can almost instantly generate blocks on demand for testing events, and create private gros with no real-world value. Running GroestlMix Backend on RegTest allows you to emulate network events and observe how the Backend and the Client react on that.
You do not need to download the blockchain for this setup!

## Setup Groestlcoin Core with RegTest

Todo:

1. Install [Groestlcoin Core](http://groestlcoin.org/) on your computer.
2. Start Groestlcoin Core with: groestlcoin-qt.exe -regtest then quit immediately. In this way the data directory and the config files will be generated.
```
Windows: "C:\Program Files\Groestlcoin\groestlcoin-qt.exe" -regtest
macOS: "/Applications/Groestlcoin-Qt.app/Contents/MacOS/Groestlcoin-Qt" -regtest
Linux:
```
3. Go to Groestlcoin Core data directory. If the directory is missing run core groestlcoin-qt, then quit immediately. In this way the data directory and the config files will be generated.
```
Windows: %APPDATA%\Groestlcoin\
macOS: $HOME/Library/Application Support/Groestlcoin/
Linux: $HOME/.groestlcoin/
```
4. Edit groestlcoin.conf file and add these lines there:
```C#
regtest.server = 1
regtest.listen = 1
regtest.whitebind = 127.0.0.1:18888
regtest.rpchost = 127.0.0.1
regtest.rpcport = 18443
regtest.rpcuser = 7c9b6473600fbc9be1120ae79f1622f42c32e5c78d
regtest.rpcpassword = 309bc9961d01f388aed28b630ae834379296a8c8e3
```
5. Start Groestlcoin Core with: groestlcoin-qt.exe -regtest.
6. Do not worry about "Syncing Headers" just press the Hide button. Because you run on Regtest, no Mainnet blocks will be downloaded.
7. Go to MainMenu / Window / Console.
8. Generate a new address with:
`getnewaddress`
9. Generate the first 101 blocks with:
`generatetoaddress 101 <replace_new_address_here>`
10. You can create transactions with the Send button and confirm with:
`generatetoaddress 1 <replace_new_address_here>`

## Setup GroestlMix Backend

Here you will have to build from source, follow [these instructions here](https://github.com/Groestlcoin/WalletWasabi#build-from-source-code).

Todo:
1. Go to `WalletWasabi\WalletWasabi.Backend` folder.
2. Open the command line and enter:
`dotnet run`
3. You will get some errors, but the data directory will be created. Stop the backend if it is still running with CTRL-C.
4. Go to the Backend folder:
```
Windows: "C:\Users\{your username}\AppData\Roaming\WalletWasabi\Backend"
macOS: "/Users/{your username}/.walletwasabi/backend"
Linux: "/home/{your username}/.walletwasabi/backend"
```
5. Edit `Config.json` file by replacing everything with:
```json
{
  "Network": "RegTest",
  "BitcoinRpcConnectionString": "7c9b6473600fbc9be1120ae79f1622f42c32e5c78d:309bc9961d01f388aed28b630ae834379296a8c8e3",
  "MainNetBitcoinP2pEndPoint": "127.0.0.1:1331",
  "TestNetBitcoinP2pEndPoint": "127.0.0.1:17777",
  "RegTestBitcoinP2pEndPoint": "127.0.0.1:18888",
  "MainNetBitcoinCoreRpcEndPoint": "127.0.0.1:1441",
  "TestNetBitcoinCoreRpcEndPoint": "127.0.0.1:17766",
  "RegTestBitcoinCoreRpcEndPoint": "127.0.0.1:18443"
}
```
6. Edit one line in `CcjRoundConfig.json` file. With this the Coordinator waits only 2 participants for CoinJoin.
```
"AnonymitySet": 2,
```
7. Start Groestlcoin Core in RegTest.
8. Go to WalletWasabi folder
9. Open the command line and enter. This will build all the projects under this directory.
`dotnet build`
10. Go to WalletWasabi\WalletWasabi.Backend folder.
`dotnet run --no-build`
11. Now the Backend is generating the filters and it is running. (You can quit with CTRL-C any time)

## Setup GroestlMix Client

Todo:

1. Go to `WalletWasabi\WalletWasabi.Gui` folder.
2. Open the command line and run the GroestlMix Client with:
`dotnet run --no-build`
3. Go to Tools/Settings and set the network to RegTest
4. Close GroestlMix and restart it with:
`dotnet run --no-build`
5. Generate a wallet in GroestlMix named: R1.
6. Generate a receive address in GroestlMix, now go to Groestlcoin Core gui to the Send tab.
7. Send 1 GRS to that address.
8. Open another GroestlMix instance from another command line:
`dotnet run --no-build`
9. Generate a wallet in GroestlMix named: R2.
10. Generate a receive address in GroestlMix, now go to Groestlcoin Core gui to the Send tab.
11. Send 1 GRS to that address.
12. Now in both instance go to CoinJoin tab and enqueue. CoinJoin should happen.
13. If you see Waiting for confirmation in the GroestlMix CoinList you can generate a block in Groestlcoin Core to continue coinjoining.

Happy CoinJoin!
