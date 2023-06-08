import { formatDate } from "@angular/common";
import { Injectable } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { CalibrationAgencyDto, CalibrationTypeDto, ItemAccessoryDto, ItemAttachmentDto, ItemMasterDto, ItemRateRevisionDto, ItemSpareDto, ItemStorageConditionDto, ItemSupplierDto, ProcurementDto } from "@shared/service-proxies/service-proxies";

@Injectable()
export class ItemFormBuilderService{
    constructor(
        private formBuilder: FormBuilder,
    ){

    }

    private createBookTypeForm(itemMaster: ItemMasterDto) : FormGroup
    {
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let form = this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            itemId: new FormControl(itemMaster.itemId, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
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

        return form;
    }

    private createToolsAndTacklesTypeForm(itemMaster: ItemMasterDto) : FormGroup
    {
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            //itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
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

    private createFurnitureAndFixturesTypeForm(itemMaster: ItemMasterDto) : FormGroup {
        let itemAccessory : ItemAccessoryDto = new ItemAccessoryDto();
        let itemSupplier : ItemSupplierDto = new ItemSupplierDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let itemStorageCondition: ItemStorageConditionDto = new ItemStorageConditionDto();
       return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            //itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
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
            itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
                itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
                    this.createItemStorageCondition(x)
                )
            ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)]),
            // itemRateRevisions: itemMaster.itemRateRevisions && itemMaster.itemRateRevisions.length > 0 ? this.formBuilder.array(
            //     itemMaster.itemRateRevisions.map((x: ItemRateRevisionDto) =>
            //         this.createItemRateRevision(x)
            //     )
            // ) : this.formBuilder.array([this.createItemRateRevision(itemRateRevision)])
        });

    }

    private createRAndMTypeForm(itemMaster: ItemMasterDto) : FormGroup {
        let itemSupplier:ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            // itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
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

    private createOfficeEquipmentTypeForm(itemMaster: ItemMasterDto) : FormGroup {
        let itemAccessory: ItemAccessoryDto = new ItemAccessoryDto();
        let itemSupplier : ItemSupplierDto = new ItemSupplierDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let itemStorageCondition : ItemStorageConditionDto = new ItemStorageConditionDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            // itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            alias: new FormControl(itemMaster.alias, []),
            itemType: new FormControl(itemMaster.itemType ? <number>itemMaster.itemType : null, []),
            amcRequired: new FormControl(itemMaster.amcRequired ? <number>itemMaster.amcRequired : null, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
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
            quantityPerOrderingUOM : new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, []),
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
            itemAccessories: itemMaster.itemAccessories && itemMaster.itemAccessories.length > 0 ? this.formBuilder.array(
                itemMaster.itemAccessories.map((x: ItemAccessoryDto) =>
                    this.createItemAccessory(x)
                )
            ) : this.formBuilder.array([this.createItemAccessory(itemAccessory)]),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)]),
            itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
                itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
                    this.createItemStorageCondition(x)
                )
            ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)])
        });
    }

    private createMaterialTypeForm(itemMaster: ItemMasterDto): FormGroup{
        let itemSupplier : ItemSupplierDto = new ItemSupplierDto();
        let itemAttachment: ItemAttachmentDto = new ItemAttachmentDto();
        let itemStorageCondition: ItemStorageConditionDto = new ItemStorageConditionDto();
        let itemProcurement : ProcurementDto = new ProcurementDto();
        let itemRateRevision: ItemRateRevisionDto = new ItemRateRevisionDto();
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            //itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, [Validators.required]),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            alias: new FormControl(itemMaster.alias, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
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
            comment : new FormControl(itemMaster.comment ? itemMaster.comment : null, []),
            msl: new FormControl(itemMaster.msl ? itemMaster.msl : null, []),
            materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            stockUOMId : new FormControl(itemMaster.stockUOMId ? itemMaster.stockUOMId : null, []),
            orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
            ctqRequirement : new FormControl(itemMaster.ctqRequirement ? <number>itemMaster.ctqRequirement : null, []),
            ctqSpecifications : new FormControl(itemMaster.ctqSpecifications ? itemMaster.ctqSpecifications : null, []),
            expiryApplicable : new FormControl(itemMaster.expiryApplicable ? <number>itemMaster.expiryApplicable : null, []),
            quantityPerOrderingUOM : new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, []),
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
            itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
                itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
                    this.createItemStorageCondition(x)
                )
            ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)]),
            itemProcurements: itemMaster.itemProcurements && itemMaster.itemProcurements.length > 0 ? this.formBuilder.array(
                itemMaster.itemProcurements.map((x: ProcurementDto) =>
                    this.createItemProcurement(x)
                )
            ) : this.formBuilder.array([this.createItemProcurement(itemProcurement)]),
            itemRateRevisions: itemMaster.itemRateRevisions && itemMaster.itemRateRevisions.length > 0 ? this.formBuilder.array(
                itemMaster.itemRateRevisions.map((x: ItemRateRevisionDto) =>
                    this.createItemRateRevision(x)
                )
            ) : this.formBuilder.array([this.createItemRateRevision(itemRateRevision)])
        });
    }

    private createChemicalsTypeForm(itemMaster: ItemMasterDto) : FormGroup{
        let itemAttachment: ItemAttachmentDto = new ItemAttachmentDto();
        let itemRateRevision: ItemRateRevisionDto = new ItemRateRevisionDto(); 
        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            //itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            alias: new FormControl(itemMaster.alias, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
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
            comment : new FormControl(itemMaster.comment ? itemMaster.comment : null, []),
            msl: new FormControl(itemMaster.msl ? itemMaster.msl : null, []),
            materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            stockUOMId : new FormControl(itemMaster.stockUOMId ? itemMaster.stockUOMId : null, []),
            orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
            ctqRequirement : new FormControl(itemMaster.ctqRequirement ? <number>itemMaster.ctqRequirement : null, []),
            ctqSpecifications : new FormControl(itemMaster.ctqSpecifications ? itemMaster.ctqSpecifications : null, []),
            expiryApplicable : new FormControl(itemMaster.expiryApplicable ? <number>itemMaster.expiryApplicable : null, []),
            quantityPerOrderingUOM : new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, []),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)]),
            itemRateRevisions: itemMaster.itemRateRevisions && itemMaster.itemRateRevisions.length > 0 ? this.formBuilder.array(
                itemMaster.itemRateRevisions.map((x: ItemRateRevisionDto) =>
                    this.createItemRateRevision(x)
                )
            ) : this.formBuilder.array([this.createItemRateRevision(itemRateRevision)])
        });
    }

    private createGlasswareTypeForm(itemMaster : ItemMasterDto)  : FormGroup {
        let itemCalibrationType : CalibrationTypeDto = new CalibrationTypeDto();
        let itemCalibrationAgency : CalibrationAgencyDto = new CalibrationAgencyDto();
        let itemSupplier : ItemSupplierDto = new ItemSupplierDto();
        let itemSpare : ItemSpareDto = new ItemSpareDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();
        let itemStorageCondition: ItemStorageConditionDto = new ItemStorageConditionDto();
        let itemProcurement: ProcurementDto = new ProcurementDto();

        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl({value : itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            // itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            alias: new FormControl(itemMaster.alias, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
            calibrationRequirement: new FormControl(itemMaster.calibrationRequirement ? <number>itemMaster.calibrationRequirement : null, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
            quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
            rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.rateAsOnDate.toString()).toFixed(2) : null, []), 
            leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            comment : new FormControl(itemMaster.comment ? itemMaster.comment : null, []),
            msl: new FormControl(itemMaster.msl ? itemMaster.msl : null, []),
            materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            unitOrderId : new FormControl(itemMaster.unitOrderId ? itemMaster.unitOrderId : null, []),
            unitStockId : new FormControl(itemMaster.unitStockId ? itemMaster.unitStockId : null, []),
            orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
            itemCalibrationTypes: itemMaster.itemCalibrationTypes && itemMaster.itemCalibrationTypes.length > 0 ? this.formBuilder.array(
                itemMaster.itemCalibrationTypes.map((x: CalibrationTypeDto) =>
                    this.createCalibrationType(x)
                )
            ) : this.formBuilder.array([this.createCalibrationType(itemCalibrationType)]),
            itemCalibrationAgencies: itemMaster.itemCalibrationAgencies && itemMaster.itemCalibrationAgencies.length > 0 ? this.formBuilder.array(
                itemMaster.itemCalibrationAgencies.map((x: CalibrationAgencyDto) =>
                    this.createCalibrationAgency(x)
                )
            ) : this.formBuilder.array([this.createCalibrationAgency(itemCalibrationAgency)]),
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
            itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
                itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
                    this.createItemStorageCondition(x)
                )
            ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)]),
            itemProcurements: itemMaster.itemProcurements && itemMaster.itemProcurements.length > 0 ? this.formBuilder.array(
                itemMaster.itemProcurements.map((x: ProcurementDto) =>
                    this.createItemProcurement(x)
                )
            ) : this.formBuilder.array([this.createItemProcurement(itemProcurement)]),
        });
    }

    private createLabInstrumentTypeForm(itemMaster : ItemMasterDto): FormGroup{
        let itemSpare: ItemSpareDto = new ItemSpareDto();
        let itemAccessory: ItemAccessoryDto = new ItemAccessoryDto();
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemStorageCondition: ItemStorageConditionDto = new ItemStorageConditionDto();
        let itemCalibrationType: CalibrationTypeDto = new CalibrationTypeDto();
        let itemCalibrationAgency: CalibrationAgencyDto = new CalibrationAgencyDto();
        let itemAttachment : ItemAttachmentDto = new ItemAttachmentDto();

        return this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, []),
            genericName: new FormControl(itemMaster.genericName, []),
            itemName: new FormControl({value : itemMaster.itemName ? itemMaster.itemName : null, disabled: itemMaster.id != null ? true : false}, [Validators.required]),
            alias: new FormControl(itemMaster.alias, []),
            itemType: new FormControl(itemMaster.itemType ? <number>itemMaster.itemType : null, []),
            amcRequired: new FormControl(itemMaster.amcRequired ? <number>itemMaster.amcRequired : null, []),
            make: new FormControl(itemMaster.make, []),
            model: new FormControl(itemMaster.model, []),
            serialNumber: new FormControl(itemMaster.serialNumber, []),
            specifications: new FormControl(itemMaster.specifications, []),
            storageConditions: new FormControl(itemMaster.storageConditions, []),
            itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
            calibrationRequirement: new FormControl(itemMaster.calibrationRequirement ? <number>itemMaster.calibrationRequirement : null, []),
            supplierId: new FormControl(itemMaster.supplierId, []),
            hsnCode : new FormControl(itemMaster.hsnCode, []),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
            quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
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
            itemCalibrationTypes: itemMaster.itemCalibrationTypes && itemMaster.itemCalibrationTypes.length > 0 ? this.formBuilder.array(
                itemMaster.itemCalibrationTypes.map((x: CalibrationTypeDto) =>
                    this.createCalibrationType(x)
                )
            ) : this.formBuilder.array([this.createCalibrationType(itemCalibrationType)]),
            itemCalibrationAgencies: itemMaster.itemCalibrationAgencies && itemMaster.itemCalibrationAgencies.length > 0 ? this.formBuilder.array(
                itemMaster.itemCalibrationAgencies.map((x: CalibrationAgencyDto) =>
                    this.createCalibrationAgency(x)
                )
            ) : this.formBuilder.array([this.createCalibrationAgency(itemCalibrationAgency)]),
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
            itemAccessories: itemMaster.itemAccessories && itemMaster.itemAccessories.length > 0 ? this.formBuilder.array(
                itemMaster.itemAccessories.map((x: ItemAccessoryDto) =>
                    this.createItemAccessory(x)
                )
            ) : this.formBuilder.array([this.createItemAccessory(itemAccessory)]),
            itemAttachments: itemMaster.itemAttachments && itemMaster.itemAttachments.length > 0 ? this.formBuilder.array(
                itemMaster.itemAttachments.map((x: ItemAttachmentDto) =>
                    this.createItemAttachment(x)
                )
            ) : this.formBuilder.array([this.createItemAttachment(itemAttachment)]),
            itemStorageConditions: itemMaster.itemStorageConditions && itemMaster.itemStorageConditions.length > 0 ? this.formBuilder.array(
                itemMaster.itemStorageConditions.map((x: ItemStorageConditionDto) =>
                    this.createItemStorageCondition(x)
                )
            ) : this.formBuilder.array([this.createItemStorageCondition(itemStorageCondition)])
        });
    }

    public loadCategoryWiseItemForm(itemMaster : ItemMasterDto) : FormGroup{
        let categoryId = itemMaster.itemCategory; 
        
        if(categoryId == 10001){
            return this.createLabInstrumentTypeForm(itemMaster);
        }

        if(categoryId == 20001){
            return this.createOfficeEquipmentTypeForm(itemMaster);
        }

        if(categoryId == 30001){
            return this.createRAndMTypeForm(itemMaster);
        }

        if(categoryId == 40001){
            return this.createBookTypeForm(itemMaster);
        }

        if(categoryId == 50001){
            return this.createGlasswareTypeForm(itemMaster);
        }

        if(categoryId == 60001){
            return this.createChemicalsTypeForm(itemMaster);
        }

        if(categoryId == 70001){
            return this.createMaterialTypeForm(itemMaster);
        }

        if(categoryId == 80001){
            return this.createFurnitureAndFixturesTypeForm(itemMaster);
        }

        if(categoryId == 90001){
            return this.createToolsAndTacklesTypeForm(itemMaster);
        }
    }

    public initialisePrimaryItemMasterForm(itemMaster: ItemMasterDto) : FormGroup {
            return this.formBuilder.group({
                id: new FormControl(itemMaster.id, []),
                categoryId: new FormControl(itemMaster.categoryId, []),
                itemId: new FormControl(itemMaster.itemId, []),
                itemCategory: new FormControl(itemMaster.itemCategory ? <number>itemMaster.itemCategory : null, [Validators.required]),
                genericName: new FormControl(itemMaster.genericName, []),
                itemName: new FormControl(itemMaster.itemName, [Validators.required]),
                alias: new FormControl(itemMaster.alias, []),
                itemType: new FormControl(itemMaster.itemType ? <number>itemMaster.itemType : null, []),
                amcRequired: new FormControl(itemMaster.amcRequired ? <number>itemMaster.amcRequired : null, []),
                make: new FormControl(itemMaster.make, []),
                model: new FormControl(itemMaster.model, []),
                serialNumber: new FormControl(itemMaster.serialNumber, []),
                specifications: new FormControl(itemMaster.specifications, []),
                storageConditions: new FormControl(itemMaster.storageConditions, []),
                itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
                calibrationRequirement: new FormControl(itemMaster.calibrationRequirement ? <number>itemMaster.calibrationRequirement : null, []),
                supplierId: new FormControl(itemMaster.supplierId, []),
                hsnCode : new FormControl(itemMaster.hsnCode, []),
                gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
                purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
                purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
                orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
                quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
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
                comment : new FormControl(itemMaster.comment ? itemMaster.comment : null, []),
                msl: new FormControl(itemMaster.msl ? itemMaster.msl : null, []),
                materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
                unitOrderId : new FormControl(itemMaster.unitOrderId ? itemMaster.unitOrderId : null, []),
                unitStockId : new FormControl(itemMaster.unitStockId ? itemMaster.unitStockId : null, []),
                stockUOMId : new FormControl(itemMaster.stockUOMId ? itemMaster.stockUOMId : null, []),
                orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
                ctqRequirement : new FormControl(itemMaster.ctqRequirement ? <number>itemMaster.ctqRequirement : null, []),
                ctqSpecifications : new FormControl(itemMaster.ctqSpecifications ? itemMaster.ctqSpecifications : null, []),
                expiryApplicable : new FormControl(itemMaster.expiryApplicable ? <number>itemMaster.expiryApplicable : null, []),
                quantityPerOrderingUOM : new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
                minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, [])
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

    createCalibrationType(calibrationTypeItem: CalibrationTypeDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(calibrationTypeItem.id, []),
            frequency: new FormControl(calibrationTypeItem.frequency, []),
            type: new FormControl(calibrationTypeItem.type, [])
        });
    }

    createCalibrationAgency(calibrationAgencyItem: CalibrationAgencyDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(calibrationAgencyItem.id, []),
            supplierId: new FormControl(calibrationAgencyItem.supplierId, [])
        });
    }

    createItemRateRevision(itemRateRevision: ItemRateRevisionDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemRateRevision.id, []),
            make: new FormControl(itemRateRevision.make ? itemRateRevision.make : null, []),
            orderingQuantity: new FormControl(itemRateRevision.orderingQuantity? parseFloat(itemRateRevision.orderingQuantity.toString()).toFixed(2) : null, []),
            catalogueNumber : new FormControl(itemRateRevision.catalogueNumber ? itemRateRevision.catalogueNumber : null, []),
            dateOfEntry: new FormControl(itemRateRevision.dateOfEntry ? formatDate(new Date(<string><unknown>itemRateRevision.dateOfEntry), "yyyy-MM-dd", "en") : null, []),
            ratePerOrderingQuantity : new FormControl(itemRateRevision.ratePerOrderingQuantity? parseFloat(itemRateRevision.ratePerOrderingQuantity.toString()).toFixed(2) : null, []),
            stockQuantityPerOrderingUOM : new FormControl(itemRateRevision.stockQuantityPerOrderingUOM? parseFloat(itemRateRevision.stockQuantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            ratePerStockUOM : new FormControl(itemRateRevision.ratePerStockUOM? parseFloat(itemRateRevision.ratePerStockUOM.toString()).toFixed(2) : 0, []),
            orderingUOMId: new FormControl(itemRateRevision.orderingUOMId? itemRateRevision.orderingUOMId : null, []),
            stockUOMId : new FormControl(itemRateRevision.stockUOMId? itemRateRevision.stockUOMId : null, []),
            itemId: new FormControl(itemRateRevision.itemId, [])
        });
    }

    createItemStorageCondition(itemStorageCondition: ItemStorageConditionDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemStorageCondition.id, []),
            hazardous: new FormControl(itemStorageCondition.hazardous ? <number>itemStorageCondition.hazardous : null, []),
            thresholdQuantity: new FormControl(itemStorageCondition.thresholdQuantity? <number>itemStorageCondition.thresholdQuantity : null, []),
            location: new FormControl(itemStorageCondition.location, []),
            itemId: new FormControl(itemStorageCondition.itemId, [])
        });
    }

    createItemProcurement(itemProcurement: ProcurementDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemProcurement.id, []),
            make: new FormControl(itemProcurement.make ? itemProcurement.make : null, []),
            quantityPerOrderingUOM: new FormControl(itemProcurement.quantityPerOrderingUOM? <number>itemProcurement.quantityPerOrderingUOM : null, []),
            ratePerPack : new FormControl(itemProcurement.ratePerPack? <number>itemProcurement.ratePerPack : null, []),
            ratePerStockUOM : new FormControl(itemProcurement.ratePerStockUOM? <number>itemProcurement.ratePerStockUOM : null, []),
            catalogueNumber : new FormControl(itemProcurement.catalogueNumber? itemProcurement.catalogueNumber : null, []),
            itemId: new FormControl(itemProcurement.itemId? itemProcurement.itemId : null, [])
        });
    }


}