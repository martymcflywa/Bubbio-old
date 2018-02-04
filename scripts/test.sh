#!/bin/bash
set -e

TEST_SUFFIX=".Tests.csproj"
COVERALLS=./tools/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe

# only execute dotnet test on test projects
echo -e "\n****************\nTESTING SOLUTION\n****************\n"
find . -name "*$TEST_SUFFIX" -exec dotnet test {} --configuration Release --no-build \;

# echo -e "\n*****************\nCHECKING COVERAGE\n*****************\n"
# mono $COVERALLS...