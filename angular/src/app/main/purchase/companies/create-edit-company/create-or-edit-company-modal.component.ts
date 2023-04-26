import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from "@angular/core";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AppComponentBase } from "@shared/common/app-component-base";
import { CompanyAddressDto, CompanyContactPersonDto, CompanyDto, CompanyInputDto, CompanyServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from "ngx-bootstrap/modal";
import { finalize } from "rxjs/operators";

@Component({
    selector: 'create-edit-company-modal',
    templateUrl: './create-or-edit-company-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-company-modal.component.less']
})
export class CreateOrEditCompanyModalComponent extends AppComponentBase {
    @ViewChild('createOrEditCompanyModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    
    companyForm!: FormGroup;
    active: boolean = false;
    submitted: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _companyService: CompanyServiceProxy
    ) {
        super(injector);
    }

    async show(companyId?: string) {
        // await this.loadDropdownList();
        if (!companyId) {
            let companyItem = new CompanyDto();
            this.initialiseCompanyForm(companyItem);
            this.active = true;
            this.modal.show();
        }
        else {
            this._companyService.getCompanyMasterById(companyId).subscribe((response: CompanyDto) => {
                let companyItem = response;
                this.initialiseCompanyForm(companyItem);
                this.active = true;
                this.modal.show();
            });
        }
    }

    initialiseCompanyForm(companyItem : CompanyDto){
        let companyAddressItem : CompanyAddressDto = new CompanyAddressDto();
        let companyContactPersonItem : CompanyContactPersonDto = new CompanyContactPersonDto();
        this.companyForm = this.formBuilder.group({
            id: new FormControl(companyItem.id, []),
            name: new FormControl(companyItem.name, [Validators.required]),
            gstNumber : new FormControl(companyItem.gstNumber, []),
            companyAddresses: companyItem.companyAddresses && companyItem.companyAddresses.length > 0 ? this.formBuilder.array(
                companyItem.companyAddresses.map((x: CompanyAddressDto) =>
                    this.createCompanyAddress(x)
                )
            ) : this.formBuilder.array([this.createCompanyAddress(companyAddressItem)]),
            companyContactPersons: companyItem.companyContactPersons && companyItem.companyContactPersons.length > 0 ? this.formBuilder.array(
                companyItem.companyContactPersons.map((x: CompanyContactPersonDto) =>
                    this.createCompanyContactPerson(x)
                )
            ) : this.formBuilder.array([this.createCompanyContactPerson(companyContactPersonItem)])
        });
    }

    createCompanyAddress(companyAddress: CompanyAddressDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(companyAddress.id, []),
            address: new FormControl(companyAddress.address, [])
        });
    }

    createCompanyContactPerson(companyContactPerson: CompanyContactPersonDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(companyContactPerson.id, []),
            contactPersonName: new FormControl(companyContactPerson.contactPersonName, []),
            designation: new FormControl(companyContactPerson.designation, []),
            emailId: new FormControl(companyContactPerson.emailId, []),
            mobileNumber: new FormControl(companyContactPerson.mobileNumber, [])
        });
    }

    get companyAddresses(): FormArray {
        return (<FormArray>this.companyForm.get('companyAddresses'));
    }

    get companyContactPersons(): FormArray {
        return (<FormArray>this.companyForm.get('companyContactPersons'));
    }

    onShown(): void {
        // document.getElementById('name').focus();
    }

    addCompanyAddress() {
        let companyAddressItem: CompanyAddressDto = new CompanyAddressDto();
        let addressForm = this.createCompanyAddress(companyAddressItem);
        this.companyAddresses.push(addressForm);
    }

    addCompanyContactPerson() {
        let companyContactPersonItem: CompanyContactPersonDto = new CompanyContactPersonDto();
        let contactPersonForm = this.createCompanyContactPerson(companyContactPersonItem);
        this.companyContactPersons.push(contactPersonForm);
    }

    save(): void {
        this.submitted = true;
        if(this.companyForm.valid){
            let input = new CompanyInputDto();
            input = this.companyForm.value;
            this.saving = true;

            this._companyService.insertOrUpdateCompany(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
        }
    }

    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }
}