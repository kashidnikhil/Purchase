import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    MaterialRequisitionDto,
    MaterialRequisitionInputDto,
    MaterialRequisitionServiceProxy,
    ResponseDto,
    TermsOfPaymentServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'create-edit-material-requisition-modal',
    templateUrl: './create-or-edit-material-requisition-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-material-requisition-modal.component.less']
})
export class CreateOrEditMaterialRequisitionModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    // @Output() restoreTermsOfPayment: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    materialRequisitionForm!: FormGroup;
    active = false;
    saving = false;
    materialRequisitionItem : MaterialRequisitionDto = new MaterialRequisitionDto();
    
    
    constructor(
        injector: Injector,
        private _materialRequisitionService : MaterialRequisitionServiceProxy,
        private _termsOfPaymentService : TermsOfPaymentServiceProxy
    ) {
        super(injector);
    }

    async show(materialRequisitionId?: string){
        if (!materialRequisitionId) {
            //Needs to change this line of code. 
            //this.materialRequisitionForm = new TermsOfPaymentDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._materialRequisitionService.getMaterialRequisitionById(materialRequisitionId).subscribe((response : MaterialRequisitionDto)=> {
                this.materialRequisitionItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        if (this.materialRequisitionForm.valid) {
            let input = new MaterialRequisitionInputDto();
            input = this.materialRequisitionForm.getRawValue();
            this.saving = true;
            this._materialRequisitionService.insertOrUpdateMaterialRequisition(input)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe((response : ResponseDto) => {
                    if(!response.dataMatchFound){
                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                    else{
                        this.close();
                        // this.restoreTermsOfPayment.emit(response);
                    }
                });
        }
        
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
