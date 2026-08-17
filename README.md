# CMDarsenal Control Console

A unified, dark-themed GUI for essential Windows terminal commands.

<div align="center">

![Windows Logo](https://img.shields.io/badge/Platform-Windows-0078D4?style=for-the-badge&color=7E3FF2)
![.NET Logo](https://img.shields.io/badge/.NET-6%2B%7C8-5C2D91?style=for-the-badge&color=7E3FF2)
![License-MIT](https://img.shields.io/badge/License-MIT-green?style=for-the-badge&color=7E3FF2)
![GitHub Stars](https://img.shields.io/github/stars/yourusername/cmdarsenal?style=for-the-badge&color=7E3FF2)
![GitHub Issues](https://img.shields.io/github/issues/yourusername/cmdarsenal?style=for-the-badge&color=7E3FF2)

</div>

* [Description](#description)
* [Features](#features)
* [Tech Stack](#tech-stack)
* [Installation](#installation)
* [Usage](#usage)
* [Project Structure](#project-structure)
* [Contributing](#contributing)
* [License](#license)

## Description

CMDarsenal Control Console is a lightweight C# Windows Forms application designed for IT support and network administration. It consolidates critical diagnostic and maintenance terminal utilities into a unified, dark-themed GUI, simplifying command execution and parameter handling for elevated terminal sessions.

## Features

*   9-tab logical organization for command categorization.
*   Seamless Windows Terminal integration with fallback to `cmd.exe`.
*   Automatic UAC elevation for administrative commands.
*   Dynamic input prompts for command parameters.
*   Customizable dark-mode theme for reduced eye strain.
*   100% programmatic UI, avoiding Visual Studio Designer dependencies.

## Tech Stack

| Technology           | Purpose                                     |
| :------------------- | :------------------------------------------ |
| C#                   | Primary programming language                |
| .NET 6.0+            | Runtime environment                         |
| Windows Forms        | GUI framework                               |
| Windows Terminal     | Modern terminal execution environment       |

## Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/yourusername/cmdarsenal.git
    ```
2.  **Navigate to the project directory:**
    ```bash
    cd cmdarsenal
    ```
3.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
4.  **Build and run:**
    ```bash
    dotnet run
    ```

## Usage

To ping a specific IP address, select "Network" from the tabs, choose "ping <IPaddress>" from the dropdown, and enter the IP address when prompted.

```csharp
// Example of running a command with user input
string? ipAddress = ShowInputDialog("Enter the IP Address or Hostname:", "Ping Input");
if (!string.IsNullOrEmpty(ipAddress))
{
    RunCommandInTerminal($"ping {ipAddress}");
}
```

## Project Structure

```
cmdarsenal/
├── CMDarsenal.sln
├── CMDarsenal/
│   ├── Form1.cs
│   └── Program.cs
└── Properties/
    └── Resources.Designer.cs
```

## Contributing

1.  **Fork the repository.**
2.  **Create a new branch** for your feature or bug fix.
3.  **Make your changes** and ensure they are well-documented.
4.  **Submit a pull request** with a clear description of your changes.

## License

This project is licensed under the MIT License.
