import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AssemblyDto, AssemblyServiceProxy, LegalEntityDto, LegalEntityServiceProxy, ResponseDto, SubAssemblyItemDto, SubAssemblyItemServiceProxy, UnitDto, UnitServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
// import { CreateOrEditAssemblyMasterModalComponent } from '../create-edit-assembly-master/create-or-edit-assembly-master-modal.component';
// import { CreateOrEditLegalEntityModalComponent } from '../create-edit-legal-entity/create-or-edit-legal-entity-modal.component';

@Component({
    templateUrl: './sub-assembly-item-list.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./sub-assembly-item-list.component.less'],
    animations: [appModuleAnimation()],
})
export class SubAssemblyItemListComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditAssemblyMasterModal', { static: true }) createOrEditAssemblyMasterModal: any; //CreateOrEditAssemblyMasterModalComponent;
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

    createAssemblyMaster(): void {
        this.createOrEditAssemblyMasterModal.show();
    }

    deleteAssembly(subAssemblyItem: SubAssemblyItemDto): void {
        this.message.confirm(this.l('UnitDeleteWarningMessage', subAssemblyItem.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._subAssemblyItemService.deleteSubAssemblyItem(subAssemblyItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreAssembly(subAssemblyItemResponse: ResponseDto):void {
        if(subAssemblyItemResponse.id == null){
            if(subAssemblyItemResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('AssemblyMasterRestoreMessage', subAssemblyItemResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._subAssemblyItemService.restoreSubAssemblyItem(subAssemblyItemResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('AssemblyMasterSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingAssemblyMasterErrorMessage',subAssemblyItemResponse.name));
            }
        }
        else{
            if(subAssemblyItemResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewAssemblyMasterErrorMessage', subAssemblyItemResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._subAssemblyItemService.restoreSubAssemblyItem(subAssemblyItemResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('AssemblyMasterSuccessfullyRestored'));
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
