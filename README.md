[![Buy Me A Coffee](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/rbicelli)

# DesktopComposer

This is a software designed to simplify the task of managing Windows Start Menu and Desktop Shortcuts (and hopefully in the future some other Desktop related settings) for Windows 10 and Windows Server 2019 in an Active Directory environment.

![](images/screenshot.png?raw=true)

DesktopComposer consists of two programs:

- ComposerAdmin: GUI program use to build Start Menus
- ComposerAgent: Agent program which runs at user logon/logoff and creates the Start Menu according to settings defined in ComposerAdmin

## ComposerAdmin Features

- Build a Start Menu from scratch (creates Menus and Shortcuts) in an easy and friendly way
- Import Shortcuts from Local Start Menu
- Import Shortcuts from other Computer in same Network (e.g. your terminal server)
- Import Shortcuts from Folder
- Define Shortcut Properties: Put on Desktop, Put on Start Menu, Set ACL on Shortcuts

## ComposerAgent Features
 
 - Self disabling access of Common Start Menu and Common Desktop of Composition enabled users
 - Deploy Shortcuts according to composition rules set in ACL
 - Agent Settings are manageable via Group Policy, with inclued ADMX templates

# Installation

## System Requirements

- An Active Directory Domain
- For the agents, Windows Server 2019 or Windows 10

## Prepare the required environment

Grab setup files in [releases](https://github.com/rbicelli/DesktopComposer/releases/).

- Install Shortcuts Editor on administrator machine, typically the one you use to manage Group Policies ordirectly on your Terminal Server.
- Create a folder widely accessible in Read-Only mode for the end user where you will put composition file, typically a subfolder under NETLOGON, eg. **\\MYDOMAIN\NETLOGON\DesktopComposerFiles**. 
- Create your first start menu, save the file in the directory, for exampe **\\\\MYDOMAIN\netlogon\dcomposer\rd-servers.dcxml**

## Deploy Agents

Install ComposerAgent on every computer you wish to deploy Start Menus and Desktop Settings.

You can deploy Agents silently calling the setup executable with /VERYSILENT flag:

```
ComposerAgentSetup-%VERSION%.exe /VERYSILENT
```

## Create the required Group Policy Objects

First you need to deploy ADMX templates to your Windows Policy Folder or the Central Store.

Open ADMX Folder (You can easily access is under **Start Menu->Desktop Composer->ADMX Template Folder**) and copy the content of the folder to **C:\Windows\PolicyDefinitions** or to the central store of your AD Domain.

Open the Group Policy Management Snap-In and create the Group Policy

These instructions are for a typical RDS Server Farm setup, consider an organizational structure like this:


```
+ Domain
  + RD Farm
  + RD Session Hosts
    - RDS Server 01	
```

Create a GPO and link it to the **RD Session Hosts** OU.

Then Edit the GPO just created.

### Computer Settings

1. Set the Group Policy Loopback Prcessing Mode to Merge:

Open **Computer Configuration\Policies\Administrative Templates\System\Group Policy\Configure user Group Policy loopback processing mode**, set it to **Enabled** and **Merge**
This will apply th user policy to users logged in to computer

2. Add users to DesktopComposer Local User Group:

Open **Computer Configuration\Preferences\Control Panel Settings\Local Users and Groups**, then create new local group called **DesktopComposer Users** and add as members the needed Users groups.


### User Settings

1. Open **User Configuration\Windows Settings\Scripts (Logon/Logoff)\Logon**, add a new logon script:
 - **Script Name**: %PROGRAMFILES%\Sequence Software\Composer Agent\ComposerAgent.exe
 - **Script Parameters:** -compose
This will trigger the Composition of Start Menu and Desktop Shortcuts at User Logon

2. (optional) Open **User Configuration\Windows Settings\Scripts (Logon/Logoff)\Logoff**, add a new logonff script, 
 - **Script Name**: %PROGRAMFILES%\Sequence Software\Composer Agent\ComposerAgent.exe
 - **Script Parameters:** -decompose
This will restore the initial Start Menu and Desktop Shortcuts at user User Logoff

### ComposerAgent Settings

Open **User Configuration\Administrative Templates\Sequence Software\DesktopComposer Agent**

1. Set **Enable Composition Agent** to **Enabled**

2. Set **Composition File Location** to the composition file previously saved (e.g. **\\\\MYDOMAIN\netlogon\dcomposer\rd-servers.dcxml** )

3. Optionally you can set the **Log File Location** (e.g. **%TEMP%\COMPOSERLOG.LOG**) and **Log Threshold**. By default it saves logs in %APPDATA%.

## Troubleshooting

If you have problems first check the logs. You could raise the verbosity of logs simply editing the **Log Threshold** GPO Item.
For further troubleshooting you can open a console and launch the agent Manually:

```
%PROGRAMFILES%\Sequence Software\Composer Agent\ComposerAgent.exe

Commandline Args are:
-compose: run the composition
-decompose: restores initial state
-install: install related tasks (creates Local User Group and sets Deny ACLs on common Desktop and Common Start Menu)
-uninstall: uninstall related tasks (rollback install)

NOTE: Install and uninstall requires elevated privileges and are executed by the installer/uninstaller.
```

# License

This software is released under the MIT License.