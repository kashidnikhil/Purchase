import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    LegalEntityDto,
    LegalEntityInputDto,
    LegalEntityServiceProxy,
    ResponseDto,
    UnitDto,
    UnitInputDto,
    UnitServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-item-category-modal',
    templateUrl: './create-or-edit-item-category-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-item-category-modal.component.less']
})
export class CreateOrEditItemCategoryModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreItemCategory: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    legalEntityItem : LegalEntityDto = new LegalEntityDto();
    
    constructor(
        injector: Injector,
        private _legalEntityService : LegalEntityServiceProxy
    ) {
        super(injector);
    }

    show(legalEntityId?: string): void {
        if (!legalEntityId) {
            this.legalEntityItem = new LegalEntityDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._legalEntityService.getLegalEntityById(legalEntityId).subscribe((response : UnitDto)=> {
                this.legalEntityItem = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new LegalEntityInputDto();
        input = this.legalEntityItem;
        this.saving = true;
        this._legalEntityService
            .insertOrUpdateLegalEntity(input)
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
                    this.restoreItemCategory.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
