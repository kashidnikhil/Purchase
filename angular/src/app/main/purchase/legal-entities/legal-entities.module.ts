import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { CreateOrEditLegalEntityModalComponent } from "./create-edit-legal-entity/create-or-edit-legal-entity-modal.component";
import { LegalEntitiesRoutingModule } from "./legal-entities-routing.module";
import { LegalEntitiesComponent } from "./legal-entity-list/legal-entities.component";

@NgModule({
    declarations: [
        LegalEntitiesComponent,
        CreateOrEditLegalEntityModalComponent
    ],
    imports: [
        AppSharedModule,  
        LegalEntitiesRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class LegalEntitiesModule {}