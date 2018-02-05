#!/bin/bash
source ./scripts/constants.sh

set -e

# only execute dotnet test on test projects
echo -e "\n****************\nTESTING SOLUTION\n****************\n"
for project in test/**/*.csproj; do
    dotnet test $project --configuration Release --no-build;
done