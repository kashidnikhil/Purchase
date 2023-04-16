import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    SupplierDto,
    SupplierInputDto,
    SupplierServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'create-edit-supplier-modal',
    templateUrl: './create-or-edit-supplier-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-supplier-modal.component.less']
})
export class CreateOrEditSupplierModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    // @Output() restoreSupplierCategory: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    active = false;
    saving = false;
    supplierItem : SupplierDto = new SupplierDto();
    
    constructor(
        injector: Injector,
        private _supplierService : SupplierServiceProxy
    ) {
        super(injector);
    }

    show(supplierId?: string): void {
        if (!supplierId) {
            this.supplierItem = new SupplierDto(); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._supplierService.getSupplierMasterById(supplierId).subscribe((response : SupplierDto)=> {
                this.supplierItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

   
    onShown(): void {
        document.getElementById('name').focus();
    }

    save(): void {
        let input = new SupplierInputDto();
        input = this.supplierItem;
        this.saving = true;
        this._supplierService
            .insertOrUpdateSupplier(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((response : string) => {
                if(!response){
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(null);
                }
                // else{
                //     this.close();
                //     this.restoreSupplierCategory.emit(response);
                // }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
