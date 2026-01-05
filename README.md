# BrandingChanger
A simple CLI program that changes the Windows branding.

# Usage
The program doesn't have any GUI, it's meant to run as a command:
BrandingChanger /{branding}

## Currently Available
/ultimate - for Windows 7 Ultimate <br>
/homepremium - for Windows 7 Home Premium <br>
/enterprise - for Windows 7 Enterprise <br>
/professional - for Windows 7 Professional <br>

# How does it work?
You have to provide the files yourself, the path that the program copies the branding from is **C:\Relive\SetupFiles\Branding\{branding}**. <br>
There's also a license.rtf change, which is the singular LinkLabel in winver, it's in the same directory as the rest of the branding files, but copies it to **C:\Windows\System32** instead.
