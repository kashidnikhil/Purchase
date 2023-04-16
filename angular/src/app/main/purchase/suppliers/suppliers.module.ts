import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { SuppliersRoutingModule } from "./suppliers-routing.module";

@NgModule({
    declarations: [
        // SupplierCategoriesComponent,
        // CreateOrEditSupplierCategoryModalComponent
    ],
    imports: [
        AppSharedModule,  
        SuppliersRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class SuppliersModule {}