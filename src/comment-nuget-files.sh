#!/bin/bash

# Comment out NuGet.Core dependent files

files=(
    "./Libraries/SmartStore.Core/Packaging/Updater/AppUpdater.cs"
    "./Libraries/SmartStore.Core/Packaging/PackageInstaller.cs"
    "./Libraries/SmartStore.Core/Packaging/PackagingUtils.cs"
    "./Libraries/SmartStore.Core/Packaging/NuGet/ExtensionReferenceRepository.cs"
    "./Libraries/SmartStore.Core/Packaging/NuGet/NugetLogger.cs"
    "./Libraries/SmartStore.Core/Packaging/NuGet/NullSourceRepository.cs"
    "./Libraries/SmartStore.Core/Packaging/NuGet/FileBasedProjectSystem.cs"
    "./Libraries/SmartStore.Core/Packaging/PackageBuilder.cs"
)

for file in "${files[@]}"; do
    if [ -f "$file" ]; then
        # Add #if false at the beginning after usings
        awk '
        BEGIN { in_usings = 1; printed_if = 0 }
        /^using / { print; next }
        /^namespace / {
            if (!printed_if) {
                print "// TODO: .NET 8 Migration - NuGet.Core not compatible, needs replacement with NuGet.Protocol"
                print "#if FALSE_NUGET_CORE_NOT_SUPPORTED"
                printed_if = 1
            }
            print
            next
        }
        { print }
        END { if (printed_if) print "#endif" }
        ' "$file" > "$file.tmp" && mv "$file.tmp" "$file"
        echo "Commented out: $file"
    fi
done

echo "NuGet files commented out"
