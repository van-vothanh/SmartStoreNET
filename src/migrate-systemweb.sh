#!/bin/bash

# System.Web namespace migration script for .NET 8

echo "Starting System.Web migration..."

# Find all C# files
find . -name "*.cs" -type f | while read file; do
    # Skip if file doesn't exist or is in obj/bin directories
    if [[ ! -f "$file" ]] || [[ "$file" == *"/obj/"* ]] || [[ "$file" == *"/bin/"* ]]; then
        continue
    fi
    
    # Core ASP.NET replacements
    sed -i 's/using System\.Web;/using Microsoft.AspNetCore.Http;/g' "$file"
    sed -i 's/using System\.Web\.Caching;/using Microsoft.Extensions.Caching.Memory;/g' "$file"
    sed -i 's/using System\.Web\.Configuration;/using Microsoft.Extensions.Configuration;/g' "$file"
    sed -i 's/using System\.Web\.Hosting;/using Microsoft.AspNetCore.Hosting;/g' "$file"
    sed -i 's/using System\.Web\.Mvc;/using Microsoft.AspNetCore.Mvc;/g' "$file"
    sed -i 's/using System\.Web\.Routing;/using Microsoft.AspNetCore.Routing;/g' "$file"
    sed -i 's/using System\.Web\.Security;/using Microsoft.AspNetCore.Identity;/g' "$file"
    sed -i 's/using System\.Web\.SessionState;/using Microsoft.AspNetCore.Http;/g' "$file"
    sed -i 's/using System\.Web\.Optimization;/\/\/ TODO: Replace with WebOptimizer/g' "$file"
    sed -i 's/using System\.Web\.Helpers;/\/\/ TODO: Replace with TagHelpers/g' "$file"
    sed -i 's/using System\.Web\.Http;/using Microsoft.AspNetCore.Mvc;/g' "$file"
    sed -i 's/using System\.Web\.Http\.Controllers;/using Microsoft.AspNetCore.Mvc.Controllers;/g' "$file"
    sed -i 's/using System\.Web\.Http\.Filters;/using Microsoft.AspNetCore.Mvc.Filters;/g' "$file"
    sed -i 's/using System\.Web\.Http\.Results;/using Microsoft.AspNetCore.Mvc;/g' "$file"
    sed -i 's/using System\.Web\.Http\.Routing;/using Microsoft.AspNetCore.Routing;/g' "$file"
    sed -i 's/using System\.Web\.ModelBinding;/using Microsoft.AspNetCore.Mvc.ModelBinding;/g' "$file"
    sed -i 's/using System\.Web\.Script\.Serialization;/using System.Text.Json;/g' "$file"
    
    # Infrastructure / Legacy - comment out
    sed -i 's/using System\.Web\.Compilation;/\/\/ TODO: .NET 8 - System.Web.Compilation removed/g' "$file"
    sed -i 's/using System\.Web\.UI;/\/\/ TODO: .NET 8 - System.Web.UI removed/g' "$file"
    sed -i 's/using System\.Web\.Util;/\/\/ TODO: .NET 8 - System.Web.Util removed/g' "$file"
    sed -i 's/using System\.Web\.Management;/\/\/ TODO: .NET 8 - System.Web.Management removed/g' "$file"
    sed -i 's/using System\.Web\.Profile;/\/\/ TODO: .NET 8 - System.Web.Profile removed/g' "$file"
    sed -i 's/using System\.Web\.Providers;/\/\/ TODO: .NET 8 - System.Web.Providers removed/g' "$file"
    sed -i 's/using System\.Web\.ClientServices;/\/\/ TODO: .NET 8 - System.Web.ClientServices removed/g' "$file"
    sed -i 's/using System\.Web\.Mail;/using System.Net.Mail;/g' "$file"
    sed -i 's/using System\.Web\.Handlers;/\/\/ TODO: .NET 8 - Replace with middleware/g' "$file"
    sed -i 's/using System\.Web\.Modules;/\/\/ TODO: .NET 8 - Replace with middleware/g' "$file"
    
    # Framework & Data
    sed -i 's/using System\.Runtime\.Caching;/using Microsoft.Extensions.Caching.Memory;/g' "$file"
    sed -i 's/using System\.Data\.Entity;/using Microsoft.EntityFrameworkCore;/g' "$file"
    sed -i 's/using System\.Data\.Objects;/using Microsoft.EntityFrameworkCore;/g' "$file"
    
    # Remove Microsoft.Web.Infrastructure
    sed -i 's/using Microsoft\.Web\.Infrastructure.*;/\/\/ TODO: .NET 8 - Microsoft.Web.Infrastructure removed/g' "$file"
done

echo "System.Web migration complete!"
