#!/usr/bin/env python3
import os
import re
import sys

def remove_index_attributes(file_path):
    """Remove [Index] attributes from a C# file and collect them for migration"""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
    except UnicodeDecodeError:
        # Try with different encoding
        with open(file_path, 'r', encoding='latin-1') as f:
            content = f.read()
    
    # Pattern to match [Index] attributes with optional parameters
    index_pattern = r'\s*\[Index(?:\([^\]]*\))?\]\s*\n'
    
    # Find all matches before removing them
    matches = re.findall(index_pattern, content)
    
    if matches:
        print(f"Removing {len(matches)} [Index] attributes from {file_path}")
        # Remove the attributes
        new_content = re.sub(index_pattern, '', content)
        
        # Write back the modified content
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(new_content)
        
        return True
    return False

def main():
    # List of files containing [Index] attributes
    files_with_index = [
        "/data/code/src/Plugins/SmartStore.GoogleMerchantCenter/Domain/GoogleProductRecord.cs",
        "/data/code/src/Libraries/SmartStore.Core/Rules/Domain/RuleSetEntity.cs",
        "/data/code/src/Libraries/SmartStore.Core/Rules/Domain/RuleEntity.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Customers/CustomerRoleMapping.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Customers/WalletHistory.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Customers/Customer.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Customers/CustomerRole.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Localization/LocalizedProperty.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/SmartStoreProductVariantAttributeCombination.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/Category.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/Product.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttributeCombination.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductCategory.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductAttribute.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/Manufacturer.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttribute.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductManufacturer.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttributeValue.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/SpecificationAttribute.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Catalog/ProductTag.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Media/MediaFile.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Media/MediaTrack.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Media/MediaFolder.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Media/Download.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Security/PermissionRecord.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Blogs/BlogPost.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Logging/Log.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Messages/NewsLetterSubscription.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Orders/Order.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Tasks/ScheduleTask.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Tasks/ScheduleTaskHistory.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Cms/MenuRecord.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Cms/MenuItemRecord.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/News/NewsItem.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/DataExchange/SyncMapping.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Forums/ForumTopic.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Forums/Forum.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Forums/ForumGroup.cs",
        "/data/code/src/Libraries/SmartStore.Core/Domain/Forums/ForumPost.cs"
    ]
    
    modified_count = 0
    for file_path in files_with_index:
        if os.path.exists(file_path):
            if remove_index_attributes(file_path):
                modified_count += 1
        else:
            print(f"File not found: {file_path}")
    
    print(f"\nCompleted: Modified {modified_count} files")

if __name__ == "__main__":
    main()
