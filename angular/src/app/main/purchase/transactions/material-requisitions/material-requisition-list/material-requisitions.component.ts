import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { MaterialRequisitionDto, MaterialRequisitionServiceProxy, ResponseDto, TermsOfPaymentDto, TermsOfPaymentServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
// import { CreateOrEditTermsOfPaymentModalComponent } from '../create-edit-terms-of-payment/create-or-edit-terms-of-payment-modal.component';

@Component({
    templateUrl: './material-requisitions.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./material-requisitions.component.less'],
    animations: [appModuleAnimation()],
})
export class MaterialRequisitionsComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditMaterialRequisitionModal', { static: true }) createOrEditMaterialRequisitionModal: any; //CreateOrEditTermsOfPaymentModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _materialRequisitionService: MaterialRequisitionServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getMaterialRequisitions(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._materialRequisitionService
            .getMaterialRequisitions(
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

    createMaterialRequisition(): void {
        this.createOrEditMaterialRequisitionModal.show();
    }

    deleteMaterialRequisition(materialRequisition: MaterialRequisitionDto): void {
        this.message.confirm(this.l('MaterialRequisitionDeleteWarningMessage', materialRequisition.mriNumber), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._materialRequisitionService.deleteMaterialRequisition(materialRequisition.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    //Restoring material requisition needs to be used 
    // restoreTermsOfPayment(termsOfPaymentResponse: ResponseDto):void {
    //     if(termsOfPaymentResponse.id == null){
    //         if(termsOfPaymentResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('TermsOfPaymentRestoreMessage', termsOfPaymentResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._termsOfPaymentService.restoreTermsOfPayment(termsOfPaymentResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('TermsOfPaymentSuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }
    //         else{
    //             this.notify.error(this.l('ExistingTermsOfPaymentErrorMessage',termsOfPaymentResponse.name));
    //         }
    //     }
    //     else{
    //         if(termsOfPaymentResponse.isExistingDataAlreadyDeleted){
    //             this.message.confirm(this.l('NewTermsOfPaymentErrorMessage', termsOfPaymentResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
    //                 if (isConfirmed) {
    //                     this._termsOfPaymentService.restoreTermsOfPayment(termsOfPaymentResponse.restoringItemId).subscribe(() => {
    //                         this.reloadPage();
    //                         this.notify.success(this.l('TermsOfPaymentSuccessfullyRestored'));
    //                     });
    //                 }
    //             });
    //         }   
    //         else{
    //             this.notify.error(this.l('ExistingSupplierCategoryErrorMessage',termsOfPaymentResponse.name));
    //         }
    //     }
    // }
}
