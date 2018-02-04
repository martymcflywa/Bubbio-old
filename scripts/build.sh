#!/bin/bash
source ./scripts/semver.sh

set -e

echo -e "\n*****************\nBUILDING SOLUTION\n*****************\n"

dotnet build --configuration Release /p:VersionPrefix=$MAJOR.$MINOR.$PATCH /p:VersionSuffix=$VERSION_SUFFIX