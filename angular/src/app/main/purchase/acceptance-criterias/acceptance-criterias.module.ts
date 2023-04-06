import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { AcceptanceCriteriasRoutingModule } from "./acceptance-criterias-routing.module";
import { AcceptanceCriteriasComponent } from "./acceptance-criteria-list/acceptance-criterias.component";
import { CreateOrEditAcceptanceCriteriaModalComponent } from "./create-edit-acceptance-criteria/create-or-edit-acceptance-criteria-modal.component";

@NgModule({
    declarations: [
        AcceptanceCriteriasComponent,
        CreateOrEditAcceptanceCriteriaModalComponent
    ],
    imports: [
        AppSharedModule,  
        AcceptanceCriteriasRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class AcceptanceCriteriasModule {}