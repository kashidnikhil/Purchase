import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AssemblyDto,
    AssemblyInputDto,
    AssemblyServiceProxy,
    ItemMasterListDto,
    ItemServiceProxy,
    ModelDto,
    ModelServiceProxy,
    ResponseDto,
    SubAssemblyDto,
    SubAssemblyInputDto,
    SubAssemblyItemDto,
    SubAssemblyItemInputDto,
    SubAssemblyServiceProxy
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

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

    subAssemblyForm!: FormGroup;

    modelList : ModelDto[] = [];
    assemblyList: AssemblyDto[] = [];

    itemMasterList  : ItemMasterListDto [] = [];
    
    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _modelService: ModelServiceProxy,
        private _itemMasterService: ItemServiceProxy,
        private _assemblyService : AssemblyServiceProxy,
        private _subAssemblyService: SubAssemblyServiceProxy
    ) {
        super(injector);
    }

    async show(subAssmeblyItemId?: string) {
        await this.loadDropdownList();
        let subAssemblyData = new SubAssemblyDto();
        if (!subAssmeblyItemId) {
            this.initialiseSubAssemblyItemForm(subAssemblyData);
            this.active = true;
            this.modal.show();
        }
        else{
            this._subAssemblyService.getSubAssemblyById(subAssmeblyItemId).subscribe(async (response : SubAssemblyDto)=> {
                if(response.modelId){
                    await this.onModelChange(response.modelId);
                }
                let subAssemblyData = response;
                this.initialiseSubAssemblyItemForm(subAssemblyData);
                this.active = true;
                this.modal.show();
            });
        }        
    }

    initialiseSubAssemblyItemForm(subAssemblyItem : SubAssemblyDto) : void{
        this.subAssemblyForm = this.formBuilder.group({
            id: new FormControl(subAssemblyItem.id, []),
            name: new FormControl(subAssemblyItem.name, []),
            modelId: new FormControl(subAssemblyItem.modelId ? subAssemblyItem.modelId : null, [Validators.required]),
            assemblyId: new FormControl(subAssemblyItem.assemblyId? subAssemblyItem.assemblyId : null, []),
            subAssemblyItems : [this.unMapSubAssemblyItems(subAssemblyItem.subAssemblyItems), []]
        });
    }

    unMapSubAssemblyItems(subAssemblyItemList: SubAssemblyItemDto[]): ItemMasterListDto[] {
        let tempItemList: ItemMasterListDto[] = [];
        if (subAssemblyItemList && subAssemblyItemList.length > 0) {
            subAssemblyItemList.forEach(item => {
                let tempItemMaster: ItemMasterListDto = new ItemMasterListDto(
                    {
                        id: item.itemId ? item.itemId : "",
                        itemName : item.itemName,
                        categoryId : item.categoryId,
                        genericName : item.genericName,
                        itemId : item.existingItemId,
                        make :item.make,
                        unitName : item.unitName
                    }
                );
                tempItemList.push(tempItemMaster);
            });
        }
        return tempItemList;
    }

    mapSubAssemblyItems(subAssemblyItemList: SubAssemblyItemInputDto[]): SubAssemblyItemInputDto[] {
        let tempAssemblyItemList: SubAssemblyItemInputDto[] = [];
        subAssemblyItemList.forEach(item => {
            let tempSupplierCategoryItem: SubAssemblyItemInputDto = new SubAssemblyItemInputDto(
                {
                    id: item.id? item.id : "",
                    itemId: item.itemId,
                    subAssemblyId: item.subAssemblyId,
                }
            );
            tempAssemblyItemList.push(tempSupplierCategoryItem);
        });
        return tempAssemblyItemList;
    }

    async loadDropdownList() {
        await this.loadModels();
        await this.loadItemMasters();
    }

    async loadModels() {
        this.modelList = await this._modelService.getModelList().toPromise();
    }

    async loadItemMasters() {
        this.itemMasterList = await this._itemMasterService.getItemMasterList().toPromise();
    }

    async onModelChange(modelId: string){
        this.assemblyList = await this._assemblyService.getAssemblyList(modelId).toPromise();
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new SubAssemblyInputDto();
        input = this.subAssemblyForm.value;
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
