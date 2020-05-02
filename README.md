# DesktopComposer

This set of softwares is designed to simplify the task of managing Windows Start Menu Shortcuts (and hopefully in the future some other Desktop settings) in an Active Directory environment.

DesktopComposer of 2 programs:

- ComposerAdmin: GUI program in which you can build your Start Menu
- ComposerAgent: Agent Service which runs at user logon/logoff and builds the Start Menu according to settings defined in ComposerAdmin

## ComposerAdmin Features

- Build a start menu from scratch (creates Menus and Shortcuts) in an intuitive way
- Import Shortcuts from Local Start Menu
- Import Shortcuts from other Computer in same Network (e.g. your terminal server)
- Import Shortcuts from Folder
- Shortcut Properties: Put on Desktop, Put on Start Menu, Set ACL

## ComposerAgent Features
 
 - Self disabling access of Common Start Menu and Common Desktop of Composition enabled users
 - Agent Settings manageable via Group Policy, with ADMX templates included