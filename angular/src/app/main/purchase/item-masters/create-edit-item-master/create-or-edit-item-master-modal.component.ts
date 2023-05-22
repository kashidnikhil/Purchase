import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    CalibrationAgencyDto,
    CalibrationAgencyInputDto,
    CalibrationTypeDto,
    ItemMasterDto,
    ItemMasterInputDto,
    ItemServiceProxy,
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
    amcRequirementList : DropdownDto[] = [];
    itemMobilityList : DropdownDto[] = [];
    calibrationRequirementList : DropdownDto[] = [];
    calibrationTypeList : DropdownDto[] = [];
    calibrationFrequencyList : DropdownDto[] = [];
    supplierList : SupplierDto[] = [];

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService: SupplierServiceProxy,
        private _itemMasterService: ItemServiceProxy,
        private _itemMockService : ItemMockService
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

    initialiseItemMasterForm(itemMaster : ItemMasterDto){
        let itemCalibrationType: CalibrationTypeDto = new CalibrationTypeDto();
        let itemCalibrationAgency: CalibrationAgencyDto = new CalibrationAgencyDto();
        this.itemMasterForm = this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory : new FormControl(itemMaster.itemCategory? <number>itemMaster.itemCategory: null, [Validators.required]),
            genericName : new FormControl(itemMaster.genericName, []),
            itemName : new FormControl(itemMaster.itemName, [Validators.required]),
            alias : new FormControl(itemMaster.alias, []),
            itemType : new FormControl(itemMaster.itemType ? <number>itemMaster.itemType : null, []),
            amcRequired : new FormControl(itemMaster.amcRequired? <number>itemMaster.amcRequired : null, []),
            make: new FormControl(itemMaster.make, []),
            model : new FormControl(itemMaster.model, []),
            serialNumber : new FormControl(itemMaster.serialNumber, []),
            specifications : new FormControl(itemMaster.specifications, []),
            storageConditions : new FormControl(itemMaster.storageConditions, []),
            itemMobility : new FormControl(itemMaster.itemMobility? <number>itemMaster.itemMobility : null, []),
            calibrationRequirement : new FormControl(itemMaster.calibrationRequirement? <number>itemMaster.calibrationRequirement : null, []),
            supplierId : new FormControl(itemMaster.supplierId, []),
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


    async loadDropdownList() {
        await this.loadSuppliers();
        this.loadItemCategories();
        this.loadItemTypes();
        this.loadAMCRequirementList();
        this.loadItemMobilityList();
        this.loadCalibrationRequirementList();
        this.loadCalibrationTypeList();
        this.loadCalibrationFrequencyList();
    }

    async loadSuppliers(){
        this.supplierList = await this._supplierService.getSupplierList().toPromise();
    }

    loadItemCategories(){
        this.itemCategoriesList = this._itemMockService.loadItemCategories();
    }

    loadItemTypes(){
        this.itemTypeList = this._itemMockService.loadItemTypes();
    }

    loadAMCRequirementList(){
        this.amcRequirementList = this._itemMockService.loadYesOrNoTypeDropdownData();
    }

    loadItemMobilityList(){
        this.itemMobilityList = this._itemMockService.loadItemMobilityList();
    }

    loadCalibrationRequirementList(){
        this.calibrationRequirementList = this._itemMockService.loadYesOrNoTypeDropdownData();
    }

    loadCalibrationTypeList(){
        this.calibrationTypeList = this._itemMockService.loadCalibrationTypeList();
    }

    loadCalibrationFrequencyList(){
        this.calibrationFrequencyList = this._itemMockService.loadCalibrationFrequencyList();
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
                let temptemCalibrationAgencies = this.mapCalibrationAgencies(input.itemCalibrationAgencies);
                input.itemCalibrationAgencies = temptemCalibrationAgencies;
            }

            if (input.itemCalibrationTypes && input.itemCalibrationTypes.length > 0) {
                // let tempSupplierCategories = this.mapSupplierCategories(input.supplierCategories);
                // input.supplierCategories = tempSupplierCategories;
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

    mapCalibrationAgencies(calibrationAgencyList: CalibrationAgencyInputDto[]): CalibrationAgencyInputDto[]|null {
        
        let tempCalibrationAgencyList: CalibrationAgencyInputDto[] = [];
        calibrationAgencyList.forEach(item => {
            if(item.supplierId != null){
                let tempCalibrationAgencyItem: CalibrationAgencyInputDto = new CalibrationAgencyInputDto(
                    {
                        id: "",
                        itemId: item.itemId,
                        supplierId: item.supplierId,
                    }
                );
                tempCalibrationAgencyList.push(tempCalibrationAgencyItem);
            }
            
        });
        let result = tempCalibrationAgencyList.length>1 ? tempCalibrationAgencyList : null;
        return result;
    }   

    unMapSupplierCategories(supplierCategoryList: MappedSupplierCategoryDto[]): SupplierCategoryDto[] {
        let tempSupplierList: SupplierCategoryDto[] = [];
        if (supplierCategoryList && supplierCategoryList.length > 0) {
            supplierCategoryList.forEach(item => {
                let tempSupplierCategoryItem: SupplierCategoryDto = new SupplierCategoryDto(
                    {
                        id: item.supplierCategoryId,
                        name: item.name,
                        description : item.description
                    }
                );
                tempSupplierList.push(tempSupplierCategoryItem);
            });
        }
        return tempSupplierList;
    }

    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }
}
