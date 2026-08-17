## Project Title

CMDarsenal Control Console

## Description

CMDarsenal Control Console is a lightweight, high-performance C# Windows Forms utility for desktop support technicians and IT operations. It consolidates critical diagnostic and maintenance terminal commands into a unified, dark-themed GUI, simplifying complex CLI operations and handling parameters via intuitive prompts.

## Installation

1.  **Via .NET CLI (Recommended):**
    bash
    dotnet new winforms -n CMDarsenal
    cd CMDarsenal
    # Replace Form1.cs content with project code
    dotnet run
    
2.  **Via Visual Studio IDE:**
    *   Create a new Windows Forms App project (`.NET 6.0`, `.NET 7.0`, or `.NET 8.0`).
    *   Replace the contents of `Form1.cs` with the project's code.
    *   Build and run (`F5`).

## Usage

To quickly flush the DNS cache, select "DNS Fixes" from the tabs, then choose "ipconfig /flushdns - Clear DNS resolver cache" from the dropdown.

## Features

*   9-tab logical organization for command categorization.
*   Leverages Windows Terminal (wt.exe) with fallback to cmd.exe.
*   Automatic UAC elevation for administrative commands.
*   Dynamic modal dialogs for user-supplied command parameters.
*   Custom owner-drawn dark theme for reduced eye strain.
*   100% programmatic UI, avoiding designer dependencies.

## Tech Stack

| Technology        | Purpose                                        |
| :---------------- | :--------------------------------------------- |
| .NET 8.0 / 6.0    | Core application framework                     |
| C#                | Programming language                           |
| Windows Forms     | UI framework                                   |
| Windows Terminal  | Integrated terminal execution environment      |

## Project Structure

```
CMDarsenal/
├── CMDarsenal.csproj
├── Form1.cs
└── Program.cs
```

## Contributing

1.  **Fork** the repository.
2.  **Create** a new branch for your feature (`git checkout -b feature/YourFeature`).
3.  **Commit** your changes (`git commit -m 'Add some YourFeature'`).
4.  **Push** to the branch (`git push origin feature/YourFeature`).
5.  **Open** a Pull Request.

## License

This project is open-source and free for distribution, modification, and adaptation.
