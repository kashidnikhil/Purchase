import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AssemblyDto,
    AssemblyInputDto,
    AssemblyServiceProxy,
    ModelDto,
    ModelServiceProxy,
    ResponseDto
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-sub-assembly-item-modal',
    templateUrl: './create-or-edit-sub-assembly-item-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-sub-assembly-item-modal.component.less']
})
export class CreateOrEditSubAssemblyItemModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreSubAssemblyItem: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    assemblyMasterItem : AssemblyDto = new AssemblyDto();

    modelList : ModelDto[] = [];
    
    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy,
        private _assemblyService : AssemblyServiceProxy
    ) {
        super(injector);
    }

    async show(assmeblyMasterId?: string) {
        await this.loadDropdownList();
        if (!assmeblyMasterId) {
            this.assemblyMasterItem = new AssemblyDto({id : null, name: "", modelId : ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._assemblyService.getAssemblyById(assmeblyMasterId).subscribe((response : AssemblyDto)=> {
                this.assemblyMasterItem = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

    async loadDropdownList() {
        await this.loadModels();
    }

    async loadModels() {
        this.modelList = await this._modelService.getModelList().toPromise();
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new AssemblyInputDto();
        input = this.assemblyMasterItem;
        this.saving = true;
        this._assemblyService
            .insertOrUpdateAssembly(input)
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
                    this.restoreSubAssemblyItem.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
