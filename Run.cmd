@echo off
rem ***************************************************************************
rem * Build Script for Debug Run
rem ***************************************************************************
rem powershell .\build.ps1 --Script="Run.cake" --Verbosity="Diagnostic" --Configuration="Debug"
powershell .\build.ps1 -Script Run.cake 
pause