#!/usr/bin/env fish
# Expects the pwd to be the repo/docs

echo Starting docs
dotnet build ../MonkeStatistics.csproj
docfx build
docfx serve _site --port 8081
