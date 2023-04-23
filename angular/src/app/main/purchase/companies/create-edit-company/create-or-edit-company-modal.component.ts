import { Component, Injector, ViewChild, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { AppComponentBase } from "@shared/common/app-component-base";
import { CompanyDto, CompanyServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from "ngx-bootstrap/modal";

@Component({
    selector: 'create-edit-company-modal',
    templateUrl: './create-or-edit-company-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-company-modal.component.less']
})
export class CreateOrEditCompanyModalComponent extends AppComponentBase {
    @ViewChild('createOrEditCompanyModal', { static: true }) modal: ModalDirective;
    
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
                // this.initialiseSupplierForm(companyItem);
                this.active = true;
                this.modal.show();
            });
        }
    }

    initialiseCompanyForm(companyItem : CompanyDto){

    }

    onShown(): void {
        // document.getElementById('name').focus();
    }

    save(): void {

    }

    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }
}