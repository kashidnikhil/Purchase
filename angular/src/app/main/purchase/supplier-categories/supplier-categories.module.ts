import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { SupplierCategoriesRoutingModule } from "./supplier-categories-routing.module";
import { SupplierCategoriesComponent } from "./supplier-category-list/supplier-categories.component";
import { CreateOrEditSupplierCategoryModalComponent } from "./create-edit-supplier-category/create-or-edit-supplier-category-modal.component";

@NgModule({
    declarations: [
        SupplierCategoriesComponent,
        CreateOrEditSupplierCategoryModalComponent
    ],
    imports: [
        AppSharedModule,  
        SupplierCategoriesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class SupplierCategoriesModule {}