#!/bin/bash
set -e

echo -e "\n********************\nSETTING UP MINICOVER\n********************\n"

cd tools
dotnet minicover instrument --workdir ../
dotnet minicover reset
cd ..