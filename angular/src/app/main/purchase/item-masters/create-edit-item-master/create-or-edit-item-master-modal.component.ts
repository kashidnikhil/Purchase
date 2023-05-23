import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    CalibrationAgencyDto,
    CalibrationAgencyInputDto,
    CalibrationTypeDto,
    CalibrationTypeInputDto,
    ItemAttachmentDto,
    ItemAttachmentInputDto,
    ItemMasterDto,
    ItemMasterInputDto,
    ItemMasterListDto,
    ItemServiceProxy,
    ItemSpareDto,
    ItemSpareInputDto,
    ItemSupplierDto,
    ItemSupplierInputDto,
    LegalEntityServiceProxy,
    MappedSupplierCategoryDto,
    MappedSupplierCategoryInputDto,
    SupplierAddressDto,
    SupplierBankDto,
    SupplierCategoryDto,
    SupplierCategoryServiceProxy,
    SupplierContactPersonDto,
    SupplierDto,
    SupplierInputDto,
    SupplierServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DropdownDto } from '@app/shared/common/data-models/dropdown';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { ItemMockService } from '@app/shared/common/mock-data-services/item.mock.service';
import { formatDate } from '@angular/common';

@Component({
    selector: 'create-edit-item-master-modal',
    templateUrl: './create-or-edit-item-master-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-item-master-modal.component.less']
})
export class CreateOrEditItemMasterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditItemMasterModal', { static: true }) modal: ModalDirective;
    @ViewChild(TabsetComponent) tabSet: TabsetComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    itemMasterForm!: FormGroup;
    active: boolean = false;
    submitted: boolean = false;
    saving: boolean = false;

    itemCategoriesList: DropdownDto[] = [];
    itemTypeList: DropdownDto[] = [];
    amcRequirementList: DropdownDto[] = [];
    itemMobilityList: DropdownDto[] = [];
    calibrationRequirementList: DropdownDto[] = [];
    calibrationTypeList: DropdownDto[] = [];
    calibrationFrequencyList: DropdownDto[] = [];
    itemStatusList: DropdownDto[] = [];
    supplierList: SupplierDto[] = [];
    itemMasterList  : ItemMasterListDto [] = [];

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService: SupplierServiceProxy,
        private _itemMasterService: ItemServiceProxy,
        private _itemMockService: ItemMockService
    ) {
        super(injector);
    }

    async show(itemMasterId?: string) {
        await this.loadDropdownList();
        let itemMaster = new ItemMasterDto();
        if (!itemMasterId) {
            this.initialiseItemMasterForm(itemMaster);
            // this.initialiseSupplierForm(supplierItem);
            this.active = true;
            this.modal.show();
        }
        else {
            this._supplierService.getSupplierMasterById(itemMasterId).subscribe((response: SupplierDto) => {
                let supplierItem = response;
                // this.initialiseSupplierForm(supplierItem);
                this.active = true;
                this.modal.show();
            });
        }
    }

    initialiseItemMasterForm(itemMaster: ItemMasterDto) {
        let itemCalibrationType: CalibrationTypeDto = new CalibrationTypeDto();
        let itemCalibrationAgency: CalibrationAgencyDto = new CalibrationAgencyDto();
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemSpare: ItemSpareDto = new ItemSpareDto();
        this.itemMasterForm = this.formBuilder.group({
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
            hsnCode : new FormControl(itemMaster.hsnCode, [Validators.required]),
            gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, [Validators.required]),
            purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
            quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
            ratePerQuantity : new FormControl(itemMaster.ratePerQuantity ? parseFloat(itemMaster.ratePerQuantity.toString()).toFixed(2) : null, []), 
            rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.ratePerQuantity.toString()).toFixed(2) : null, []), 
            leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
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
                    this.createItemSpare(x)
                )
            ) : this.formBuilder.array([this.createItemSpare(itemSpare)]),
        });

    }

    // initialiseSupplierForm(supplierItem: SupplierDto) {
    //     let supplierAddressItem: SupplierAddressDto = new SupplierAddressDto();
    //     let supplierContactPersonItem: SupplierContactPersonDto = new SupplierContactPersonDto();
    //     let supplierBankItem: SupplierBankDto = new SupplierBankDto();
    //     this.itemMasterForm = this.formBuilder.group({
    //         id: new FormControl(supplierItem.id, []),
    //         name: new FormControl(supplierItem.name, [Validators.required]),
    //         phoneNumber: new FormControl(supplierItem.telephoneNumber, []),
    //         mobile: new FormControl(supplierItem.mobile, []),
    //         email: new FormControl(supplierItem.email, []),
    //         website: new FormControl(supplierItem.website, []),
    //         certifications: new FormControl(supplierItem.certifications, []),
    //         legalEntityId: new FormControl(supplierItem.legalEntityId, []),
    //         gstNumber: new FormControl(supplierItem.gstNumber, []),
    //         yearOfEstablishment: new FormControl(supplierItem.yearOfEstablishment, []),
    //         deliveryBy: new FormControl(supplierItem.deliveryBy, []),
    //         category: new FormControl(supplierItem.category, []),
    //         paymentMode: new FormControl(supplierItem.paymentMode, []),
    //         supplierCategories : [this.unMapSupplierCategories(supplierItem.supplierCategories), []],
    //         // supplierCategories: new FormControl(<SupplierCategoryDto[]>(this.unMapSupplierCategories(supplierItem.supplierCategories)), []),
    //         supplierAddresses: supplierItem.supplierAddresses && supplierItem.supplierAddresses.length > 0 ? this.formBuilder.array(
    //             supplierItem.supplierAddresses.map((x: SupplierAddressDto) =>
    //                 this.createSupplierAddress(x)
    //             )
    //         ) : this.formBuilder.array([this.createSupplierAddress(supplierAddressItem)]),
    //         supplierContactPersons: supplierItem.supplierContactPersons && supplierItem.supplierContactPersons.length > 0 ? this.formBuilder.array(
    //             supplierItem.supplierContactPersons.map((x: SupplierContactPersonDto) =>
    //                 this.createSupplierContactPerson(x)
    //             )
    //         ) : this.formBuilder.array([this.createSupplierContactPerson(supplierContactPersonItem)]),
    //         supplierBanks: supplierItem.supplierBanks && supplierItem.supplierBanks.length > 0 ? this.formBuilder.array(
    //             supplierItem.supplierBanks.map((x: SupplierBankDto) =>
    //                 this.createSupplierBank(x)
    //             )
    //         ) : this.formBuilder.array([this.createSupplierBank(supplierBankItem)])

    //     });
    //     console.log(this.itemMasterForm.value);
    // }

    // Calibration Type related functions
    get itemCalibrationTypes(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemCalibrationTypes'));
    }


    addCalibrationType() {
        let calibrationTypeItem: CalibrationTypeDto = new CalibrationTypeDto();
        let calibrationTypeForm = this.createCalibrationType(calibrationTypeItem);
        this.itemCalibrationTypes.push(calibrationTypeForm);
    }

    createCalibrationType(calibrationTypeItem: CalibrationTypeDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(calibrationTypeItem.id, []),
            frequency: new FormControl(calibrationTypeItem.frequency, []),
            type: new FormControl(calibrationTypeItem.type, [])
        });
    }

    deleteCalibrationTypeItem(indexValue: number) {
        const itemCalibrationTypeArray = this.itemCalibrationTypes;
        itemCalibrationTypeArray.removeAt(indexValue);
    }

    // Calibration Type related functions
    get itemCalibrationAgencies(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemCalibrationAgencies'));
    }

    addCalibrationAgency() {
        let calibrationAgencyItem: CalibrationAgencyDto = new CalibrationAgencyDto();
        let calibrationAgencyForm = this.createCalibrationAgency(calibrationAgencyItem);
        this.itemCalibrationAgencies.push(calibrationAgencyForm);
    }

    createCalibrationAgency(calibrationAgencyItem: CalibrationAgencyDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(calibrationAgencyItem.id, []),
            supplierId: new FormControl(calibrationAgencyItem.supplierId, [])
        });
    }

    deleteCalibrationAgencyItem(indexValue: number) {
        const itemCalibrationAgencyArray = this.itemCalibrationAgencies;
        itemCalibrationAgencyArray.removeAt(indexValue);
    }

    get itemSuppliers(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemSuppliers'));
    }

    addItemSupplier() {
        let itemSupplier: ItemSupplierDto = new ItemSupplierDto();
        let itemSupplierForm = this.createItemSupplier(itemSupplier);
        this.itemSuppliers.push(itemSupplierForm);
    }

    createItemSupplier(itemSupplier: ItemSupplierDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemSupplier.id, []),
            supplierId: new FormControl(itemSupplier.supplierId, [])
        });
    }

    deleteItemSupplier(indexValue: number) {
        const itemSupplierArray = this.itemSuppliers;
        itemSupplierArray.removeAt(indexValue);
    }

    get itemSpares(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemSpares'));
    }

    addItemSpare() {
        let itemSpare: ItemSpareDto = new ItemSpareDto();
        let itemSpareForm = this.createItemSpare(itemSpare);
        this.itemSpares.push(itemSpareForm);
    }

    createItemSpare(itemSpare: ItemSpareDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemSpare.id, []),
            itemId: new FormControl(itemSpare.itemId, [])
        });
    }

    deleteItemSpare(indexValue: number) {
        const itemSpareArray = this.itemSpares;
        itemSpareArray.removeAt(indexValue);
    }

    get itemAttachments(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemAttachments'));
    }

    addItemAttachment() {
        let itemAttachment: ItemAttachmentDto = new ItemAttachmentDto();
        let itemAttachmentForm = this.createItemAttachment(itemAttachment);
        this.itemAttachments.push(itemAttachmentForm);
    }

    createItemAttachment(itemAttachment: ItemAttachmentDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemAttachment.id, []),
            path: new FormControl(itemAttachment.path, []),
            description: new FormControl(itemAttachment.description, []),
            itemId: new FormControl(itemAttachment.itemId, [])
        });
    }

    deleteItemAttachment(indexValue: number) {
        const itemAttachmentArray = this.itemAttachments;
        itemAttachmentArray.removeAt(indexValue);
    }

    async loadDropdownList() {
        await this.loadSuppliers();
        await this.loadItemMasters();
        this.loadItemCategories();
        this.loadItemTypes();
        this.loadAMCRequirementList();
        this.loadItemMobilityList();
        this.loadCalibrationRequirementList();
        this.loadCalibrationTypeList();
        this.loadCalibrationFrequencyList();
        this.loadItemStatusList();
    }

    async loadSuppliers() {
        this.supplierList = await this._supplierService.getSupplierList().toPromise();
    }

    async loadItemMasters() {
        this.itemMasterList = await this._itemMasterService.getItemMasterList().toPromise();
    }

    loadItemCategories() {
        this.itemCategoriesList = this._itemMockService.loadItemCategories();
    }

    loadItemTypes() {
        this.itemTypeList = this._itemMockService.loadItemTypes();
    }

    loadAMCRequirementList() {
        this.amcRequirementList = this._itemMockService.loadYesOrNoTypeDropdownData();
    }

    loadItemMobilityList() {
        this.itemMobilityList = this._itemMockService.loadItemMobilityList();
    }

    loadCalibrationRequirementList() {
        this.calibrationRequirementList = this._itemMockService.loadYesOrNoTypeDropdownData();
    }

    loadCalibrationTypeList() {
        this.calibrationTypeList = this._itemMockService.loadCalibrationTypeList();
    }

    loadCalibrationFrequencyList() {
        this.calibrationFrequencyList = this._itemMockService.loadCalibrationFrequencyList();
    }

    loadItemStatusList() {
        this.itemStatusList = this._itemMockService.loadItemStatusList();
    }

    onShown(): void {
        // document.getElementById('name').focus();
    }

    save(): void {
        this.submitted = true;
        if (this.itemMasterForm.valid) {
            let input = new ItemMasterInputDto();
            this.saving = true;
            input = this.itemMasterForm.value;
            if (input.itemCalibrationAgencies && input.itemCalibrationAgencies.length > 0) {
                let tempCalibrationAgencies = this.mapCalibrationAgencies(input.itemCalibrationAgencies);
                input.itemCalibrationAgencies = tempCalibrationAgencies;
            }

            if (input.itemCalibrationTypes && input.itemCalibrationTypes.length > 0) {
                let tempCalibrationTypes = this.mapCalibrationTypes(input.itemCalibrationTypes);
                input.itemCalibrationTypes = tempCalibrationTypes;
            }

            if (input.itemSuppliers && input.itemSuppliers.length > 0) {
                let tempItemSuppliers = this.mapItemSuppliers(input.itemSuppliers);
                input.itemSuppliers = tempItemSuppliers;
            }

            if (input.itemSpares && input.itemSpares.length > 0) {
                let tempItemSpares = this.mapItemSpares(input.itemSpares);
                input.itemSpares = tempItemSpares;
            }

            if (input.itemAttachments && input.itemAttachments.length > 0) {
                let tempItemAttachments = this.mapItemAttachments(input.itemAttachments);
                input.itemAttachments = tempItemAttachments;
            }

            this._itemMasterService
                .insertOrUpdateItem(input)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe((response: string) => {
                    if (response) {
                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                    // else{
                    //     this.close();
                    //     this.restoreSupplierCategory.emit(response);
                    // }
                });
        }
    }

    mapCalibrationTypes(calibrationTypeList: CalibrationTypeInputDto[]): CalibrationTypeInputDto[] | null {
        let tempCalibrationTypeList: CalibrationTypeInputDto[] = [];
        calibrationTypeList.forEach(item => {
            if (item.frequency != null && item.type != null) {
                let tempCalibrationTypeItem: CalibrationTypeInputDto = new CalibrationTypeInputDto(
                    {
                        id: item.id ? item.id : "",
                        itemId: item.itemId,
                        frequency: item.frequency,
                        type: item.type
                    }
                );
                tempCalibrationTypeList.push(tempCalibrationTypeItem);
            }

        });
        let result = tempCalibrationTypeList.length > 1 ? tempCalibrationTypeList : null;
        return result;
    }


    mapCalibrationAgencies(calibrationAgencyList: CalibrationAgencyInputDto[]): CalibrationAgencyInputDto[] | null {
        let tempCalibrationAgencyList: CalibrationAgencyInputDto[] = [];
        calibrationAgencyList.forEach(item => {
            if (item.supplierId != null) {
                let tempCalibrationAgencyItem: CalibrationAgencyInputDto = new CalibrationAgencyInputDto(
                    {
                        id: item.id ? item.id : "",
                        itemId: item.itemId,
                        supplierId: item.supplierId,
                    }
                );
                tempCalibrationAgencyList.push(tempCalibrationAgencyItem);
            }

        });
        let result = tempCalibrationAgencyList.length > 1 ? tempCalibrationAgencyList : null;
        return result;
    }

    mapItemSuppliers(itemSupplierList: ItemSupplierInputDto[]): ItemSupplierInputDto[] | null {
        let tempItemSupplierList: ItemSupplierInputDto[] = [];
        itemSupplierList.forEach(item => {
            if (item.supplierId != null) {
                let tempItemSupplier: ItemSupplierInputDto = new ItemSupplierInputDto(
                    {
                        id: item.id ? item.id : "",
                        itemId: item.itemId,
                        supplierId: item.supplierId,
                    }
                );
                tempItemSupplierList.push(tempItemSupplier);
            }

        });
        let result = tempItemSupplierList.length > 1 ? tempItemSupplierList : null;
        return result;
    }

    mapItemSpares(itemSpareList: ItemSpareInputDto[]): ItemSpareInputDto[] | null {
        let tempItemSpareList: ItemSpareInputDto[] = [];
        itemSpareList.forEach(item => {
            if (item.itemId != null) {
                let tempItemSpare: ItemSpareInputDto = new ItemSpareInputDto(
                    {
                        id: item.id ? item.id : "",
                        itemId: item.itemId,
                    }
                );
                tempItemSpareList.push(tempItemSpare);
            }

        });
        let result = tempItemSpareList.length > 1 ? tempItemSpareList : null;
        return result;
    }

    mapItemAttachments(itemAttachmentList: ItemAttachmentInputDto[]): ItemAttachmentInputDto[] | null {
        let tempItemAttachmentList: ItemAttachmentInputDto[] = [];
        itemAttachmentList.forEach(item => {
            if (item.description != null && item.path != null) {
                let tempItemAttachment: ItemAttachmentInputDto = new ItemAttachmentInputDto(
                    {
                        id: item.id ? item.id : "",
                        path : item.path,
                        description : item.description,
                        itemId: item.itemId
                    }
                );
                tempItemAttachmentList.push(tempItemAttachment);
            }

        });
        let result = tempItemAttachmentList.length > 1 ? tempItemAttachmentList : null;
        return result;
    }

    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }
}
