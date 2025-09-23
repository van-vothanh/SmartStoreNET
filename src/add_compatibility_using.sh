#!/bin/bash

# Files that need the compatibility using statement
files=(
    "Libraries/SmartStore.Core/Domain/Catalog/Category.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/Manufacturer.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/Product.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductAttribute.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductCategory.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductManufacturer.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductTag.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttribute.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttributeCombination.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/ProductVariantAttributeValue.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/SmartStoreProductVariantAttributeCombination.cs"
    "Libraries/SmartStore.Core/Domain/Catalog/SpecificationAttribute.cs"
    "Libraries/SmartStore.Core/Domain/Cms/MenuItemRecord.cs"
    "Libraries/SmartStore.Core/Domain/Cms/MenuRecord.cs"
    "Libraries/SmartStore.Core/Domain/Customers/Customer.cs"
    "Libraries/SmartStore.Core/Domain/Customers/CustomerRole.cs"
    "Libraries/SmartStore.Core/Domain/Customers/CustomerRoleMapping.cs"
    "Libraries/SmartStore.Core/Domain/Customers/WalletHistory.cs"
    "Libraries/SmartStore.Core/Domain/DataExchange/SyncMapping.cs"
    "Libraries/SmartStore.Core/Domain/Forums/Forum.cs"
    "Libraries/SmartStore.Core/Domain/Forums/ForumGroup.cs"
    "Libraries/SmartStore.Core/Domain/Forums/ForumPost.cs"
    "Libraries/SmartStore.Core/Domain/Forums/ForumTopic.cs"
    "Libraries/SmartStore.Core/Domain/Localization/LocalizedProperty.cs"
    "Libraries/SmartStore.Core/Domain/Logging/Log.cs"
    "Libraries/SmartStore.Core/Domain/Media/Download.cs"
    "Libraries/SmartStore.Core/Domain/Media/MediaFile.cs"
    "Libraries/SmartStore.Core/Domain/Media/MediaFolder.cs"
    "Libraries/SmartStore.Core/Domain/Media/MediaTrack.cs"
    "Libraries/SmartStore.Core/Domain/Messages/NewsLetterSubscription.cs"
    "Libraries/SmartStore.Core/Domain/News/NewsItem.cs"
    "Libraries/SmartStore.Core/Domain/Orders/Order.cs"
    "Libraries/SmartStore.Core/Domain/Security/PermissionRecord.cs"
    "Libraries/SmartStore.Core/Domain/Tasks/ScheduleTask.cs"
    "Libraries/SmartStore.Core/Domain/Tasks/ScheduleTaskHistory.cs"
)

for file in "${files[@]}"; do
    if [ -f "$file" ]; then
        # Check if the using statement is already there
        if ! grep -q "using SmartStore.Core.Compatibility;" "$file"; then
            # Find the first using statement and add our using after it
            sed -i '1,/^using /s/^using /using SmartStore.Core.Compatibility;\nusing /' "$file"
        fi
    fi
done
