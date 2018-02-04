#!/bin/bash
source ./scripts/semver.sh

set -e

ARTIFACTS=../artifacts

echo -e "\n****************\nPACKING SOLUTION\n****************\n"
dotnet pack --configuration Release --no-build --output $ARTIFACTS /p:VersionPrefix=$MAJOR.$MINOR.$PATCH /p:VersionSuffix=$VERSION_SUFFIX