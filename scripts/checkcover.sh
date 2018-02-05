#!/bin/bash
source ./scripts/constants.sh

set -e

echo -e "\n*****************\nCHECKING COVERAGE\n*****************\n"

cd tools
dotnet minicover uninstrument --workdir ../
dotnet minicover report --workdir ../ --threshold $MIN_COVER
cd ..