import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    LegalEntityDto,
    LegalEntityServiceProxy,
    SupplierAddressDto,
    SupplierBankDto,
    SupplierContactPersonDto,
    SupplierDto,
    SupplierInputDto,
    SupplierServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { IDropdownDto } from '@app/shared/common/data-models/dropdown';

@Component({
    selector: 'create-edit-supplier-modal',
    templateUrl: './create-or-edit-supplier-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-supplier-modal.component.less']
})
export class CreateOrEditSupplierModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('createOrEditModal') public tabSetElement : any;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    // @Output() restoreSupplierCategory: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    supplierForm!: FormGroup;
    active = false;
    saving = false;
    supplierItem : SupplierDto = new SupplierDto();
    
    legalEntityList : LegalEntityDto[] =[];
    yearList : IDropdownDto[] = [];
    
    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _supplierService : SupplierServiceProxy,
        private _legalEntityService : LegalEntityServiceProxy
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
        else{
            this._supplierService.getSupplierMasterById(supplierId).subscribe((response : SupplierDto)=> {
                this.supplierItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

    initialiseSupplierForm(supplierItem: SupplierDto){
        let supplierAddressItem : SupplierAddressDto = new SupplierAddressDto();
        this.supplierForm = this.formBuilder.group({
            id: new FormControl(supplierItem.id, []),
            name: new FormControl(supplierItem.name, []),
            faxNumber: new FormControl(supplierItem.faxNumber, []),
            email: new FormControl(supplierItem.email, []),
            mobile: new FormControl(supplierItem.mobile, []),
            website: new FormControl(supplierItem.website, []),
            certifications: new FormControl(supplierItem.certifications, []),
            legalEntityId: new FormControl(supplierItem.legalEntityId, []),
            gstNumber: new FormControl(supplierItem.gstNumber, []),
            yearOfEstablishment: new FormControl(supplierItem.yearOfEstablishment, []),
            deliveryBy: new FormControl(supplierItem.deliveryBy, []),
            category: new FormControl(supplierItem.category, []),
            paymentMode: new FormControl(supplierItem.paymentMode, []),
            supplierAddresses : supplierItem.supplierAddresses && supplierItem.supplierAddresses.length > 0 ? 
            supplierItem.supplierAddresses.map((x: SupplierAddressDto) => this.createSupplierAddress(x)) : this.createSupplierAddress(supplierAddressItem),
            supplierBanks : supplierItem.supplierBanks && supplierItem.supplierBanks.length > 0 ? 
            supplierItem.supplierBanks.map((x: SupplierBankDto) => this.createSupplierBank(x)) : this.formBuilder.array([]),
            supplierContactPersons : supplierItem.supplierContactPersons && supplierItem.supplierContactPersons.length > 0 ? 
            supplierItem.supplierContactPersons.map((x: SupplierContactPersonDto) => this.createSupplierContactPerson(x)) : this.formBuilder.array([]),

        });
    }

    createSupplierAddress(supplierAddress: SupplierAddressDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierAddress.id, []),
            address: new FormControl(supplierAddress.address, []),
            addressType: new FormControl(supplierAddress.addressType, [])
        });
    }

    createSupplierBank(supplierBank: SupplierBankDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierBank.id, []),
            bankName: new FormControl(supplierBank.bankName, []),
            branchName: new FormControl(supplierBank.branchName, []),
            address: new FormControl(supplierBank.address, []),
            accountNumber: new FormControl(supplierBank.accountNumber, []),
            micrCode: new FormControl(supplierBank.micrCode, []),
            rtgs: new FormControl(supplierBank.rtgs, []),
            paymentMode: new FormControl(supplierBank.paymentMode, []),
        });
    }

    createSupplierContactPerson(supplierContactPerson: SupplierContactPersonDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(supplierContactPerson.id, []),
            contactPersonName: new FormControl(supplierContactPerson.contactPersonName, []),
            branchName: new FormControl(supplierContactPerson.designation, []),
            address: new FormControl(supplierContactPerson.emailId, []),
            accountNumber: new FormControl(supplierContactPerson.mobileNumber, [])
        });
    }

    async loadDropdownList() {
        await this.loadLegalEntityList();
        this.loadYearList();
    }

    async loadLegalEntityList(){
        let legalEntityList = await this._legalEntityService.getLegalEntityList().toPromise();
        if (legalEntityList.length > 0) {
            this.legalEntityList = [];
            legalEntityList.forEach((legalEntityItem: LegalEntityDto) => {
                this.legalEntityList.push(legalEntityItem);
            });
        }
    }

    loadYearList(){
        for(let i = 1950; i <= 2050; i++){
            let yearItem  : IDropdownDto = new IDropdownDto();
            yearItem.title = <string> <unknown>i;
            yearItem.value = i;
            this.yearList.push(yearItem);
        }
    }

    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new SupplierInputDto();
        input = this.supplierItem;
        this.saving = true;
        this._supplierService
            .insertOrUpdateSupplier(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((response : string) => {
                if(!response){
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

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
