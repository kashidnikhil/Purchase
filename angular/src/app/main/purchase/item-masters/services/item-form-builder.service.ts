import { formatDate } from "@angular/common";
import { Injectable } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { ItemAccessoryDto, ItemAttachmentDto, ItemMasterDto, ItemSpareDto, ItemSupplierDto } from "@shared/service-proxies/service-proxies";

@Injectable()
export class ItemFormBuilderService{
    constructor(
        private formBuilder: FormBuilder,
    ){

    }

    itemMasterForm!: FormGroup;

    public createBookTypeForm(itemMaster: ItemMasterDto) : FormGroup
    {
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            itemId: new FormControl(itemMaster.itemId, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl(itemMaster.itemName, []),
            alias: new FormControl(itemMaster.alias, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            author : new FormControl(itemMaster.author ? itemMaster.author : null, []),
            publication : new FormControl(itemMaster.publication ? itemMaster.publication : null, []),
            publicationYear : new FormControl(itemMaster.publicationYear ? <number>itemMaster.publicationYear : null, []),
            subjectCategory : new FormControl(itemMaster.subjectCategory ? <number>itemMaster.subjectCategory : null, []),
            purchasedBy : new FormControl(itemMaster.purchasedBy ? <number>itemMaster.purchasedBy : null, []),
            itemSuppliers: itemMaster.itemSuppliers && itemMaster.itemSuppliers.length > 0 ? this.formBuilder.array(
                itemMaster.itemSuppliers.map((x: ItemSupplierDto) =>
                    this.createItemSupplier(x)
                )
            ) : this.formBuilder.array([this.createItemSupplier(itemSupplier)]),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)]),
        });
    }

    public createToolsAndTacklesTypeForm(itemMaster: ItemMasterDto) : FormGroup
    {
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl(itemMaster.itemName, []),
            alias: new FormControl(itemMaster.alias, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
            orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []), 
            ratePerQuantity : new FormControl(itemMaster.ratePerQuantity ? parseFloat(itemMaster.ratePerQuantity.toString()).toFixed(2) : null, []),
            rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.rateAsOnDate.toString()).toFixed(2) : null, []), 
            leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            author : new FormControl(itemMaster.author ? itemMaster.author : null, []),
            publication : new FormControl(itemMaster.publication ? itemMaster.publication : null, []),
            publicationYear : new FormControl(itemMaster.publicationYear ? <number>itemMaster.publicationYear : null, []),
            subjectCategory : new FormControl(itemMaster.subjectCategory ? <number>itemMaster.subjectCategory : null, []),
            purchasedBy : new FormControl(itemMaster.purchasedBy ? <number>itemMaster.purchasedBy : null, []),
            itemSuppliers: itemMaster.itemSuppliers && itemMaster.itemSuppliers.length > 0 ? this.formBuilder.array(
                itemMaster.itemSuppliers.map((x: ItemSupplierDto) =>
                    this.createItemSupplier(x)
                )
            ) : this.formBuilder.array([this.createItemSupplier(itemSupplier)]),
            itemSpares: itemMaster.itemSpares && itemMaster.itemSpares.length > 0 ? this.formBuilder.array(
                itemMaster.itemSpares.map((x: ItemSpareDto) =>
                    this.createItemSpare(x)
                )
            ) : this.formBuilder.array([this.createItemSpare(itemSpare)]),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)])
           
        });
    }

    createFurnitureAndFixturesTypeForm(itemMaster: ItemMasterDto) {
        let itemAccessory : ItemAccessoryDto = new ItemAccessoryDto();
        let itemSupplier : ItemSupplierDto = new ItemSupplierDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
       return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl(itemMaster.itemName, []),
            alias: new FormControl(itemMaster.alias, []),
            itemType: new FormControl(itemMaster.itemType ? <number>itemMaster.itemType : null, []),
            amcRequired: new FormControl(itemMaster.amcRequired ? <number>itemMaster.amcRequired : null, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
            rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.rateAsOnDate.toString()).toFixed(2) : null, []), 
            leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            stockUOMId : new FormControl(itemMaster.stockUOMId ? itemMaster.stockUOMId : null, []),
            orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
            quantityPerOrderingUOM: new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, []),
            itemAccessories: itemMaster.itemAccessories && itemMaster.itemAccessories.length > 0 ? this.formBuilder.array(
                itemMaster.itemAccessories.map((x: ItemAccessoryDto) =>
                    this.createItemAccessory(x)
                )
            ) : this.formBuilder.array([this.createItemAccessory(itemAccessory)]),
            itemSuppliers: itemMaster.itemSuppliers && itemMaster.itemSuppliers.length > 0 ? this.formBuilder.array(
                itemMaster.itemSuppliers.map((x: ItemSupplierDto) =>
                    this.createItemSupplier(x)
                )
            ) : this.formBuilder.array([this.createItemSupplier(itemSupplier)]),
            itemSpares: itemMaster.itemSpares && itemMaster.itemSpares.length > 0 ? this.formBuilder.array(
                itemMaster.itemSpares.map((x: ItemSpareDto) =>
                    this.createItemSpare(x)
                )
            ) : this.formBuilder.array([this.createItemSpare(itemSpare)]),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)]),
            // itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
            //     itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
            //         this.createItemStorageCondition(x)
            //     )
            // ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)]),
            // itemRateRevisions: itemMaster.itemRateRevisions && itemMaster.itemRateRevisions.length > 0 ? this.formBuilder.array(
            //     itemMaster.itemRateRevisions.map((x: ItemRateRevisionDto) =>
            //         this.createItemRateRevision(x)
            //     )
            // ) : this.formBuilder.array([this.createItemRateRevision(itemRateRevision)])
        });

    }

    createRAndMTypeForm(itemMaster: ItemMasterDto){
        let itemSupplier:ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl(itemMaster.itemName, []),
            alias: new FormControl(itemMaster.alias, []),
            specifications: new FormControl(itemMaster.specifications, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            ratePerQuantity : new FormControl(itemMaster.ratePerQuantity ? parseFloat(itemMaster.ratePerQuantity.toString()).toFixed(2) : null, []), 
            rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.rateAsOnDate.toString()).toFixed(2) : null, []), 
            leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            itemSuppliers: itemMaster.itemSuppliers && itemMaster.itemSuppliers.length > 0 ? this.formBuilder.array(
                itemMaster.itemSuppliers.map((x: ItemSupplierDto) =>
                    this.createItemSupplier(x)
                )
            ) : this.formBuilder.array([this.createItemSupplier(itemSupplier)]),
             itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)])
        });
    }

    createItemSupplier(itemSupplier: ItemSupplierDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemSupplier.id, []),
            supplierId: new FormControl(itemSupplier.supplierId, [])
        });
    }

    createItemAttachment(itemAttachment: ItemAttachmentDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemAttachment.id, []),
            path: new FormControl(itemAttachment.path? itemAttachment.path : null, []),
            description: new FormControl(itemAttachment.description ? itemAttachment.description : null, []),
            itemId: new FormControl(itemAttachment.itemId, [])
        });
    }

    createItemSpare(itemSpare: ItemSpareDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemSpare.id, []),
            itemSparesId: new FormControl(itemSpare.itemSparesId ? itemSpare.itemSparesId : null, []),
            itemId: new FormControl(itemSpare.itemId, [])
        });
    }

    createItemAccessory(itemAccessory: ItemAccessoryDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemAccessory.id, []),
            accessoryId: new FormControl(itemAccessory.accessoryId ? itemAccessory.accessoryId : null, []),
            itemId: new FormControl(itemAccessory.itemId, [])
        });
    }


}