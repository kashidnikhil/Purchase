import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    LegalEntityDto,
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

@Component({
    selector: 'create-edit-supplier-modal',
    templateUrl: './create-or-edit-supplier-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-supplier-modal.component.less']
})
export class CreateOrEditSupplierModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild(TabsetComponent) tabSet: TabsetComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    // @Output() restoreSupplierCategory: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    supplierForm!: FormGroup;
    active: boolean = false;
    submitted: boolean = false;
    saving: boolean = false;
    currentTab: number = 0;

    legalEntityList: LegalEntityDto[] = [];
    supplierCategoryList: SupplierCategoryDto[] = [];
    yearList: DropdownDto[] = [];
    paymentModeList: DropdownDto[] = [];
    bankPaymentModeList: DropdownDto[] = [];
    deliveryList: DropdownDto[] = [];
    msmeStatusList: DropdownDto[] = [];
    statusList: DropdownDto[] = [];
    

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService: SupplierServiceProxy,
        private _supplierCategoryService: SupplierCategoryServiceProxy,
        private _legalEntityService: LegalEntityServiceProxy
    ) {
        super(injector);
    }

    async show(supplierId?: string) {
        await this.loadDropdownList();
        if (!supplierId) {
            let supplierItem = new SupplierDto();
            this.initialiseSupplierForm(supplierItem);
            this.active = true;
            this.modal.show();
        }
        else {
            this._supplierService.getSupplierMasterById(supplierId).subscribe((response: SupplierDto) => {
                let supplierItem = response;
                this.initialiseSupplierForm(supplierItem);
                this.active = true;
                this.modal.show();
            });
        }
    }

    initialiseSupplierForm(supplierItem: SupplierDto) {
        let supplierAddressItem: SupplierAddressDto = new SupplierAddressDto();
        let supplierContactPersonItem: SupplierContactPersonDto = new SupplierContactPersonDto();
        let supplierBankItem: SupplierBankDto = new SupplierBankDto();
        this.supplierForm = this.formBuilder.group({
            id: new FormControl(supplierItem.id, []),
            name: new FormControl(supplierItem.name, [Validators.required]),
            phoneNumber: new FormControl(supplierItem.telephoneNumber, []),
            mobile: new FormControl(supplierItem.mobile, []),
            email: new FormControl(supplierItem.email, []),
            website: new FormControl(supplierItem.website, []),
            certifications: new FormControl(supplierItem.certifications, []),
            legalEntityId: new FormControl(supplierItem.legalEntityId, []),
            gstNumber: new FormControl(supplierItem.gstNumber, []),
            yearOfEstablishment: new FormControl(supplierItem.yearOfEstablishment, []),
            deliveryBy: new FormControl(supplierItem.deliveryBy, []),
            category: new FormControl(supplierItem.category, []),
            status: new FormControl(supplierItem.status, []),
            servicesProvided: new FormControl(supplierItem.servicesProvided, []),
            commentsOrObservations: new FormControl(supplierItem.commentsOrObservations, []),
            commentsOrApprovalByCeo: new FormControl(supplierItem.commentsOrApprovalByCeo, []),
            poComments: new FormControl(supplierItem.poComments, []),
            pan: new FormControl(supplierItem.pan, []),
            licenseDetails: new FormControl(supplierItem.licenseDetails, []),
            msmeStatus: new FormControl(supplierItem.msmeStatus, []),
            msmeNumber: new FormControl(supplierItem.msmeNumber, []),
            statusChangeReason: new FormControl(supplierItem.statusChangeReason, []),
            referredBy: new FormControl(supplierItem.referredBy, []),
            positiveObservation: new FormControl(supplierItem.positiveObservation, []),
            negativeObservation: new FormControl(supplierItem.negativeObservation, []),
            paymentMode: new FormControl(supplierItem.paymentMode, []),
            supplierCategories : [this.unMapSupplierCategories(supplierItem.supplierCategories), []],
            // supplierCategories: new FormControl(<SupplierCategoryDto[]>(this.unMapSupplierCategories(supplierItem.supplierCategories)), []),
            supplierAddresses: supplierItem.supplierAddresses && supplierItem.supplierAddresses.length > 0 ? this.formBuilder.array(
                supplierItem.supplierAddresses.map((x: SupplierAddressDto) =>
                    this.createSupplierAddress(x)
                )
            ) : this.formBuilder.array([this.createSupplierAddress(supplierAddressItem)]),
            supplierContactPersons: supplierItem.supplierContactPersons && supplierItem.supplierContactPersons.length > 0 ? this.formBuilder.array(
                supplierItem.supplierContactPersons.map((x: SupplierContactPersonDto) =>
                    this.createSupplierContactPerson(x)
                )
            ) : this.formBuilder.array([this.createSupplierContactPerson(supplierContactPersonItem)]),
            supplierBanks: supplierItem.supplierBanks && supplierItem.supplierBanks.length > 0 ? this.formBuilder.array(
                supplierItem.supplierBanks.map((x: SupplierBankDto) =>
                    this.createSupplierBank(x)
                )
            ) : this.formBuilder.array([this.createSupplierBank(supplierBankItem)])

        });
    }

    get supplierAddresses(): FormArray {
        return (<FormArray>this.supplierForm.get('supplierAddresses'));
    }

    get supplierBanks(): FormArray {
        return (<FormArray>this.supplierForm.get('supplierBanks'));
    }

    get supplierContactPersons(): FormArray {
        return (<FormArray>this.supplierForm.get('supplierContactPersons'));
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
        this.loadYearList();
        this.loadPaymentModeList();
        this.loadBankPaymentModeList();
        this.loadDeliveryList();
        this.loadMSMEStatusList();
        this.loadStatusList();
        await this.loadLegalEntityList();
        await this.loadSupplierCategoryList();
    }

    loadPaymentModeList(): DropdownDto[] {
        this.paymentModeList = [
            {
                title: "Advance",
                value: 1
            },
            {
                title: "Against Performa",
                value: 2
            },
            {
                title: "Against Delivery",
                value: 3
            },
            {
                title: "Credit",
                value: 4
            },
        ];

        return this.paymentModeList;
    }

    loadDeliveryList(): DropdownDto[] {
        this.deliveryList = [
            {
                title: "Supplier",
                value: 1
            },
            {
                title: "Self",
                value: 2
            }
        ];
        return this.deliveryList;
    }

    loadMSMEStatusList(): DropdownDto[] {
        this.msmeStatusList = [
            {
                title: "Yes",
                value: 1
            },
            {
                title: "No",
                value: 2
            }
        ];
        return this.msmeStatusList;
    }

    loadStatusList(): DropdownDto[] {
        this.statusList = [
            {
                title: "Inactive",
                value: 1
            },
            {
                title: "Active",
                value: 2
            }
        ];
        return this.statusList;
    }


    loadBankPaymentModeList(): DropdownDto[] {
        this.bankPaymentModeList = [
            {
                title: "None",
                value: 1
            },
            {
                title: "Card",
                value: 2
            },
            {
                title: "UPI",
                value: 3
            },
            {
                title: "RTGS",
                value: 4
            },
            {
                title: "NEFT",
                value: 5
            },
            {
                title: "CASH",
                value: 6
            }
        ];

        return this.paymentModeList;
    }

    async loadLegalEntityList() {
        let legalEntityList = await this._legalEntityService.getLegalEntityList().toPromise();
        if (legalEntityList.length > 0) {
            this.legalEntityList = [];
            legalEntityList.forEach((legalEntityItem: LegalEntityDto) => {
                this.legalEntityList.push(legalEntityItem);
            });
        }
    }

    async loadSupplierCategoryList() {
        let supplierCategoryList = await this._supplierCategoryService.getSupplierCategoryList().toPromise();
        if (supplierCategoryList.length > 0) {
            this.supplierCategoryList = [];
            supplierCategoryList.forEach((supplierCategoryItem: SupplierCategoryDto) => {
                this.supplierCategoryList.push(supplierCategoryItem);
            });
        }
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
        if (this.supplierForm.valid) {
            let input = new SupplierInputDto();
            this.saving = true;
            input = this.supplierForm.value;
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

    changeTab(tabIndex: number) {
        this.currentTab = tabIndex;
        this.tabSet.tabs[tabIndex].active = true;
    }
}
