#!/bin/bash
source ./scripts/constants.sh

set -e

echo -e "\n*****************\nCLEANING SOLUTION\n*****************\n"
dotnet clean --configuration Release

if [ -d $ARTIFACTS ]; then
    rm -rf $ARTIFACTS
fi
if [ -d $TOOLS ]; then
    rm -rf $TOOLS
fi