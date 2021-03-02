@echo off
rem ***************************************************************************
rem * Build Script for AppVeyor Run
rem ***************************************************************************
powershell .\build.ps1 --target=CI
rem powershell .\build.ps1 -Target AppVeyor -Script RunNuGet.cake
rem powershell .\Tools\Resources\build.ps1 -Target AppVeyor -Script RunNuGet.cake -Verbosity Diagnostic
