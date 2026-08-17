<div align="center">

🛡️ CMDarsenal Control Console

A lightweight, high-performance forensic security and administration console for Windows.

[![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&color=7E3FF2)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&color=7E3FF2)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D4?style=for-the-badge&color=7E3FF2)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge&color=7E3FF2)](https://opensource.org/licenses/MIT)

</div>

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Description
CMDarsenal Control Console is a lightweight, high-performance C# Windows Forms utility designed for Desktop Support technicians, System Administrators, SOC Analysts, and DFIR investigators. It consolidates critical diagnostic, network maintenance, and cybersecurity forensic utilities into a unified, dark-themed GUI, eliminating the need to memorize complex CLI arguments and handling parameters via clean pop-up prompts.

## Features
*   **10-Tab Logical Organization**: Categorizes utilities into distinct workflows (Network, DNS, System, Storage, Processes, Security, Help).
*   **Interactive Security Artifacts Browser**: Built-in panel to check existence and explore critical Windows forensic files.
*   **Windows Terminal Integration**: Leverages `wt.exe` for commands, gracefully falling back to `cmd.exe`.
*   **Auto-Elevation (Admin Privileges)**: Executes processes using `runas` verb, ensuring UAC elevation for system utilities.
*   **Dynamic Argument Interception**: Prompts users with modal input dialogs for commands requiring parameters.
*   **Modern Dark Theme**: Customized developer-focused dark mode to prevent eye strain.

## Tech Stack
| Technology       | Purpose                                   |
| :--------------- | :---------------------------------------- |
| C#               | Primary programming language              |
| .NET 8.0         | Application runtime and framework         |
| Windows Forms    | GUI framework for desktop application     |
| Windows Terminal | Preferred command execution environment   |

## Installation
CMDarsenal can be easily compiled and run using the .NET CLI or Visual Studio.

### Prerequisites
*   Windows 10 (Build 1903+) or Windows 11
*   .NET Desktop Runtime (6.0, 7.0, or 8.0)
*   Administrator privileges for full functionality

### Option 1: Via .NET CLI
1.  Clone the repository:
    ```bash
    git clone https://github.com/stopete/CMDarsenal.git
    cd CMDarsenal
    ```
2.  Run the application:
    ```bash
    dotnet run
    ```

### Option 2: Via Visual Studio
1.  Clone the repository.
2.  Open the `CMDarsenal.csproj` file in Visual Studio 2022.
3.  Build and run the project (F5).

## Usage
CMDarsenal provides a streamlined interface for executing system commands. To use a utility, simply navigate to the relevant tab, select a command from the dropdown menu, and follow any on-screen prompts for parameters.

For example, to trace a route to a hostname:
1.  Navigate to the `Network` tab.
2.  Select `tracert <hostname>` from the dropdown.
3.  Enter the target hostname in the pop-up dialog.
4.  The command will execute in an elevated Windows Terminal session, displaying the output.

## Project Structure
```
.
├── CMDarsenal.csproj
├── Form1.cs
├── Program.cs
├── README.md
└── .gitignore
```

## Contributing
We welcome contributions to CMDarsenal! To contribute:
1.  Fork the repository.
2.  Create a new branch (`git checkout -b feature/your-feature`).
3.  Make your changes and commit them (`git commit -m 'Add new feature'`).
4.  Push to the branch (`git push origin feature/your-feature`).
5.  Open a Pull Request.

## License
This project is licensed under the MIT License.