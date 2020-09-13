# Developers' Guide for debugging GroestlMix

This guide is for giving detailed instructions about how to debug GroestlMix Wallet components to those developers who want to contribute to the project.
We will focus on how to achieve this with `vscode` first because that is the cross-platform IDE used by some of the developer team members.

## Before Starting
We assume the reader has already read the project [README](https://github.com/zkSNACKs/WalletWasabi/blob/master/README.md) file and has installed the [.NET Core 3.1 SDK](https://www.microsoft.com/net/download), and knows how to clone the repository and build the GroestlMix solution.


## Install VS Code and C# extension
### 1: Get Visual Studio Code
Install Visual Studio Code (VSC).
Pick the latest VSC version from here: [https://code.visualstudio.com/download](https://code.visualstudio.com/download)


### 2: Install C# Extension
Open the command palette in VS Code (press <kbd>F1</kbd>) and type `ext install C#` to trigger the installation of the extension.
VS Code will show a message that the extension has been installed and it will restart.
Installing this extension can take a while.


### 3: Open the WalletWasabi directory in VS Code
Go to `File->Open Folder` and open the WalletWasabi directory in Visual Studio Code.
The GroestlMix vscode settings forces a download of the latest omnisharp versions every time the folder is opened and this can take a while.

After this step we are ready to start configuring the build tasks and the launches.

## Understanding the GroestlMix components

GroestlMix Wallet is a client/server system where the client part is the GroestlMix Wallet that users download and install on their machines, and the server part is the GroestlMix backend that runs on the Groestlcoin servers.
Additionally, the solution contains other components like the Packager used to prepare the GroestlMix client to be distributed, integration tests, project, and others.

Here we are going to focus first on how to debug the client component, and then the backend component.

### GroestlMix Client

Let us start by creating the launcher (it goes into the `.vscode/launch.json` file).
This file contains the list of projects that can be launched, how to do it, what tasks have to be executed before the launching, etc.

```json
{
   "version": "0.2.0",
   "configurations": [{
      "name": "GroestlMix GUI .NET Core",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build-client",
      "program": "${workspaceFolder}/WalletWasabi.Gui/bin/Debug/netcoreapp3.1/WalletWasabi.Gui.dll",
      "args": [],
      "cwd": "${workspaceFolder}/WalletWasabi.Gui",
      "stopAtEntry": false,
      "internalConsoleOptions": "openOnSessionStart",
      "sourceFileMap": {
          ".": "${workspaceFolder}"
      }
   }]
}
```


It is important to see that this launcher requires the execution of a `build-client` task.
That is defined in the tasks file (it goes into `.vscode/tasks.json` file).

```json
{
    "version": "2.0.0",
    "tasks": [{
        "label": "build-client",
        "command": "dotnet",
        "type": "process",
        "args": [
           "build",
           "${workspaceFolder}/WalletWasabi.Gui/WalletWasabi.Gui.csproj"
        ],
        "problemMatcher": "$msCompile"
    }]
}
```

After these two files are created press (CTRL+SHIFT+D) to go to the debugger, set a couple of breakpoints, select the `Wasabi GUI .NET Core` launcher (it should be the only one available) and press the play button.
That is all.

### GroestlMix Backend

In the same way we did with the client part, we need to create a launcher and a task for running and debugging the server-side component.
Let us start with the launcher.
Add the following launcher to the array of `configurations` in the `.vscode/launch.json` file.

```json
{
   "name": "Wasabi Backend .NET Core",
   "type": "coreclr",
   "request": "launch",
   "preLaunchTask": "build-backend",
   "program": "${workspaceFolder}/WalletWasabi.Backend/bin/Debug/netcoreapp3.1/WalletWasabi.Backend.dll",
   "args": [],
   "cwd": "${workspaceFolder}/WalletWasabi.Backend",
   "stopAtEntry": false,
   "internalConsoleOptions": "openOnSessionStart",
   "launchBrowser": {
       "enabled": true,
       "args": "${auto-detect-url}",
       "windows": {
           "command": "cmd.exe",
           "args": "/C start ${auto-detect-url}"
       },
       "osx": {
           "command": "open"
       },
       "linux": {
           "command": "xdg-open"
       }
   },
   "env": {
       "ASPNETCORE_ENVIRONMENT": "Development"
   },
   "sourceFileMap": {
       "/Views": "${workspaceFolder}/Views",
        ".": "${workspaceFolder}"
   }
}
```
As before, we need to create a task for compiling the backend project before executing the code, and this is done again in the `.vscode/tasks.json` file.
Add the following task to the array of tasks:

```json
{
   "label": "build-backend",
   "command": "dotnet",
   "type": "process",
   "args": [
      "build",
      "${workspaceFolder}/WalletWasabi.Backend/WalletWasabi.Backend.csproj"
   ],
   "problemMatcher": "$msCompile"
}
```

And that is all.
Once this has been done a developer can press (CTRL+SHIFT+D) to go to the debugger, set a couple of breakpoints, select the `Wasabi Backend .NET Core` launcher and press the play button to start debugging.



### Files

#### .vscode/launch.json

```json

{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Wasabi GUI .NET Core",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build-client",
            "program": "${workspaceFolder}/WalletWasabi.Gui/bin/Debug/netcoreapp3.1/WalletWasabi.Gui.dll",
            "args": [],
            "cwd": "${workspaceFolder}/WalletWasabi.Gui",
            "stopAtEntry": false,
            "internalConsoleOptions": "openOnSessionStart",
            "sourceFileMap": {
                ".": "${workspaceFolder}"
            }
        },
        {
            "name": "Wasabi Backend .NET Core",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build-backend",
            "program": "${workspaceFolder}/WalletWasabi.Backend/bin/Debug/netcoreapp3.1/WalletWasabi.Backend.dll",
            "args": [],
            "cwd": "${workspaceFolder}/WalletWasabi.Backend",
            "stopAtEntry": false,
            "internalConsoleOptions": "openOnSessionStart",
            "launchBrowser": {
                "enabled": true,
                "args": "${auto-detect-url}",
                "windows": {
                    "command": "cmd.exe",
                    "args": "/C start ${auto-detect-url}"
                },
                "osx": {
                    "command": "open"
                },
                "linux": {
                    "command": "xdg-open"
                }
            },
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views",
                ".": "${workspaceFolder}"
            }
        },
        {
            "name": "Wasabi Deamon .NET Core",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build-client",
            "program": "${workspaceFolder}/WalletWasabi.Gui/bin/Debug/netcoreapp3.1/WalletWasabi.Gui.dll",
            "args": [
                "mix", "--wallet:TestNet"
            ],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
            "console": "internalConsole",
            "sourceFileMap": {
                ".": "${workspaceFolder}"
            }
        },
        {
            "name": "Wasabi Unit Tests .NET Core",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build-all",
            "program": "dotnet",
            "args": [
                "test", "--filter", "UnitTests"
            ],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
            "console": "internalConsole"
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach",
            "processId": "${command:pickProcess}"
        }
    ]
}
```


#### .vscode/tasks.json

```json
{
    "version": "2.0.0",
    "tasks": [{
        "label": "build-client",
        "command": "dotnet",
        "type": "process",
        "args": [
            "build",
            "${workspaceFolder}/WalletWasabi.Gui/WalletWasabi.Gui.csproj",
            "/property:GenerateFullPaths=true",
            "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "build-backend",
            "command": "dotnet",
            "type": "process",
            "args": [
               "build",
               "${workspaceFolder}/WalletWasabi.Backend/WalletWasabi.Backend.csproj",
               "/property:GenerateFullPaths=true",
               "/consoleloggerparameters:NoSummary"
           ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "build-all",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
                ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "watch",
            "command": "dotnet",
            "type": "process",
            "args": [
                "watch",
                "run",
                "${workspaceFolder}/WalletWasabi.Gui/WalletWasabi.Gui.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```

## Debugging code with unit tests

Because `dotnet test` will run the test code in a child process, it isn't possible to configure a "unit test debugging" configuration in `launch.json`.
You can tweak debugging options for unit tests by addind the followin setting to your local `.vscode/settings.json` file (if it doesn't exists then create it):

```json
   "csharp.unitTestDebuggingOptions": {
        "sourceFileMap": {
            "WalletWasabi/": "${workspaceFolder}/WalletWasabi",
            "WalletWasabi.Gui/": "${workspaceFolder}/WalletWasabi.Gui",
            "WalletWasabi.Tests/": "${workspaceFolder}/WalletWasabi.Tests",
            "WalletWasabi.Backend/": "${workspaceFolder}/WalletWasabi.Backend"
        }
    },
```

## How to setup and run the whole system

Sometimes, as developers, we need to test an advanced interaction between the Groestlcoin Core node, the Coordinator, and one or more GroestlMix clients.
Just imagine a case in which we want to verify the system behavior after a blockchain reorg.
In these cases we have to setup and run all the components, and use regtest.

### Install Groestlcoin Core

Download and install Groestlcoin Core from [https://groestlcoin.org/](https://groestlcoin.org/).


### Running the backend (coordinator)

There is more than one way to do this:

* From **VSCode** go to the Debug panel (CTRL+SHIFT+D), select `Wasabi Backend .NET Core` and press play.
* From **Terminal** just type `dotnet run <GroestlMixProjectFolder>/WalletWasabi.Backend/WalletWasabi.Backend.csproj`

### Get some coins to test

Once the GroestlMix backend, the groestlcoin node and the GroestlMix client are running, we can mine some blocks and get the new mined coins into our GroestlMix wallet.

1. Go to GroestlMix and generate a new address
2. Open a terminal and type `groestlcoin-cli -regtest generatetoaddress 101 <your-groestlmix-address>`

After this we should receive our first 50 groestlcoins!
