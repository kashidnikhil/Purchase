import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { ModelsRoutingModule } from "./models-routing.module";
import { ModelsComponent } from "./model-list/models.component";
import { CreateOrEditModelModalComponent } from "./create-edit-model/create-or-edit-model-modal.component";

@NgModule({
    declarations: [
        ModelsComponent,
        CreateOrEditModelModalComponent
    ],
    imports: [
        AppSharedModule,  
        ModelsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class ModelsModule {}
