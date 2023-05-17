import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ItemMastersRoutingModule } from "./item-masters-routing.module";
// import { SupplierListComponent } from "./supplier-list/supplier-list.component";
// import { CreateOrEditSupplierModalComponent } from "./create-edit-supplier/create-or-edit-supplier-modal.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MultiSelectModule } from 'primeng/multiselect';
import { ItemMasterListComponent } from "./item-master-list/item-master-list.component";

@NgModule({
    declarations: [
        ItemMasterListComponent,
        // CreateOrEditSupplierModalComponent
    ],
    imports: [
        AppSharedModule,  
        ItemMastersRoutingModule,
        MultiSelectModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [],
})

export class ItemMastersModule {}