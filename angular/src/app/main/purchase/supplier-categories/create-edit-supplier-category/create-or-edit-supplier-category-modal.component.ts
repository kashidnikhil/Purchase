import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ResponseDto,
    SupplierCategoryDto,
    SupplierCategoryInputDto,
    SupplierCategoryServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-supplier-category-modal',
    templateUrl: './create-or-edit-supplier-category-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-supplier-category-modal.component.less']
})
export class CreateOrEditSupplierCategoryModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() restoreSupplierCategory: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    supplierCategoryItem : SupplierCategoryDto = new SupplierCategoryDto();
    
    constructor(
        injector: Injector,
        private _supplierCategoryService : SupplierCategoryServiceProxy
    ) {
        super(injector);
    }

    show(poGeneralTermId?: string): void {
        if (!poGeneralTermId) {
            this.supplierCategoryItem = new SupplierCategoryDto({id : null, name: "", description: ""}); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._supplierCategoryService.getSupplierCategoryById(poGeneralTermId).subscribe((response : SupplierCategoryDto)=> {
                this.supplierCategoryItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new SupplierCategoryInputDto();
        input = this.supplierCategoryItem;
        this.saving = true;
        this._supplierCategoryService
            .insertOrUpdateSupplierCategory(input)
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
                    this.restoreSupplierCategory.emit(response);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
