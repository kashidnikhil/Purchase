import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { MaterialGradeDto, MaterialGradeServiceProxy, ResponseDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { CreateOrEditMaterialGradeModalComponent } from '../create-edit-material-grade/create-or-edit-material-grade-modal.component';

@Component({
    templateUrl: './material-grades.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./material-grades.component.less'],
    animations: [appModuleAnimation()],
})
export class MaterialGradesComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditMaterialGradeModal', { static: true }) createOrEditMaterialGradeModal: CreateOrEditMaterialGradeModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    //Filters
    filterText = '';

    constructor(
        injector: Injector,
        private _materialGradeService: MaterialGradeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getMaterialGrades(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._materialGradeService
            .getMaterialGrades(
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

    createMaterialGrade(): void {
        this.createOrEditMaterialGradeModal.show();
    }

    deleteMaterialGrade(deliveryTerm: MaterialGradeDto): void {
        this.message.confirm(this.l('MaterialGradeDeleteWarningMessage', deliveryTerm.name), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._materialGradeService.deleteMaterialGrade(deliveryTerm.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    restoreMaterialGrade(materialGradeResponse: ResponseDto):void {
        if(materialGradeResponse.id == null){
            if(materialGradeResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('MaterialGradeRestoreMessage', materialGradeResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._materialGradeService.restoreMaterialGrade(materialGradeResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('MaterialGradeSuccessfullyRestored'));
                        });
                    }
                });
            }
            else{
                this.notify.error(this.l('ExistingMaterialGradeErrorMessage',materialGradeResponse.name));
            }
        }
        else{
            if(materialGradeResponse.isExistingDataAlreadyDeleted){
                this.message.confirm(this.l('NewMaterialGradeErrorMessage', materialGradeResponse.name), this.l('AreYouSure'), async (isConfirmed) => {
                    if (isConfirmed) {
                        this._materialGradeService.restoreMaterialGrade(materialGradeResponse.restoringItemId).subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('MaterialGradeSuccessfullyRestored'));
                        });
                    }
                });
            }   
            else{
                this.notify.error(this.l('ExistingMaterialGradeErrorMessage',materialGradeResponse.name));
            }
        }
    }
}
