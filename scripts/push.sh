#!/bin/bash
source ./scripts/constants.sh

set -e

echo -e "\n*************\nPUSH PACKAGES\n*************\n"
find $ARTIFACTS -name "*.nupkg" -exec dotnet nuget push {} --source https://www.myget.org/F/feedtheneedy/api/v2/package --api-key $MYGET_APIKEY \;