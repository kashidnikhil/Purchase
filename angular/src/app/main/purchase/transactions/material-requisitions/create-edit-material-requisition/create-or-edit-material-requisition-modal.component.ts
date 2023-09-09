import { Component, EventEmitter, Injector, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AssemblyDto,
    AssemblyServiceProxy,
    ItemCategoryDto,
    ItemCategoryServiceProxy,
    ItemMasterListDto,
    ItemServiceProxy,
    MaterialRequisitionDto,
    MaterialRequisitionInputDto,
    MaterialRequisitionItemDto,
    MaterialRequisitionItemInputDto,
    MaterialRequisitionServiceProxy,
    ModelDto,
    ModelServiceProxy,
    ResponseDto,
    SubAssemblyDto,
    SubAssemblyServiceProxy
} from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { map as _map, filter as _filter } from 'lodash-es';
import { finalize } from 'rxjs/operators';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
    assemblyList : AssemblyDto[] = [];
    subAssemblyList: SubAssemblyDto[] =[];
    modelList: ModelDto[] =[];
    // finalMaterialRequisitionItemList : MaterialRequisitionItemInputDto[] =[];

    selectedItemCategoryId:string="";
    selectedAssemblyId : string = "";
    selectedModelId : string = "";

    constructor(
        injector: Injector,
        private formBuilder: FormBuilder,
        private _materialRequisitionMockService: MaterialRequisitionMockService,
        private _materialRequisitionService : MaterialRequisitionServiceProxy,
        private _itemCategoryService: ItemCategoryServiceProxy,
        private _assemblyService: AssemblyServiceProxy,
        private _itemService: ItemServiceProxy,
        private _subAssemblyService: SubAssemblyServiceProxy,
        private _modelService: ModelServiceProxy,
        
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
        await this.loadAssemblyList();
        await this.loadModelList();
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

    async loadAssemblyList(){
        this.assemblyList = await this._assemblyService.getAssemblyList("").toPromise();
    }

    async loadModelList(){
        this.modelList = await this._modelService.getModelList().toPromise();
    }

    selectMaterialRequisition(materialRequisitionType: number){
        this.materialRequisitionForm.patchValue({
            materialRequisitionType: materialRequisitionType
          });
          this.materialRequisitionItems?.clear();
          console.log(this.materialRequisitionForm.value);
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
            materialRequisitionItems : materialRequisitionItem.id ?  this.formBuilder.array(
                materialRequisitionItem.materialRequisitionItems.map((x : MaterialRequisitionItemDto) => 
                    this.createMaterialRequisitionItem(x))
            ) : this.formBuilder.array([])
        });
    }

    async onItemCategorySelect(itemCategoryId: string){
        this.itemList = [];
        this.itemList = await this._itemService.getItemsByItemCategory(itemCategoryId).toPromise();
    }

    async onAssemblySelect(assemblyId: string){
        this.subAssemblyList = [];
        this.subAssemblyList = await this._subAssemblyService.getSubAssemblyList(assemblyId).toPromise();
    }

    async onModelSelect(modelId: string){
        this.subAssemblyList = [];
        // this.subAssemblyList = await this._subAssemblyService.getSubAssemblyList(assemblyId).toPromise();
    }

    onItemCategoryWiseItemsAdd(){
        if(this.selectedItemCategoryId !=""){
            let finalMaterialRequisitionItemList = this.materialRequisitionForm.get('materialRequisitionItems').value;
            this.itemList.forEach(item =>{
                if(!finalMaterialRequisitionItemList.some(x=> x.itemId == item.id)){
                    let tempMaterialRequisitionItem : MaterialRequisitionItemDto = <MaterialRequisitionItemDto>{
                        itemId: item.id,
                        requiredQuantity : 0,
                        itemName: item.itemName,
                        itemCategoryName : item.categoryName,
                        unitName : item.unitName
                    };
                    let tempMaterialRequisitionForm = this.createMaterialRequisitionItem(tempMaterialRequisitionItem);
                    this.materialRequisitionItems?.push(tempMaterialRequisitionForm);
                }
            });
            console.log(this.materialRequisitionForm.value);
        }
    }

    get materialRequisitionItems(): FormArray{
        return (<FormArray>this.materialRequisitionForm.get('materialRequisitionItems'));
    }

    // addCustomerPO() {
    //     let customerPOItem : CustomerPODto = new CustomerPODto();
    //     let customerPOForm = this.createCustomerPO(customerPOItem);
    //     this.customerPOs.push(customerPOForm);
    // }

    onAssemblyWiseItemsAdd(){
        if(this.selectedAssemblyId !=""){

        }
    }

    onModelWiseItemsAdd(){
        if(this.selectedModelId !=""){

        }
    }

    createMaterialRequisitionItem(materialRequisitionItem : MaterialRequisitionItemDto) : FormGroup {
        return this.formBuilder.group({
            id: new FormControl(materialRequisitionItem.id, []),
            itemCategoryName: new FormControl(materialRequisitionItem.itemCategoryName, []),
            itemId: new FormControl(materialRequisitionItem.itemId, []),
            itemName: new FormControl(materialRequisitionItem.itemName, []),
            materialRequisitionId: new FormControl(materialRequisitionItem.materialRequisitionId, []),
            requiredQuantity: new FormControl(materialRequisitionItem.requiredQuantity, []),
            unitName: new FormControl(materialRequisitionItem.unitName, [])
        });
    }
}
