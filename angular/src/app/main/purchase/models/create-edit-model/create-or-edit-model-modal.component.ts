import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ModelDto,
    ModelInputDto,
    ModelServiceProxy,
    ResponseDto,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-model-modal',
    templateUrl: './create-or-edit-model-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-model-modal.component.less']
})
export class CreateOrEditModelModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreModel: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    modelItem : ModelDto = new ModelDto();
    
    constructor(
        injector: Injector,
        private _modelService : ModelServiceProxy
    ) {
        super(injector);
    }

    show(modelId?: string): void {
        if (!modelId) {
            this.modelItem = new ModelDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else {
            this._modelService.getModelById(modelId).subscribe((response: ModelDto) => {
                this.modelItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new ModelInputDto();
        input = this.modelItem;
        this.saving = true;
        this._modelService
            .insertOrUpdateModel(input)
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
                    this.restoreModel.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
