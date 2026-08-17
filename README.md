Note: the links and images in this document may point to external sources. Please review them before opening.
CMDarsenal Control Console
.NET 8.0 | C# | Windows Forms | Forensic Security & Administration Console
CMDarsenal Control Console is a lightweight, high-performance C# Windows Forms utility designed specifically for Desktop Support technicians, System Administrators, SOC Analysts, and DFIR (Digital Forensics & Incident Response) investigators.
The application consolidates critical diagnostic, network maintenance, and cybersecurity forensic utilities into a unified, dark-themed, rapid-access GUI panel. It eliminates the need to memorize complex CLI arguments, handles parameters via clean pop-up prompt dialogs, and runs selected commands inside elevated terminal sessions.

Key Features
•	10-Tab Logical Organization: Neatly categorizes utilities into distinct workflows (Network, DNS, System, Storage, Processes, Security, and Help).
•	Interactive Security Artifacts Browser: A built-in cybersecurity investigative panel to immediately check path existence and explore critical Windows forensic files (SAM database, Prefetch, Amcache.hve, NTUSER.dat, and local startup hooks).
•	Dashboard Modules Catalog: A centralized home screen index that acts as a quick-reference guide explaining the functionality of every tab.
•	Windows Terminal Integration: Leverages the modern Windows Terminal (wt.exe) for running commands, falling back gracefully to standard Command Prompt (cmd.exe) if not installed.
•	Auto-Elevation (Admin Privileges): Configured to execute processes using the runas verb, ensuring system utilities such as sfc, DISM, or sc automatically request UAC elevation.
•	Dynamic Argument Interception: Detects commands requiring user-supplied parameters (IPs, Hostnames, PIDs, or folder paths) and prompts the user using a modal input dialog box.
•	Modern Dark Theme: Styled with a customized developer-focused dark-mode theme (#2D2D30) and custom owner-drawn tabs (#007ACC highlight) to prevent eye strain.
•	100% Programmatic UI: Avoids Visual Studio Designer dependencies, ensuring compile-time reliability and preventing visual layout bugs.

Terminal Command Architecture
Below is the blueprint of the command suite embedded directly inside CMDarsenal.
Tab Name	UI Action	Command Utility & Description
Dashboard	Modules Directory	Large header and a built-in, scrollable overview catalog explaining each tab's purpose.
Network Quick Checks	Dropdown Menu	ipconfig /all — Full IP configuration
ipconfig /release — Release IPv4 address
ipconfig /renew — Renew IPv4 address
ping <IPaddress> — Test connection (User Input)
tracert <hostname> — Trace route (User Input)
netstat -ano — Active connections with PIDs
DNS Fixes	Dropdown Menu	ipconfig /flushdns — Clear DNS resolver cache
ipconfig /displaydns — View DNS resolver cache
nslookup <domain> — Query DNS for domain (User Input)
nslookup <domain> <dns_server> — Query specific server (User Input)
System + Files	Dropdown Menu	systeminfo — Detailed system configuration
whoami /all — Current user and privileges
hostname — Display computer name
sfc /scannow — Scan and repair system files
DISM ... /RestoreHealth — Repair Windows system image
robocopy <src> <dst> — Mirror directory tree (User Input)
Disk + Storage	Dropdown Menu	chkdsk c: /scan — Scan local volume for errors
wmic logicaldisk get ... — View disk capacity & free space
diskpart — Interactive disk, partition, and volume manager
Processes + Services	Dropdown Menu	tasklist — View running tasks & PIDs
taskkill /PID <id> /F — Force terminate process (User Input)
sc query — Enumerate active services
sc stop <service> — Stop a running service (User Input)
sc start <service> — Start a configured service (User Input)
Remote + Shares	Dropdown Menu	mstsc — Launch Remote Desktop Connection client
ssh <user@host> — Connect via Secure Shell (User Input)
net use \\\\<pc>\\C$ — Map remote admin share (User Input)
net use * /delete — Disconnect all network mapped drives
Extra Goodies	Dropdown Menu	cls — Clear screen command sequence
echo %PATH% — Display the environment path variable
ver — Display detailed Windows OS version
driverquery — Display installed kernel device drivers
getmac — Display MAC addresses of active network adapters
arp -a — View Address Resolution Protocol cache table
ipconfig /registerdns — Refresh DHCP leases and register DNS names
powercfg /batteryreport — Generate detailed battery health report
shutdown /s /t 0 — Shutdown the computer immediately
Security Artifacts	Interactive Tool	Check presence, read forensic significance analysis, and launch File Explorer focusing on critical directories (SAM, Prefetch, Amcache.hve, NTUSER.dat, hosts, etc.).
Help	Scrollable Box	Core requirements and a comprehensive guide to building/compiling installer setups with troubleshooting for standard error warnings (0x80004003).

Technical Setup & Installation
You can compile this project easily using either Visual Studio or the cross-platform .NET CLI.
Option 1: Quick Start via .NET CLI (Recommended)
1.	Open your terminal (Command Prompt or PowerShell) and navigate to your target coding directory.
2.	Run the following command to scaffold a new Windows Forms application:


dotnet new winforms -n CMDarsenal
3.	Navigate into your new project directory:


cd CMDarsenal
4.	Replace the default boilerplate in Form1.cs with the production-ready code.
5.	Run the application directly from the CLI:


dotnet run
Option 2: Setup in Visual Studio IDE
6.	Open Visual Studio 2022 and select Create a new project.
7.	Search for Windows Forms App, select it using C#, and click Next.
8.	Set the project name to CMDarsenal, choose .NET 6.0, .NET 7.0, or .NET 8.0 (Long-Term Support), and click Create.
9.	In Solution Explorer, double-click Form1.cs to open its code editor.
10.	Delete all existing code inside Form1.cs and paste the updated programmatic, dark-themed code.
11.	Press F5 or click the green Start/Debug button to build and run.

Code Overview & Architecture
Custom Owner-Drawn Tab Control
Standard Windows Forms tab pages do not support default background styles well under dark themes. To bypass this, the tab control's drawing behavior is intercepted:
tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);
During rendering, tabControl_DrawItem colors the background of inactive tabs to matching dark grey and renders the currently active tab using Visual Studio's signature #007ACC blue overlay, keeping the UI modern and readable.
Safe Dialog Inputs
To run parameterized commands securely without compiler warnings or runtime exceptions, specifically satisfying C# nullable reference standards such as CS8600 and CS8604, modal prompts are triggered safely using the custom dialog routine:
string? selectedItem = commandsBox.SelectedItem?.ToString();

if (!string.IsNullOrEmpty(selectedItem))
{
    RunCommandInTerminal(selectedItem);
}
Security Artifact Verification & Explorer Selection
The Security Artifacts tab utilizes robust checking mechanisms. It handles locked system files gracefully by inspecting parent configurations and calls Windows Explorer utilizing /select arguments to automatically highlight targeted files on screen:
string explorerArg = isFile ? $"/select,\"{path}\"" : $"\"{path}\"";
Process.Start(new ProcessStartInfo()
{
    FileName = "explorer.exe",
    Arguments = explorerArg,
    UseShellExecute = true
});

Developer Guide: Customizing & Adding Commands
Adding a new command to CMDarsenal is straightforward. Depending on whether the command runs as a static string or requires custom user input, follow the steps below.
Step 1: Add the Command to the Tab Dropdown
Locate the GetCommandsForTab(string tabName) method in Form1.cs. Find the switch block for the tab you want to modify and add your new command entry to the array.
"your_command - Your Human-Readable Description"
Step 2: Handle User Parameters (Optional)
If your command includes a placeholder argument such as <target_ip> or <username>, intercept it inside the RunCommandInTerminal(string fullItemText) method.
12.	Ensure your dropdown string in Step 1 includes a placeholder, such as ping <IPaddress>.
13.	Inside RunCommandInTerminal, find the switch (commandToRun) statement and add a new case for your isolated command string.
14.	Call ShowInputDialog to prompt the user, verify they did not select Cancel, and construct the final command string.

Git Configuration & GitHub Deployment
To upload your local files to your GitHub repository at https://github.com/stopete/CMDarsenal using your email stopete@outlook.com, run the following commands in your command prompt or terminal inside your project directory:
# 1. Initialize local git repository
git init

# 2. Configure your local Git identity
git config --global user.name "stopete"
git config --global user.email "stopete@outlook.com"

# 3. Create a clean .gitignore to skip building folders (bin, obj, .vs)
dotnet new gitignore

# 4. Stage all project files
git add .

# 5. Commit the files
git commit -m "Initial commit of CMDarsenal: Programmatic multi-tab dark UI with interactive security tools"

# 6. Set the main branch
git branch -M main

# 7. Add remote GitHub origin
git remote add origin https://github.com/stopete/CMDarsenal.git

# 8. Push to GitHub
git push -u origin main

Packaging & Installer Guide
This project contains comprehensive setup support. If you package your project using Visual Studio's Microsoft Visual Studio Installer Projects 2022 extension, you can reference the complete local documentation on your Help tab.
Resolving Critical Setup Build Errors
🔴 ERROR: "The target of shortcut... is invalid."
Root Cause: The installer expects to place folder structures dynamically on installation but cannot verify folder existence before deployment.
Solution:
15.	Open the File System Editor in your Setup Project.
16.	Select Application Folder, press F4 to open Properties, and set AlwaysCreate to True.
17.	Repeat this process to change AlwaysCreate to True for the User's Desktop and User's Programs Menu folders.
🔴 ERROR: "Unrecoverable build error - 0x80004003"
Root Cause: A Null Pointer exception occurs when there is a compilation configuration mismatch (e.g., trying to compile a Setup project locked to a hardcoded configuration).
Solution:
18.	Open the File System Editor of your setup project, delete the old Primary Output entry.
19.	Right-click the folder > Add > Project Output.
20.	Select Primary Output and set the Configuration dropdown strictly to (Active) instead of hardcoded configurations.
21.	Clear your build cache (delete the hidden .vs folder as well as local bin and obj directories) and perform a Rebuild Solution under your target configuration (Debug/Release).

System Requirements
•	Operating System: Windows 10 (Build 1903 or later) or Windows 11.
•	Runtime: .NET Desktop Runtime (6.0, 7.0, or 8.0) or .NET Framework 4.8+.
•	Terminal: For the optimal tabbed experience, Windows Terminal (wt.exe) should be installed. If it is not found, commands automatically spawn inside elevated cmd.exe panels.
•	Privileges: Users must run the compiled application with an administrator-authorized account to satisfy UAC authorization when utilizing terminal tools such as system file repairs (sfc), registry audits, background service modifications, and OS image restorations.

Security & Operational Notes
CMDarsenal provides direct access to commands that can modify system configuration, terminate processes, stop services, repair Windows components, disconnect network drives, and shut down the computer.
Use administrative commands carefully and verify parameters before execution. The application should be used only on systems you are authorized to administer.

License
This project is open-source and free for distribution, modification, and adaptation in administrative workspace, educational, and commercial environments.
