# Hardware wallet integration into GroestlMix

## Introduction

This is a technical document written for Hardware Wallet manufacturers. It describes the steps that need to be done before a hardware wallet can be supported by GroestlMix.
GroestlMix does not directly support Hardware Wallets, but it uses [Bitcoin Core's Hardware Wallet Interface (HWI)](https://github.com/Groestlcoin/HWI) which is a command-line tool that unifies the commands for many devices. GroestlMix includes the HWI binary and calls it every time whenever it interacts with the device.

GroestlMix's main priorities regarding hardware wallets:
- Compatibility - past, present, future
- Privacy
- Industry standardization
