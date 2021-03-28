# Wox.LastPass - A LastPass Plugin
A simple Wox Plugin that leverages LastPass's CLI through the Windows Subsystem for Linux.

## Requirements
- Wox - http://www.wox.one (https://github.com/Wox-launcher/Wox)
    - This is a desktop application for your PC, allowing for plugins (like this one!)
- Windows Subsystem for Linux - https://docs.microsoft.com/en-us/windows/wsl/install-win10
    - I prefer Ubuntu so this has only been tested on Ubuntu. I'd be surprised if it does not work on others.
- lastpass-cli - https://github.com/lastpass/lastpass-cli
    - Follow the steps on their github page to install on your chosen linux distro
- (Optional) Windows Terminal - https://www.microsoft.com/en-us/p/windows-terminal/9n0dx20hk701
    - If you need to login to LastPass via lastpass-cli, It will spawn a terminal so you can do so. I chose Windows Terminal. If you don't want to use Windows Terminal, you'll need to modify a line of code to spawn something else, like "wsl.exe"


## Installation
I'm not going to publish this plugin, so I'll outline some basic steps to get this running on Wox manually.

1. Navigate to `%appdata%\Wox\Plugins`.
2. Create a new folder called `LastPass-b110d29d-770b-4f48-871d-873f9cb04ef5`
3. Build the project.
    - This should be as simple as pulling down this project, running `dotnet restore` and `dotnet build`.
4. Copy `plugin.json` from the root of this project into the newly created folder.
5. Copy `Newtonsoft.Json.dll` and `Wox.LastPass.dll` from the build folder (`bin/Debug/net452`) to the newly created folder.
6. The `LastPass-b110d29d-770b-4f48-871d-873f9cb04ef5` should now have the following files:
    - `Newtonsoft.Json.dll`
    - `Wox.LastPass.dll`
    - `plugin.json`
7. Run Wox! Confirm the plugin exists via going to the settings and looking at the list of plugins.