#define ProjectDir "D:\SoftwareEntwicklung\Taschenrechner\Taschenrechner\Taschenrechner"
#define PublishDir ProjectDir + "\bin\Release\net8.0-windows\win-x64\publish"

; dotnet publish wird automatisch VOR dem Kompilieren ausgeführt
#expr Exec("dotnet", "publish """ + ProjectDir + """ -c Release -r win-x64 --self-contained true", ProjectDir, , SW_SHOW)

[Setup]
AppName=Taschenrechner
AppVersion=1.0
DefaultDirName={pf}\Taschenrechner
DefaultGroupName=Taschenrechner
OutputBaseFilename=TaschenrechnerSetup
Compression=lzma
SolidCompression=yes
OutputDir=Output

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\Taschenrechner"; Filename: "{app}\Taschenrechner.exe"
Name: "{commondesktop}\Taschenrechner"; Filename: "{app}\Taschenrechner.exe"

[Run]
Filename: "{app}\Taschenrechner.exe"; Description: "Taschenrechner starten"; Flags: nowait postinstall skipifsilent