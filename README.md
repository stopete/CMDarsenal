Note: the links and images in this document may point to external sources. Please review them before opening.
CMDarsenal Control Console
..NEETT 88..00 || 66..00 || FFrraameewoorrkk 44..88 Framework PPllaattffoorrm Wiinnddoowss Platform UII Wiinnddoowss FFoorrmss Daarrkk Mooddee UI
CMDarsenal Control Console is a lightweight, high-performance C# Windows Forms utility designed specifically for
Desktop Support technicians, Tier-1/Tier-2 IT Operations, and Network Administrators.
The application consolidates critical diagnostic and maintenance terminal utilities into a unified, dark-themed, rapid-access
GUI panel. It eliminates the need to memorize complex CLI arguments, handles parameters via clean pop-up prompt
dialogs, and runs selected commands inside elevated terminal sessions.
Key Features
9-Tab Logical Organization: Neatly categorizes commands into dedicated sections (Network, DNS, System,
Storage, Processes, Remote, etc.).
Windows Terminal Integration: Leverages the modern Windows Terminal (wt.exe) for running commands, falling
back gracefully to standard Command Prompt (cmd.exe) if not installed.
Auto-Elevation (Admin Privileges): Configured to execute processes using the "runas" verb, ensuring system
utilities (like sfc, DISM, or sc) automatically request UAC elevation.
Dynamic Argument Interception: Detects commands requiring user-supplied parameters (like IP addresses,
hostnames, process IDs, or folder paths) and prompts the user using a modal input dialog box.
Modern Dark Theme: Styled with a customized developer-focused dark-mode theme (#2D2D30 background) and
custom owner-drawn tabs (#007ACC highlight) to prevent eye strain during long shifts.
100% Programmatic UI: Avoids Visual Studio Designer dependencies, ensuring compile-time reliability and
preventing visual layout bugs.
Terminal Command Architecture
Below is the blueprint of the command suite embedded directly inside CMDarsenal:
Tab Name UI Action Command Utility & Description
Dashboard Static Info Overview, mission statement, and utility guide.
Network Quick
Checks
Dropdown
Menu
• ipconfig /all - Full IP configuration
• ipconfig /release - Release IPv4 address
• ipconfig /renew - Renew IPv4 address
• ping <IPaddress> - Test connection (User Input)
• tracert <hostname> - Trace route (User Input)
• netstat -ano - Active connections with PIDs
DNS Fixes
Dropdown
Menu
• ipconfig /flushdns - Clear DNS resolver cache
• ipconfig /displaydns - View DNS resolver cache
• nslookup <domain> - Query DNS for domain (User Input)
• nslookup <domain> <dns_server> - Query specific server (User
Input)
Tab Name UI Action Command Utility & Description
System + Files
Dropdown
Menu
• systeminfo - Detailed system configuration
• whoami /all - Current user and privileges
• hostname - Display computer name
• sfc /scannow - Scan and repair system files
• DISM ... /RestoreHealth - Repair Windows system image
• robocopy <src> <dst> - Mirror directory tree (User Input)
Disk + Storage
Dropdown
Menu
• chkdsk c: /scan - Scan local volume for errors
• wmic logicaldisk get ... - View disk capacity & free space
• diskpart - Interactive disk, partition, and volume manager
Processes + Services
Dropdown
Menu
• tasklist - View running tasks & PIDs
• taskkill /PID <id> /F - Force terminate process (User Input)
• sc query - Enumerate active services
• sc stop <service> - Stop a running service (User Input)
• sc start <service> - Start a configured service (User Input)
Remote + Shares
Dropdown
Menu
• mstsc - Launch Remote Desktop Connection client
• ssh <user@host> - Connect via Secure Shell (User Input)
• net use \\\\<pc>\\C$ - Map remote admin share (User Input)
• net use * /delete - Disconnect all network mapped drives
Extra Goodies
Dropdown
Menu
• cls - Clear screen command sequence
• echo %PATH% - Display the environment path variable
• ver - Display detailed Windows OS version
• driverquery - Display installed kernel device drivers
• getmac - Display MAC addresses of active network adapters
• arp -a - View Address Resolution Protocol cache table
• ipconfig /registerdns - Refresh DHCP leases and register DNS
names
• powercfg /batteryreport - Generate detailed battery health
report
• shutdown /s /t 0 - Shutdown the computer immediately
Help Rich Text Instructions, system requirements, and troubleshooting advice.
Technical Setup & Installation
You can compile this project easily using either Visual Studio or the cross-platform .NET CLI.
Option 1: Quick Start via .NET CLI (Recommended)
1. Open your terminal (Command Prompt or PowerShell) and navigate to your target coding directory.
2. Run the following command to scaffold a new Windows Forms application:
dotnet new winforms -n CMDarsenal
3. Navigate into your new project directory:
cd CMDarsenal
4. Replace the default boilerplate in Form1.cs with the production-ready code.
5. Run the application directly from the CLI:
dotnet run
Option 2: Setup in Visual Studio IDE
1. Open Visual Studio (2022 recommended) and select Create a new project.
2. Search for Windows Forms App, select it (C#), and click Next.
3. Set the project name to CMDarsenal, choose .NET 6.0, .NET 7.0, or .NET 8.0 (Long-Term Support), and click
Create.
4. In the Solution Explorer (right panel), double-click Form1.cs to open its code editor.
5. Delete all existing code inside Form1.cs and paste the updated programmatic, dark-themed code.
6. Press F5 (or click the green Start/Debug button) to build and run.
Code Overview & Architecture
Custom Owner-Drawn Tab Control
Standard Windows Forms tab pages do not support default background styles under dark themes. To bypass this, the tab
control's drawing behavior is intercepted:
tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);
During rendering, tabControl_DrawItem colors the background of inactive tabs to matching dark grey, and renders the
currently active tab using Visual Studio's signature #007ACC blue overlay, keeping the UI highly modern and readable.
Safe Dialog Inputs
To run parameterized commands securely without compiler warnings or runtime exceptions (specifically satisfying C#
nullable reference standards such as CS8600 and CS8604), modal prompts are triggered safely using the custom dialog
routine:
string? selectedItem = commandsBox.SelectedItem?.ToString();
if (!string.IsNullOrEmpty(selectedItem))
{
RunCommandInTerminal(selectedItem);
}
The dynamic pop-up prompts for parameters and formats the exact system strings automatically prior to execution.
Developer Guide: Customizing & Adding Commands
Adding a new command to CMDarsenal is incredibly simple. Depending on whether the command runs as a static string or
requires custom user inputs, follow the steps below:
Step 1: Add the Command to the Tab Dropdown
Locate the GetCommandsForTab(string tabName) method in Form1.cs. Find the switch block for the tab you want
to modify, and add your new command entry to the array.
The format must always be: "your_command - Your Human-Readable Description".
Example: Adding a command to "DNS Fixes"
case "DNS Fixes":
return ("Select a DNS troubleshooting utility command:", new string[]
{
"ipconfig /flushdns - Clear DNS resolver cache",
"nslookup - This is a newly added simple nslookup query", // <-- Added static command
"nslookup <domain> - Query DNS for domain"
});
Step 2: Handle User Parameters (Optional)
If your command includes a placeholder argument (like <target_ip> or <username>), you must intercept it inside the
RunCommandInTerminal(string fullItemText) method.
1. Ensure your dropdown string in Step 1 includes a placeholder (e.g. ping <IPaddress>).
2. Inside RunCommandInTerminal, find the switch (commandToRun) statement.
3. Add a new case for your isolated command string.
4. Call ShowInputDialog to prompt the user, verify they didn't hit cancel, and construct the final command string.
Example: Intercepting a new command with user input
switch (commandToRun)
{
case "ping <IPaddress>":
userInput = ShowInputDialog("Enter the IP Address or Hostname:", "Ping Input");
if (string.IsNullOrEmpty(userInput)) return; // User clicked cancel
commandToRun = $"ping {userInput}"; // Overwrite with executed parameters
break;
}
System Requirements
Operating System: Windows 10 (Build 1903 or later) or Windows 11.
Runtime: .NET Desktop Runtime (6.0, 7.0, or 8.0) or .NET Framework 4.8+.
Terminal: For the optimal tabbed experience, Windows Terminal (wt.exe) should be installed. If not found,
commands automatically spawn inside legacy cmd.exe panels.
Privileges: Users must run the compiled application with an administrator-authorized account to satisfy UAC
authorization when utilizing terminal tools like disk formatting, registry edits, service modifications, and OS repairs.
License
This project is open-source and free for distribution, modification, and adaptation in administrative workspace, educational,
and commercial environments.
