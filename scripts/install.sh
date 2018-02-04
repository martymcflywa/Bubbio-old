#!/bin/bash
source ./scripts/constants.sh

set -e

# install tools here, not doing this atm, installing mono takes too long

# echo -e "\n****************\nINSTALLING TOOLS\n****************\n"
# mkdir ./tools
# curl -L -o $NUGET https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
# mono $NUGET install coveralls.net -Version 0.7.0 -OutputDirectory ./tools