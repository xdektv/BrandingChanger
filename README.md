# BrandingChanger
A simple CLI program that changes the Windows branding.

# Usage
The program doesn't have any GUI, it's meant to run as a command:
BrandingChanger /{branding}

## Currently Available
/ultimate - for Windows 7 Ultimate
/homepremium - for Windows 7 Home Premium
/enterprise - for Windows 7 Enterprise
/professional - for Windows 7 Professional

# How does it work?
You have to provide the files yourself, the path that the program copies the branding from is **C:\Relive\SetupFiles\Branding\{branding}**.
There's also a license.rtf change, which is the singular LinkLabel in winver, it's in the same directory as the rest of the branding files, but copies it to **C:\Windows\System32** instead.
