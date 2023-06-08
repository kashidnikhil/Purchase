import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ResponseDto, SubAssemblyItemDto, SubAssemblyItemServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSubAssemblyItemModalComponent } from '../create-edit-sub-assembly-item/create-or-edit-sub-assembly-item-modal.component';

@Component({
    templateUrl: './sub-assembly-item-list.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./sub-assembly-item-list.component.less'],
    animations: [appModuleAnimation()],
})
export class SubAssemblyItemListComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditSubAssemblyItemModal', { static: true }) createOrEditAssemblyMasterModal: CreateOrEditSubAssemblyItemModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _subAssemblyItemService: SubAssemblyItemServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSubAssemblyItems(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._subAssemblyItemService
            .getSubAssemblyItems(
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

    createSubAssemblyItem(): void {
        this.createOrEditAssemblyMasterModal.show();
    }

    deleteSubAssemblyItem(subAssemblyItem: SubAssemblyItemDto): void {
        this.message.confirm(this.l('UnitDeleteWarningMessage', subAssemblyItem.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._subAssemblyItemService.deleteSubAssemblyItem(subAssemblyItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreSubAssemblyItem(subAssemblyItemResponse: ResponseDto):void {
        if(subAssemblyItemResponse.id == null){
            if(subAssemblyItemResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('SubAssemblyItemRestoreMessage', subAssemblyItemResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._subAssemblyItemService.restoreSubAssemblyItem(subAssemblyItemResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SubAssemblyItemSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingSubAssemblyItemErrorMessage',subAssemblyItemResponse.name));
            }
        }
        else{
            if(subAssemblyItemResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewSubAssemblyItemErrorMessage', subAssemblyItemResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._subAssemblyItemService.restoreSubAssemblyItem(subAssemblyItemResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SubAssemblyItemSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingAssemblyMasterErrorMessage',subAssemblyItemResponse.name));
            }
        }
    }
}
