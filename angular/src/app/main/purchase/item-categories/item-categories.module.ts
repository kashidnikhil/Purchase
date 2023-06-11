import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ItemCategoriesRoutingModule } from "./item-categories-routing.module";
import { ItemCategoriesComponent } from "./item-category-list/item-categories.component";
import { CreateOrEditItemCategoryModalComponent } from "./create-edit-item-category/create-or-edit-item-category-modal.component";

@NgModule({
    declarations: [
        ItemCategoriesComponent,
        CreateOrEditItemCategoryModalComponent
    ],
    imports: [
        AppSharedModule,  
        ItemCategoriesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class ItemCategoriesModule {}