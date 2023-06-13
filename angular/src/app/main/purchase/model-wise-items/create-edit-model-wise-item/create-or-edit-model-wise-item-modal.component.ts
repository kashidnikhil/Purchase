import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {ItemMasterInputDto,ItemMasterListDto,ItemServiceProxy,ModelDto,ModelServiceProxy,ModelWiseItemDto,ModelWiseItemMasterDto,ModelWiseItemMasterInputDto,ModelWiseItemServiceProxy,ResponseDto} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { formatDate } from '@angular/common';

@Component({
    selector: 'create-or-edit-model-wise-item-modal',
    templateUrl: './create-or-edit-model-wise-item-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-model-wise-item-modal.component.less']
})
export class CreateOrEditModelWiseItemMasterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();


    modelWiseItemMasterForm!: FormGroup;
    active: boolean = false;
    submitted: boolean = false;
    saving: boolean = false;
    itemMasterList  : ItemMasterListDto [] = [];
    modelList  : ModelDto [] = [];
    
    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _itemMasterService: ItemServiceProxy,
        private _modelService: ModelServiceProxy,
        private _modelWiseItemService: ModelWiseItemServiceProxy
    ) {
        super(injector);
    }

    async show(itemMasterId?: string) {
        await this.loadDropdownList();
        let modelWiseItemMaster = new ModelWiseItemMasterDto();
        if (!itemMasterId) {
            this.modelWiseItemMasterForm =  this.initialiseModelWiseItemMasterForm(modelWiseItemMaster);
            this.active = true;
            this.modal.show();
        }
        else {
            this._modelWiseItemService.getModelWiseItemMasterById(itemMasterId).subscribe((response:ModelWiseItemMasterDto) => {
                let modelWiseItemMaster = response;
                this.modelWiseItemMasterForm = this.initialiseModelWiseItemMasterForm(modelWiseItemMaster);
                this.active = true;
                this.modal.show();
            });
        }
    }

    public initialiseModelWiseItemMasterForm(modelWiseItemMaster: ModelWiseItemMasterDto) : FormGroup {
        let modelWiseItem : ModelWiseItemDto = new ModelWiseItemDto();
        return this.formBuilder.group({
            id: new FormControl(modelWiseItemMaster.id, []),
            modelId: new FormControl(modelWiseItemMaster.modelId, []),
            modelWiseItemData: modelWiseItemMaster.modelWiseItemData && modelWiseItemMaster.modelWiseItemData.length > 0 ? this.formBuilder.array(
                modelWiseItemMaster.modelWiseItemData.map((x : ModelWiseItemDto) =>
                    this.createModelWiseItemData(x)
                )
            ) : this.formBuilder.array([this.createModelWiseItemData(modelWiseItem)]),
        });
    }

    onItemSelect(itemId : string, indexValue?: number) : string|null{
        if(itemId){
            let selectedItem = this.itemMasterList.find(x=> x.id == itemId);
            if(indexValue){
                const modelWiseItemArray = this.modelWiseItemData;
                modelWiseItemArray.at(indexValue).patchValue({
                    itemDescription : this.parseFieldValue(selectedItem.genericName) + this.parseFieldValue(selectedItem.itemName) + this.parseFieldValue(selectedItem.make) + this.parseFieldValue(selectedItem.model) + this.parseFieldValue(selectedItem.serialNumber) + this.parseFieldValue(selectedItem.specifications) + this.parseFieldValue(formatDate(new Date(<string><unknown>selectedItem.purchaseDate), "yyyy-MM-dd", "en")) + this.parseFieldValue(<string><unknown>selectedItem.purchaseValue) 
                });
                return null;
            }
            else{
                return this.parseFieldValue(selectedItem?.genericName) + this.parseFieldValue(selectedItem?.itemName) + this.parseFieldValue(selectedItem?.make) + this.parseFieldValue(selectedItem?.model) + this.parseFieldValue(selectedItem?.serialNumber) + this.parseFieldValue(selectedItem?.specifications) + this.parseFieldValue(formatDate(new Date(<string><unknown>selectedItem?.purchaseDate), "yyyy-MM-dd", "en")) + this.parseFieldValue(<string><unknown>selectedItem?.purchaseValue);
            }
        }
        return null;
    }

    parseFieldValue(inputString : string){
        let value = inputString ? inputString + " - " : "";
        return value;
    }

    createModelWiseItemData(modelWiseItem: ModelWiseItemDto): FormGroup {
        return this.formBuilder.group({
            id: new FormControl(modelWiseItem.id, []),
            itemId: new FormControl(modelWiseItem.itemId, []),
            itemDescription : new FormControl({value : this.onItemSelect(modelWiseItem.itemId,null), disabled: true}, []),
            comments: new FormControl(modelWiseItem.comments, [])
        });
    }

    get modelWiseItemData(): FormArray {
        return (<FormArray>this.modelWiseItemMasterForm.get('modelWiseItemData'));
    }

    addModelWiseItemData() {
        let modelWiseItem: ModelWiseItemDto = new ModelWiseItemDto();
        let modelWiseItemForm = this.createModelWiseItemData(modelWiseItem);
        this.modelWiseItemData.push(modelWiseItemForm);
    }

    deleteModelWiseItemData(indexValue: number) {
        const modelWiseItemArray = this.modelWiseItemData;
        modelWiseItemArray.removeAt(indexValue);
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

    onShown(): void {
        // document.getElementById('name').focus();
    }

    save(): void {
        this.submitted = true;
        if (this.modelWiseItemMasterForm.valid) {
            let input = new ModelWiseItemMasterInputDto();
            this.saving = true;
            input = this.modelWiseItemMasterForm.getRawValue();
            
            this._modelWiseItemService
                .insertOrUpdateModelWiseItem(input)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe((response: ResponseDto) => {
                    if(!response.dataMatchFound){
                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                    else{
                        this.close();
                    }
                });
        }
    }


    close(): void {
        this.submitted = false;
        this.active = false;
        this.modal.hide();
    }
}
