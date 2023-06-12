import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ModelWiseItemsRoutingModule } from "./model-wise-items-routing.module";
import { ModelWiseItemsComponent } from "./model-wise-item-list/model-wise-items.component";


@NgModule({
    declarations: [
        ModelWiseItemsComponent
    ],
    imports: [
        AppSharedModule,  
        ModelWiseItemsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class ModelWiseItemsModule {}