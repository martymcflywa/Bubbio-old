#!/bin/bash
set -e

ARTIFACTS=./artifacts
TOOLS=./tools

echo -e "\n*****************\nCLEANING SOLUTION\n*****************\n"
dotnet clean --configuration Release

if [ -d $ARTIFACTS ]; then
    rm -rf $ARTIFACTS
fi
if [ -d $TOOLS ]; then
    rm -rf $TOOLS
fi