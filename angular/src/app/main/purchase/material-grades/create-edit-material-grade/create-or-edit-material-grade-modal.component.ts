import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {MaterialGradeServiceProxy,
    MaterialGradeDto,
    MaterialGradeInputDto,
    ResponseDto,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-material-grade-modal',
    templateUrl: './create-or-edit-material-grade-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-material-grade-modal.component.less']
})
export class CreateOrEditMaterialGradeModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreMaterialGrade: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    materialGradeItem : MaterialGradeDto = new MaterialGradeDto();
    
    constructor(
        injector: Injector,
        private _materialGradeService : MaterialGradeServiceProxy
    ) {
        super(injector);
    }

    show(materialGradeId?: string): void {
        if (!materialGradeId) {
            this.materialGradeItem = new MaterialGradeDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._materialGradeService.getMaterialGradeById(materialGradeId).subscribe((response : MaterialGradeDto)=> {
                this.materialGradeItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new MaterialGradeInputDto();
        input = this.materialGradeItem;
        this.saving = true;
        this._materialGradeService
            .insertOrUpdateMaterialGrade(input)
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
                    this.restoreMaterialGrade.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
