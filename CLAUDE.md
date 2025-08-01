# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Windows Forms C# application (.NET 6) that runs on a factory line at the Hendersonville Lamp Plant. The application controls test equipment via USB interfaces to perform automated lamp testing and data collection.

## Build and Development Commands

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore

# Publish for Windows deployment
dotnet publish -c Release -r win-x64 --self-contained
```

## Architecture

### Hardware Integration
The application interfaces with multiple USB-connected test instruments:
- **ITECH IT 7321** - AC power supply for voltage/frequency control
- **ITECH IT6922A** - DC power supply for voltage/current control  
- **NPA101 Power Analyzer** - Power measurement (AC/DC modes)
- **StellarNet BlueWave Spectrometer** - Optical spectrum analysis (380-780nm)
- **USB Temperature Sensor** - Environmental monitoring

### Test Process Flow
1. User configures test parameters (voltage, AC/DC mode)
2. Light curtain closes for safety
3. Power ramps up to target voltage over time
4. Spectrometer captures optical readings
5. Power ramps down safely
6. Light curtain opens
7. Results stored to Excel and SQL Server database

### Application Structure
- `Program.cs` - Application entry point with standard Windows Forms initialization
- `Form1.cs/Form1.Designer.cs` - Main UI form (currently minimal implementation)
- Target Framework: .NET 6.0 Windows with Windows Forms enabled

### Data Storage
- Local Excel files for immediate data storage
- SQL Server database integration for centralized data management
- Simple SQL calls for database operations

## Development Notes

This is an early-stage project with basic Windows Forms scaffolding in place. The main application logic for equipment control and test automation is yet to be implemented.