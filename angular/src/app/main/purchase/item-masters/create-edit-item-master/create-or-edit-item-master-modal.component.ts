import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    CalibrationAgencyDto,
    CalibrationAgencyInputDto,
    CalibrationTypeDto,
    CalibrationTypeInputDto,
    ItemAccessoryDto,
    ItemAccessoryInputDto,
    ItemAttachmentDto,
    ItemAttachmentInputDto,
    ItemMasterDto,
    ItemMasterInputDto,
    ItemMasterListDto,
    ItemRateRevisionDto,
    ItemRateRevisionInputDto,
    ItemServiceProxy,
    ItemSpareDto,
    ItemSpareInputDto,
    ItemStorageConditionDto,
    ItemStorageConditionInputDto,
    ItemSupplierDto,
    ItemSupplierInputDto,
    MaterialGradeDto,
    MaterialGradeServiceProxy,
    ProcurementInputDto,
    ResponseDto,
    SupplierDto,
    SupplierServiceProxy,
    UnitDto,
    UnitServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DropdownDto } from '@app/shared/common/data-models/dropdown';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { ItemMockService } from '@app/shared/common/mock-data-services/item.mock.service';
import { formatDate } from '@angular/common';
import { ProcurementDto } from '@shared/service-proxies/service-proxies';
import { ItemFormBuilderService } from '../services/item-form-builder.service';

@Component({
    selector: 'create-edit-item-master-modal',
    templateUrl: './create-or-edit-item-master-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-item-master-modal.component.less'],
    providers: [ItemFormBuilderService]
})
export class CreateOrEditItemMasterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditItemMasterModal', { static: true }) modal: ModalDirective;
    @ViewChild(TabsetComponent) tabSet: TabsetComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() saveItemMaster: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();


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
    hazardousList : DropdownDto[] = [];
    subjectCategoryList : DropdownDto[] = [];
    yearList : DropdownDto[] = [];
    supplierList: SupplierDto[] = [];
    itemMasterList  : ItemMasterListDto [] = [];
    materialGradeList : MaterialGradeDto[] =[];
    unitList : UnitDto[] = [];
    
    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService: SupplierServiceProxy,
        private _itemMasterService: ItemServiceProxy,
        private _materialGradeServie: MaterialGradeServiceProxy,
        private _unitService : UnitServiceProxy,
        private _itemMockService: ItemMockService,
        private _itemFormBuilderService: ItemFormBuilderService
    ) {
        super(injector);
    }

    async show(itemMasterId?: string) {
        await this.loadDropdownList();
        let itemMaster = new ItemMasterDto();
        if (!itemMasterId) {
            this.itemMasterForm =  this._itemFormBuilderService.initialisePrimaryItemMasterForm(itemMaster);
            this.active = true;
            this.modal.show();
        }
        else {
            this._itemMasterService.getItemMasterById(itemMasterId).subscribe((response:ItemMasterDto) => {
                let itemMaster = response;
                this.itemMasterForm = this._itemFormBuilderService.loadCategoryWiseItemForm(itemMaster);
                this.active = true;
                this.modal.show();
            });
        }
    }

    onItemCategorySelect(categoryId : number){
        let itemMaster = new ItemMasterDto();
        itemMaster.itemCategory = categoryId;
        this.itemMasterForm = this._itemFormBuilderService.loadCategoryWiseItemForm(itemMaster);
    }

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
            itemSparesId: new FormControl(itemSpare.itemSparesId ? itemSpare.itemSparesId : null, []),
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
            path: new FormControl(itemAttachment.path? itemAttachment.path : null, []),
            description: new FormControl(itemAttachment.description ? itemAttachment.description : null, []),
            itemId: new FormControl(itemAttachment.itemId, [])
        });
    }

    deleteItemAttachment(indexValue: number) {
        const itemAttachmentArray = this.itemAttachments;
        itemAttachmentArray.removeAt(indexValue);
    }

    get itemAccessories(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemAccessories'));
    }

    addItemAccessory() {
        let itemAccessory: ItemAccessoryDto = new ItemAccessoryDto();
        let itemAccessoryForm = this.createItemAccessory(itemAccessory);
        this.itemAccessories.push(itemAccessoryForm);
    }

    createItemAccessory(itemAccessory: ItemAccessoryDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(itemAccessory.id, []),
            accessoryId: new FormControl(itemAccessory.accessoryId ? itemAccessory.accessoryId : null, []),
            itemId: new FormControl(itemAccessory.itemId, [])
        });
    }

    deleteItemAccessory(indexValue: number) {
        const itemAccessoryArray = this.itemAccessories;
        itemAccessoryArray.removeAt(indexValue);
    }

    get itemStorageConditions(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemStorageConditions'));
    }

    addItemStorageCondition() {
        let itemStorageCondition: ItemStorageConditionDto = new ItemStorageConditionDto();
        let itemStorageConditionForm = this.createItemStorageCondition(itemStorageCondition);
        this.itemStorageConditions.push(itemStorageConditionForm);
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

    deleteItemStorageCondition(indexValue: number) {
        const itemStorageConditionArray = this.itemStorageConditions;
        itemStorageConditionArray.removeAt(indexValue);
    }

    get itemProcurements(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemProcurements'));
    }

    addItemProcurement() {
        let itemProcurement: ProcurementDto = new ProcurementDto();
        let itemProcurementForm = this.createItemProcurement(itemProcurement);
        this.itemProcurements.push(itemProcurementForm);
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

    deleteItemProcurement(indexValue: number) {
        const itemProcurementArray = this.itemProcurements;
        itemProcurementArray.removeAt(indexValue);
    }

    get itemRateRevisions(): FormArray {
        return (<FormArray>this.itemMasterForm.get('itemRateRevisions'));
    }

    addItemRateRevision() {
        let itemRateRevision: ItemRateRevisionDto = new ItemRateRevisionDto();
        let itemRateRevisionForm = this.createItemRateRevision(itemRateRevision);
        this.itemRateRevisions.push(itemRateRevisionForm);
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

    deleteItemRateRevision(indexValue: number) {
        const itemRateRevisionArray = this.itemRateRevisions;
        itemRateRevisionArray.removeAt(indexValue);
    }

    async loadDropdownList() {
        await this.loadSuppliers();
        await this.loadItemMasters();
        await this.loadMaterialGrades();
        await this.loadUnitList();
        this.loadItemCategories();
        this.loadItemTypes();
        this.loadAMCRequirementList();
        this.loadItemMobilityList();
        this.loadCalibrationRequirementList();
        this.loadCalibrationTypeList();
        this.loadCalibrationFrequencyList();
        this.loadItemStatusList();
        this.loadHazardousList();
        this.loadSubjectCategoryList();
        this.loadYearList();
    }

    async loadSuppliers() {
        this.supplierList = await this._supplierService.getSupplierList().toPromise();
    }

    async loadItemMasters() {
        this.itemMasterList = await this._itemMasterService.getItemMasterList().toPromise();
    }

    async loadMaterialGrades() {
        this.materialGradeList = await this._materialGradeServie.getMaterialGradeList().toPromise();
    }

    async loadUnitList() {
        this.unitList = await this._unitService.getUnitList().toPromise();
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

    loadHazardousList() {
        this.hazardousList = this._itemMockService.loadYesOrNoTypeDropdownData();
    }

    loadSubjectCategoryList() {
        this.subjectCategoryList = this._itemMockService.loadSubjectCategoryList();
    }

    loadYearList() {
        for (let i = 1950; i <= 2050; i++) {
            let yearItem: DropdownDto = new DropdownDto();
            yearItem.title = <string><unknown>i;
            yearItem.value = i;
            this.yearList.push(yearItem);
        }
    }

    onShown(): void {
        // document.getElementById('name').focus();
    }

    save(): void {
        this.submitted = true;
        if (this.itemMasterForm.valid) {
            let input = new ItemMasterInputDto();
            this.saving = true;
            input = this.itemMasterForm.getRawValue();
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

            if (input.itemAccessories && input.itemAccessories.length > 0) {
                let tempItemAccessories = this.mapItemAccessories(input.itemAccessories);
                input.itemAccessories = tempItemAccessories;
            }

            if (input.itemAttachments && input.itemAttachments.length > 0) {
                let tempItemAttachments = this.mapItemAttachments(input.itemAttachments);
                input.itemAttachments = tempItemAttachments;
            }

            if (input.itemStorageConditions && input.itemStorageConditions.length > 0) {
                let itemStorageConditions = this.mapItemStorageConditions(input.itemStorageConditions);
                input.itemStorageConditions = itemStorageConditions;
            }

            if (input.itemProcurements && input.itemProcurements.length > 0) {
                let itemProcurements = this.mapItemProcurements(input.itemProcurements);
                input.itemProcurements = itemProcurements;
            }

            if (input.itemRateRevisions && input.itemRateRevisions.length > 0) {
                let itemRateRevisions = this.mapItemRateRevisions(input.itemRateRevisions);
                input.itemRateRevisions = itemRateRevisions;
            }

            this._itemMasterService
                .insertOrUpdateItem(input)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                
                .subscribe((response: ResponseDto) => {
                    if(!response.dataMatchFound){
                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                    else{
                        this.close();
                        this.saveItemMaster.emit(response);
                    }
                    
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

    mapItemAccessories(itemAccessoryList: ItemAccessoryInputDto[]): ItemAccessoryInputDto[] | null {
        let tempItemAccessoryList: ItemAccessoryInputDto[] = [];
        itemAccessoryList.forEach(item => {
            if (item.accessoryId != null) {
                let tempItemAccessory: ItemAccessoryInputDto = new ItemAccessoryInputDto(
                    {
                        id: item.id ? item.id : "",
                        accessoryId: item.accessoryId,
                        itemId: item.itemId,
                    }
                );
                tempItemAccessoryList.push(tempItemAccessory);
            }

        });
        let result = tempItemAccessoryList.length > 1 ? tempItemAccessoryList : null;
        return result;
    }

    mapItemSpares(itemSpareList: ItemSpareInputDto[]): ItemSpareInputDto[] | null {
        let tempItemSpareList: ItemSpareInputDto[] = [];
        itemSpareList.forEach(item => {
            if (item.itemId != null) {
                let tempItemSpare: ItemSpareInputDto = new ItemSpareInputDto(
                    {
                        id: item.id ? item.id : "",
                        itemSparesId : item.itemSparesId,
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

    mapItemStorageConditions(itemStorageConditionList: ItemStorageConditionInputDto[]): ItemStorageConditionInputDto[] | null {
        let tempItemStorageConditionList: ItemStorageConditionInputDto[] = [];
        itemStorageConditionList.forEach(item => {
            if (item.location != null ||  item.thresholdQuantity != null || item.hazardous != null) {
                let tempItemStorageCondition: ItemStorageConditionInputDto = new ItemStorageConditionInputDto(
                    {
                        id: item.id ? item.id : "",
                        location : item.location,
                        hazardous : item.hazardous,
                        thresholdQuantity : item.thresholdQuantity,
                        itemId: item.itemId
                    }
                );
                tempItemStorageConditionList.push(tempItemStorageCondition);
            }

        });
        let result = tempItemStorageConditionList.length > 1 ? tempItemStorageConditionList : null;
        return result;
    }

    mapItemProcurements(itemProcurementList: ProcurementInputDto[]): ProcurementInputDto[] | null {
        let tempItemProcurementList: ProcurementInputDto[] = [];
        itemProcurementList.forEach(itemProcurement => {
            if (itemProcurement.ratePerPack != null && itemProcurement.quantityPerOrderingUOM != null) {
                let tempItemProcurement: ProcurementInputDto = new ProcurementInputDto(
                    {
                        id: itemProcurement.id ? itemProcurement.id : "",
                        catalogueNumber : itemProcurement.catalogueNumber,
                        make : itemProcurement.make,
                        quantityPerOrderingUOM: itemProcurement.quantityPerOrderingUOM,
                        ratePerPack : itemProcurement.ratePerPack,
                        ratePerStockUOM : itemProcurement.ratePerStockUOM,
                        itemId: itemProcurement.itemId
                    }
                );
                tempItemProcurementList.push(tempItemProcurement);
            }

        });
        let result = tempItemProcurementList.length > 1 ? tempItemProcurementList : null;
        return result;
    }

    mapItemRateRevisions(itemRateRevisionList: ItemRateRevisionInputDto[]): ItemRateRevisionInputDto[] | null {
        let tempItemRateRevisionList: ItemRateRevisionInputDto[] = [];
        itemRateRevisionList.forEach(itemRateRevision => {
            if (itemRateRevision.ratePerOrderingQuantity != null ||  itemRateRevision.orderingQuantity != null) {
                let tempItemRateRevision: ItemRateRevisionInputDto = new ItemRateRevisionInputDto(
                    {
                        id: itemRateRevision.id ? itemRateRevision.id : "",
                        make: itemRateRevision.make,
                        orderingQuantity: itemRateRevision.orderingQuantity,
                        catalogueNumber : itemRateRevision.catalogueNumber,
                        dateOfEntry: itemRateRevision.dateOfEntry,
                        ratePerOrderingQuantity : itemRateRevision.ratePerOrderingQuantity,
                        stockQuantityPerOrderingUOM :itemRateRevision.stockQuantityPerOrderingUOM,
                        ratePerStockUOM : itemRateRevision.ratePerStockUOM,
                        orderingUOMId: itemRateRevision.orderingUOMId,
                        stockUOMId : itemRateRevision.stockUOMId,
                        itemId: itemRateRevision.itemId
                    }
                );
                tempItemRateRevisionList.push(tempItemRateRevision);
            }
        });
        let result = tempItemRateRevisionList.length > 1 ? tempItemRateRevisionList : null;
        return result;
    }

    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }

    calculateRatePerStockUOM(itemRateRevisionForm : FormGroup) : void{
        let tempItemRateRevision = itemRateRevisionForm.value;
        let tempRatePerStockUOM = tempItemRateRevision.ratePerOrderingQuantity != null && tempItemRateRevision.orderingQuantity != null ? parseFloat((tempItemRateRevision.ratePerOrderingQuantity / tempItemRateRevision.orderingQuantity).toString()).toFixed(2) : 0;
        itemRateRevisionForm.patchValue(
            {
                ratePerStockUOM : tempRatePerStockUOM
            }
        );
    }
}
