import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModelDto, ModelServiceProxy, ResponseDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditModelModalComponent } from '../create-edit-model/create-or-edit-model-modal.component';

@Component({
    templateUrl: './models.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./models.component.less'],
    animations: [appModuleAnimation()],
})
export class ModelsComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditModelModal', { static: true }) createOrEditModelModal: CreateOrEditModelModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getModels(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._modelService
            .getModels(
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

    createModel(): void {
        this.createOrEditModelModal.show();
    }

    deleteModel(model: ModelDto): void {
        this.message.confirm(this.l('ModelDeleteWarningMessage', model.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._modelService.deleteModel(model.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreModel(modelResponse: ResponseDto):void {
        if(modelResponse.id == null){
            if(modelResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('ModelRestoreMessage', modelResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._modelService.restoreModel(modelResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ModelSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingModelErrorMessage',modelResponse.name));
            }
        }
        else{
            if(modelResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewModelErrorMessage', modelResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._modelService.restoreModel(modelResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ModelSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingModelErrorMessage',modelResponse.name));
            }
        }
    }
}
