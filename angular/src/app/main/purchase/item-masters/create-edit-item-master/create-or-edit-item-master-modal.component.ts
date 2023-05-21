import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ItemMasterDto,
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

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService: SupplierServiceProxy,
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
        this.itemMasterForm = this.formBuilder.group({
            id: new FormControl(itemMaster.id, []),
            categoryId: new FormControl(itemMaster.categoryId, []),
            itemId: new FormControl(itemMaster.itemId, []),
            itemCategory : new FormControl(<number>itemMaster.itemCategory, []),
            genericName : new FormControl(itemMaster.genericName, []),
            itemName : new FormControl(itemMaster.itemName, []),
            alias : new FormControl(itemMaster.alias, []),
            itemType : new FormControl(itemMaster.itemType, []),
            amcRequired : new FormControl(itemMaster.amcRequired, []),
            make: new FormControl(itemMaster.make, []),
            model : new FormControl(itemMaster.model, []),
            serialNumber : new FormControl(itemMaster.serialNumber, []),
            specifications : new FormControl(itemMaster.specifications, []),
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

    get supplierAddresses(): FormArray {
        return (<FormArray>this.itemMasterForm.get('supplierAddresses'));
    }

    get supplierBanks(): FormArray {
        return (<FormArray>this.itemMasterForm.get('supplierBanks'));
    }

    get supplierContactPersons(): FormArray {
        return (<FormArray>this.itemMasterForm.get('supplierContactPersons'));
    }

    addSupplierAddress() {
        let suppplierAddressItem: SupplierAddressDto = new SupplierAddressDto();
        let addressForm = this.createSupplierAddress(suppplierAddressItem);
        this.supplierAddresses.push(addressForm);
    }

    addSupplierContactPerson() {
        let supplierContactPersonItem: SupplierContactPersonDto = new SupplierContactPersonDto();
        let contactPersonForm = this.createSupplierContactPerson(supplierContactPersonItem);
        this.supplierContactPersons.push(contactPersonForm);
    }

    addSupplierBank() {
        let suppplierBankItem: SupplierBankDto = new SupplierBankDto();
        let bankForm = this.createSupplierBank(suppplierBankItem);
        this.supplierBanks.push(bankForm);
    }

    createSupplierAddress(supplierAddressItem: SupplierAddressDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierAddressItem.id, []),
            address: new FormControl(supplierAddressItem.address, [Validators.required]),
            addressType: new FormControl(supplierAddressItem.addressType, [])
        });
    }

    deleteSupplierContactPersonItem(indexValue: number) {
        const supplierContactPersonArray = this.supplierContactPersons;
        supplierContactPersonArray.removeAt(indexValue);
    }

    deleteSupplierBankItem(indexValue: number) {
        const supplierBankArray = this.supplierBanks;
        supplierBankArray.removeAt(indexValue);
    }

    deleteSupplierAddressItem(indexValue: number) {
        const supplierAddressArray = this.supplierAddresses;
        supplierAddressArray.removeAt(indexValue);
    }

    createSupplierBank(supplierBank: SupplierBankDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierBank.id, []),
            bankName: new FormControl(supplierBank.bankName, [Validators.required]),
            branchName: new FormControl(supplierBank.branchName, []),
            address: new FormControl(supplierBank.address, []),
            accountNumber: new FormControl(supplierBank.accountNumber, [Validators.required]),
            micrCode: new FormControl(supplierBank.micrCode, []),
            ifscCode: new FormControl(supplierBank.ifscCode, []),
            rtgs: new FormControl(supplierBank.rtgs, []),
            paymentMode: new FormControl(supplierBank.paymentMode, []),
        });
    }

    createSupplierContactPerson(supplierContactPerson: SupplierContactPersonDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierContactPerson.id, []),
            contactPersonName: new FormControl(supplierContactPerson.contactPersonName, [Validators.required]),
            designation: new FormControl(supplierContactPerson.designation, []),
            emailId: new FormControl(supplierContactPerson.emailId, []),
            mobileNumber: new FormControl(supplierContactPerson.mobileNumber, [])
        });
    }

    async loadDropdownList() {
        this.loadItemCategories();
        this.loadItemTypes();
    }

    loadItemCategories(){
        this.itemCategoriesList = this._itemMockService.loadItemCategories();
    }

    loadItemTypes(){
        this.itemTypeList = this._itemMockService.loadItemTypes();
    }

    onShown(): void {
        // document.getElementById('name').focus();
    }

    save(): void {
        this.submitted = true;
        if (this.itemMasterForm.valid) {
            let input = new SupplierInputDto();
            this.saving = true;
            input = this.itemMasterForm.value;
            if (input.supplierCategories && input.supplierCategories.length > 0) {
                let tempSupplierCategories = this.mapSupplierCategories(input.supplierCategories);
                input.supplierCategories = tempSupplierCategories;
            }
            this._supplierService
                .insertOrUpdateSupplier(input)
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

    mapSupplierCategories(supplierCategoryList: MappedSupplierCategoryInputDto[]): MappedSupplierCategoryInputDto[] {
        let tempSupplierList: MappedSupplierCategoryInputDto[] = [];
        supplierCategoryList.forEach(item => {
            let tempSupplierCategoryItem: MappedSupplierCategoryInputDto = new MappedSupplierCategoryInputDto(
                {
                    id: "",
                    supplierCategoryId: item.id,
                    supplierId: item.supplierId,
                }
            );
            // tempSupplierCategoryItem.id = "";
            // tempSupplierCategoryItem.supplierCategoryId = item.id;
            tempSupplierList.push(tempSupplierCategoryItem);
        });
        return tempSupplierList;
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
