import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import {  DeliveryTermDto, DeliveryTermServiceProxy, ResponseDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditDeliveryTermModalComponent } from '../create-edit-delivery-term/create-or-edit-delivery-term-modal.component';

@Component({
    templateUrl: './delivery-terms.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./delivery-terms.component.less'],
    animations: [appModuleAnimation()],
})
export class DeliveryTermsComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditDeliveryTermModal', { static: true }) createOrEditAcceptanceCriteriaModal: CreateOrEditDeliveryTermModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _deliveryTermService: DeliveryTermServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getDeliveryTerms(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._deliveryTermService
            .getDeliveryTerms(
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

    createDeliveryTerm(): void {
        this.createOrEditAcceptanceCriteriaModal.show();
    }

    deleteTechnique(deliveryTerm: DeliveryTermDto): void {
        this.message.confirm(this.l('DeliveryTermDeleteWarningMessage', deliveryTerm.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._deliveryTermService.deleteDeliveryTerm(deliveryTerm.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreDeliveryTerm(deliveryTermResponse: ResponseDto):void {
        if(deliveryTermResponse.id == null){
            if(deliveryTermResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('DeliveryTermRestoreMessage', deliveryTermResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._deliveryTermService.restoreDeliveryTerm(deliveryTermResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('DeliveryTermSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingDeliveryTermErrorMessage',deliveryTermResponse.name));
            }
        }
        else{
            if(deliveryTermResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewDeliveryTermErrorMessage', deliveryTermResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._deliveryTermService.restoreDeliveryTerm(deliveryTermResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('DeliveryTermSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingDeliveryTermErrorMessage',deliveryTermResponse.name));
            }
        }
    }
}
