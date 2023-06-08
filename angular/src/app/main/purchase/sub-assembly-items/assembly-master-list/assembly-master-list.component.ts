import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AssemblyDto, AssemblyServiceProxy, LegalEntityDto, LegalEntityServiceProxy, ResponseDto, UnitDto, UnitServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditAssemblyMasterModalComponent } from '../create-edit-sub-assembly-item/create-or-edit-sub-assembly-item-modal.component';
// import { CreateOrEditLegalEntityModalComponent } from '../create-edit-legal-entity/create-or-edit-legal-entity-modal.component';

@Component({
    templateUrl: './assembly-master-list.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./assembly-master-list.component.less'],
    animations: [appModuleAnimation()],
})
export class AssemblyMasterListComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditAssemblyMasterModal', { static: true }) createOrEditAssemblyMasterModal: CreateOrEditAssemblyMasterModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _assemblyService: AssemblyServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getAssemblies(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._assemblyService
            .getAssemblies(
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

    deleteAssembly(assemblyItem: AssemblyDto): void {
        this.message.confirm(this.l('UnitDeleteWarningMessage', assemblyItem.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._assemblyService.deleteAssembly(assemblyItem.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreAssembly(assemblyResponse: ResponseDto):void {
        if(assemblyResponse.id == null){
            if(assemblyResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('AssemblyMasterRestoreMessage', assemblyResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._assemblyService.restoreAssembly(assemblyResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('AssemblyMasterSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingAssemblyMasterErrorMessage',assemblyResponse.name));
            }
        }
        else{
            if(assemblyResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewAssemblyMasterErrorMessage', assemblyResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._assemblyService.restoreAssembly(assemblyResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('AssemblyMasterSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingAssemblyMasterErrorMessage',assemblyResponse.name));
            }
        }
    }
}
