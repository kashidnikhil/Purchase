import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AssemblyDto,
    AssemblyInputDto,
    AssemblyServiceProxy,
    ModelDto,
    ModelServiceProxy,
    ResponseDto,
    SubAssemblyDto,
    SubAssemblyInputDto,
    SubAssemblyServiceProxy
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
    subAssemblyItem : SubAssemblyDto = new SubAssemblyDto();

    modelList : ModelDto[] = [];
    assemblyList: AssemblyDto[] = [];
    
    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy,
        private _assemblyService : AssemblyServiceProxy,
        private _subAssemblyService: SubAssemblyServiceProxy
    ) {
        super(injector);
    }

    async show(subAssmeblyItemId?: string) {
        await this.loadDropdownList();
        if (!subAssmeblyItemId) {
            this.subAssemblyItem = new SubAssemblyDto({id : null, name: "", modelId : "",assemblyId:""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._subAssemblyService.getSubAssemblyById(subAssmeblyItemId).subscribe(async (response : SubAssemblyDto)=> {
                if(response.modelId){
                    await this.onModelChange(response.modelId);
                }
                this.subAssemblyItem = response;
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

    async onModelChange(modelId: string){
        this.assemblyList = await this._assemblyService.getAssemblyList(modelId).toPromise();
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new SubAssemblyInputDto();
        input = this.subAssemblyItem;
        this.saving = true;
        this._subAssemblyService
            .insertOrUpdateSubAssembly(input)
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
        this.assemblyList = [];
        this.active = false;
        this.modal.hide();
    }
}
