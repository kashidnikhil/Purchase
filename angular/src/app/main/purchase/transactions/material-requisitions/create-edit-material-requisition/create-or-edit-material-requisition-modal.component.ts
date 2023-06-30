import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ResponseDto,
    TermsOfPaymentDto,
    TermsOfPaymentInputDto,
    TermsOfPaymentServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

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

    active = false;
    saving = false;
    termsOfPaymentItem : TermsOfPaymentDto = new TermsOfPaymentDto();
    
    constructor(
        injector: Injector,
        private _termsOfPaymentService : TermsOfPaymentServiceProxy
    ) {
        super(injector);
    }

    show(termsOfPaymentId?: string): void {
        if (!termsOfPaymentId) {
            this.termsOfPaymentItem = new TermsOfPaymentDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._termsOfPaymentService.getTermsOfPaymentById(termsOfPaymentId).subscribe((response : TermsOfPaymentDto)=> {
                this.termsOfPaymentItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new TermsOfPaymentInputDto();
        input = this.termsOfPaymentItem;
        this.saving = true;
        this._termsOfPaymentService
            .insertOrUpdateTermsOfPayment(input)
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

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
