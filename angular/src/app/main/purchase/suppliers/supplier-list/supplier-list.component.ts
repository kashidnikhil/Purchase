import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ResponseDto, SupplierCategoryDto, SupplierCategoryServiceProxy, SupplierDto, SupplierServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';

@Component({
    templateUrl: './supplier-list.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./supplier-list.component.less'],
    animations: [appModuleAnimation()],
})
export class SupplierListComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditSupplierModal', { static: true }) createOrEditSupplierModal: any; //CreateOrEditSupplierCategoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _supplierService: SupplierServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSuppliers(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._supplierService
            .getSuppliers(
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

    createSupplier(): void {
        this.createOrEditSupplierModal.show();
    }

    deleteSupplier(supplierItem: SupplierDto): void {
        this.message.confirm(this.l('SupplierDeleteWarningMessage', supplierItem.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._supplierService.deleteSupplierMasterData(supplierItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    // restoreSupplierCategory(supplierCategoryResponse: ResponseDto):void {
    //     if(supplierCategoryResponse.id == null){
    //         if(supplierCategoryResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('SupplierCategoryRestoreMessage', supplierCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._supplierCategoryService.restoreSupplierCategory(supplierCategoryResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('SupplierCategorySuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }
    //         else{
    //             this.notify.error(this.l('ExistingSupplierCategoryErrorMessage',supplierCategoryResponse.name));
    //         }
    //     }
    //     else{
    //         if(supplierCategoryResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('NewSupplierCategoryErrorMessage', supplierCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._supplierCategoryService.restoreSupplierCategory(supplierCategoryResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('SupplierCategorySuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }   
    //         else{
    //             this.notify.error(this.l('ExistingSupplierCategoryErrorMessage',supplierCategoryResponse.name));
    //         }
    //     }
    // }
}
