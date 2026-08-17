using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace CMDarsenal
{
    public partial class Form1 : Form
    {
        // Define our theme colors
        private readonly Color darkBackground = Color.FromArgb(45, 45, 48);
        private readonly Color lightText = Color.FromArgb(241, 241, 241);
        private readonly Color mediumBlue = Color.FromArgb(0, 122, 204);
        private readonly Color darkerBlue = Color.FromArgb(44, 88, 122);
        private readonly Color controlBackground = Color.FromArgb(60, 60, 63);

        public Form1()
        {
            InitializeComponent();
            SetupMultiTabForm();
        }

        private void SetupMultiTabForm()
        {
            // 1. Apply theme to the main form
            this.BackColor = darkBackground;
            this.Text = "CMDarsenal Control Console";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. Create and configure the main TabControl
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                // FIX: Corrected typo from TabControlDrawMode to TabDrawMode
                DrawMode = TabDrawMode.OwnerDrawFixed 
            };
            tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);

            // 3. Define tab names
            string[] tabNames = { "Dashboard", "Network Quick Checks", "DNS Fixes", "System + Files", "Disk + Storage", "Processes + Services", "Remote + Shares", "Extra Goodies", "Help" };

            // 4. Loop through and generate each tab
            foreach (string tabName in tabNames)
            {
                TabPage newTab = new TabPage(tabName) { BackColor = darkBackground, Padding = new Padding(15) };

                if (tabName == "Dashboard")
                {
                    Label titleLabel = new Label() { Text = "IT TECHNICIAN CMD ARSENAL", Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = mediumBlue, AutoSize = true, Location = new Point(20, 25) };
                    newTab.Controls.Add(titleLabel);
                    Panel divider = new Panel() { BackColor = mediumBlue, Size = new Size(400, 2), Location = new Point(24, 65) };
                    newTab.Controls.Add(divider);
                    Label purposeText = new Label() { Text = "A rapid-access console for critical command-line utilities. Navigate the tabs to select, customize, and execute commands with administrator privileges.", Font = new Font("Segoe UI", 10F), ForeColor = lightText, Size = new Size(740, 80), Location = new Point(20, 85) };
                    newTab.Controls.Add(purposeText);
                }
                else if (tabName == "Help")
                {
                    Label helpLabel = new Label() { Text = "This application requires the modern Windows Terminal ('wt.exe') for the best experience. If not installed, it will fall back to the standard Command Prompt ('cmd.exe').\n\nAll commands are executed with administrator rights, which will trigger a UAC prompt.", Font = new Font("Segoe UI", 10F, FontStyle.Italic), ForeColor = lightText, Size = new Size(740, 120), Location = new Point(20, 20) };
                    newTab.Controls.Add(helpLabel);
                }
                else
                {
                    AddCommandDropDown(newTab, tabName);
                }
                tabControl.TabPages.Add(newTab);
            }
            this.Controls.Add(tabControl);
        }
        
        // This method handles the custom drawing of the tabs
        // FIX: Method signature updated to allow nullable sender
        private void tabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            // FIX: Check for null after 'as' cast to prevent crash
            TabControl? tabControl = sender as TabControl;
            if (tabControl == null) return;
            
            TabPage page = tabControl.TabPages[e.Index];
            Graphics g = e.Graphics;
            Rectangle tabBounds = tabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                g.FillRectangle(new SolidBrush(mediumBlue), e.Bounds);
            }
            else
            {
                g.FillRectangle(new SolidBrush(darkBackground), e.Bounds);
                g.DrawRectangle(new Pen(controlBackground, 1), tabBounds);
            }
            
            TextRenderer.DrawText(g, page.Text, this.Font, tabBounds, lightText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void AddCommandDropDown(TabPage tab, string tabName)
        {
            var commands = GetCommandsForTab(tabName);

            Label commandLabel = new Label() { Text = commands.Label, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = lightText, AutoSize = true, Location = new Point(20, 20) };
            tab.Controls.Add(commandLabel);

            ComboBox commandsBox = new ComboBox() { Location = new Point(20, 50), Width = 600, Font = new Font("Segoe UI", 9F), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = controlBackground, ForeColor = lightText, FlatStyle = FlatStyle.Flat };
            commandsBox.Items.AddRange(commands.Items);
            commandsBox.SelectedIndex = 0;
            tab.Controls.Add(commandsBox);

            Button executeBtn = new Button() { Text = "LAUNCH AS ADMIN", Location = new Point(20, 85), Size = new Size(160, 35), Font = new Font("Segoe UI", 9F, FontStyle.Bold), BackColor = darkerBlue, ForeColor = lightText, FlatStyle = FlatStyle.Flat };
            executeBtn.FlatAppearance.BorderColor = mediumBlue;
            executeBtn.FlatAppearance.BorderSize = 1;
            tab.Controls.Add(executeBtn);

            executeBtn.Click += (sender, e) =>
            {
                string? selectedItem = commandsBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedItem)) { RunCommandInTerminal(selectedItem); }
                else { MessageBox.Show("Please select a command first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            };
        }

        private (string Label, string[] Items) GetCommandsForTab(string tabName)
        {
            switch(tabName)
            {
                case "Network Quick Checks": return ("Select a network utility command:", new string[] { "ipconfig /all - Full IP configuration", "ipconfig /release - Release IPv4 address", "ipconfig /renew - Renew IPv4 address", "ping <IPaddress> - Test network connectivity", "tracert <hostname> - Trace route to destination", "netstat -ano - List active connections and PIDs" });
                case "DNS Fixes": return ("Select a DNS troubleshooting utility command:", new string[] { "ipconfig /flushdns - Clear DNS resolver cache", "ipconfig /displaydns - View DNS resolver cache", "nslookup <domain> - Query DNS for domain", "nslookup <domain> <dns_server> - Query specific DNS server" });
                case "System + Files": return ("Select a system or file utility command:", new string[] { "systeminfo - View detailed system configuration", "whoami /all - Display current user and privileges", "hostname - Display computer name", "sfc /scannow - Scan and repair system files", "DISM /online /Cleanup-Image /RestoreHealth - Repair Windows image", "robocopy <src> <dst> - Mirror directory tree" });
                case "Disk + Storage": return ("Select a disk or storage utility command:", new string[] { "chkdsk c: /scan - Scan volume for errors", "wmic logicaldisk get name, freespace, size - List disk capacity and free space", "diskpart (list disk, list vol) - Manage disks, partitions, or volumes" });
                case "Processes + Services": return ("Select a process or service utility command:", new string[] { "tasklist - List running tasks", "taskkill /PID <id> /F - Force terminate process by PID", "sc query - Enumerate active services", "sc stop <service> - Stop a running service", "sc start <service> - Start a configured service" });
                case "Remote + Shares": return ("Select a remote or network share utility command:", new string[] { "mstsc - Open Remote Desktop Connection", "ssh <user@host> - Connect via Secure Shell", "net use \\\\<pc>\\C$ - Map remote C$ administrative share", "net use * /delete - Disconnect all network drives" });
                case "Extra Goodies": return ("Select an extra utility command:", new string[] { "cls - Clear the command prompt screen", "echo %PATH% - Display the system PATH variable", "ver - Display Windows version", "driverquery - Display installed device drivers", "getmac - Display MAC addresses", "arp -a - View ARP cache", "ipconfig /registerdns - Refresh DHCP and reregister DNS", "powercfg /batteryreport - Generate battery health report", "shutdown /s /t 0 - Shutdown the computer immediately" });
                default: return ("", Array.Empty<string>());
            }
        }
        
        private void RunCommandInTerminal(string fullItemText)
        {
            string commandToRun = fullItemText.Split(new string[] { " - " }, StringSplitOptions.None)[0].Trim();
            string? userInput;

            switch (commandToRun)
            {
                case "ping <IPaddress>": userInput = ShowInputDialog("Enter the IP Address or Hostname:", "Ping Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"ping {userInput}"; break;
                case "tracert <hostname>": userInput = ShowInputDialog("Enter the Hostname or IP Address:", "Tracert Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"tracert {userInput}"; break;
                case "nslookup <domain>": userInput = ShowInputDialog("Enter the Domain to look up:", "NSLookup Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"nslookup {userInput}"; break;
                case "nslookup <domain> <dns_server>": string? domain = ShowInputDialog("Enter the Domain to look up:", "NSLookup Input"); if (string.IsNullOrEmpty(domain)) return; string? dnsServer = ShowInputDialog("Enter the DNS Server to use:", "NSLookup Input"); if (string.IsNullOrEmpty(dnsServer)) return; commandToRun = $"nslookup {domain} {dnsServer}"; break;
                case "robocopy <src> <dst>": string? source = ShowInputDialog("Enter the Source Path:", "Robocopy Input"); if (string.IsNullOrEmpty(source)) return; string? dest = ShowInputDialog("Enter the Destination Path:", "Robocopy Input"); if (string.IsNullOrEmpty(dest)) return; commandToRun = $"robocopy \"{source}\" \"{dest}\" /MIR /R:1 /W:1"; break;
                case "taskkill /PID <id> /F": userInput = ShowInputDialog("Enter the Process ID (PID) to terminate:", "Taskkill Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"taskkill /PID {userInput} /F"; break;
                case "sc stop <service>": userInput = ShowInputDialog("Enter the exact Service Name to stop:", "Service Control"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"sc stop \"{userInput}\""; break;
                case "sc start <service>": userInput = ShowInputDialog("Enter the exact Service Name to start:", "Service Control"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"sc start \"{userInput}\""; break;
                case "ssh <user@host>": userInput = ShowInputDialog("Enter the user and host (e.g., admin@192.168.1.100):", "SSH Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"ssh {userInput}"; break;
                case "net use \\\\<pc>\\C$": userInput = ShowInputDialog("Enter the PC Name or IP Address:", "Map Drive Input"); if (string.IsNullOrEmpty(userInput)) return; commandToRun = $"net use \\\\{userInput}\\C$"; break;
                case "diskpart (list disk, list vol)": commandToRun = "diskpart"; break;
            }

            try { Process.Start(new ProcessStartInfo() { FileName = "wt.exe", Arguments = $"cmd.exe /k \"{commandToRun}\"", UseShellExecute = true, Verb = "runas" }); }
            catch { try { Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = $"/k \"{commandToRun}\"", UseShellExecute = true, Verb = "runas" }); } catch (Exception ex) { MessageBox.Show($"Failed to execute command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
        }
        
        private string? ShowInputDialog(string text, string caption)
        {
            using (Form prompt = new Form()
            {
                Width = 400, Height = 180, BackColor = darkBackground, ForeColor = lightText,
                FormBorderStyle = FormBorderStyle.FixedDialog, Text = caption, StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false, MinimizeBox = false
            })
            {
                Label textLabel = new Label() { Left = 20, Top = 20, Width = 350, Text = text, ForeColor = lightText };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350, BackColor = controlBackground, ForeColor = lightText, BorderStyle = BorderStyle.FixedSingle };
                Button confirmation = new Button() { Text = "OK", Left = 210, Width = 80, Top = 90, DialogResult = DialogResult.OK, BackColor = darkerBlue, ForeColor = lightText, FlatStyle = FlatStyle.Flat };
                confirmation.FlatAppearance.BorderColor = mediumBlue;
                Button cancel = new Button() { Text = "Cancel", Left = 295, Width = 80, Top = 90, DialogResult = DialogResult.Cancel, BackColor = controlBackground, ForeColor = lightText, FlatStyle = FlatStyle.Flat };

                confirmation.Click += (sender, e) => { prompt.Close(); };
                cancel.Click += (sender, e) => { prompt.Close(); };
            
                prompt.Controls.AddRange(new Control[] { textLabel, textBox, confirmation, cancel });
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
    }
}
