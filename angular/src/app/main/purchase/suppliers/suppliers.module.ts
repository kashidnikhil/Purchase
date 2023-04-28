import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { SuppliersRoutingModule } from "./suppliers-routing.module";
import { SupplierListComponent } from "./supplier-list/supplier-list.component";
import { CreateOrEditSupplierModalComponent } from "./create-edit-supplier/create-or-edit-supplier-modal.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MultiSelectModule } from 'primeng/multiselect';

@NgModule({
    declarations: [
        SupplierListComponent,
        CreateOrEditSupplierModalComponent
    ],
    imports: [
        AppSharedModule,  
        SuppliersRoutingModule,
        MultiSelectModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [],
})

export class SuppliersModule {}