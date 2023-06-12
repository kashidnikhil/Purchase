import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ModelWiseItemsRoutingModule } from "./model-wise-items-routing.module";
import { ModelWiseItemsComponent } from "./model-wise-item-list/model-wise-items.component";
import { CreateOrEditModelWiseItemMasterModalComponent } from "./create-edit-model-wise-item/create-or-edit-model-wise-item-modal.component";
import { ReactiveFormsModule } from "@angular/forms";


@NgModule({
    declarations: [
        ModelWiseItemsComponent,
        CreateOrEditModelWiseItemMasterModalComponent
    ],
    imports: [
        AppSharedModule,  
        ModelWiseItemsRoutingModule,
        ReactiveFormsModule,
        SubheaderModule,
    ],
    providers: [],
})

export class ModelWiseItemsModule {}