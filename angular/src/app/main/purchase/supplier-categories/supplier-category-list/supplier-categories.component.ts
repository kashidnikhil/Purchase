import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ResponseDto, SupplierCategoryDto, SupplierCategoryServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSupplierCategoryModalComponent } from '../create-edit-supplier-category/create-or-edit-supplier-category-modal.component';

@Component({
    templateUrl: './supplier-categories.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./supplier-categories.component.less'],
    animations: [appModuleAnimation()],
})
export class SupplierCategoriesComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditSupplierCategoryModal', { static: true }) createOrEditSupplierCategoryModal: CreateOrEditSupplierCategoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _supplierCategoryService: SupplierCategoryServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSupplierCategories(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._supplierCategoryService
            .getSupplierCategories(
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

    createSupplierCategory(): void {
        this.createOrEditSupplierCategoryModal.show();
    }

    deleteSupplierCategory(supplierCategory: SupplierCategoryDto): void {
        this.message.confirm(this.l('SupplierCategoryDeleteWarningMessage', supplierCategory.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._supplierCategoryService.deleteSupplierCategory(supplierCategory.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreSupplierCategory(supplierCategoryResponse: ResponseDto):void {
        if(supplierCategoryResponse.id == null){
            if(supplierCategoryResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('SupplierCategoryRestoreMessage', supplierCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._supplierCategoryService.restoreSupplierCategory(supplierCategoryResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SupplierCategorySuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingSupplierCategoryErrorMessage',supplierCategoryResponse.name));
            }
        }
        else{
            if(supplierCategoryResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewSupplierCategoryErrorMessage', supplierCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._supplierCategoryService.restoreSupplierCategory(supplierCategoryResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SupplierCategorySuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingSupplierCategoryErrorMessage',supplierCategoryResponse.name));
            }
        }
    }
}
