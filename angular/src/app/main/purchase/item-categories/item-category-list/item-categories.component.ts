import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ItemCategoryDto, ItemCategoryServiceProxy,  ResponseDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditItemCategoryModalComponent } from '../create-edit-item-category/create-or-edit-item-category-modal.component';

@Component({
    templateUrl: './item-categories.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./item-categories.component.less'],
    animations: [appModuleAnimation()],
})
export class ItemCategoriesComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditItemCategoryModal', { static: true }) createOrEditItemCategoryModal: CreateOrEditItemCategoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _itemCategoryService: ItemCategoryServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getItemCategories(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._itemCategoryService
            .getItemCategories(
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

    createItemCategory(): void {
        this.createOrEditItemCategoryModal.show();
    }

    deleteItemCategory(itemCategory: ItemCategoryDto): void {
        this.message.confirm(this.l('ItemCategoryDeleteWarningMessage', itemCategory.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._itemCategoryService.deleteItemCategory(itemCategory.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreItemCategory(itemCategoryResponse: ResponseDto):void {
        if(itemCategoryResponse.id == null){
            if(itemCategoryResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('ItemCategoryRestoreMessage', itemCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._itemCategoryService.restoreItemCategory(itemCategoryResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ItemCategorySuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingUnitErrorMessage',itemCategoryResponse.name));
            }
        }
        else{
            if(itemCategoryResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewItemCategoryErrorMessage', itemCategoryResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._itemCategoryService.restoreItemCategory(itemCategoryResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ItemCategorySuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingItemCategoryErrorMessage',itemCategoryResponse.name));
            }
        }
    }
}
