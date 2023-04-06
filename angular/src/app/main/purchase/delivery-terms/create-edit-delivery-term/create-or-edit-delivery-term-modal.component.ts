import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    DeliveryTermDto,
    DeliveryTermInputDto,
    DeliveryTermServiceProxy,
    ResponseDto,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-delivery-term-modal',
    templateUrl: './create-or-edit-delivery-term-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-delivery-term-modal.component.less']
})
export class CreateOrEditDeliveryTermModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreDeliveryTerm: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    deliveryTermItem : DeliveryTermDto = new DeliveryTermDto();
    
    constructor(
        injector: Injector,
        private _deliveryTermService : DeliveryTermServiceProxy
    ) {
        super(injector);
    }

    show(deliveryTermId?: string): void {
        if (!deliveryTermId) {
            this.deliveryTermItem = new DeliveryTermDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._deliveryTermService.getDeliveryTermById(deliveryTermId).subscribe((response : DeliveryTermDto)=> {
                this.deliveryTermItem = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new DeliveryTermInputDto();
        input = this.deliveryTermItem;
        this.saving = true;
        this._deliveryTermService
            .insertOrUpdateDeliveryTerm(input)
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
                    this.restoreDeliveryTerm.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
