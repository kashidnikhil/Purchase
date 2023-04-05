import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { SupplierCategoriesRoutingModule } from "./supplier-categories-routing.module";

@NgModule({
    declarations: [
    ],
    imports: [
        AppSharedModule,  
        SupplierCategoriesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class SupplierCategoriesModule {}