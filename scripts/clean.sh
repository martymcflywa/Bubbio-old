#!/bin/bash
source ./scripts/constants.sh

set -e

echo -e "\n*****************\nCLEANING SOLUTION\n*****************\n"
dotnet clean --configuration Release
dotnet clean --configuration Debug

if [ -d $ARTIFACTS ]; then
    rm -rf $ARTIFACTS
fi