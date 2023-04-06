import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AcceptanceCriteriaDto,
    AcceptanceCriteriaInputDto,
    AcceptanceCriteriaServiceProxy,
    ResponseDto,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-acceptance-criteria-modal',
    templateUrl: './create-or-edit-acceptance-criteria-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-acceptance-criteria-modal.component.less']
})
export class CreateOrEditAcceptanceCriteriaModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreAcceptanceCriteria: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    acceptanceCriteriaItem : AcceptanceCriteriaDto = new AcceptanceCriteriaDto();
    
    constructor(
        injector: Injector,
        private _acceptanceCriteriaService : AcceptanceCriteriaServiceProxy
    ) {
        super(injector);
    }

    show(acceptanceCriteriaId?: string): void {
        if (!acceptanceCriteriaId) {
            this.acceptanceCriteriaItem = new AcceptanceCriteriaDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._acceptanceCriteriaService.getAcceptanceCriteriaById(acceptanceCriteriaId).subscribe((response : AcceptanceCriteriaDto)=> {
                this.acceptanceCriteriaItem = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new AcceptanceCriteriaInputDto();
        input = this.acceptanceCriteriaItem;
        this.saving = true;
        this._acceptanceCriteriaService
            .insertOrUpdateAcceptanceCriteria(input)
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
                    this.restoreAcceptanceCriteria.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
