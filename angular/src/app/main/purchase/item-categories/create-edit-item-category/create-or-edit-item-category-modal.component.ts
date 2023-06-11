import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ItemCategoryDto,
    ItemCategoryInputDto,
    ItemCategoryServiceProxy,
    ResponseDto
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
    itemCategory : ItemCategoryDto = new ItemCategoryDto();
    
    constructor(
        injector: Injector,
        private _itemCategoryService: ItemCategoryServiceProxy
    ) {
        super(injector);
    }

    show(itemCategoryId?: string): void {
        if (!itemCategoryId) {
            this.itemCategory = new ItemCategoryDto({id : null, name: "", description: "",itemCategoryCode : 0}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._itemCategoryService.getItemCategoryById(itemCategoryId).subscribe((response : ItemCategoryDto)=> {
                this.itemCategory = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new ItemCategoryInputDto();
        input = this.itemCategory;
        this.saving = true;
        this._itemCategoryService
            .insertOrUpdateItemCategory(input)
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
