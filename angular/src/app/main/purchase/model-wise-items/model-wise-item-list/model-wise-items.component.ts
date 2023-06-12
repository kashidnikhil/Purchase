import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { LegalEntityDto, LegalEntityServiceProxy, ModelWiseItemDto, ModelWiseItemMasterDto, ModelWiseItemServiceProxy, ResponseDto, UnitDto, UnitServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditItemMasterModalComponent } from '../create-edit-model-wise-item/create-or-edit-model-wise-item-modal.component';
// import { CreateOrEditLegalEntityModalComponent } from '../create-edit-legal-entity/create-or-edit-legal-entity-modal.component';

@Component({
    templateUrl: './model-wise-items.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./model-wise-items.component.less'],
    animations: [appModuleAnimation()],
})
export class ModelWiseItemsComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditModelWiseItemModal', { static: true }) createOrEditModelWiseItemModal: CreateOrEditItemMasterModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _modelWiseItemService: ModelWiseItemServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getModelWiseItems(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._modelWiseItemService
            .getModelWiseItems(
                    this.filterText,
                    this.primengTableHelper.getSorting(this.dataTable),
                    this.primengTableHelper.getMaxResultCount(this.paginator, event),
                    this.primengTableHelper.getSkipCount(this.paginator, event)
            )
            .pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator()))
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createModelWiseItem(): void {
        this.createOrEditModelWiseItemModal.show();
    }

    deleteModelWiseItem(modelWiseItem: ModelWiseItemMasterDto): void {
        this.message.confirm(this.l('UnitDeleteWarningMessage', ""), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._modelWiseItemService.deleteModelWiseItemMasterData(modelWiseItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    // restoreLegalEntity(legalEntityResponse: ResponseDto):void {
    //     if(legalEntityResponse.id == null){
    //         if(legalEntityResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('UnitRestoreMessage', legalEntityResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._legalEntityService.restoreLegalEntity(legalEntityResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('UnitSuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }
    //         else{
    //             this.notify.error(this.l('ExistingUnitErrorMessage',legalEntityResponse.name));
    //         }
    //     }
    //     else{
    //         if(legalEntityResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('NewUnitErrorMessage', legalEntityResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._legalEntityService.restoreLegalEntity(legalEntityResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('UnitSuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }   
    //         else{
    //             this.notify.error(this.l('ExistingUnitErrorMessage',legalEntityResponse.name));
    //         }
    //     }
    // }
}
