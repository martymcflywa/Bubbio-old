#!/bin/bash
set -e

# Setting semver here, modify MAJOR/MINOR when ready
# PATCH uses pull request number
# VERSION_SUFFIX includes date and build number

# PULL_REQ is a custom environment variable in travis-ci
# If build is for pr, update PULL_REQ
# If build is for pr merge to master, use current value
# If build is for local, use zero

PULL_REQ=0
COMMIT_REGEX="^Merge pull request \#([0-9]*)"

# this is a master build
if [[ $TRAVIS_PULL_REQUEST == false ]] && [[ $TRAVIS_BRANCH == master ]]; then    
    if [[ $TRAVIS_COMMIT_MESSAGE =~ $COMMIT_REGEX ]]; then
        PULL_REQ=${BASH_REMATCH[1]}
    fi
fi

# this is a pr build
if [[ $TRAVIS_PULL_REQUEST =~ "^[0-9]*$" ]]; then
    PULL_REQ=$TRAVIS_PULL_REQUEST
fi

if [[ -n "$TRAVIS_BUILD_NUMBER" ]]; then
    BUILD_NUMBER=$TRAVIS_BUILD_NUMBER
else
    BUILD_NUMBER=0
fi

MAJOR=0
MINOR=0
PATCH=$PULL_REQ
DATE=`date +%Y%m%d`
VERSION_SUFFIX="prealpha-$DATE-$BUILD_NUMBER"

echo -e "\nSEMVER $MAJOR.$MINOR.$PATCH-$VERSION_SUFFIX"