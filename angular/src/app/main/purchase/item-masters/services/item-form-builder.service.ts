import { formatDate } from "@angular/common";
import { Injectable } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { ItemAccessoryDto, ItemAttachmentDto, ItemMasterDto, ItemSupplierDto } from "@shared/service-proxies/service-proxies";

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
        this.itemMasterForm = this.formBuilder.group({
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
        return this.itemMasterForm;
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


}