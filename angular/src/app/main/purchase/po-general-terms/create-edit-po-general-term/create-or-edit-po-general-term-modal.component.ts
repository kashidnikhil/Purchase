import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    POGeneralTermDto,
    POGeneralTermInputDto,
    POGeneralTermServiceProxy,
    ResponseDto,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-po-general-term-modal',
    templateUrl: './create-or-edit-po-general-term-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-po-general-term-modal.component.less']
})
export class CreateOrEditPOGeneralTermModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restorePOGeneralTerm: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    poGeneralTermItem : POGeneralTermDto = new POGeneralTermDto();
    
    constructor(
        injector: Injector,
        private _poGeneralTermService : POGeneralTermServiceProxy
    ) {
        super(injector);
    }

    show(poGeneralTermId?: string): void {
        if (!poGeneralTermId) {
            this.poGeneralTermItem = new POGeneralTermDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._poGeneralTermService.getPOGeneralTermById(poGeneralTermId).subscribe((response : POGeneralTermDto)=> {
                this.poGeneralTermItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new POGeneralTermInputDto();
        input = this.poGeneralTermItem;
        this.saving = true;
        this._poGeneralTermService
            .insertOrUpdatePOGeneralTerm(input)
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
                    this.restorePOGeneralTerm.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
