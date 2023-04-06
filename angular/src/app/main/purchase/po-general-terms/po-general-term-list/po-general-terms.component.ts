import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { POGeneralTermDto, POGeneralTermServiceProxy, ResponseDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPOGeneralTermModalComponent } from '../create-edit-po-general-term/create-or-edit-po-general-term-modal.component';

@Component({
    templateUrl: './po-general-terms.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./po-general-terms.component.less'],
    animations: [appModuleAnimation()],
})
export class POGeneralTermsComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditPOGeneralTermModal', { static: true }) createOrEditPOGeneralTermModal: CreateOrEditPOGeneralTermModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _poGeneralTermService: POGeneralTermServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPOGeneralTerms(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._poGeneralTermService
            .getPOGeneralTerms(
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

    createPOGeneralTerm(): void {
        this.createOrEditPOGeneralTermModal.show();
    }

    deletePOGeneralTerm(poGeneralTerm: POGeneralTermDto): void {
        this.message.confirm(this.l('POGeneralTermDeleteWarningMessage', poGeneralTerm.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._poGeneralTermService.deletePOGeneralTerm(poGeneralTerm.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restorePOGeneralTerm(poGeneralTermResponse: ResponseDto):void {
        if(poGeneralTermResponse.id == null){
            if(poGeneralTermResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('POGeneralTermRestoreMessage', poGeneralTermResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._poGeneralTermService.restorePOGeneralTerm(poGeneralTermResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('POGeneralTermSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingPOGeneralTermErrorMessage',poGeneralTermResponse.name));
            }
        }
        else{
            if(poGeneralTermResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewPOGeneralTermErrorMessage', poGeneralTermResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._poGeneralTermService.restorePOGeneralTerm(poGeneralTermResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('POGeneralTermSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingPOGeneralTermErrorMessage',poGeneralTermResponse.name));
            }
        }
    }
}
