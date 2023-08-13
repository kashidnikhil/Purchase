import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    ItemCategoryDto,
    ItemCategoryServiceProxy,
    ItemMasterListDto,
    ItemServiceProxy,
    MaterialRequisitionDto,
    MaterialRequisitionInputDto,
    MaterialRequisitionServiceProxy,
    ResponseDto
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { MaterialRequisitionMockService } from '@app/shared/common/mock-data-services/material-requisition.mock.service';
import { DropdownDto } from '@app/shared/common/data-models/dropdown';

@Component({
    selector: 'create-edit-material-requisition-modal',
    templateUrl: './create-or-edit-material-requisition-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['create-or-edit-material-requisition-modal.component.less']
})
export class CreateOrEditMaterialRequisitionModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    // @Output() restoreTermsOfPayment: EventEmitter<ResponseDto> = new EventEmitter<ResponseDto>();

    materialRequisitionForm!: FormGroup;
    active = false;
    saving = false;
    materialRequisitionItem : MaterialRequisitionDto = new MaterialRequisitionDto();

    locationList: DropdownDto[] = [];
    materialRequisitionTypeList : DropdownDto[] = [];
    itemCategories: ItemCategoryDto[] =[];
    itemList : ItemMasterListDto[] = [];

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _materialRequisitionMockService: MaterialRequisitionMockService,
        private _materialRequisitionService : MaterialRequisitionServiceProxy,
        private _itemCategoryService: ItemCategoryServiceProxy,
        private _itemService: ItemServiceProxy
    ) {
        super(injector);
    }

    async show(materialRequisitionId?: string){
        await this.loadDropdownList();
        if (!materialRequisitionId) {
            //Need to add the logic for retriving MRINumber
            let MRINumber = await this.setMRINumber();
            let materialRequisitionItem = new MaterialRequisitionDto();
            materialRequisitionItem.mriNumber = MRINumber;
            this.materialRequisitionForm = this.initialiseMaterialRequisitionForm(materialRequisitionItem); 
            this.active = true;
            this.modal.show();
        }
        else{
            this._materialRequisitionService.getMaterialRequisitionById(materialRequisitionId).subscribe((response : MaterialRequisitionDto)=> {
                this.materialRequisitionItem  = response;
                this.active = true;
                this.modal.show();
            });
        }        
    }

    async loadDropdownList() {
        await this.loadMaterialRequisitionType();
        await this.loadMaterialRequisitionLocations();
        await this.loadItemCategoryList();
    }

    async setMRINumber() : Promise<string>{
        let mriNumber: string = "";
        
        mriNumber = await this._materialRequisitionService.getLatestMaterialRequisitionNumber().toPromise();
        if( mriNumber != "" ){
            let tmpMriNumberValue : string = ((+mriNumber.slice(2))+1).toString();
            let finalMRICounter = this.addLeadingZeros(tmpMriNumberValue.toString(),"0", 5);
            mriNumber = "MR" + finalMRICounter;
        }
        else{
            mriNumber = "MR00001";
        }
        return mriNumber;
    }
    
    private addLeadingZeros(text: string, padChar: string, size: number): string {
        return (String(padChar).repeat(size) + text).substr(size * -1, size);
      }

    onShown(): void {
        //document.getElementById('name').focus();
    }

    save(): void {
        if (this.materialRequisitionForm.valid) {
            let input = new MaterialRequisitionInputDto();
            input = this.materialRequisitionForm.getRawValue();
            this.saving = true;
            this._materialRequisitionService.insertOrUpdateMaterialRequisition(input)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe((response : ResponseDto) => {
                    if(!response.dataMatchFound){
                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                    else{
                        this.close();
                        // this.restoreTermsOfPayment.emit(response);
                    }
                });
        }
        
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    loadMaterialRequisitionLocations() {
        this.locationList = this._materialRequisitionMockService.loadLocations();
    }

    loadMaterialRequisitionType(){
        this.materialRequisitionTypeList = this._materialRequisitionMockService.loadMaterialRequisitionType();
    }

    async loadItemCategoryList(){
        this.itemCategories = await this._itemCategoryService.getItemCategoryList().toPromise();
    }

    selectMaterialRequisition(materialRequisitionType: number){
        this.materialRequisitionForm.patchValue({
            materialRequisitionType: materialRequisitionType
          });
    }


    public initialiseMaterialRequisitionForm(materialRequisitionItem: MaterialRequisitionDto) : FormGroup {
        return this.formBuilder.group({
            id: new FormControl(materialRequisitionItem.id, []),
            mriDate: new FormControl(materialRequisitionItem.mriDate ? formatDate(new Date(<string><unknown>materialRequisitionItem.mriDate), "yyyy-MM-dd", "en") : formatDate(new Date(Date.now()), "yyyy-MM-dd", "en"), []),
            mriNumber: new FormControl({value: materialRequisitionItem.mriNumber, disabled: true}, []),
            location: new FormControl(materialRequisitionItem.location ? materialRequisitionItem.location : null, []),
            userId: new FormControl(materialRequisitionItem.userId ? materialRequisitionItem.userId : null, []),
            usedFor: new FormControl(materialRequisitionItem.usedFor, []),
            materialRequisitionType: new FormControl(materialRequisitionItem.materialRequisitionType, []),
            projectNumber: new FormControl(materialRequisitionItem.projectNumber, []),
            comments: new FormControl(materialRequisitionItem.comments, []),
            requireByDate: new FormControl(materialRequisitionItem.requireByDate ? formatDate(new Date(<string><unknown>materialRequisitionItem.requireByDate), "yyyy-MM-dd", "en") : null, []),
          
            // amcRequired: new FormControl(itemMaster.amcRequired ? <number>itemMaster.amcRequired : null, []),
            // make: new FormControl(itemMaster.make, []),
            // model: new FormControl(itemMaster.model, []),
            // serialNumber: new FormControl(itemMaster.serialNumber, []),
            // specifications: new FormControl(itemMaster.specifications, []),
            // storageConditions: new FormControl(itemMaster.storageConditions, []),
            // itemMobility: new FormControl(itemMaster.itemMobility ? <number>itemMaster.itemMobility : null, []),
            // calibrationRequirement: new FormControl(itemMaster.calibrationRequirement ? <number>itemMaster.calibrationRequirement : null, []),
            // supplierId: new FormControl(itemMaster.supplierId, []),
            // hsnCode : new FormControl(itemMaster.hsnCode, []),
            // gst : new FormControl(itemMaster.gst ? parseFloat(itemMaster.gst.toString()).toFixed(2) : null, []),
            // purchaseValue : new FormControl(itemMaster.purchaseValue ? parseFloat(itemMaster.purchaseValue.toString()).toFixed(2) : null, []),
            // purchaseDate: new FormControl(itemMaster.purchaseDate ? formatDate(new Date(<string><unknown>itemMaster.purchaseDate), "yyyy-MM-dd", "en") : null, []),
            // orderingRate : new FormControl(itemMaster.orderingRate ? parseFloat(itemMaster.orderingRate.toString()).toFixed(2) : null, []),
            // quantity : new FormControl(itemMaster.quantity ? <number>itemMaster.quantity : null, []),
            // ratePerQuantity : new FormControl(itemMaster.ratePerQuantity ? parseFloat(itemMaster.ratePerQuantity.toString()).toFixed(2) : null, []), 
            // rateAsOnDate : new FormControl(itemMaster.rateAsOnDate ? parseFloat(itemMaster.rateAsOnDate.toString()).toFixed(2) : null, []), 
            // leadTime: new FormControl(itemMaster.leadTime ? <number>itemMaster.leadTime : null, []),
            // supplierItemName: new FormControl(itemMaster.supplierItemName ? itemMaster.supplierItemName : null, []),
            // status : new FormControl(itemMaster.status ? <number>itemMaster.status : null, []),
            // recordedBy : new FormControl(itemMaster.recordedBy ? <number>itemMaster.recordedBy : null, []),
            // approvedBy : new FormControl(itemMaster.approvedBy ? <number>itemMaster.approvedBy : null, []),
            // discardedOn: new FormControl(itemMaster.discardedOn ? formatDate(new Date(<string><unknown>itemMaster.discardedOn), "yyyy-MM-dd", "en") : null, []),
            // discardApprovedBy : new FormControl(itemMaster.discardApprovedBy ? <number>itemMaster.discardApprovedBy : null, []),
            // discardedReason : new FormControl(itemMaster.discardedReason ? itemMaster.discardedReason : null, []),
            // comment : new FormControl(itemMaster.comment ? itemMaster.comment : null, []),
            // msl: new FormControl(itemMaster.msl ? itemMaster.msl : null, []),
            // materialGradeId : new FormControl(itemMaster.materialGradeId ? itemMaster.materialGradeId : null, []),
            // unitOrderId : new FormControl(itemMaster.unitOrderId ? itemMaster.unitOrderId : null, []),
            // unitStockId : new FormControl(itemMaster.unitStockId ? itemMaster.unitStockId : null, []),
            // stockUOMId : new FormControl(itemMaster.stockUOMId ? itemMaster.stockUOMId : null, []),
            // orderingUOMId : new FormControl(itemMaster.orderingUOMId ? itemMaster.orderingUOMId : null, []),
            // ctqRequirement : new FormControl(itemMaster.ctqRequirement ? <number>itemMaster.ctqRequirement : null, []),
            // ctqSpecifications : new FormControl(itemMaster.ctqSpecifications ? itemMaster.ctqSpecifications : null, []),
            // expiryApplicable : new FormControl(itemMaster.expiryApplicable ? <number>itemMaster.expiryApplicable : null, []),
            // quantityPerOrderingUOM : new FormControl(itemMaster.quantityPerOrderingUOM ? parseFloat(itemMaster.quantityPerOrderingUOM.toString()).toFixed(2) : null, []),
            // minimumOrderQuantity : new FormControl(itemMaster.minimumOrderQuantity ? parseFloat(itemMaster.minimumOrderQuantity.toString()).toFixed(2) : null, [])
        });
    }

    async onItemCategorySelect(itemCategoryId: string){
        this.itemList = [];
        this.itemList = await this._itemService.getItemsByItemCategory(itemCategoryId).toPromise();
    }
}
